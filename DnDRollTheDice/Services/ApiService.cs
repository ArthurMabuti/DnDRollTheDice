using DnDRollTheDice.Character;
using System.Text.Json;

namespace DnDRollTheDice.Services;
internal class ApiService
{
    public async Task<Monster?> GetMonsterFromApiAsync(string monster)
    {
        using (HttpClient client = new HttpClient())
        {
            string answer = await client.GetStringAsync($"https://www.dnd5eapi.co/api/monsters/{monster}/");
            Monster? newMonster = JsonSerializer.Deserialize<Monster>(answer);
            newMonster?.SettingAbilityScores();
            newMonster?.CopyPropertiesToBase();
            return newMonster;
        }
    }
}
