using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Utils;
using Services.Models;

namespace Services.Converters
{
	public class BitcoinConverter : IRequestConverter
	{
		public string CreateCommandMessage(Request request)
		{
			BaseCommandModel command;
			switch (request.Action)
			{
				case ActionType.CashIn:
					command = new CashIn();
					break;
				case ActionType.CashOut:
					command = new CashOut();
					break;
				case ActionType.Swap:
					command = new Swap();
					break;
				case ActionType.OrdinaryCashOut:
					command = new OrdinaryCashOut();
					break;
				case ActionType.Transfer:
					command = new Transfer();
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
			command.InitFromParameters(request.Parameters);
			return $"{request.Action.ToString()}:{command.ToJson()}";
		}

		public string CreateSignedRequestMessage(Guid requestId, string multisigAddress, string sign)
		{
			return new { requestId = requestId, multisigAddress = multisigAddress, sign = sign }.ToJson();
		}
	}
}
