namespace DnDRollTheDice.Character.CharacterDetails.Conditions;
internal class ConditionsService
{
    public void ApplyCondition(Conditions condition, Character character)
    {
        string methodName = $"Apply{condition}Condition";
        MethodInfo method = GetType().GetMethod(methodName)!;

        if (method != null)
        {
            method.Invoke(this, new object[] { character });
            character.Conditions!.Add(condition);
        }
        else
        {
            Console.WriteLine($"Método para aplicar a condição {condition} não encontrado.");
        }
    }
    public static void ApplyPoisonedCondition(Character character)
    {
        // Implementação para aplicar a condição "Poisoned" ao personagem
    }

    public static void RemoveFrightenedCondition(Character character)
    {
        // Implementação para remover a condição "Frightened" do personagem
    }

    // Outros métodos relacionados às condições
}
