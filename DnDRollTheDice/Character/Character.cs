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
    public List<ArmorClass> ArmorClass { get; set; }
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
        ArmorClass = [];
        Speed = new();
        Weapon = new();
    }

    public int ModifierValue(int abilityScore)
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
        Console.WriteLine($@"{Name} CA: {ArmorClass.First().Value}");
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
    public bool ReachArmorClass(Character character, int attackRoll)
    {
        if (attackRoll >= character.ArmorClass.First().Value)
        {
            return true;
        }
        return false;
    }

    public void DealingDamage(Character character)
    {
        int attackRoll = AttackRoll();
        if (ReachArmorClass(character, attackRoll))
        {
            Console.WriteLine("Attack successful!");
            int damage = Weapon.DamageRoll(CriticalHit(attackRoll));
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
