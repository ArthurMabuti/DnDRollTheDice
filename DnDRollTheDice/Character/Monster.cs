using DnDRollTheDice.Character.CharacterDetails;
using DnDRollTheDice.Items;
using System.Text.Json.Serialization;

namespace DnDRollTheDice.Character;
public class Monster : CharacterStatus
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
        AbilityScores = new Dictionary<string, int>();
        ArmorClass = new List<ArmorClass>();
        Speed = new Speed();
    }

    public void SettingAbilityScores()
    {
        foreach (var property in typeof(Monster).GetProperties())
        {
            if (property.Name == "HitPoints")
            {
                continue;
            }

            if (property.PropertyType == typeof(int) && property.GetCustomAttributes(typeof(JsonPropertyNameAttribute), true).Length > 0)
            {
                var attributeName = ((JsonPropertyNameAttribute)property.GetCustomAttributes(typeof(JsonPropertyNameAttribute), true)[0]).Name;
                AbilityScores.Add(property.Name, (int)property.GetValue(this));
            }
        }
    }

    public void UseManualStatus()
    {
        Name = "Goblin";
        ArmorClass.Add(new() { Type = "armor", Value = 15 });
        HitPoints = 7;
        Speed = new() { Locomotion = "9m"};
        Strength = 8;
        Dexterity = 14;
        Constitution = 10;
        Intelligence = 10;
        Wisdom = 8;
        Charisma = 8;
    }

    public override string? ToString()
    {
        return $"Monster: {Name} | AC {ArmorClass.First().Value} | HP {HitPoints}";
    }
}
