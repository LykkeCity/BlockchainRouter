using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureRepositories.Azure.Queue;
using Core;

namespace Services.CommandSenders
{
	public class BitcoinCommandSender : ICommandSender
	{
		private readonly IQueueExt _queue;

		public BitcoinCommandSender(Func<string, IQueueExt> queueFactory)
		{
			_queue = queueFactory(Constants.BitcoinQueue);
		}

		public Task SendCommandAsync(string message)
		{
			return _queue.PutRawMessageAsync(message);
		}
	}
}
