using Coins.Test.Fixtures;
using Moq;

namespace Coins.Test.UnitTests.Repository
{
    public class CoinTests
    {
        [Fact]
        public async Task AddCoin_ShouldReturn_Void_Success()
        {
            //Arrange
            var coins= new Models.Coin() { Amount = 200, Volume = 42 };
            var fixture = new CoinRepoFixture()
               .GivenConfiguration();

            var coinRepo = fixture.BuildCoinRepository();

            coinRepo.AddCoinAsync(coins);
            fixture.VerifyGetChainAsyncWasCalled(Times.Once(), coins); 
        }
    }
}