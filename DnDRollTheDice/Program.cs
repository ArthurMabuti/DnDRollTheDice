﻿using DnDRollTheDice;
using DnDRollTheDice.Character;
using DnDRollTheDice.Character.CharacterItems;
using DnDRollTheDice.Services;
using System.Xml.Linq;

#region Testing Character Status

PlayerCharacter bruenor = new()
{
    Name = "Bruenor",
    Class = "Fighter",
    Proficiency = 2,
    HitPoints = 20
};
bruenor.ArmorClass = new ArmorClass() { Type = "Armor", Value = 18, Armor = new List<Armor>() };
bruenor.CharacterWithRandomAbilityScore();

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

Monster? monster1 = await apiService.GetMonsterFromApiAsync("goblin");
Monster? monster2 = await apiService.GetMonsterFromApiAsync("kobold");

//Monster? monster = new();
//monster.UseManualStatus();
//monster.SettingAbilityScores();
//monster.Interface();
#endregion

#region Testing Weapon API

Weapon? greatsword = await apiService.GetWeaponFromApiAsync("greatsword");
bruenor.Weapon = greatsword!;
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

#endregion

#region Testing Initiative Order

CombatSystem.EngageInBattle();

#endregion