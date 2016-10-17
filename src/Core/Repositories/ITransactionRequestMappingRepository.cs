using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Repositories
{
	public interface ITransactionRequestMapping
	{
		string TransactionHash { get; }
		Guid RequestId { get; }
	}

	public class TransactionRequestMapping : ITransactionRequestMapping
	{
		public string TransactionHash { get; set; }
		public Guid RequestId { get; set; }
	}

    public interface ITransactionRequestMappingRepository
    {
	    Task InsertTransactionRequestMapping(ITransactionRequestMapping mapping);
	    Task<ITransactionRequestMapping> GetTransactionRequestMapping(string transactionHash);
	    void DeleteTable();
    }
}
