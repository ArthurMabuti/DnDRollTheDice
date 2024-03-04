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
        Console.WriteLine("* Disadvantage Dice Roll *");
        int dice1 = DiceRoll(1, 20);
        int dice2 = DiceRoll(1, 20);
        Console.WriteLine($"Dice 1 = {dice1} | Dice 2 = {dice2}");
        int lowerDice = (dice1 < dice2) ? dice1 : dice2;
        Console.WriteLine("It will be used the lower dice:");
        Console.WriteLine($"Result = {lowerDice}");
        return lowerDice;
    }

    public static int AdvantageDiceRoll()
    {
        // Return the higher DiceRoll
        Console.WriteLine("* Advantage Dice Roll *");
        int dice1 = DiceRoll(1, 20);
        int dice2 = DiceRoll(1, 20);
        Console.WriteLine($"Dice 1 = {dice1} | Dice 2 = {dice2}");
        int higherDice = (dice1 > dice2) ? dice1 : dice2;
        Console.WriteLine("It will be used the higher dice:");
        Console.WriteLine($"Result = {higherDice}");
        return higherDice;
    }
}
