
using Coins.Models;

namespace Coins.Repository.Interfaces
{
    public interface ICoinRepository
    {
        /// <summary>
        /// Add Coins
        /// </summary> 
        /// <returns>Returns false if it fails or true if valid</returns>
        void AddCoinAsync(Coin coin);
        /// <summary>
        /// Get the total amount of our coins
        /// </summary> 
        /// <returns>Returns sum of coins</returns>
        decimal GetTotalAmount();
        /// <summary>
        /// Reset the coins.
        /// </summary>  
        void Reset();
    }
}