using DnDRollTheDice;
using DnDRollTheDice.Character;

//Testing Modifier Value
CharacterStatus bruenor = new()
{
    Name = "Bruenor",
    Class = "Fighter",
    Dexterity = 20
};
Console.WriteLine(bruenor.ModifierValue(bruenor.Dexterity));

//Testing Dice Roll
Roll roll = new();
//Fireball
Console.WriteLine($"Damage from Fireball: {roll.DiceRoll(8, 6)}");