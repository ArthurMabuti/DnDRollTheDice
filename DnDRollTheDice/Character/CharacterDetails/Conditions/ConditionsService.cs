using System.Reflection;

namespace DnDRollTheDice.Character.CharacterDetails.Conditions;
internal class ConditionsService
{
    public void ApplyCondition(Conditions condition, Character target, Character spellCaster)
    {
        string methodName = $"Apply{condition}Condition";
        MethodInfo method = GetType().GetMethod(methodName)!;
        List<Conditions> unattackableConditions = [Conditions.Charmed, Conditions.Frightened];

        if (method != null)
        {
            // If condition has a unattackable target send spellcaster information
            if(unattackableConditions.Contains(condition)) method.Invoke(this, [target, spellCaster]);
            else method.Invoke(this, [target]);
            target.Conditions!.Add(condition);
            Console.WriteLine($"{target.Name} is {condition}");
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

    public static void ApplyCharmedCondition(Character target, Character charmer)
    {
        // Charmed creatures can't attack their charmer
        target.UnattackableTarget.Add(charmer);
    }

    public static void RemoveFrightenedCondition(Character character)
    {
        // Implementação para remover a condição "Frightened" do personagem
    }

    // Outros métodos relacionados às condições
}