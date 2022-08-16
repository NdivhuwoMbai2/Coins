using Coins.Models;
using Coins.Repository.Interfaces;
using Microsoft.Extensions.Caching.Memory;
namespace Coins.Repository
{
    public class CoinRepository : ICoinRepository
    {
        private readonly ILogger _logger;
        private const string coinListCacheKey = "coinListCacheKey";
        private readonly IMemoryCache _memoryCache;
        private MemoryCacheEntryOptions cacheEntryOptions;
        public CoinRepository(ILogger<CoinRepository> logger, IMemoryCache memoryCache)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            cacheEntryOptions = SetupMemoOptions();
        }

        private MemoryCacheEntryOptions SetupMemoOptions()
        {
            return new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                        .SetPriority(CacheItemPriority.Normal)
                        .SetSize(1024);
        }

        public void AddCoinAsync(Coin coin)
        {
            try
            {
                _logger.Log(LogLevel.Information, "Trying to retrieve coins");
                if (_memoryCache.TryGetValue(coinListCacheKey, out decimal TotalCoins))
                {
                    _logger.Log(LogLevel.Information, "Summing up the total");
                    TotalCoins = TotalCoins + coin.Amount;
                    _memoryCache.Set(coinListCacheKey, TotalCoins, cacheEntryOptions);
                }
                else
                {
                    _logger.Log(LogLevel.Information, "Adding coins to a list");
                    _memoryCache.Set(coinListCacheKey, coin.Amount, cacheEntryOptions);
                }
            }
            catch (Exception exception)
            {
                throw new Exception($"Exception on '{nameof(AddCoinAsync)}'. Error Message: '{exception.Message}'", exception);
            }

        }
        public decimal GetTotalAmount()
        {
            try
            {
                _logger.Log(LogLevel.Information, "Trying to get total amount coins");
                if (_memoryCache.TryGetValue(coinListCacheKey, out decimal TotalCoins))
                {
                    return TotalCoins;
                }
                else
                {
                    _logger.Log(LogLevel.Warning, "No totalCoins found");
                    throw new Exception($"Exception on GetTotalAmount'. Error Message: No totalCoins found'");
                }
            }
            catch (Exception exception)
            {
                throw new Exception($"Exception on '{nameof(GetTotalAmount)}'. Error Message: '{exception.Message}'", exception);
            }

        }

        public void Reset()
        {
            _logger.Log(LogLevel.Information, "Trying to reset total amount coins");
            if (_memoryCache.TryGetValue(coinListCacheKey, out decimal TotalCoins))
            {
                _memoryCache.Remove(coinListCacheKey);
                _logger.Log(LogLevel.Information, "Removed total coin amount");
            }
            else
            {
                _logger.Log(LogLevel.Information, "Total Amount not found"); 
            }
        }

    }
}