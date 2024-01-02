using System.Text.Json.Serialization;

namespace DnDRollTheDice.Character.CharacterDetails;
public class Damage
{
    [JsonPropertyName("damage_dice")]
    public string? DamageDice { get; set; }
}