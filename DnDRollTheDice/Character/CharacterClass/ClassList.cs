using System.Text.Json.Serialization;

namespace DnDRollTheDice.Character.CharacterClass;
internal class ClassList
{
    [JsonPropertyName("results")]
    public List<Class>? Class { get; set; }
}
