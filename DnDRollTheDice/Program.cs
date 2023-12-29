using DnDRollTheDice;
using DnDRollTheDice.Character;
using DnDRollTheDice.Services;
using System.Text.Json;

#region Testing Character Status

//Testing Modifier Value
CharacterStatus bruenor = new()
{
    Name = "Bruenor",
    Class = "Fighter",
};
bruenor.CharacterWithRandomAbilityScore();

//Interface(bruenor);

//Testing Dice Roll
//Fireball
Console.WriteLine($"Damage from Fireball: {Roll.DiceRoll(8, 6)}");

//Testing Initiative
//Console.WriteLine($"Bruenor Dexterity: {bruenor.AbilityScores["Dexterity"]}");
//bruenor.InitiativeCheck();
//Console.WriteLine($"Bruenor initiative value is {bruenor.Initiative}");

//Testing RandomAbilityScore
bruenor.GenerateRandomAbilityScore();
#endregion

//Testing Monster API

MonsterService monsterService = new();
//Monster? goblin = await monsterService.GetMonsterFromApiAsync("goblin");
Monster? goblin = new();
goblin.UseManualStatus();
goblin.SettingAbilityScores();
Console.WriteLine(goblin.Name);
ShowAbilityScores(goblin);

void Interface(CharacterStatus character)
{
    Console.WriteLine($@"{character.Name} {character.Class}");
    ShowAbilityScores(character);
}

void ShowAbilityScores(CharacterStatus character)
{
    Dictionary<string, int> abilityScore = character.AbilityScores;
    foreach (string abilityName in abilityScore.Keys.ToList())
    {
        string abilityNameAbbreviated = abilityName.Substring(0,3);
        Console.WriteLine($"{abilityNameAbbreviated}: {abilityScore[abilityName]} | {character.ModifierValue(abilityScore[abilityName])}");
    }
}