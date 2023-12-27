using System.Text.Json.Serialization;

namespace DnDRollTheDice.Character.CharacterDetails;
internal class Speed
{
    [JsonPropertyName("walk")]
    public string? Locomotion { get; set; }
}
