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

    private static async Task<T?> GetEntityFromApiAsync<T>(string baseUrl, string entity)
    {
        using (HttpClient client = new())
        {
            string apiUrl = $"{baseUrl}{entity}";
            string answer = await client.GetStringAsync(apiUrl);
            return JsonSerializer.Deserialize<T>(answer);
        }
    }

    public static async Task<Monster?> GetMonsterFromApiAsync(string monster)
    {
        Monster? newMonster = await GetEntityFromApiAsync<Monster>(Dnd5eApiBaseUrl, $"monsters/{monster}");
        newMonster?.SettingAbilityScores();
        return newMonster;
    }

    public static async Task<Weapon?> GetWeaponFromApiAsync(string weapon)
    {
        return await GetEntityFromApiAsync<Weapon>(Dnd5eApiBaseUrl, $"equipment/{weapon}");
    }

    public static async Task<SpellList?> GetSpellListFromApiAsync(int level, string characterClass)
    {
        return await GetEntityFromApiAsync<SpellList>(Open5eApiBaseUrl, $"spells/?document__slug__in=wotc-srd%2Co5e&spell_level={level}&spell_lists={characterClass.ToLower()}");
    }

    public static async Task<ClassList?> GetClassFromApiAsync(string characterClass)
    {
        return await GetEntityFromApiAsync<ClassList>(Open5eApiBaseUrl, $"classes/?slug__in={characterClass.ToLower()}");
    }
}