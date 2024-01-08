using DnDRollTheDice.Character;
using CharacterType = DnDRollTheDice.Character.Character;

namespace DnDRollTheDice;
internal class CombatSystem
{
    public static List<CharacterType> AllCharacters { get; set; }

    static CombatSystem() 
    {  
        AllCharacters = []; 
    }

    public static void EngageInBattle()
    {
        List<Monster> allMonsters = GetAllCharactersOfType<Monster>();
        List<PlayerCharacter> allPlayerCharacters = GetAllCharactersOfType<PlayerCharacter>();

        OrderCharactersByInitiative();

        while (!allPlayerCharacters.All(cha => cha.IsUnconscious()) && !allMonsters.All(mon => mon.IsUnconscious()))
        {
            foreach (var character in AllCharacters)
            {
                
                if (character is PlayerCharacter playerCharacter)
                {
                    if (character.HitPoints <= 0) break;
                    playerCharacter.DealingDamage(allMonsters);
                    continue;
                }
                if (character is Monster monsterCharacter && monsterCharacter.HitPoints > 0)
                {
                    monsterCharacter.AttackAction(allPlayerCharacters);
                }
            }
        }
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
