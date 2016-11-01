using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface ICoin
    {
        string Blockchain { get; }
        string Id { get; }
        string AssetAddress { get; }
        int Multiplier { get; }
    }

    public class Coin : ICoin
    {
        public string Blockchain { get; set; }
        public string Id { get; set; }
        public string AssetAddress { get; set; }
        public int Multiplier { get; set; }
    }

    public interface ICoinRepository
    {
        Task<ICoin> GetCoin(string name);
        Task InsertOrReplace(ICoin coin);
        Task<ICoin> GetCoinByAddress(string coinAddress);
    }
}
