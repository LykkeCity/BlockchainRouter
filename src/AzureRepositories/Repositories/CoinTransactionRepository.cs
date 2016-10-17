using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureRepositories.Azure;
using Core.Repositories;
using Microsoft.WindowsAzure.Storage.Table;

namespace AzureRepositories.Repositories
{

	public class CoinTransationEntity : TableEntity, ICoinTransaction
	{
		public const string Key = "CoinTransactionEntity";

		public Guid RequestId => new Guid(RowKey);
		public string TransactionHash { get; set; }
		public int ConfirmaionLevel { get; set; }
		public bool Error { get; set; }

		public string ClientA { get; set; }
		public string ClientB { get; set; }

		public string QueueName { get; set; }
		public bool HasChildClientA { get; set; }
		public bool HasChildClientB { get; set; }
		public DateTime CreateDt { get; set; }
		public string RequestData { get; set; }

		public static CoinTransationEntity Create(ICoinTransaction coinTransaction)
		{
			var entity = new CoinTransationEntity
			{
				ClientA = coinTransaction.ClientA,
				ClientB = coinTransaction.ClientB,
				TransactionHash = coinTransaction.TransactionHash,
				ConfirmaionLevel = coinTransaction.ConfirmaionLevel,
				CreateDt = coinTransaction.CreateDt,
				Error = coinTransaction.Error,
				HasChildClientA = coinTransaction.HasChildClientA,
				HasChildClientB = coinTransaction.HasChildClientB,
				QueueName = coinTransaction.QueueName,
				PartitionKey = Key,
				RowKey = coinTransaction.RequestId.ToString(),
				RequestData = coinTransaction.RequestData
			};
			return entity;
		}

	}


	public class CoinTransactionRepository : ICoinTransactionRepository
	{
		private readonly INoSQLTableStorage<CoinTransationEntity> _table;

		public CoinTransactionRepository(INoSQLTableStorage<CoinTransationEntity> table)
		{
			_table = table;
		}

		public async Task<ICoinTransaction> GetCoinTransaction(Guid requestId)
		{
			return await _table.GetDataAsync(CoinTransationEntity.Key, requestId.ToString());
		}

		public Task AddCoinTransaction(ICoinTransaction transaction)
		{
			var entity = CoinTransationEntity.Create(transaction);
			return _table.InsertAsync(entity);
		}

		public async Task<IEnumerable<ICoinTransaction>> GetCoinTransactions(List<string> clients, int minConfirmationLevel)
		{
			var clientA = clients[0];
			var clientB = clients.Count > 1 ? clients[1] : null;
			return (await _table.GetDataAsync(o => ((o.ClientA == clientA || o.ClientA == clientB) && !o.HasChildClientA) ||
									 ((o.ClientB == clientA || clientB != null && o.ClientB == clientB) && !o.HasChildClientB)
									 && o.ConfirmaionLevel < minConfirmationLevel)).ToList();
		}

		public async Task SetChildFlags(IEnumerable<ICoinTransaction> transactions, List<string> clients)
		{
			foreach (var coinTransaction in transactions)
			{
				coinTransaction.HasChildClientA = clients.Contains(coinTransaction.ClientA);
				coinTransaction.HasChildClientB = clients.Contains(coinTransaction.ClientB);
				await _table.ReplaceAsync(CoinTransationEntity.Key, coinTransaction.RequestId.ToString(), entity =>
			   {
				   entity.HasChildClientA = coinTransaction.HasChildClientA;
				   entity.HasChildClientB = coinTransaction.HasChildClientB;
				   return entity;
			   });
			}

		}

		public async Task SetTransactionHash(ICoinTransaction transaction)
		{
			await _table.ReplaceAsync(CoinTransationEntity.Key, transaction.RequestId.ToString(), entity =>
			{
				entity.TransactionHash = transaction.TransactionHash;
				return entity;
			});
		}

		public async Task SetTransactionConfirmationLevel(ICoinTransaction transaction)
		{
			await _table.ReplaceAsync(CoinTransationEntity.Key, transaction.RequestId.ToString(), entity =>
			{
				entity.ConfirmaionLevel = transaction.ConfirmaionLevel;
				entity.Error = transaction.Error;
				return entity;
			});
		}

		public void DeleteTable()
		{
			_table.DeleteIfExists();
		}
	}
}
