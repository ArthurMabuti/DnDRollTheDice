using System.Text.Json.Serialization;

namespace DnDRollTheDice.Character.CharacterSpells;

internal class SpellList
{
    [JsonPropertyName("count")]
    public int SpellCount { get; set; }
    [JsonPropertyName("results")]
    public List<Spells>? Spells { get; set; }
}
