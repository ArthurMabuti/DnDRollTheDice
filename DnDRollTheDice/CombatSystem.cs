using CharacterType = DnDRollTheDice.Character.Character;

namespace DnDRollTheDice;
internal class CombatSystem
{
    public static List<CharacterType> AllCharacters { get; set; }

    static CombatSystem() 
    {  
        AllCharacters = []; 
    }

    private static void OrderCharactersByInitiative()
    {
        AllCharacters = AllCharacters.OrderByDescending(c => c.Initiative).ToList();
    }
    public static void AddCharacter(CharacterType character)
    {
        AllCharacters.Add(character);
    }
}
