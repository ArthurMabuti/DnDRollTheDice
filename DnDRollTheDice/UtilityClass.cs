namespace DnDRollTheDice;
internal class UtilityClass
{
    public static string FirstLetterUpper(string text)
    {
        return char.ToUpper(text[0]) + text[1..];
    }

    public static string FormatNameWithSpaces(string targetName)
    {
        // Verifies if the targetName has a composed name, if yes, stores each name in the full name variable, then stores it in targetName
        if (targetName.Split(" ").Length >= 2)
        {
            string[] composedName = targetName.Split(" ");
            string fullName = "";
            foreach (string name in composedName)
            {
                // Store each name with the first char upper
                fullName += FirstLetterUpper(name) + " ";
            }
            return fullName.TrimEnd();
        }
        // If has a single name, just Upper the first letter
        else
        {
            return FirstLetterUpper(targetName);
        }
    }
}
