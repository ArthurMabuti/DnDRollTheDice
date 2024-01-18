using System.Text.Json.Serialization;
using DnDRollTheDice.DiceRolls;

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

        string[] sidesAndBonusInformation = diceRollInformation[1].Split('+');
        int diceSides = int.Parse(sidesAndBonusInformation[0]);
        int attackBonus = 0;
        if (sidesAndBonusInformation.Length > 1)
        {
            attackBonus = int.Parse(sidesAndBonusInformation[1]);
        }

        int rollValue = Roll.DiceRoll(numberOfDices, diceSides);
        return rollValue + attackBonus;
    }

    public override string ToString()
    {
        return DamageDice!;
    }
}