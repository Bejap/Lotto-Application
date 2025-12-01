using LottoApplication.Models;

namespace LottoApplication.Services;

/// <summary>
/// Interface for the lotto service that handles frequency data loading and number generation.
/// </summary>
public interface ILottoService
{
    /// <summary>
    /// Gets the currently loaded frequency data.
    /// </summary>
    IReadOnlyList<FrequencyData> FrequencyData { get; }

    /// <summary>
    /// Indicates whether frequency data has been loaded.
    /// </summary>
    bool IsDataLoaded { get; }

    /// <summary>
    /// Loads frequency data from the CSV file.
    /// </summary>
    /// <returns>A read-only list of frequency data items.</returns>
    Task<IReadOnlyList<FrequencyData>> LoadFrequencyDataAsync();

    /// <summary>
    /// Generates a row of lotto numbers using weighted probability based on frequency data.
    /// </summary>
    /// <returns>A lotto result containing the generated numbers.</returns>
    LottoResult GenerateNumbers();
}
