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
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    public Monster() : base()
    {
        Actions = [];
    }

    public Actions SelectMonsterAction(string actionName)
    {
        Actions monsterAction = Actions.Find(act => act.Name == actionName)!;
        return monsterAction;
    }

    public string ChooseMonsterAction()
    {
        Console.WriteLine("Which action do you want to use to attack?");
        foreach (var monsterAction in Actions!)
        {
            Console.WriteLine(monsterAction.Name);
        }
        string attackOption = Console.ReadLine()!;

        return attackOption;
    }

    public void MultiAttack(Character character)
    {
        int numberOfAttacks = 0;
        List<MultiAttackActions>? multiAttackActions = Actions.Find(act => act.Name == "Multiattack")!.MultiAttackActions;
        foreach (var action in Actions)
        {
            if (action.Name == "Multiattack") continue;
            
            MultiAttackActions multiAttackInformation = multiAttackActions!.Find(multiAct => multiAct.MultiAttackActionName == action.Name)!;

            if (multiAttackInformation == null) continue;

            Actions attackAction = SelectMonsterAction(multiAttackInformation!.MultiAttackActionName!);
            numberOfAttacks = multiAttackInformation.MultiAttackCount;
            while (numberOfAttacks > 0)
            {
                int attackRoll = AttackRoll(this, attackAction.Name);
                MakingAnAttack(this, character, attackAction.Name!, attackRoll);
                numberOfAttacks--;
            }
        }
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