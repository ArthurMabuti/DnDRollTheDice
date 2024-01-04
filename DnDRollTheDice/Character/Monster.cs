using System.Text.Json.Serialization;
using DnDRollTheDice.Character.CharacterDetails;
using DnDRollTheDice.Character.CharacterItems;

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

    public Actions SelectMonsterAction(string actionName)
    {
        Actions monsterAction = Actions.Find(act => act.Name == actionName)!;
        return monsterAction;
    }

    public int AttackRoll(string actionName)
    {
        int diceRolled = Roll.DiceRoll(1, 20);

        int attackValue = diceRolled + SelectMonsterAction(actionName)!.AttackBonus;
        return attackValue;
    }

    public void DealingDamage(Character character, string actionName)
    {
        int attackRoll = AttackRoll(actionName);
        if (ReachArmorClass(character, attackRoll))
        {
            Console.WriteLine("Attack successful!");
            int damage = SelectMonsterAction(actionName).Damage!.DamageRoll(CriticalHit(attackRoll));
            Console.WriteLine($"Damage = {damage}");
            character.HitPoints -= damage;
            Console.WriteLine($"Actual HP from {character.Name} = {character.HitPoints}");
        }
        else
            Console.WriteLine("Attack missed!");
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
        ArmorClass = new ArmorClass { Type = "armor", Value = 15 };
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