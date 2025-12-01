using LottoApplication.Models;
using LottoApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace LottoApplication.Controllers;

/// <summary>
/// API controller for lotto number generation operations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class LottoController : ControllerBase
{
    private readonly ILottoService _lottoService;

    public LottoController(ILottoService lottoService)
    {
        _lottoService = lottoService;
    }

    /// <summary>
    /// Loads frequency data from the CSV file.
    /// </summary>
    [HttpPost("load-data")]
    public async Task<ActionResult<LoadDataResponse>> LoadFrequencyData()
    {
        try
        {
            var data = await _lottoService.LoadFrequencyDataAsync();
            return Ok(new LoadDataResponse
            {
                Success = true,
                Message = $"Successfully loaded {data.Count} numbers",
                FrequencyData = data.OrderByDescending(x => x.Frequency).ToList()
            });
        }
        catch (FileNotFoundException ex)
        {
            return NotFound(new LoadDataResponse
            {
                Success = false,
                Message = $"Error: {ex.Message}"
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new LoadDataResponse
            {
                Success = false,
                Message = $"Error: {ex.Message}"
            });
        }
    }

    /// <summary>
    /// Gets the currently loaded frequency data.
    /// </summary>
    [HttpGet("frequency-data")]
    public ActionResult<List<FrequencyData>> GetFrequencyData()
    {
        if (!_lottoService.IsDataLoaded)
        {
            return BadRequest(new { message = "Please load frequency data first" });
        }

        return Ok(_lottoService.FrequencyData.OrderByDescending(x => x.Frequency).ToList());
    }

    /// <summary>
    /// Generates a row of lotto numbers based on weighted probability.
    /// </summary>
    [HttpPost("generate")]
    public ActionResult<LottoResult> GenerateNumbers()
    {
        try
        {
            var result = _lottoService.GenerateNumbers();
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = $"Error: {ex.Message}" });
        }
    }
}

/// <summary>
/// Response model for the load data endpoint.
/// </summary>
public class LoadDataResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public List<FrequencyData>? FrequencyData { get; set; }
}
