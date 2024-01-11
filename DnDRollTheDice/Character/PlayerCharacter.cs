using DnDRollTheDice.Character.CharacterSpells;
using DnDRollTheDice.Character.CharacterItems;

namespace DnDRollTheDice.Character;

internal class PlayerCharacter : Character
{
    public string? Class { get; set; }
    public List<Spells> KnownSpells { get; set; }
    public PlayerCharacter()
    {
        KnownSpells = [];
        CreatingAbilityScores();
    }

    public void ActionList()
    {
        Console.WriteLine("Weapon Attack");
        Console.WriteLine("Spell Casting");
    }

    public void SpellsLevelList()
    {
        Console.WriteLine(@"Choose a spell level:
Cantrip
Level 1
Level 2
Level 3");
    }
    public async Task GetSpells(int level)
    {
        ApiService api = new ApiService();
        SpellList? spellList = await api.GetSpellListFromApiAsync(0, Class!);
        foreach(var spell in spellList!.Spells!)
        {
            KnownSpells.Add(spell);
        }
    }

    private void CreatingAbilityScores()
    {
        string[] abilityNames = { "Strength", "Dexterity", "Constitution", "Intelligence", "Wisdom", "Charisma" };

        foreach (string abilityName in abilityNames)
        {
            AbilityScores.Add(abilityName, 0);
        }
    }

    public int GenerateRandomAbilityScore()
    {
        //Roll 4 6-sized dice
        List<int> rollsResult = new();
        for (int i = 0; i < 4; i++)
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
        foreach (string abilityName in AbilityScores.Keys.ToList())
        {
            AbilityScores[abilityName] = GenerateRandomAbilityScore();
        }
    }
}