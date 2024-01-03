using DnDRollTheDice.Character.CharacterDetails;
using System.Text.Json.Serialization;

namespace DnDRollTheDice.Character.CharacterItems;

internal class Weapon
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("weapon_range")]
    public string? Range { get; set; }
    [JsonPropertyName("damage")]
    public Damage? Damage { get; set; }

    public Weapon()
    {
        Damage = new Damage();
    }

    public int DamageRoll(bool criticalStrike)
    {
        string[]? diceRollInformation = Damage?.DamageDice?.Split('d');
        int numberOfDices = int.Parse(diceRollInformation![0]);
        if (criticalStrike)
            numberOfDices *= 2;
        int diceSides = int.Parse(diceRollInformation[1]);

        int rollValue = Roll.DiceRoll(numberOfDices, diceSides);
        return rollValue;
    }
}
