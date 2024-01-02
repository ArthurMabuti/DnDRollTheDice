using DnDRollTheDice;
using DnDRollTheDice.Character;
using DnDRollTheDice.Services;

#region Testing Character Status

//Testing Modifier Value
PlayerCharacter bruenor = new()
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

ApiService monsterService = new();
Monster? goblin = await monsterService.GetMonsterFromApiAsync("goblin");
//Monster? goblin = new();
//goblin.UseManualStatus();
//goblin.SettingAbilityScores();
goblin.Interface();

