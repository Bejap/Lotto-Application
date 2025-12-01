namespace LottoApplication.Models;

/// <summary>
/// Represents a generated lotto row result.
/// </summary>
public class LottoResult
{
    /// <summary>
    /// Timestamp when the numbers were generated.
    /// </summary>
    public DateTime GeneratedAt { get; set; }

    /// <summary>
    /// The generated lotto numbers, sorted in ascending order.
    /// </summary>
    public List<int> Numbers { get; set; } = [];
}
