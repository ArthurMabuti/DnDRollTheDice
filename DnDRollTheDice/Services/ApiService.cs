using DnDRollTheDice.Character;
using DnDRollTheDice.Character.CharacterItems;
using DnDRollTheDice.Character.CharacterSpells;
using DnDRollTheDice.Character;
using System.Text.Json;

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
            newMonster?.SettingAbilityScores();
            return newMonster;
        }
    }

    public async Task<Weapon?> GetWeaponFromApiAsync(string weapon)
    {
        using (HttpClient client = new HttpClient())
        {
            string answer = await client.GetStringAsync($"https://www.dnd5eapi.co/api/equipment/{weapon}/");
            Weapon? newWeapon = JsonSerializer.Deserialize<Weapon>(answer);
            return newWeapon;
        }
    }

    public async Task<SpellList?> GetSpellListFromApiAsync(int level, string characterClass)
    {
        using (HttpClient client = new HttpClient())
        {
            string answer = await client.GetStringAsync($"https://api.open5e.com/v1/spells/?document__slug=wotc-srd&spell_level={level}&spell_lists={characterClass.ToLower()}");
            SpellList? spellList = JsonSerializer.Deserialize<SpellList>(answer);
            return spellList;
        }
    }
}
