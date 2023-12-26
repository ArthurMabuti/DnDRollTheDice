using DnDRollTheDice.Character;

//Testing Modifier Value
CharacterStatus bruenor = new()
{
    Name = "Bruenor",
    Class = "Fighter",
    Dexterity = 20
};
Console.WriteLine(bruenor.ModifierValue(bruenor.Dexterity));