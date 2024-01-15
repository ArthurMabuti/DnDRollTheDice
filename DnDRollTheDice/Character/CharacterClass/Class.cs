using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace DnDRollTheDice.Character.CharacterClass;
internal class Class
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("desc")]
    public string? Description { get; set; }
    [JsonPropertyName("hit_dice")]
    public string? HitPointsDice { get; set; }
    [JsonPropertyName("prof_armor")]
    private string? proficiencyArmors { get; set; }
    public List<string>? ProficiencyArmors => FormatAndSplit(proficiencyArmors!);
    [JsonPropertyName("prof_weapons")]
    private string? proficiencyWeapons { get; set; }
    public List<string>? ProficiencyWeapons => FormatAndSplit(proficiencyWeapons!);
    [JsonPropertyName("prof_saving_throws")]
    private string? proficiencySavingThrow { get; set; }
    public List<string>? ProficiencySavingThrow => FormatAndSplit(proficiencySavingThrow!);
    [JsonPropertyName("spellcasting_ability")]
    public string? SpellCastingAbility { get; set; }

    private List<string> FormatAndSplit(string input)
    {
        // Remove 's' at the end of each word
        string result = Regex.Replace(input, @"\b(\w+?)s\b", "$1", RegexOptions.IgnoreCase);

        // Split the words using ", "
        List<string> formattedList = result.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries).ToList();

        return formattedList;
    }
}
