using System.Text.Json.Serialization;

namespace DnDRollTheDice.Character.CharacterDetails;
public class Actions
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("desc")]
    public string? Description { get; set; }
    [JsonPropertyName("attack_bonus")]
    public int AttackBonus { get; set; }
    [JsonPropertyName("damage")]
    private List<Damage>? damage { get; set; }
    public Damage Damage
    {
        get => damage!.First();
        set => damage = new List<Damage> { value };
    }
}