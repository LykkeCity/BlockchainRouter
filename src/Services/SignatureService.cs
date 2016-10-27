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

namespace Services
{

	public interface ISignatureService
	{
		Task<bool> ProcessNextSignedRequest();
	}

	public class SignatureService : ISignatureService
	{
		private readonly Func<string, IRequestConverter> _convertersFactory;
		private readonly Func<string, ICommandSender> _commandSenderFactory;
		private readonly ILog _logger;
		private IQueueExt _queue;


		public SignatureService(Func<string, IQueueExt> queueFactory,
			Func<string, IRequestConverter> convertersFactory,
			Func<string, ICommandSender> commandSenderFactory, ILog logger)
		{
			_convertersFactory = convertersFactory;
			_commandSenderFactory = commandSenderFactory;
			_logger = logger;
			_queue = queueFactory(Constants.RouterSignedRequestQueue);
		}

		public async Task<bool> ProcessNextSignedRequest()
		{
			var msg = await _queue.PeekRawMessageAsync();
			if (msg == null)
				return false;
			await _logger.WriteInfo("SignatureService", "ProcessNextSignedRequest", "", "New signed request -" + msg.AsString);
			var signedData = msg.AsString.DeserializeJson<SignedData>();
			var converter = _convertersFactory(signedData.Blockchain);
			if (converter == null)
				throw new Exception("Unregistered converter for blockchain - " + signedData.Blockchain);
			var message = converter.CreateSignedRequestMessage(signedData.RequestId, signedData.MultisigAddress, signedData.Signature);

			var sender = _commandSenderFactory(signedData.Blockchain);
			if (sender == null)
				throw new Exception("Unregistered command sender for blockchain - " + signedData.Blockchain);
			await sender.SendSignedRequestAsync(message);

			await _logger.WriteInfo("SignatureService", "ProcessNextSignedRequest", "", "Message sent to blockchain successfuly");

			msg = await _queue.GetRawMessageAsync();
			await _queue.FinishRawMessageAsync(msg);

			return true;
		}


		// ReSharper disable once ClassNeverInstantiated.Local
		private class SignedData
		{
			public Guid RequestId { get; set; }
			public string MultisigAddress { get; set; }
			public string Signature { get; set; }
			public string Blockchain { get; set; }
		}
	}
}
