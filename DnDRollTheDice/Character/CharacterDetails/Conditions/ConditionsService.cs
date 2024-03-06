using System.Reflection;

namespace DnDRollTheDice.Character.CharacterDetails.Conditions;
internal class ConditionsService
{
    public void ApplyCondition(Conditions condition, Character character)
    {
        string methodName = $"Apply{condition}Condition";
        MethodInfo method = GetType().GetMethod(methodName)!;

        if (method != null)
        {
            method.Invoke(this, [character]);
            character.Conditions!.Add(condition);
            Console.WriteLine($"{character.Name} is {condition}");
        }
        else
        {
            Console.WriteLine($"Method to apply the {condition} condition not found.");
        }
    }
    public static void ApplyPoisonedCondition(Character target)
    {
        // Poisoned character attack with disadvantage
        target.RollType = DiceRolls.RollType.Disadvantage;
    }

    public static void ApplyBlindedCondition(Character target)
    {
        // Blinded character attack with disadvantage
        target.RollType = DiceRolls.RollType.Disadvantage;
    }

    public static void ApplyBlindedCondition(Character character)
    {
        // Implementation to apply the "Blinded" condition to the character
        character.RollType = DiceRolls.RollType.Disadvantage;
    }

    public static void RemoveFrightenedCondition(Character character)
    {
        // Implementação para remover a condição "Frightened" do personagem
    }

    // Outros métodos relacionados às condições
}
