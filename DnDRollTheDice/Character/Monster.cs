﻿using System.Text.Json.Serialization;
using DnDRollTheDice.Character.CharacterDetails;

namespace DnDRollTheDice.Character;
internal class Monster : Character
{
    [JsonPropertyName("strength")]
    public int Strength { get; set; }
    [JsonPropertyName("dexterity")]
    public int Dexterity { get; set; }
    [JsonPropertyName("constitution")]
    public int Constitution { get; set; }
    [JsonPropertyName("intelligence")]
    public int Intelligence { get; set; }
    [JsonPropertyName("wisdom")]
    public int Wisdom { get; set; }
    [JsonPropertyName("charisma")]
    public int Charisma { get; set; }
    [JsonPropertyName("actions")]
    public List<Actions> Actions { get; set; }

    public Monster() : base()
    {
        Actions = [];
    }

    public void SettingAbilityScores()
    {
        foreach (var property in typeof(Monster).GetProperties())
        {
            if (property.Name == "HitPoints")
            {
                continue;
            }

            if (property.PropertyType == typeof(int) && property.GetCustomAttributes(typeof(JsonPropertyNameAttribute), true).Length > 0)
            {
                AbilityScores.Add(property.Name, (int)property.GetValue(this)!);
            }
        }
    }

    public void UseManualStatus()
    {
        Name = "Goblin";
        ArmorClass.Add(new() { Type = "armor", Value = 15 });
        HitPoints = 7;
        Speed = new() { Locomotion = "9m"};
        Strength = 8;
        Dexterity = 14;
        Constitution = 10;
        Intelligence = 10;
        Wisdom = 8;
        Charisma = 8;
    }
}