using DnDRollTheDice.Character.CharacterDetails;
using DnDRollTheDice.Items;
using System.Text.Json.Serialization;

namespace DnDRollTheDice.Character;
internal class Monster : CharacterStatus
{
    [JsonPropertyName("name")]
    public new string? Name { get; set; }
    [JsonPropertyName("armor_class")]
    public new List<ArmorClass> ArmorClass { get; set; }
    [JsonPropertyName("hit_points")]
    public new int HitPoints { get; set; }
    [JsonPropertyName("speed")]
    public new Speed Speed { get; set; }
    [JsonPropertyName("strength")]
    public int Strength { get; set; }
    [JsonPropertyName("dexterity")]
    public int Dexterity { get; set; }
    [JsonPropertyName("constitution")]
    public int Constitution { get; set; }
    [JsonPropertyName("intelligence")]
    public int Intelligence { get; set; }
    [JsonPropertyName("wisdom")]
    public int Wisdom { get; set; }
    [JsonPropertyName("charisma")]
    public int Charisma { get; set; }

    public Monster()
    {
        ArmorClass = new List<ArmorClass>();
        Speed = new Speed();
    }

    public override string? ToString()
    {
        return $"Monster: {Name} | AC {ArmorClass.First().Value} | HP {HitPoints}";
    }
}
