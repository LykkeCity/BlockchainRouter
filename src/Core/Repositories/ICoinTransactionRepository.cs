using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Core.Repositories
{
	public interface ICoinTransaction
	{
		Guid RequestId { get; }
		string QueueName { get; set; }

		string TransactionHash { get; set; }
		int ConfirmaionLevel { get; set; }
		bool Error { get; set; }

		string ClientA { get; set; }
		string ClientB { get; set; }

		bool HasChildClientA { get; set; }
		bool HasChildClientB { get; set; }

		DateTime CreateDt { get; set; }

		string RequestData { get; set; }
	}

	public class CoinTransaction : ICoinTransaction
	{
		public Guid RequestId { get; set; }
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
	}

	public interface ICoinTransactionRepository
	{
		Task<ICoinTransaction> GetCoinTransaction(Guid requestId);

		Task AddCoinTransaction(ICoinTransaction transaction);

		Task<IEnumerable<ICoinTransaction>> GetCoinTransactions(List<string> clients, int minConfirmationLevel);
		Task SetChildFlags(IEnumerable<ICoinTransaction> transactions, List<string> clients);
		Task SetTransactionHash(ICoinTransaction transaction);

		Task SetTransactionConfirmationLevel(ICoinTransaction transaction);

		void DeleteTable();
	}
}
