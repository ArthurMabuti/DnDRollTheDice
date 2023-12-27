using DnDRollTheDice;
using DnDRollTheDice.Character;
using System.Text.Json;

#region Testing Character Status

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
Console.WriteLine($"Bruenor Dexterity: {bruenor.AbilitiesScores["Dexterity"]}");
bruenor.InitiativeCheck();
Console.WriteLine($"Bruenor initiative value is {bruenor.Initiative}");

//Testing RandomAbilityScore
bruenor.GenerateRandomAbilityScore();
#endregion

//Testing Monster API

using(HttpClient client = new HttpClient())
{
    string resposta = await client.GetStringAsync("https://www.dnd5eapi.co/api/monsters/goblin/");
    //Console.WriteLine(resposta);
    //var personagens = JsonSerializer.Deserialize<List<HarryPotter>>(resposta);
    Monster? goblin = JsonSerializer.Deserialize<Monster>(resposta);
    Console.WriteLine(goblin);
}

void Interface(CharacterStatus character)
{
    Console.WriteLine($@"{character.Name}    {character.Class}");
    ShowAbilityScores(character);
}

void ShowAbilityScores(CharacterStatus character)
{
    Dictionary<string, int> abilityScore = character.AbilitiesScores;
    foreach (string abilityName in abilityScore.Keys.ToList())
    {
        string abilityNameAbbreviated = abilityName.Substring(0,3);
        Console.WriteLine($"{abilityNameAbbreviated}: {abilityScore[abilityName]} | {character.ModifierValue(abilityScore[abilityName])}");
    }
}