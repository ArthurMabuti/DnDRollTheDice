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
    Proficiency = 2
};
bruenor.ArmorClass = new(){ new ArmorClass() { Type = "Armor", Value = 18, Armor = new List<Armor>() }};
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

while(goblin!.HitPoints > 0)
{
    Console.WriteLine($"Goblin HP({goblin.HitPoints})");
    bruenor.DealingDamage(goblin);
}

#endregion