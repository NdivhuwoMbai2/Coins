using Coins.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Coins.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoinJarController : ControllerBase
{
    private readonly ILogger<CoinJarController> _logger;
    private readonly IMemoryCache _cache;
    private readonly ICoinUoW _coinUoW;
    public CoinJarController(ILogger<CoinJarController> logger, IMemoryCache cache, ICoinUoW coinUoW)
    {
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _coinUoW = coinUoW ?? throw new ArgumentNullException(nameof(coinUoW));
    }

    [HttpPost]
    [Route("addCoin")]
    public async Task<IActionResult> Add(Coin coin)
    {
        try
        {
            var result = await _coinUoW.AddCoinAsync(coin);
            if (result)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(304, new { message = "An error occurred while adding a coin" });

            }
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
          var result = await _coinUoW.GetTotalAmount();
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
    public async Task<IActionResult> Reset()
    {
        try
        {
            throw new NotImplementedException();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Exception on '{MethodName}'. Error Message: '{Message}'", nameof(Reset), exception.Message);
            return StatusCode(500, new { message = "An error occurred while resetting" });
        }
    }
}