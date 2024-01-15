using DnDRollTheDice.Character.CharacterItems;
using DnDRollTheDice.Character.CharacterSpells;
using DnDRollTheDice.Character;
using System.Text.Json;
using DnDRollTheDice.Character.CharacterClass;

namespace DnDRollTheDice.Services;
internal class ApiService
{
    private const string Dnd5eApiBaseUrl = "https://www.dnd5eapi.co/api/";
    private const string Open5eApiBaseUrl = "https://api.open5e.com/v1/";

    private async Task<T?> GetEntityFromApiAsync<T>(string baseUrl, string entity)
    {
        using (HttpClient client = new HttpClient())
        {
            string apiUrl = $"{baseUrl}{entity}";
            string answer = await client.GetStringAsync(apiUrl);
            return JsonSerializer.Deserialize<T>(answer);
        }
    }

    public async Task<Monster?> GetMonsterFromApiAsync(string monster)
    {
        Monster? newMonster = await GetEntityFromApiAsync<Monster>(Dnd5eApiBaseUrl, $"monsters/{monster}");
        newMonster?.SettingAbilityScores();
        return newMonster;
    }

    public async Task<Weapon?> GetWeaponFromApiAsync(string weapon)
    {
        return await GetEntityFromApiAsync<Weapon>(Dnd5eApiBaseUrl, $"equipment/{weapon}");
    }

    public async Task<SpellList?> GetSpellListFromApiAsync(int level, string characterClass)
    {
        return await GetEntityFromApiAsync<SpellList>(Open5eApiBaseUrl, $"spells/?document__slug=wotc-srd&spell_level={level}&spell_lists={characterClass.ToLower()}");
    }

    public async Task<ClassList?> GetClassFromApiAsync(string characterClass)
    {
        return await GetEntityFromApiAsync<ClassList>(Open5eApiBaseUrl, $"classes/?slug__in={characterClass.ToLower()}");
    }
}