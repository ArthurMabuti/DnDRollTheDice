using CharacterType = DnDRollTheDice.Character.Character;

namespace DnDRollTheDice;
internal class CombatSystem
{
    public static List<CharacterType> AllCharacters { get; set; }

    static CombatSystem() 
    {  
        AllCharacters = []; 
    }

    public static void AddCharacter(CharacterType character)
    {
        AllCharacters.Add(character);
    }
}
