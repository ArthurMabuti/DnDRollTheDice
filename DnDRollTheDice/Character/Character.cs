using DnDRollTheDice.Character.CharacterClass;
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

    public int ModifierValue(int abilityScore)
    {
        double modifier = (double)(abilityScore - 10) / 2;
        return (int)Math.Floor(modifier);
    }

    public int InitiativeCheck()
    {
        int rollValue = Roll.DiceRoll(1, 20);
        int finalResult = rollValue + ModifierValue(AbilityScores["Dexterity"]);
        Console.WriteLine($"The dice roll from {Name} for initiative was {finalResult}");
        return finalResult;
    }

    public int SavingThrow(string ability)
    {
        int rollValue = Roll.DiceRoll(1, 20);
        int finalResult = rollValue + ModifierValue(AbilityScores[ability]);
        Console.WriteLine($"The dice roll from {Name} for the saving throw was {finalResult}");
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

    public int AttackRoll(string? actionName = null, Spells? spell = null)
    {
        // Verifies if the dice roll will be made with normally or with advantage or disadvantage and makes the roll
        int diceRolled = DetermineDiceRoll();

        int attackBonus;
        int attackValue;
        if(this is Monster monster)
        {
            attackBonus = monster.SelectMonsterAction(actionName!).AttackBonus;
            attackValue = diceRolled + attackBonus;
            Console.WriteLine($"Attack Roll = Dice({diceRolled}) + AttackBonus({attackBonus}) = {attackValue}");
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

    private int BestFightingSkill()
    {
        int fightingSkill = (AbilityScores["Strength"] > AbilityScores["Dexterity"]) ? AbilityScores["Strength"] : AbilityScores["Dexterity"];
        return fightingSkill;
    }

    private int AssignAttackBonus(Spells? spell)
    {
        string spellCastingAbility = ClassInformation!.SpellCastingAbility!;
        int attackBonus = (spell != null) ? ModifierValue(AbilityScores[spellCastingAbility]) : RangeSkillBased();
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
        // If monster, shows it's name
        if(this is Monster) Console.WriteLine($"** {Name}'s turn **");
        // Choose in which character will occur the action
        Character target = ChooseTarget(allCharacters);
        // Choose the name of the action
        string? attackSource = AttackSource(spell);

        // If a monster chose multiattack it makes the combination of attacks from it's class
        if(attackSource.ToLower() == "multiattack")
        {
            Monster? monster = this as Monster;
            monster!.MultiAttack(target);
        }
        // Else makes an attack based the name of the action
        else
        { 
            // Makes a dice roll to try hit the target
            int attackRoll = AttackRoll(attackSource, spell);
            // Makes the attack based on the chose action
            MakingAnAttack(target, attackSource, attackRoll, spell);
        }
    }

    public void MakingAnAttack(Character target, string actionName, int attackRoll, Spells? spell = null)
    {
        // Write which action is happening to whom
        Console.WriteLine($"Making a {actionName} attack against {target.Name!}!");
        // If the dice attackRoll surpasses the Armor Class from the Target, do the damage
        if (ReachArmorClass(target, attackRoll))
        {
            Console.WriteLine("Attack successful!");
            // Sums the many damage dices roll + ability scores
            int damage = CalculateDamage(actionName, attackRoll, spell);

            Console.WriteLine($"Damage = {damage}");
            // Subtracts the target total HitPoints with the damage taken
            target.HitPoints -= damage;
            // Shows actual HitPoints total from target
            Console.WriteLine($"Actual HP from {target.Name} = {target.HitPoints}");
            // Set Unconsciuous if the HP gets to 0
            SetUnconscious(target);
        }
        else
        {
            Console.WriteLine("Attack missed!");
        }
    }

    protected string AttackSource(Spells? spell)
    {
        // Returns the weapon/spell name or, if a monster, the action name
        if (this is Monster monster)
            return monster.ChooseMonsterAction();
        return (spell == null) ? Weapon.Name! : spell.Name!;
    }

    protected int CalculateDamage(string actionName, int attackRoll, Spells? spell)
    {
        //If is a monster, gets the damage dices from its action and makes the rolls
        if(this is Monster monster)
            return monster.SelectMonsterAction(actionName).Damage!.DamageRoll(CriticalHit(attackRoll));
        // Else gets the damage dice from weapon/spell and makes the dice rolls
        return (spell == null)
            ? Weapon.Damage!.DamageRoll(CriticalHit(attackRoll)) + ModifierValue(BestFightingSkill())
            : spell.SpellDamage!.DamageRoll(CriticalHit(attackRoll)) + ModifierValue(BestFightingSkill());
    }

    public static T ChooseTarget<T>(List<T> allCharacters) where T : Character
    {
        Console.WriteLine("Which target do you want to attack?");
        foreach (Character character in allCharacters)
        {
            //If target it's not unconscious, show his name for selection
            if(!character.IsUnconscious())
                Console.WriteLine(character.Name);
        }
        // User writes the name of the target
        string targetName = Console.ReadLine()!;
        // Find the target in the list of all created targets
        T target = allCharacters.Find(cha => cha.Name == targetName)!;
        // Return the Character class
        return target;
    }

    public bool CriticalHit(int attackRoll)
    {
        int attackDiceRoll = attackRoll - ModifierValue(BestFightingSkill()) - Proficiency;

        if(attackDiceRoll == 20)
            return true;
        return false;
    }

    public static void SetUnconscious(Character character)
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
    //TODO Separate methods from Spells, Weapon and Monster Actions. 
    //If it is a Weapon attack, the methods about damage shall be on the class Weapon and vice-versa
}