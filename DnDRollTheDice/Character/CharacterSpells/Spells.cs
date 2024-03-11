using DnDRollTheDice.Character.CharacterDetails;
using DnDRollTheDice.Character.CharacterDetails.Conditions;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace DnDRollTheDice.Character.CharacterSpells;
internal partial class Spells
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("desc")]
    public string? Description { get; set; }
    [JsonPropertyName("target_range_sort")]
    public int Range { get; set; }
    [JsonPropertyName("duration")]
    public string? Duration { get; set; }
    [JsonPropertyName("casting_time")]
    public string? CastingTime { get; set; }
    [JsonPropertyName("spell_level")]
    public int Level { get; set; }
    [JsonPropertyName("school")]
    public string? School { get; set; }
    [JsonPropertyName("requires_concentration")]
    public bool Concentration { get; set; }
    public Damage? SpellDamage => SpellDamageByDescription();
    public Conditions Condition => SpellConditionByDescription();
    public string? SavingThrow => SpellSavingThrowByDescription();
    public bool MakeSpellAttack => MakeSpellAttackByDescription();
    //Components
    [JsonPropertyName("requires_verbal_components")]
    public bool Verbal { get; set; }
    [JsonPropertyName("requires_somatic_components")]
    public bool Somatic { get; set; }
    [JsonPropertyName("requires_material_components")]
    public bool Material { get; set; }

    public void CastingSpell<T>(Character spellCaster, List<T> allCharacters) where T : Character
    {
        Character target = Character.ChooseTarget(allCharacters);
        bool madeSpellAttack = false;
        bool spellHit = false;
        // Verifies if the spell has to make an attackRoll
        if (MakeSpellAttack)
        {
            // Stores the diceRoll result into attackRoll and make an attack depending on the result
            int attackRoll = spellCaster.AttackRoll(target, null, this);
            spellCaster.DealingDamage(target, Name!, attackRoll, this);
            // Confirms that the spell attack was made
            madeSpellAttack = true;
            // Confirms if the spell hit the target
            spellHit = Character.ReachArmorClass(target, attackRoll);
        }
        if (SavingThrow is not null)
        {
            if ((madeSpellAttack && !spellHit) || target.IsUnconscious())
            {
                // If matches the condition, nothing happens
            }
            else
            {
                string ability = UtilityClass.FirstLetterUpper(SavingThrow!);
                int savingThrowResult = target.SavingThrow(ability, SpellCasterAbility(spellCaster, ability));
                int totalDamage;
                bool savingThrowFailed = false;
                
                // If the spell caster ability is higher than the result the target gets the full damage
                if (savingThrowResult > SpellCasterAbility(spellCaster, ability))
                {
                    Console.WriteLine("Saving Throw Successful");
                }
                else
                {
                    Console.WriteLine("Saving Throw Failed");
                    savingThrowFailed = true;
                }
                //Verify if it was already made a spell attack to the target and there's damage to be applied
                if (!madeSpellAttack && SpellDamage != null)
                {
                    // Depending if it passed the saving throw, takes full damage or half damage
                    totalDamage = savingThrowFailed ? SpellDamage!.DamageRoll(false) : SpellDamage!.DamageRoll(false) / 2;
                    Console.WriteLine($"Damage = {totalDamage}");
                    // Subtracts the target total HitPoints with the damage taken
                    target.HitPoints -= totalDamage;
                    // Shows actual HitPoints total from target
                    Console.WriteLine($"Actual HP from {target.Name} = {target.HitPoints}");
                    // Set Unconsciuous if the HP gets to 0
                    Character.SetUnconscious(target);
                }
                // If the spell has a condition, apply on the target
                if (Condition != Conditions.None)
                {
                    // Verify if it failed on the saving throw
                    if (savingThrowFailed)
                    {
                        // Search at the ConditionService class for the method corresponding the condition
                        ConditionsService cs = new();
                        cs.ApplyCondition(Condition, target, spellCaster);
                    }
                }
            }
        }
        Console.ReadKey();
    }

    private static int SpellCasterAbility(Character spellCaster, string ability)
    {
        return 8 + spellCaster.Proficiency + Character.ModifierValue(spellCaster.AbilityScores[ability]);
    }

    private Damage SpellDamageByDescription()
    {
        Damage damage = new();
        Match match = DamageDiceRegex().Match(Description!);
        if (match.Success)
        {
            damage.DamageDice = match.Value;
            return damage;
        }
        return null!;
    }

    private Conditions SpellConditionByDescription()
    {
        List<string> conditions = Enum.GetNames(typeof(Conditions)).ToList();

        foreach (var condition in conditions)
        {
            if (Regex.IsMatch(Description!, @"\b" + condition.ToLower() + @"\b"))
            {
                return (Conditions)Enum.Parse(typeof(Conditions), condition);
            }
        }

        return Conditions.None;
    }

    private string SpellSavingThrowByDescription()
    { 
        // A regular expression to find a word before "saving throw"
        string pattern = @"(\w+)\s+saving\s+throw";

        // Find matches in the input using the regular expression
        Match match = Regex.Match(Description!, pattern);

        // Return the match's first word
        if(match.Success) return match.Groups[1].Value;
        return null!;
    }

    private bool MakeSpellAttackByDescription()
    {
        // A regular expression to find the sentence that states a spell attack
        string pattern = @"Make\s+a\s+(ranged|melee)\s+spell\s+attack";

        // Find matches in the input using the regular expression
        Match match = Regex.Match(Description!, pattern);

        // Return if it has to make a spell attack
        if (match.Success) return true;
        return false;
    }

    [GeneratedRegex(@"(\d{2}|\d{1})d(\d{2}|\d{1})")]
    private static partial Regex DamageDiceRegex();
}

//TODO Implement the use of saving throw and applying poisoned condition using the Ray of Sickness 1st level spell [OK]
//TODO Implement the use of saving throw and applying blinded condition using the Color Spray 1st level spell []
//TODO Add other spells that are being removed
/*
Spell Types:
Offensive = SpellAttack|SavingThrow + Damage [OK]
Control = SavingThrow + Condition [OK]
Automatic = Damage+Condition (Sleep) [] | Damage (Magic Missile) []
Healing = HitPoints [] | Temporary HitPoints []
*/