using Coins.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Coins.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/{version:apiVersion}/[controller]")]
public class CoinJarController : ControllerBase
{
    private readonly ILogger<CoinJarController> _logger;
    private readonly IMemoryCache _cache;
    private readonly ICoinJar _coinJar;
    public CoinJarController(ILogger<CoinJarController> logger, IMemoryCache cache, ICoinJar coinJar)
    {
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _coinJar = coinJar ?? throw new ArgumentNullException(nameof(coinJar));
    }

    [HttpPost]
    [Route("addCoin")]
    public async Task<IActionResult> Add(Coin coin)
    {
        try
        {
            _coinJar.AddCoinAsync(coin); 
            return Ok();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Exception on '{MethodName}'. Error Message: '{Message}'", nameof(Add), exception.Message);
            return StatusCode(500, new { message = "An error occurred while adding a coin" });
        }
    }

    [HttpGet]
    [Route("getTotalAmount")]
    public async Task<IActionResult> GetTotalAmount()
    {
        try
        {
            var result = await _coinJar.GetTotalAmount();
            if (string.IsNullOrEmpty(result.ToString()))
            {
                _logger.LogError("'{MethodName}'. Error Message: '{Message}'", nameof(GetTotalAmount), "Total Amount not found");
                return NotFound();
            }
            return Ok(result);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Exception on '{MethodName}'. Error Message: '{Message}'", nameof(GetTotalAmount), exception.Message);
            return StatusCode(500, new { message = "An error occurred while retrieving total amount" });
        }
    }
    [HttpGet]
    [Route("reset")]
    public IActionResult Reset()
    {
        try
        {
            _coinJar.Reset();
            return Ok();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Exception on '{MethodName}'. Error Message: '{Message}'", nameof(Reset), exception.Message);
            return StatusCode(500, new { message = "An error occurred while resetting" });
        }
    }
}