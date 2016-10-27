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
		private readonly IQueueExt _commandQueue;
		private IQueueExt _signedRequestQueue;

		public EthereumCommandSender(Func<string, IQueueExt> queueFactory)
		{
			_commandQueue = queueFactory(Constants.EthereumQueue);
			_signedRequestQueue = queueFactory(Constants.EthereumSignedRequestQueue);
		}

		public Task SendCommandAsync(string message)
		{
			return _commandQueue.PutRawMessageAsync(message);
		}

		public Task SendSignedRequestAsync(string message)
		{
			return _signedRequestQueue.PutRawMessageAsync(message);
		}
	}
}
