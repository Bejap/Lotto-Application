using LottoApplication.Models;

namespace LottoApplication.Services;

/// <summary>
/// Service for loading frequency data and generating weighted lotto numbers.
/// </summary>
public class LottoService
{
    private readonly IWebHostEnvironment _environment;
    private List<FrequencyData> _frequencyData = [];
    private readonly int _numbersPerRow = 7;
    private readonly int _maxAttempts = 1000;

    public LottoService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    /// <summary>
    /// Gets the currently loaded frequency data.
    /// </summary>
    public IReadOnlyList<FrequencyData> FrequencyData => _frequencyData.AsReadOnly();

    /// <summary>
    /// Indicates whether frequency data has been loaded.
    /// </summary>
    public bool IsDataLoaded => _frequencyData.Count > 0;

    /// <summary>
    /// Loads frequency data from the CSV file.
    /// </summary>
    public async Task<List<FrequencyData>> LoadFrequencyDataAsync()
    {
        var csvPath = Path.Combine(_environment.ContentRootPath, "data", "lotto_frequency.csv");
        
        if (!File.Exists(csvPath))
        {
            throw new FileNotFoundException("Frequency data file not found", csvPath);
        }

        var lines = await File.ReadAllLinesAsync(csvPath);
        
        // Skip header row and parse data
        _frequencyData = lines.Skip(1)
            .Select(line =>
            {
                var parts = line.Split(',');
                if (parts.Length >= 2 && 
                    int.TryParse(parts[0].Trim(), out var number) && 
                    int.TryParse(parts[1].Trim(), out var frequency))
                {
                    return new FrequencyData { Number = number, Frequency = frequency };
                }
                return null;
            })
            .Where(item => item != null)
            .Cast<FrequencyData>()
            .ToList();

        return _frequencyData;
    }

    /// <summary>
    /// Generates a row of lotto numbers using weighted probability based on frequency data.
    /// </summary>
    public LottoResult GenerateNumbers()
    {
        if (!IsDataLoaded)
        {
            throw new InvalidOperationException("Please load frequency data first");
        }

        var selectedNumbers = SelectWeightedNumbers(_numbersPerRow);

        return new LottoResult
        {
            GeneratedAt = DateTime.Now,
            Numbers = selectedNumbers
        };
    }

    /// <summary>
    /// Selects unique random numbers from a weighted pool based on frequency.
    /// </summary>
    private List<int> SelectWeightedNumbers(int count)
    {
        // Create a weighted pool of numbers based on frequency
        var weightedPool = new List<int>();
        
        foreach (var item in _frequencyData)
        {
            // Add the number to the pool 'frequency' times
            for (var i = 0; i < item.Frequency; i++)
            {
                weightedPool.Add(item.Number);
            }
        }

        // Select unique random numbers from the weighted pool
        var selected = new HashSet<int>();
        var random = Random.Shared;
        var attempts = 0;

        while (selected.Count < count && attempts < _maxAttempts)
        {
            var randomIndex = random.Next(weightedPool.Count);
            var number = weightedPool[randomIndex];
            selected.Add(number);
            attempts++;
        }

        // Convert to list and sort
        var result = selected.ToList();
        result.Sort();
        return result;
    }
}
