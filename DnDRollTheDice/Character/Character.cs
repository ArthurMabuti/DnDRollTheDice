﻿using DnDRollTheDice.Character.CharacterClass;
using DnDRollTheDice.Character.CharacterDetails;
using DnDRollTheDice.Character.CharacterDetails.Conditions;
using DnDRollTheDice.Character.CharacterItems;
using DnDRollTheDice.Character.CharacterSpells;
using DnDRollTheDice.DiceRolls;
using DnDRollTheDice.Services;
using System.Text.Json.Serialization;

namespace DnDRollTheDice.Character;
internal class Character
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    public string? Race { get; set; }
    [JsonPropertyName("hit_points")]
    public int HitPoints { get; set; }
    [JsonPropertyName("armor_class")]
    public List<ArmorClass> armorClass { get; set; }
    public ArmorClass ArmorClass
    {
        get => armorClass.First();
        set => armorClass = [value];
    }
    public int Initiative => InitiativeCheck();
    [JsonPropertyName("speed")]
    public Speed Speed { get; set; }
    public Dictionary<string, int> AbilityScores { get; set; }
    [JsonPropertyName("proficiency_bonus")]
    public int Proficiency { get; set; }
    public Weapon Weapon { get; set; }
    public bool Unconscious = false;
    public string? Class { get; set; }
    public Class? ClassInformation { get; set; }
    public List<Spells> KnownSpells { get; set; }
    public RollType RollType { get; set; } = RollType.Normal;
    public List<Conditions>? Conditions { get; private set; }

    public Character()
    {
        AbilityScores = [];
        Speed = new();
        Weapon = new();
        armorClass = [];
        KnownSpells = [];
        Conditions = [];
        CombatSystem.AddCharacter(this);
    }

    public static int ModifierValue(int abilityScore)
    {
        double modifier = (double)(abilityScore - 10) / 2;
        return (int)Math.Floor(modifier);
    }

    public int InitiativeCheck()
    {
        int rollValue = Roll.DiceRoll(1, 20);
        int finalResult = rollValue + ModifierValue(AbilityScores["Dexterity"]);
        Console.WriteLine($"The dice roll from {Name} for initiative was {rollValue}");
        return finalResult;
    }

    public void Interface()
    {
        Console.WriteLine($@"{Name} CA: {ArmorClass.Value}");
        ShowAbilityScores();
    }

    public void ShowAbilityScores()
    {
        Dictionary<string, int> abilityScore = AbilityScores;
        foreach (string abilityName in abilityScore.Keys.ToList())
        {
            string abilityNameAbbreviated = abilityName.Substring(0, 3);
            Console.WriteLine($"{abilityNameAbbreviated}: {abilityScore[abilityName]} | {ModifierValue(abilityScore[abilityName])}");
        }
    }

    public int AttackRoll(Character character, string? actionName = null, Spells? spell = null)
    {
        int diceRolled = DetermineDiceRoll();

        int attackBonus;
        int attackValue;
        if(character is Monster monster)
        {
            attackBonus = monster.SelectMonsterAction(actionName!).AttackBonus;
            attackValue = diceRolled + attackBonus;
            Console.WriteLine($"Attack Roll = Dice({diceRolled}) + AttackBobus({attackBonus}) = {attackValue}");
        }
        else
        {
            attackBonus = AssignAttackBonus(spell);
            attackValue = diceRolled + attackBonus + Proficiency;
            Console.WriteLine($"Attack Roll = Dice({diceRolled}) + Skill Bonus({attackBonus}) + Proficiency Bonus({Proficiency}) = {attackValue}");
        }

        return attackValue;
    }

    private int DetermineDiceRoll()
    {
        return RollType switch
        {
            RollType.Normal => Roll.DiceRoll(1, 20),
            RollType.Disadvantage => Roll.DisadvantageDiceRoll(),
            RollType.Advantage => Roll.AdvantageDiceRoll(),
            _ => 0,
        };
    }

    private int RangeSkillBased()
    {
        int rangeSkillBased = (Weapon.Range == "Ranged") ? ModifierValue(AbilityScores["Dexterity"]) : ModifierValue(BestFightingSkill());
        return rangeSkillBased;
    }

    private int AssignAttackBonus(Spells? spell)
    {
        int attackBonus = (spell != null) ? ModifierValue(AbilityScores[ClassInformation!.SpellCastingAbility!]) : RangeSkillBased();
        return attackBonus;
    }

    public static bool ReachArmorClass(Character character, int attackRoll)
    {
        if (attackRoll >= character.ArmorClass.Value)
        {
            return true;
        }
        return false;
    }

    public void DealingDamage<T>(List<T> allCharacters, Spells? spell = null) where T : Character
    {
        Character? character = this;

        if(character is Monster) Console.WriteLine($"** {Name}' turn **");
        Character target = ChooseTarget(allCharacters);
        string? attackSource = AttackSource(character!, spell);

        if(attackSource.ToLower() == "multiattack")
        {
            Monster? monster = character as Monster;
            monster!.MultiAttack(target);
        }
        else
        {
            int attackRoll = AttackRoll(character!, attackSource, spell);
            MakingAnAttack(this, target, attackSource, attackRoll, spell);
        }
    }

    public void MakingAnAttack(Character attacker, Character target, string actionName, int attackRoll, Spells? spell = null)
    {
        Console.WriteLine($"Making a {actionName} attack against {target.Name!}!");
        if (ReachArmorClass(target, attackRoll))
        {
            Console.WriteLine("Attack successful!");

            int damage = CalculateDamage(attacker!, actionName, attackRoll, spell);

            Console.WriteLine($"Damage = {damage}");
            target.HitPoints -= damage;
            Console.WriteLine($"Actual HP from {target.Name} = {target.HitPoints}");
            SetUnconscious(target);
        }
        else
        {
            Console.WriteLine("Attack missed!");
        }
    }

    protected string AttackSource(Character character, Spells? spell)
    {
        if (character is Monster monster)
            return monster.ChooseMonsterAction();
        return (spell == null) ? Weapon.Name! : spell.Name!;
    }

    protected int CalculateDamage(Character character, string actionName, int attackRoll, Spells? spell)
    {
        if(character is Monster monster)
            return monster.SelectMonsterAction(actionName).Damage!.DamageRoll(CriticalHit(attackRoll));
        return (spell == null)
            ? Weapon.Damage!.DamageRoll(CriticalHit(attackRoll)) + ModifierValue(BestFightingSkill())
            : spell.SpellDamage!.DamageRoll(CriticalHit(attackRoll)) + ModifierValue(BestFightingSkill());
    }

    public T ChooseTarget<T>(List<T> allCharacters) where T : Character
    {

        T target = null!;
        Console.WriteLine("Which target do you want to attack?");
        foreach (Character character in allCharacters)
        {
            if(!character.IsUnconscious())
                Console.WriteLine(character.Name);
        }
        string targetName = Console.ReadLine()!;
        target = allCharacters.Find(cha => cha.Name == targetName)!;
        return target;
    }

    public bool CriticalHit(int attackRoll)
    {
        int attackDiceRoll = attackRoll - ModifierValue(BestFightingSkill()) - Proficiency;

        if(attackDiceRoll == 20)
            return true;
        return false;
    }

    public int BestFightingSkill()
    {
        int fightingSkill = (AbilityScores["Strength"] > AbilityScores["Dexterity"]) ? AbilityScores["Strength"] : AbilityScores["Dexterity"];
        return fightingSkill;
    }

    public void SetUnconscious(Character character)
    {
        if(character.HitPoints <= 0) character.Unconscious = true;
    }

    public bool IsUnconscious()
    {
        return Unconscious;
    }

    public async Task AssignClassInformationAsync(ApiService? apiService)
    {
        if (Class != null)
        {
            ClassList? classList = await apiService!.GetClassFromApiAsync(Class);
            ClassInformation = classList?.Class?.First();
        }
    }

    public void ApplyCondition(Conditions condition)
    {
        Conditions!.Add(condition);
        Console.WriteLine($"{Name} is now {condition}.");
    }

    public void RemoveCondition(Conditions condition)
    {
        if (Conditions!.Contains(condition))
        {
            Conditions.Remove(condition);
            Console.WriteLine($"{Name} is no longer {condition}.");
        }
    }
}