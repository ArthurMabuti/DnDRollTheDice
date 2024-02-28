﻿using DnDRollTheDice;
using DnDRollTheDice.Character;
using DnDRollTheDice.Character.CharacterItems;
using DnDRollTheDice.Services;

ApiService apiService = new();

#region Testing Character Status

PlayerCharacter brocc = new()
{
    Name = "Brocc",
    Class = "Wizard",
    Proficiency = 20,
    HitPoints = 50
};
await brocc.AssignClassInformationAsync(apiService);
brocc.ArmorClass = new ArmorClass() { Type = "Armor", Value = 18, Armor = new List<Armor>() };
brocc.CharacterWithRandomAbilityScore();

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

#region Testing Monster API

Monster? monster1 = await apiService.GetMonsterFromApiAsync("griffon");
//Monster? monster2 = await apiService.GetMonsterFromApiAsync("goblin");

//Monster? monster = new();
//monster.UseManualStatus();
//monster.SettingAbilityScores();
//monster.Interface();
#endregion

#region Testing Weapon API

Weapon? quarterstaff = await apiService.GetWeaponFromApiAsync("quarterstaff");
brocc.Weapon = quarterstaff!;
//Console.WriteLine(bruenor.ReachArmorClass(monster!));

#endregion

#region Testing Combat System

//while(bruenor!.HitPoints > 0 && monster!.HitPoints > 0)
//{
//    Console.WriteLine($"{bruenor.Name} attacks {monster.Name}");
//    Console.WriteLine($"{monster.Name} HP({monster.HitPoints})");
//    bruenor.DealingDamage(monster);
//    Console.WriteLine($"{bruenor.Name} HP({bruenor!.HitPoints})");
//    Console.WriteLine("Which action do you want to use to attack?");
//    foreach (var monsterAction in monster!.Actions)
//    {
//        Console.WriteLine(monsterAction.Name);
//    }
//    string attackOption = Console.ReadLine()!;
//    monster.AttackAction(bruenor, attackOption);
//    Console.ReadKey();
//    Console.Clear();
//}

//CombatSystem.EngageInBattle();

#endregion

#region Testing Other API

//SpellList? listOfSpells = await apiService.GetSpellListFromApiAsync(0, brocc.Class);

//foreach (var spell in listOfSpells!.Spells!)
//{
//    Console.WriteLine(spell.Name);
//    if(spell.SpellDamage != null) Console.WriteLine(spell.SpellDamage);
//}

#endregion

#region Testing Spell Attack
{
    brocc.ShowAbilityScores();
    CombatSystem.EngageInBattle();
}

#endregion