using DnDRollTheDice.Character.CharacterDetails;
using DnDRollTheDice.Character.CharacterDetails.Conditions;
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
    public Damage? SpellDamage => SpellDamageByDescription();
    public Conditions Condition => SpellConditionByDescription();
    public string? SavingThrow => SpellSavingThrowByDescription();
    //Components
    [JsonPropertyName("requires_verbal_components")]
    public bool Verbal { get; set; }
    [JsonPropertyName("requires_somatic_components")]
    public bool Somatic { get; set; }
    [JsonPropertyName("requires_material_components")]
    public bool Material { get; set; }

    public Damage SpellDamageByDescription()
    {
        Damage damage = new();
        Match match = DamageDiceRegex().Match(Description!);
        if (match.Success)
        {
            damage.DamageDice = match.Value;
            return damage;
        }
        return null!;
    }

    public Conditions SpellConditionByDescription()
    {
        List<string> conditions = Enum.GetNames(typeof(Conditions)).ToList();

        foreach (var condition in conditions)
        {
            if (Regex.IsMatch(Description!, @"\b" + condition.ToLower() + @"\b"))
            {
                return (Conditions)Enum.Parse(typeof(Conditions), condition);
            }
        }

        return Conditions.None;
    }

    public string SpellSavingThrowByDescription()
    {
        // A regular expression to find a word before "saving throw"
        string pattern = @"(\w+)\s+saving\s+throw";

        // Find matches in the input using the regular expression
        Match match = Regex.Match(Description!, pattern);

        // Return the match's first word
        if(match.Success) return match.Groups[1].Value;
        return null!;
    }

    [GeneratedRegex(@"(\d{2}|\d{1})d(\d{2}|\d{1})")]
    private static partial Regex DamageDiceRegex();
}
