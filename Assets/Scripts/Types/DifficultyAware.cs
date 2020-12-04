using System;
/// <summary>
/// This struct is helper type to create values that scale with difficulty
/// </summary>
public struct DifficultyAware
{
    /// <summary>
    /// Creates difficulty aware struct.
    /// </summary>
    /// <param name="baseValue">Value without any difficulty applied</param>
    /// <param name="difficultyModifier">Multiplier how much value should increase</param>
    /// <param name="equation">Custom equation to calculate value</param>
    public DifficultyAware(float baseValue, float difficultyModifier, Func<DifficultyAware, float> equation = null)
    {
        BaseValue = baseValue;
        DifficultyModifier = difficultyModifier;
        if(equation != null)
        {
            Equation = equation;
        }
        else
        {
            // Default Equation 
            Equation = x => x.BaseValue * x.DifficultyModifier;
        }
    }

    /// <summary>
    /// Base value. This is the unmodified value.
    /// </summary>
    public float BaseValue { get; set; }

    /// <summary>
    /// This is the value after difficulty has been applied into it.
    /// </summary>
    public float Value => Equation(this);

    /// <summary>
    /// This defines how the scaling is applied
    /// </summary>
    public Func<DifficultyAware, float> Equation { get; set; }

    /// <summary>
    /// Global difficulty modifier
    /// </summary>
    public float DifficultyModifier { get; set; }

}
