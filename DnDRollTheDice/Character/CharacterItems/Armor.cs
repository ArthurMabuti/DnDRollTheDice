using System.Text.Json.Serialization;

namespace DnDRollTheDice.Character.CharacterItems;
public class Armor
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}
