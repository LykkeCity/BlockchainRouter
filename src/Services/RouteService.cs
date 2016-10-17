using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureRepositories.Azure.Queue;
using Core;
using Core.Log;
using Core.Utils;
using Services.CommandSenders;
using Services.Converters;
using Services.Models;

namespace Services
{
	public interface IRouteService
	{
		Task<bool> ProcessNextRequest();
	}


	public class RouteService : IRouteService
	{
		private readonly Func<string, IRequestConverter> _convertersFactory;
		private readonly Func<string, ICommandSender> _commandSenderFactory;
		private readonly ILog _logger;
		private readonly IQueueExt _incomeQueue;


		public RouteService(Func<string, IQueueExt> queueFactory, Func<string, IRequestConverter> convertersFactory,
			Func<string, ICommandSender> commandSenderFactory, ILog logger)
		{
			_convertersFactory = convertersFactory;
			_commandSenderFactory = commandSenderFactory;
			_logger = logger;
			_incomeQueue = queueFactory(Constants.RouterIncomeQueue);
		}


		public async Task<bool> ProcessNextRequest()
		{
			var msg = await _incomeQueue.PeekRawMessageAsync();
			if (msg == null)
				return false;

			await _logger.WriteInfo("RouteService", "ProcessNextRequest", "", "New request -" + msg.AsString);
			var request = msg.AsString.DeserializeJson<Request>();


			var converter = _convertersFactory(request.Blockchain);
			if (converter == null)
				throw new Exception("Unregistered converter for blockchain - " + request.Blockchain);
			var message = converter.CreateMessage(request);

			await _logger.WriteInfo("RouteService", "ProcessNextRequest", "", "Converted message -" + message);

			var sender = _commandSenderFactory(request.Blockchain);
			if (sender == null)
				throw new Exception("Unregistered command sender for blockchain - " + request.Blockchain);
			await sender.SendCommandAsync(message);
		

			await _logger.WriteInfo("RouteService", "ProcessNextRequest", "", "Message sent to blockchain successfuly");

			return true;
		}
	}
}
