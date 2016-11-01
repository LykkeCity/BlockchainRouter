using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureRepositories.Azure;
using Core;
using Core.Repositories;
using Microsoft.WindowsAzure.Storage.Table;

namespace AzureRepositories.Repositories
{
    public class CoinEntity : TableEntity, ICoin
    {
        public const string Key = "Asset";

        public string Id => RowKey;
        public string Blockchain { get; set; }
        public string AssetAddress { get; set; }
        public int Multiplier { get; set; }

        public static CoinEntity CreateCoinEntity(ICoin coin)
        {
            return new CoinEntity
            {
                AssetAddress = coin.AssetAddress,
                RowKey = coin.Id,
                Multiplier = coin.Multiplier,
                Blockchain = coin.Blockchain,
                PartitionKey = Key
            };
        }
    }


    public class CoinRepository : ICoinRepository
    {
        private readonly INoSQLTableStorage<CoinEntity> _table;

        public CoinRepository(INoSQLTableStorage<CoinEntity> table)
        {
            _table = table;
        }

        public async Task<ICoin> GetCoin(string coinName)
        {
            var coin = await _table.GetDataAsync(CoinEntity.Key, coinName);
            if (coin == null)
                throw new Exception("Unknown coin name - " + coinName);
            return coin;
        }

        public async Task InsertOrReplace(ICoin coin)
        {
            await _table.InsertOrReplaceAsync(CoinEntity.CreateCoinEntity(coin));
        }

        public async Task<ICoin> GetCoinByAddress(string coinAddress)
        {
            var coin = (await _table.GetDataAsync(CoinEntity.Key, x => x.AssetAddress == coinAddress)).FirstOrDefault();
            if (coin == null)
                throw new Exception("Unknown coin address - " + coinAddress);
            return coin;
        }
    }
}
