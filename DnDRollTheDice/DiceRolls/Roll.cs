namespace DnDRollTheDice.DiceRolls;

internal class Roll
{
    public int Sides { get; set; }
    public int NumberOfDices { get; set; }

    public static int DiceRoll(int numberOfDices, int sides)
    {
        Random random = new();
        int result = 0;
        int repetition = 0;
        while (repetition < numberOfDices)
        {
            int diceRoll = random.Next(1, sides + 1);
            result += diceRoll;
            repetition++;
        }
        return result;
    }

    public static int DisadvantageDiceRoll()
    {
        // Return the lower DiceRoll
        return Math.Min(DiceRoll(1, 20), DiceRoll(1, 20));
    }

    public static int AdvantageDiceRoll()
    {
        // Return the higher DiceRoll
        return Math.Max(DiceRoll(1, 20), DiceRoll(1, 20));
    }
}
