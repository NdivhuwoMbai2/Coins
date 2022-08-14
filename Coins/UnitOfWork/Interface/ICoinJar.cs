
namespace Coins.UnitOfWork;
public interface ICoinJar
{  
    /// <summary>
    /// Add Coins
    /// </summary> 
    /// <returns>Returns false if it fails or true if valid</returns>
    Task<bool> AddCoinAsync(Coin coin);
    /// <summary>
    /// Get the total amount of our coins
    /// </summary> 
    /// <returns>Returns sum of coins</returns>
    Task<decimal> GetTotalAmount();
    /// <summary>
    /// Reset the coins.
    /// </summary>  
    void Reset();
}