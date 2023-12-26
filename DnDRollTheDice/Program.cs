using DnDRollTheDice;
using DnDRollTheDice.Character;

//Testing Modifier Value
CharacterStatus bruenor = new()
{
    Name = "Bruenor",
    Class = "Fighter",
    Dexterity = 10
};
Console.WriteLine(bruenor.ModifierValue(bruenor.Dexterity));

//Testing Dice Roll
//Fireball
Console.WriteLine($"Damage from Fireball: {Roll.DiceRoll(8, 6)}");

//Testing Initiative
bruenor.InitiativeCheck(bruenor.Dexterity);
Console.WriteLine($"Bruenor initiative value is {bruenor.Initiative}");