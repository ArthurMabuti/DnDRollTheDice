using DnDRollTheDice.Character.CharacterItems;
using System.Text.Json.Serialization;

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

    //MultiAttack Properties
    [JsonPropertyName("actions")]
    public List<MultiAttackActions>? MultiAttackActions { get; set; }

    public Actions()
    {
    }
}