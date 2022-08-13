
namespace Coins.Repository.Interfaces;
public interface ICoinRepository
{
    /// <summary>
    /// Add Coins
    /// </summary> 
    /// <returns>Returns false if it fails or true if valid</returns>
    Task<bool> AddCoinAsync(Coin coin);
}