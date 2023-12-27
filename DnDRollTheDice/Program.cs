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
    Console.WriteLine($@"       {character.Name}    {character.Class}
For: {character.Strength} | {character.ModifierValue(character.Strength)}
Dex: {character.Dexterity} | {character.ModifierValue(character.Dexterity)}
Con: {character.Constitution} | {character.ModifierValue(character.Constitution)}
Int: {character.Intelligence} | {character.ModifierValue(character.Intelligence)}
Wis: {character.Wisdom} | {character.ModifierValue(character.Wisdom)}
Cha: {character.Charisma} | {character.ModifierValue(character.Charisma)}
");
}