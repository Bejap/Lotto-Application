namespace LottoApplication.Models;

/// <summary>
/// Represents a lotto number and its historical frequency.
/// </summary>
public class FrequencyData
{
    /// <summary>
    /// The lotto number.
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// The frequency of how many times this number has appeared in historical draws.
    /// </summary>
    public int Frequency { get; set; }
}
