
using Coins.Repository;
using Coins.Repository.Interfaces;

namespace Coins.UnitOfWork;
public class CoinUoW : ICoinUoW
{
    private readonly ILogger _logger;
    private readonly ICoinRepository _coinRepository;
    public CoinUoW(ILogger<CoinUoW> logger, ICoinRepository coinRepository)
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
}