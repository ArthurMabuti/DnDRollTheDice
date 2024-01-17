using DnDRollTheDice.Character.CharacterSpells;
using DnDRollTheDice.Services;

namespace DnDRollTheDice.Character;

internal class PlayerCharacter : Character
{
    public PlayerCharacter()
    {
        CreatingAbilityScores();
    }

    public async Task ChooseActionAsync<T>(List<T> allCharacters) where T : Character
    {
        Console.WriteLine($"** {Name}' turn **");
        Console.WriteLine("Which action do you want to use to attack?");
        ActionList();
        string option = Console.ReadLine()!.ToLower();
        switch (option)
        {
            case "weapon attack":
                DealingDamage(allCharacters, null);
                break;
            case "spell casting":
                SpellsLevelList();
                string spellLevel = Console.ReadLine()!.ToLower();
                if (spellLevel == "cantrip") spellLevel = "0";
                if (int.TryParse(spellLevel, out int level))
                {
                    await ShowListOfSpells(level, allCharacters);
                }
                else
                {
                    Console.WriteLine("Invalid spell level. Please enter a valid number.");
                }
                break;
            default:
                Console.WriteLine("That isn't an option");
                break;
        }
    }

    private void ActionList()
    {
        Console.WriteLine("Weapon Attack");
        Console.WriteLine("Spell Casting");
    }

    private void SpellsLevelList()
    {
        Console.WriteLine(@"Choose a spell level:
Cantrip
Level 1
Level 2
Level 3");
    }

    private async Task ShowListOfSpells<T>(int level, List<T> allCharacters) where T: Character
    {
        await GetSpells(level);
        Console.WriteLine("Which spell do you want to use?");
        foreach (var spell in KnownSpells!)
        {
            Console.WriteLine(spell.Name);
        }
        string spellName = Console.ReadLine()!.ToLower();
        Spells chosenSpell = KnownSpells.Find(spl => spl.Name!.ToLower() == spellName)!;
        DealingDamage(allCharacters, chosenSpell);
    }

    private async Task GetSpells(int level)
    {
        ApiService api = new ApiService();
        SpellList? spellList = await api.GetSpellListFromApiAsync(level, ClassInformation!.Name!);
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

    private int GenerateRandomAbilityScore()
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