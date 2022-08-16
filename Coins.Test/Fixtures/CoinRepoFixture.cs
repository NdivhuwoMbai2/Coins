using Coins.Models;
using Coins.Repository;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace Coins.Test.Fixtures
{
    internal class CoinRepoFixture
    {
        public IMemoryCache? _memoryCache;

        private IConfiguration? _configuration;

        private const string coinListCacheKey = "coinListCacheKey";

        #region Setup / Given Members 
        public CoinRepoFixture GivenConfiguration(Dictionary<string, string> appSettings = null)
        {
            var builder = new ConfigurationBuilder();
            builder.AddInMemoryCollection(appSettings);
            _memoryCache = BuildMemoCache();
            _configuration = builder.Build();
            return this;
        }
        public IMemoryCache BuildMemoCache()
        {
            //looks like i cannot mock the memocache .. using this route instead
            var services = new ServiceCollection();
            services.AddMemoryCache();
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider.GetService<IMemoryCache>() ?? throw new ArgumentNullException(nameof(IMemoryCache)); ;
        }
        public CoinRepoFixture AddCoins(Coin coin)
        {
            _memoryCache.Set(coinListCacheKey, coin.Amount);
            return this;
        }

        public IMemoryCache GetMemoryCache(decimal expectedValue)
        {
            _memoryCache.TryGetValue(coinListCacheKey, out expectedValue);
            return _memoryCache;
        }

        #endregion

        #region Verify
        public decimal GetCoinsTotal()
        {
            _memoryCache.TryGetValue(coinListCacheKey, out decimal TotalVal);
            return TotalVal;
        }

        public void Reset()
        {
            _memoryCache.Remove(coinListCacheKey); 
        }
        #endregion

        public CoinRepository BuildCoinRepository()
        { 
            if (_configuration == null) throw new ArgumentNullException(nameof(_configuration), "Configurations has not been setup for this test!");
            return new Repository.CoinRepository(new NullLogger<CoinRepository>(), _memoryCache);
        }

    }
}