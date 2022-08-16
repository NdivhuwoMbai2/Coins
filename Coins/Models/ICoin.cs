namespace Coins.Models
{
    public interface ICoin
    {
        decimal Amount { get; set; }
        decimal Volume { get; set; }
    }
}