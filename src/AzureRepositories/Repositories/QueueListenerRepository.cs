using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureRepositories.Azure;
using Core.Repositories;
using Microsoft.WindowsAzure.Storage.Table;

namespace AzureRepositories.Repositories
{

	public class DbQueueListenerEntity : TableEntity, IDbQueueListener
	{
		public const string Key = "QueueListener";

		public string Name => RowKey;
		public string Client { get; set; }

		public static DbQueueListenerEntity Create(IDbQueueListener listener)
		{
			return new DbQueueListenerEntity
			{
				RowKey = listener.Name,
				Client = listener.Client,
				PartitionKey = Key
			};
		}
	}


	public class QueueListenerRepository : IQueueListenerRepository
	{
		private readonly INoSQLTableStorage<DbQueueListenerEntity> _table;

		public QueueListenerRepository(INoSQLTableStorage<DbQueueListenerEntity> table)
		{
			_table = table;
		}

		public async Task<IDbQueueListener> GetQueueListener(List<string> clients)
		{
			return (await _table.GetDataAsync(o => clients.Contains(o.Client))).OrderBy(o => o.Name).FirstOrDefault();
		}

		public Task Insert(IDbQueueListener dbQueueListener)
		{
			return _table.InsertAsync(DbQueueListenerEntity.Create(dbQueueListener));
		}

		public async Task<IEnumerable<IDbQueueListener>> GetListeners()
		{
			return await _table.GetDataAsync();
		}

		public void RemoveListener(string runningListenerName)
		{
			_table.DeleteAsync(DbQueueListenerEntity.Key, runningListenerName).Wait();
		}

		public void DeleteTable()
		{
			_table.DeleteIfExists();
		}
	}
}
