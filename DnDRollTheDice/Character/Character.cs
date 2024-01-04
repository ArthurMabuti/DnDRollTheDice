using DnDRollTheDice.Character.CharacterDetails;
using DnDRollTheDice.Character.CharacterItems;
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
    public int Initiative { get; set; }
    [JsonPropertyName("speed")]
    public Speed Speed { get; set; }
    public Dictionary<string, int> AbilityScores { get; set; }
    [JsonPropertyName("proficiency_bonus")]
    public int Proficiency { get; set; }
    public Weapon Weapon { get; set; }

    public Character()
    {
        AbilityScores = [];
        Speed = new();
        Weapon = new();
        armorClass = [];
    }

    public static int ModifierValue(int abilityScore)
    {
        double modifier = (double)(abilityScore - 10) / 2;
        return (int)Math.Floor(modifier);
    }
    public void InitiativeCheck()
    {
        int rollValue = Roll.DiceRoll(1, 20);
        int finalResult = rollValue + ModifierValue(AbilityScores["Dexterity"]);
        Console.WriteLine($"The dice roll for initiative was {rollValue}");
        Initiative = finalResult;
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

    public virtual void DealingDamage(Character character)
    {
        int attackRoll = AttackRoll();
        if (ReachArmorClass(character, attackRoll))
        {
            Console.WriteLine("Attack successful!");
            int damage = Weapon.Damage!.DamageRoll(CriticalHit(attackRoll)) + ModifierValue(BestFightingSkill());
            Console.WriteLine($"Damage = {damage}");
            character.HitPoints -= damage;
            Console.WriteLine($"Actual HP from {character.Name} = {character.HitPoints}");
        }
        else
            Console.WriteLine("Attack missed!");
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
}
