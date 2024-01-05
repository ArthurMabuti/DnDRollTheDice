using System.Text.Json.Serialization;

namespace DnDRollTheDice.Character.CharacterDetails;
internal class MultiAttackActions
{
    [JsonPropertyName("action_name")]
    public string? MultiAttackActionName { get; set; }
    [JsonPropertyName("count")]
    public int MultiAttackCount { get; set; }
}
