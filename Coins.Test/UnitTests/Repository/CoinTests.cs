using Coins.Test.Fixtures;

namespace Coins.Test.UnitTests.Repository
{
    public class CoinTests
    {
        [Fact]
        public void AddCoin_ShouldReturn_Void_True()
        {
            //Arrange
            var coins = new Models.Coin() { Amount = 200, Volume = 42 };
            var fixture = new CoinRepoFixture()
               .GivenConfiguration();
            var coinRepo = fixture.BuildCoinRepository();
            coinRepo.AddCoinAsync(coins);

            var expected = fixture.GetCoinsTotal();
            Assert.Equal(coins.Amount, expected);

            //clear cache
            fixture.Reset();
        }

        [Fact]
        public void GetTotalCoin_ShouldReturn_Void_True()
        {
            //Arrange
            var coins = new Models.Coin() { Amount = 200, Volume = 42 };
            var fixture = new CoinRepoFixture()
               .GivenConfiguration();
            var coinRepo = fixture.BuildCoinRepository();
            coinRepo.AddCoinAsync(coins);
            coinRepo.AddCoinAsync(coins);

            var expected = fixture.GetCoinsTotal();
            Assert.Equal(400, expected);

            //clear cache
            fixture.Reset();
        }

        [Fact]
        public void Reset_ShouldReturn_Void_True()
        {
            //Arrange
            var coins = new Models.Coin() { Amount = 200, Volume = 42 };
            var fixture = new CoinRepoFixture()
               .GivenConfiguration();
            var coinRepo = fixture.BuildCoinRepository();
            coinRepo.AddCoinAsync(coins);
            coinRepo.Reset();

            var expected = fixture.GetCoinsTotal(); 
            var exception = Assert.Throws<Exception>(() => coinRepo.GetTotalAmount());
            Assert.Equal("Exception on 'GetTotalAmount'. Error Message: 'Exception on GetTotalAmount'. Error Message: No totalCoins found''", exception.Message);
            //clear cache
            fixture.Reset();
        }
    }
}