using DnDRollTheDice.Character.CharacterDetails;
using DnDRollTheDice.Items;

namespace DnDRollTheDice.Character;

public class CharacterStatus
{
    public string? Name { get; set; }
    public string? Race { get; set; }
    public string? Class { get; set; }
    public int HitPoints { get; set; }
    public List<ArmorClass> ArmorClass { get; set; }
    public int Initiative { get; set; }
    public Speed Speed { get; set; }
    public Dictionary<string, int> AbilityScores { get; set; }

    public CharacterStatus()
    {
        AbilityScores = new Dictionary<string, int>();
        ArmorClass = new List<ArmorClass>();
        Speed = new Speed();
        CreatingAbilityScores();
    }

    private void CreatingAbilityScores()
    {
        string[] abilityNames = { "Strength", "Dexterity", "Constitution", "Intelligence", "Wisdom", "Charisma" };

        foreach(string abilityName in abilityNames)
        {
            AbilityScores.Add(abilityName, 0);
        }
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
        foreach(string abilityName in AbilityScores.Keys.ToList())
        {
            AbilityScores[abilityName] = GenerateRandomAbilityScore();
        }
    }
}
