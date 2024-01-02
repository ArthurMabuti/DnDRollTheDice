using DnDRollTheDice.Character.CharacterDetails;
using DnDRollTheDice.Character.CharacterItems;

namespace DnDRollTheDice.Character;
internal class Character
{
    public string? Name { get; set; }
    public string? Race { get; set; }
    public int HitPoints { get; set; }
    public List<ArmorClass> ArmorClass { get; set; }
    public int Initiative { get; set; }
    public Speed Speed { get; set; }
    public Dictionary<string, int> AbilityScores { get; set; }
    public int Proficiency { get; set; }
    public Weapon Weapon { get; set; }

    public Character()
    {
        AbilityScores = new Dictionary<string, int>();
        ArmorClass = new List<ArmorClass>();
        Speed = new Speed();
        Weapon = new Weapon();
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
        int skillUsed = (AbilityScores["Strength"] > AbilityScores["Dexterity"]) ? AbilityScores["Strength"] : AbilityScores["Dexterity"];
        int rangeSkillBased = (this.Weapon.Range == "Ranged") ? ModifierValue(AbilityScores["Dexterity"]) : ModifierValue(skillUsed);
        
        int attackValue = Roll.DiceRoll(1, 20) + rangeSkillBased + Proficiency;

        return attackValue;
    }
    public bool ReachArmorClass(Character character)
    {
        if(AttackRoll() >= character.ArmorClass.First().Value)
            return true;
        return false;
    }
}
