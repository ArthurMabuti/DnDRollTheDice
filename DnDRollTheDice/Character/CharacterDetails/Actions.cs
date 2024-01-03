using System.Text.Json.Serialization;

namespace DnDRollTheDice.Character.CharacterDetails;
public class Actions
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("desc")]
    public string Description { get; set; }
    [JsonPropertyName("damage")]
    public List<Damage> Damage { get; set; }
}