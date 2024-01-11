using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace DnDRollTheDice.Character.CharacterSpells;
internal class Spells
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("desc")]
    public string? Description { get; set; }
    [JsonPropertyName("target_range_sort")]
    public int Range { get; set; }
    [JsonPropertyName("duration")]
    public string? Duration { get; set; }
    [JsonPropertyName("casting_time")]
    public string? CastingTime { get; set; }
    [JsonPropertyName("spell_level")]
    public int Level { get; set; }
    [JsonPropertyName("school")]
    public string? School { get; set; }
    [JsonPropertyName("requires_concentration")]
    public bool Concentration { get; set; }
    public string? SpellDamage { get; set; }
    //Components
    [JsonPropertyName("requires_verbal_components")]
    public bool Verbal { get; set; }
    [JsonPropertyName("requires_somatic_components")]
    public bool Somatic { get; set; }
    [JsonPropertyName("requires_material_components")]
    public bool Material { get; set; }

    public Match DescriptionHasDamageDice(string description)
    {
        Match match = Regex.Match(description, @"(\d{2}|\d{1})d(\d{2}|\d{1})");
        return match;
    }
}
