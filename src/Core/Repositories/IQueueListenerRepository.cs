using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Repositories
{
	public interface IDbQueueListener
	{
		/// <summary>
		/// Queue's name that listener is listening
		/// </summary>
		string Name { get; }

		string Client { get; }		
	}

	public class DbQueueListener : IDbQueueListener
	{
		public string Name { get; set; }
		public string Client { get; set; }		
	}

	public interface IQueueListenerRepository
	{
		Task<IDbQueueListener> GetQueueListener(List<string> clients);

		Task Insert(IDbQueueListener dbQueueListener);
		Task<IEnumerable<IDbQueueListener>>  GetListeners();
		void RemoveListener(string runningListenerName);
		void DeleteTable();
	}
}
