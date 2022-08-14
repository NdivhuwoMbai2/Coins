using Coins.Repository.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Coins.Repository;
public class CoinRepository : ICoinRepository
{
    private readonly ILogger _logger; 
    private readonly IMemoryCache _memoryCache;
    public CoinRepository(ILogger<CoinRepository> logger, IMemoryCache memoryCache)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
    }
    public async Task<bool> AddCoinAsync(Coin coin)
    {
        throw new NotImplementedException();
    }

    public async Task<decimal> GetTotalAmount()
    {
        throw new NotImplementedException();
    }
}