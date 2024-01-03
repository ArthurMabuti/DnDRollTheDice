using System.Text.Json.Serialization;

namespace DnDRollTheDice.Character.CharacterDetails;
public class Damage
{
    [JsonPropertyName("damage_dice")]
    public string? DamageDice { get; set; }

    public int DamageRoll(bool criticalHit)
    {
        string[]? diceRollInformation = DamageDice?.Split('d');
        int numberOfDices = int.Parse(diceRollInformation![0]);
        if (criticalHit)
        {
            numberOfDices *= 2;
            Console.WriteLine("*CRITICAL HIT*");
        }

        int diceSides = int.Parse(diceRollInformation[1]);

        int rollValue = Roll.DiceRoll(numberOfDices, diceSides);
        return rollValue;
    }
}