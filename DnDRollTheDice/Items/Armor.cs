using System.Text.Json.Serialization;

namespace DnDRollTheDice.Items;
internal class Armor
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}
