using DnDRollTheDice;
using DnDRollTheDice.Character;
using DnDRollTheDice.Character.CharacterItems;
using DnDRollTheDice.Services;

#region Testing Character Status

//Testing Modifier Value
PlayerCharacter bruenor = new()
{
    Name = "Bruenor",
    Class = "Fighter",
    Proficiency = 2,
    HitPoints = 16
};
bruenor.ArmorClass = new ArmorClass() { Type = "Armor", Value = 18, Armor = new List<Armor>() };
bruenor.CharacterWithRandomAbilityScore();

bruenor.Interface();

//Testing Dice Roll
//Fireball
//Console.WriteLine($"Damage from Fireball: {Roll.DiceRoll(8, 6)}");

//Testing Initiative
//Console.WriteLine($"Bruenor Dexterity: {bruenor.AbilityScores["Dexterity"]}");
//bruenor.InitiativeCheck();
//Console.WriteLine($"Bruenor initiative value is {bruenor.Initiative}");

//Testing RandomAbilityScore
//bruenor.GenerateRandomAbilityScore();
#endregion

ApiService apiService = new();

#region Testing Monster API

Monster? goblin = await apiService.GetMonsterFromApiAsync("goblin");

//Monster? goblin = new();
//goblin.UseManualStatus();
//goblin.SettingAbilityScores();
//goblin.Interface();
#endregion

#region Testing Weapon API

Weapon? greatsword = await apiService.GetWeaponFromApiAsync("greatsword");
bruenor.Weapon = greatsword!;
//Console.WriteLine(bruenor.ReachArmorClass(goblin!));

#endregion

#region Testing Combat System

while(bruenor!.HitPoints > 0 && goblin!.HitPoints > 0)
{
    Console.WriteLine("Bruenor ataca Goblin");
    Console.WriteLine($"Goblin HP({goblin.HitPoints})");
    bruenor.DealingDamage(goblin);
    Console.WriteLine($"Bruenor HP({bruenor!.HitPoints})");
    Console.WriteLine("Which action do you want to use to attack?");
    foreach (var monsterAction in goblin!.Actions)
    {
        Console.WriteLine(monsterAction.Name);
    }
    string attackOption = Console.ReadLine()!;
    goblin.DealingDamage(bruenor, attackOption);
}

#endregion