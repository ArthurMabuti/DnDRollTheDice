namespace DnDRollTheDice.Character.CharacterDetails.Conditions;
internal static class ConditionsExtensions
{
    public static bool IsPoisoned(this Conditions conditions)
    {
        return conditions.HasFlag(Conditions.Poisoned);
    }

    public static bool IsFrightened(this Conditions conditions)
    {
        return conditions.HasFlag(Conditions.Frightened);
    }

    // Outros métodos de extensão relacionados às condições
}
