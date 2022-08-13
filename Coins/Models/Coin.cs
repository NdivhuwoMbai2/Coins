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
        get => _volume;
        set => _volume = value; 
    }
}