using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureRepositories.Azure.Queue;
using Core;
using Core.Exceptions;
using Core.Log;
using Core.Repositories;
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
	    private readonly ICoinRepository _coinRepository;
	    private readonly IQueueExt _incomeQueue;


		public RouteService(Func<string, IQueueExt> queueFactory, Func<string, IRequestConverter> convertersFactory,
			Func<string, ICommandSender> commandSenderFactory, ILog logger, ICoinRepository coinRepository)
		{
			_convertersFactory = convertersFactory;
			_commandSenderFactory = commandSenderFactory;
			_logger = logger;
		    _coinRepository = coinRepository;
		    _incomeQueue = queueFactory(Constants.RouterIncomeQueue);
		}


		public async Task<bool> ProcessNextRequest()
		{
			var msg = await _incomeQueue.PeekRawMessageAsync();
			if (msg == null)
				return false;

			await _logger.WriteInfo("RouteService", "ProcessNextRequest", "", "New request -" + msg.AsString);
			var request = msg.AsString.DeserializeJson<Request>();

		    var blockchain = await DetermineBlockChain(request);
		    if (blockchain == null)
		    {
		        await FinishMessage();
                return true;
		    }

		    var converter = _convertersFactory(blockchain);
			if (converter == null)
				throw new Exception("Unregistered converter for blockchain - " + blockchain);
			var message = converter.CreateMessage(request);

			await _logger.WriteInfo("RouteService", "ProcessNextRequest", "", "Converted message -" + message);

			var sender = _commandSenderFactory(blockchain);
			if (sender == null)
				throw new Exception("Unregistered command sender for blockchain - " + blockchain);
			await sender.SendCommandAsync(message);
		

			await _logger.WriteInfo("RouteService", "ProcessNextRequest", "", "Message sent to blockchain successfuly");

		    await FinishMessage();

            return true;
		}

	    private async Task FinishMessage()
	    {
            var msg = await _incomeQueue.GetRawMessageAsync();
            await _incomeQueue.FinishRawMessageAsync(msg);
        }

	    private async Task<string> DetermineBlockChain(Request request)
	    {
	        try
	        {
	            var asset1 = request.Parameters.GetDefault<string>("Asset1");
	            var asset2 = request.Parameters.GetDefault<string>("Asset2");

	            ICoin asset1FromDb = await _coinRepository.GetCoin(asset1);
	            ICoin asset2FromDb = null;
	            if (!string.IsNullOrWhiteSpace(asset2))
	                asset2FromDb = await _coinRepository.GetCoin(asset2);

	            var blockchain1 = asset1FromDb.Blockchain.ToLower();
	            var blockchain2 = asset2FromDb?.Blockchain?.ToLower();

	            if (blockchain1 == Constants.EthereumBlockchain)
	            {
	                if (!string.IsNullOrWhiteSpace(blockchain2) && blockchain2 != Constants.EthereumBlockchain)
	                    ThrowBadBlockchain(request.Action, asset1, asset2);

	                return Constants.EthereumBlockchain;
	            }
	            if (blockchain1 == Constants.BitcoinBlockchain)
	            {
	                if (!string.IsNullOrWhiteSpace(blockchain2) && blockchain2 != Constants.BitcoinBlockchain)
	                    ThrowBadBlockchain(request.Action, asset1, asset2);

	                return Constants.BitcoinBlockchain;
	            }

	            ThrowBadBlockchain(request.Action, asset1, asset2);
	        }
	        catch (BackendException e)
	        {
	            await _logger.WriteError("RouteService", "DetermineBlockChain", "", e);
                //TODO: send email if blockchain is not supported
	        }

	        return null;
	    }

	    private void ThrowBadBlockchain(ActionType type, string asset1, string asset2)
	    {
	        throw new BackendException(BackendExceptionType.InvalidBlockchain, $"Invalid blockchain, request: {type}, asset1: {asset1}, asset2: {asset2}");
	    }
    }
}
