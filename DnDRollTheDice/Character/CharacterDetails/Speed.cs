using System.Text.Json.Serialization;

namespace DnDRollTheDice.Character.CharacterDetails;
public class Speed
{
    [JsonPropertyName("walk")]
    public string? Locomotion { get; set; }
}
