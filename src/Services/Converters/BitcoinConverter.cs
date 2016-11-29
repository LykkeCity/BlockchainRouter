using System;
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
				case ActionType.CashOut:
					command = new CashOut();
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
			command.InitFromParameters(request.Parameters);
            return new { Action = request.Action.ToString(), JsonData = command.ToJson() }.ToJson();
        }

		public string CreateSignedRequestMessage(Guid requestId, string multisigAddress, string sign)
		{
			return new { requestId = requestId, multisigAddress = multisigAddress, sign = sign }.ToJson();
		}
	}
}
