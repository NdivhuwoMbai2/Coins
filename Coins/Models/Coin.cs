namespace Coins.Models
{
    public class Coin : ICoin
    {
        private decimal _amount;
        public decimal Amount
        {
            get => _amount;
            set => _amount = value;
        }
        private decimal _volume;
        public decimal Volume
        {
            get { return 42; }
            set { }
        }
    }
}