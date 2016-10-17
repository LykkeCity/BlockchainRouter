using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureRepositories.Azure;
using Core.Repositories;
using Microsoft.WindowsAzure.Storage.Table;

namespace AzureRepositories.Repositories
{
	public class TransactionRequestMappingEntity : TableEntity, ITransactionRequestMapping
	{

		public const string Key = "TransactionRequestMapping";

		public string TransactionHash => RowKey;
		public Guid RequestId { get; set; }

		public static TransactionRequestMappingEntity Create(ITransactionRequestMapping mapping)
		{
			return new TransactionRequestMappingEntity
			{
				RowKey = mapping.TransactionHash,
				RequestId = mapping.RequestId,
				PartitionKey = Key
			};
		}
	}



	public class TransactionRequestMappingRepository : ITransactionRequestMappingRepository
	{
		private readonly INoSQLTableStorage<TransactionRequestMappingEntity> _table;

		public TransactionRequestMappingRepository(INoSQLTableStorage<TransactionRequestMappingEntity>  table)
		{
			_table = table;
		}
		public Task InsertTransactionRequestMapping(ITransactionRequestMapping mapping)
		{
			return _table.InsertAsync(TransactionRequestMappingEntity.Create(mapping));
		}

		public async Task<ITransactionRequestMapping> GetTransactionRequestMapping(string transactionHash)
		{
			return await _table.GetDataAsync(TransactionRequestMappingEntity.Key, transactionHash);
		}

		public void DeleteTable()
		{
			_table.DeleteIfExists();
		}
	}
}
