using DnDRollTheDice.Character.CharacterDetails;
using DnDRollTheDice.Items;

namespace DnDRollTheDice.Character;

internal class CharacterStatus
{
    public string? Name { get; set; }
    public string? Race { get; set; }
    public string? Class { get; set; }
    public int HitPoints { get; set; }
    public List<ArmorClass> ArmorClass { get; set; }
    public int Initiative { get; set; }
    public Speed Speed { get; set; }
    public Dictionary<string, int> AbilitiesScores { get; set; }

    public CharacterStatus()
    {
        AbilitiesScores = new Dictionary<string, int>();
        ArmorClass = new List<ArmorClass>();
        Speed = new Speed();
        CreatingAbilityScores();
    }

    private void CreatingAbilityScores()
    {
        AbilitiesScores.Add("Strength", 0);
        AbilitiesScores.Add("Dexterity", 0);
        AbilitiesScores.Add("Constitution", 0);
        AbilitiesScores.Add("Intelligence", 0);
        AbilitiesScores.Add("Wisdom", 0);
        AbilitiesScores.Add("Charisma", 0);
    }

    public int ModifierValue(int abilityScore)
    {
        double modifier = (double)(abilityScore - 10) / 2;
        return (int)Math.Floor(modifier);
    }

    public void InitiativeCheck()
    {
        int rollValue = Roll.DiceRoll(1, 20);
        int finalResult = rollValue + ModifierValue(AbilitiesScores["Dexterity"]);
        Console.WriteLine($"The dice roll for initiative was {rollValue}");
        Initiative = finalResult;
    }

    public int GenerateRandomAbilityScore()
    {
        //Roll 4 6-sized dice
        List<int> rollsResult = new();
        for(int i = 0; i < 4; i++)
        {
            int roll = Roll.DiceRoll(1, 6);
            rollsResult.Add(roll);
        }
        
        rollsResult = rollsResult.OrderBy(x => x).ToList();

        //Remove the lowest one
        rollsResult.RemoveAt(0);

        return rollsResult.Sum(); 
    }

    public void CharacterWithRandomAbilityScore()
    {
        foreach(string abilityName in AbilitiesScores.Keys.ToList())
        {
            AbilitiesScores[abilityName] = GenerateRandomAbilityScore();
        }
    }
}
