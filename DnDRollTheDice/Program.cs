using DnDRollTheDice;
using DnDRollTheDice.Character;

//Testing Modifier Value
CharacterStatus bruenor = new()
{
    Name = "Bruenor",
    Class = "Fighter",
};
bruenor.CharacterWithRandomAbilityScore();

Interface(bruenor);

//Testing Dice Roll
//Fireball
Console.WriteLine($"Damage from Fireball: {Roll.DiceRoll(8, 6)}");

//Testing Initiative
Console.WriteLine($"Bruenor Dexterity: {bruenor.Dexterity}");
bruenor.InitiativeCheck();
Console.WriteLine($"Bruenor initiative value is {bruenor.Initiative}");

//Testing RandomAbilityScore
bruenor.GenerateRandomAbilityScore();

void Interface(CharacterStatus character)
{
    Console.WriteLine($@"       {character.Name}
For: {character.Strength} | {bruenor.ModifierValue(bruenor.Strength)}
Dex: {character.Dexterity} | {bruenor.ModifierValue(bruenor.Dexterity)}
Con: {character.Constitution} | {bruenor.ModifierValue(bruenor.Constitution)}
Int: {character.Intelligence} | {bruenor.ModifierValue(bruenor.Intelligence)}
Wis: {character.Wisdom} | {bruenor.ModifierValue(bruenor.Wisdom)}
Cha: {character.Charisma} | {bruenor.ModifierValue(bruenor.Charisma)}
");
}