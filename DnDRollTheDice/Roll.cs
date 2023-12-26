namespace DnDRollTheDice;

internal class Roll
{
    public int Sides { get; set; }
    public int NumberOfDices { get; set; }

    public static int DiceRoll(int numberOfDices, int sides)
    {
        Random random = new Random();
        int result = 0;
        int repetition = 0;
        while(repetition < numberOfDices)
        {
            int diceRoll = random.Next(1, sides + 1);
            result += diceRoll;
            repetition++;
        }
        return result;
    }
}
