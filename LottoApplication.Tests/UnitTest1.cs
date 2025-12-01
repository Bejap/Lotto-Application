using LottoApplication.Services;
using Microsoft.AspNetCore.Hosting;
using Moq;

namespace LottoApplication.Tests;

public class LottoServiceTests
{
    private readonly Mock<IWebHostEnvironment> _mockEnvironment;
    private readonly string _testDataPath;

    public LottoServiceTests()
    {
        _mockEnvironment = new Mock<IWebHostEnvironment>();
        _testDataPath = Path.Combine(Path.GetTempPath(), "LottoTests");
        
        // Ensure test directory exists
        Directory.CreateDirectory(_testDataPath);
        Directory.CreateDirectory(Path.Combine(_testDataPath, "data"));
        
        _mockEnvironment.Setup(e => e.ContentRootPath).Returns(_testDataPath);
    }

    [Fact]
    public async Task LoadFrequencyDataAsync_ValidCsv_ReturnsFrequencyData()
    {
        // Arrange
        var csvContent = "number,frequency\n1,100\n2,50\n3,75";
        var csvPath = Path.Combine(_testDataPath, "data", "lotto_frequency.csv");
        await File.WriteAllTextAsync(csvPath, csvContent);
        
        var service = new LottoService(_mockEnvironment.Object);

        // Act
        var result = await service.LoadFrequencyDataAsync();

        // Assert
        Assert.Equal(3, result.Count);
        Assert.Contains(result, x => x.Number == 1 && x.Frequency == 100);
        Assert.Contains(result, x => x.Number == 2 && x.Frequency == 50);
        Assert.Contains(result, x => x.Number == 3 && x.Frequency == 75);
    }

    [Fact]
    public async Task LoadFrequencyDataAsync_FileNotFound_ThrowsFileNotFoundException()
    {
        // Arrange
        var emptyPath = Path.Combine(Path.GetTempPath(), "EmptyLottoTests");
        Directory.CreateDirectory(emptyPath);
        Directory.CreateDirectory(Path.Combine(emptyPath, "data"));
        
        var mockEnv = new Mock<IWebHostEnvironment>();
        mockEnv.Setup(e => e.ContentRootPath).Returns(emptyPath);
        
        var service = new LottoService(mockEnv.Object);

        // Act & Assert
        await Assert.ThrowsAsync<FileNotFoundException>(() => service.LoadFrequencyDataAsync());
    }

    [Fact]
    public async Task GenerateNumbers_DataLoaded_ReturnsSevenUniqueNumbers()
    {
        // Arrange
        var csvContent = "number,frequency\n1,10\n2,10\n3,10\n4,10\n5,10\n6,10\n7,10\n8,10\n9,10\n10,10";
        var csvPath = Path.Combine(_testDataPath, "data", "lotto_frequency.csv");
        await File.WriteAllTextAsync(csvPath, csvContent);
        
        var service = new LottoService(_mockEnvironment.Object);
        await service.LoadFrequencyDataAsync();

        // Act
        var result = service.GenerateNumbers();

        // Assert
        Assert.Equal(7, result.Numbers.Count);
        Assert.Equal(result.Numbers.Distinct().Count(), result.Numbers.Count); // All unique
        Assert.True(result.Numbers.All(n => n >= 1 && n <= 10)); // Within valid range
    }

    [Fact]
    public async Task GenerateNumbers_DataLoaded_ReturnsSortedNumbers()
    {
        // Arrange
        var csvContent = "number,frequency\n1,10\n2,10\n3,10\n4,10\n5,10\n6,10\n7,10\n8,10\n9,10\n10,10";
        var csvPath = Path.Combine(_testDataPath, "data", "lotto_frequency.csv");
        await File.WriteAllTextAsync(csvPath, csvContent);
        
        var service = new LottoService(_mockEnvironment.Object);
        await service.LoadFrequencyDataAsync();

        // Act
        var result = service.GenerateNumbers();

        // Assert
        var sorted = result.Numbers.OrderBy(x => x).ToList();
        Assert.Equal(sorted, result.Numbers);
    }

    [Fact]
    public void GenerateNumbers_DataNotLoaded_ThrowsInvalidOperationException()
    {
        // Arrange
        var service = new LottoService(_mockEnvironment.Object);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => service.GenerateNumbers());
    }

    [Fact]
    public async Task GenerateNumbers_ReturnsTimestamp()
    {
        // Arrange
        var csvContent = "number,frequency\n1,10\n2,10\n3,10\n4,10\n5,10\n6,10\n7,10\n8,10\n9,10\n10,10";
        var csvPath = Path.Combine(_testDataPath, "data", "lotto_frequency.csv");
        await File.WriteAllTextAsync(csvPath, csvContent);
        
        var service = new LottoService(_mockEnvironment.Object);
        await service.LoadFrequencyDataAsync();
        
        var beforeGeneration = DateTime.Now;

        // Act
        var result = service.GenerateNumbers();

        // Assert
        Assert.True(result.GeneratedAt >= beforeGeneration);
        Assert.True(result.GeneratedAt <= DateTime.Now);
    }

    [Fact]
    public async Task FrequencyData_AfterLoad_ReturnsReadOnlyList()
    {
        // Arrange
        var csvContent = "number,frequency\n1,100\n2,50";
        var csvPath = Path.Combine(_testDataPath, "data", "lotto_frequency.csv");
        await File.WriteAllTextAsync(csvPath, csvContent);
        
        var service = new LottoService(_mockEnvironment.Object);
        await service.LoadFrequencyDataAsync();

        // Act
        var data = service.FrequencyData;

        // Assert
        Assert.Equal(2, data.Count);
        Assert.True(service.IsDataLoaded);
    }

    [Fact]
    public void IsDataLoaded_BeforeLoad_ReturnsFalse()
    {
        // Arrange
        var service = new LottoService(_mockEnvironment.Object);

        // Assert
        Assert.False(service.IsDataLoaded);
    }
}
