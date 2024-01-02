﻿using System.Text.Json.Serialization;

namespace DnDRollTheDice.Character.CharacterItems;

public class ArmorClass
{
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("value")]
    public int Value { get; set; }

    [JsonPropertyName("armor")]
    public List<Armor> Armor { get; set; }

    public ArmorClass()
    {
        Armor = new List<Armor>();
    }

    public override string? ToString()
    {
        return $"CA: {Value}";
    }
}
