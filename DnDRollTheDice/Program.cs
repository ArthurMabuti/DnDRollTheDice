using DnDRollTheDice.Character;
using DnDRollTheDice.Character.CharacterItems;
using DnDRollTheDice.Services;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#region Testing Character Status

//Testing Modifier Value
PlayerCharacter bruenor = new()
{
    Name = "Bruenor",
    Class = "Fighter",
    Proficiency = 2,
    HitPoints = 20
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

List<Character> allCharacters = new()
{
    bruenor,
    monster1,
    monster2
};

List<string> charactersNameByInitiative = OrderByInitiative(allCharacters);

while(bruenor.HitPoints > 0 && (monster1.HitPoints > 0 || monster2.HitPoints > 0))
{
    foreach (var characterName in charactersNameByInitiative)
    {
        Character activeCharacter = allCharacters.Find(cha => cha.Name == characterName)!;
        if (bruenor.HitPoints <= 0) break;
        if (activeCharacter is PlayerCharacter playerCharacter)
        {
            if (monster1.HitPoints > 0)
                playerCharacter.DealingDamage(monster1);
            else playerCharacter.DealingDamage(monster2);
            continue;
        }
        if (activeCharacter is Monster monsterCharacter && monsterCharacter.HitPoints > 0)
        {
            Console.WriteLine("Which action do you want to use to attack?");
            foreach (var monsterAction in monsterCharacter!.Actions)
            {
                Console.WriteLine(monsterAction.Name);
            }
            string attackOption = Console.ReadLine()!;
            monsterCharacter.AttackAction(bruenor, attackOption);
        }
    }
}

#endregion

List<string> OrderByInitiative(List<Character> characters)
{
    List<Character> list = characters.OrderByDescending(c => c.Initiative).ToList();
    List<string> orderList = new List<string>();
    foreach(var character in list)
    {
        orderList.Add(character.Name!);
    }
    return orderList;
}