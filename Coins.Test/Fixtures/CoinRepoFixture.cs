using Coins.Models;
using Coins.Repository;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace Coins.Test.Fixtures
{
    internal class CoinRepoFixture
    {
        public Mock<IMemoryCache> MockImemoryCache { get; } = new();

        private IConfiguration _configuration;

        private const string coinListCacheKey = "coinListCacheKey";

        #region Setup / Given Members 
        public CoinRepoFixture GivenConfiguration(Dictionary<string, string> appSettings = null)
        {
            var builder = new ConfigurationBuilder();
            builder.AddInMemoryCollection(appSettings);
            _configuration = builder.Build();
            return this;
        }

        public CoinRepoFixture AddCoins(Coin coin)
        {
            MockImemoryCache.Setup(x => x.Set(coinListCacheKey, coin.Amount));
            return this;
        }


        #endregion

        #region Verify
        public CoinRepoFixture VerifyGetChainAsyncWasCalled(Times timesCalled, Coin coin)
        {
            MockImemoryCache.Verify(x => x.Set(It.IsAny<string>(), coin.Amount), timesCalled);
            return this;
        }
        #endregion
        public CoinRepository BuildCoinRepository()
        {
            if (_configuration == null) throw new ArgumentNullException(nameof(_configuration), "Configurations has not been setup for this test!");
            return new Repository.CoinRepository(new NullLogger<CoinRepository>(), MockImemoryCache.Object);
        }

    }
}