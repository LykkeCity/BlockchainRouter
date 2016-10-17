using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureRepositories.Azure.Queue;
using Core;

namespace Services.CommandSenders
{
	public class EthereumCommandSender : ICommandSender
	{
		private readonly IQueueExt _queue;

		public EthereumCommandSender(Func<string, IQueueExt> queueFactory)
		{
			_queue = queueFactory(Constants.EthereumQueue);
		}

		public Task SendCommandAsync(string message)
		{
			return _queue.PutRawMessageAsync(message);
		}
	}
}
