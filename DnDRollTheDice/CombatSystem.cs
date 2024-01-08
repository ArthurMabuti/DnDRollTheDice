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

    private static List<T> GetAllCharactersOfType<T>() where T : CharacterType
    {
        List<T> charactersOfType = [];
        foreach (var character in AllCharacters)
        {
            if (character is T typedCharacter)
            {
                charactersOfType.Add(typedCharacter);
            }
        }
        return charactersOfType;
    }

    public static void AddCharacter(CharacterType character)
    {
        AllCharacters.Add(character);
    }
}
