
using Coins.Repository;
using Coins.Repository.Interfaces;

namespace Coins.UnitOfWork;
public class CoinJar : ICoinJar
{
    private readonly ILogger _logger;
    private readonly ICoinRepository _coinRepository;
    public CoinJar(ILogger<CoinJar> logger, ICoinRepository coinRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _coinRepository = coinRepository ?? throw new ArgumentNullException(nameof(coinRepository));
    }

    public async Task<bool> AddCoinAsync(Coin coin)
    {
        var result = false;
        try
        {
            return await _coinRepository.AddCoinAsync(coin);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception occurred when trying to add coin. Message: '{Message}'", ex.Message);
            return result;
        }
    }

    public async Task<decimal> GetTotalAmount()
    { 
        try
        {
            return await _coinRepository.GetTotalAmount();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Exception occurred when trying to add coin. Message: '{Message}'", exception.Message);
            throw new Exception($"Exception on '{nameof(GetTotalAmount)}'. Error Message: '{exception.Message}'");
        }
    }

    public void Reset()
    {
        try
        {
            _coinRepository.Reset();
        }
        catch (Exception exception)
        { 
            _logger.LogError(exception, "Exception occurred resetting coins. Message: '{Message}'", exception.Message);
            throw new Exception($"Exception on '{nameof(GetTotalAmount)}'. Error Message: '{exception.Message}'");
        } 
    }
}