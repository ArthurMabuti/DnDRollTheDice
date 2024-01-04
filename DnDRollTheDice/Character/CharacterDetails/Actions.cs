using DnDRollTheDice.Character.CharacterItems;
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
    public List<Damage>? damage { get; set; }
    public Damage? Damage
    {
        get => damage!.First();
        set => damage = [value!];
    }

    //Multiattack Properties

    [JsonPropertyName("actions")]
    public List<Actions> MultiAttackActions { get; set; }
    [JsonPropertyName("action_name")]
    public string ActionName { get; set; }
    [JsonPropertyName("count")]
    public int Count { get; set; }

    public Actions()
    {
    }
}