using DnDRollTheDice.Character.CharacterItems;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace DnDRollTheDice.Character.CharacterDetails;
internal class Actions
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("desc")]
    public string? Description { get; set; }
    [JsonPropertyName("attack_bonus")]
    public int AttackBonus { get; set; }
    [JsonPropertyName("damage")]
    public List<Damage>? damage { get; set; }
    public Damage? Damage
    {
        get => damage!.First();
        set => damage = [value!];
    }
    public string AttackRange => AttackRangeByDescription();

    //MultiAttack Properties
    [JsonPropertyName("actions")]
    public List<MultiAttackActions>? MultiAttackActions { get; set; }

    public Actions()
    {
    }

    private string AttackRangeByDescription()
    {
        // A regular expression to find a word before "saving throw"
        string pattern = @"(\w+)\s+Weapon\s+Attack";

        // Find matches in the input using the regular expression
        Match match = Regex.Match(Description!, pattern);

        // Return the match's first word
        if (match.Success) return match.Groups[1].Value;
        return null!;
    }
}