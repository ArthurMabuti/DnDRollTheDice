using System.Text.Json.Serialization;

namespace DnDRollTheDice.Items;
public class Armor
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}
