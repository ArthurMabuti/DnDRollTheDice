using DnDRollTheDice.Character.CharacterDetails;
using DnDRollTheDice.Character.CharacterItems;
using DnDRollTheDice.Character.CharacterSpells;
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

    public Character()
    {
        AbilityScores = [];
        Speed = new();
        Weapon = new();
        armorClass = [];
        KnownSpells = [];
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

    public int AttackRoll()
    {
        int rangeSkillBased = (this.Weapon.Range == "Ranged") ? ModifierValue(AbilityScores["Dexterity"]) : ModifierValue(BestFightingSkill());

        int diceRolled = Roll.DiceRoll(1, 20);

        int attackValue = diceRolled + rangeSkillBased + Proficiency;

        Console.WriteLine($"Attack Roll = Dice({diceRolled}) + Skill Bonus({rangeSkillBased}) + Proficiency Bonus({Proficiency}) = {attackValue}");
        return attackValue;
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
        Character target = ChooseTarget(allCharacters);
        string? attackSource = (spell == null) ? Weapon.Name : spell.Name;

        Console.WriteLine($"Making a {attackSource} attack against {target.Name}!");

        int attackRoll = AttackRoll();

        if (ReachArmorClass(target, attackRoll))
        {
            Console.WriteLine("Attack successful!");

            int damage = (spell == null)
                ? Weapon.Damage!.DamageRoll(CriticalHit(attackRoll)) + ModifierValue(BestFightingSkill())
                : spell.SpellDamage!.DamageRoll(CriticalHit(attackRoll)) + ModifierValue(BestFightingSkill());

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

    public T ChooseTarget<T>(List<T> allCharacters) where T : Character
    {
        Console.WriteLine($"** {Name}' turn **");
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

    public async Task AssignClassInformationAsync(ApiService apiService)
    {
        if (Class != null)
        {
            ClassList classList = await apiService.GetClassFromApiAsync(Class);
            ClassInformation = classList?.Class?.First();
        }
    }
}