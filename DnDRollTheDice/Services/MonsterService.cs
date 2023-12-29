using DnDRollTheDice.Character;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DnDRollTheDice.Services;
public class MonsterService
{
    public async Task<Monster?> GetMonsterFromApiAsync(string monster)
    {
        using (HttpClient client = new HttpClient())
        {
            string answer = await client.GetStringAsync($"https://www.dnd5eapi.co/api/monsters/{monster}/");
            Monster? newMonster = JsonSerializer.Deserialize<Monster>(answer);
            newMonster?.SettingAbilityScores();
            return newMonster;
        }
    }
}
