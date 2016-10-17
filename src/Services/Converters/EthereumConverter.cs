using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Utils;
using Services.Models;

namespace Services.Converters
{
	public class EthereumConverter : IRequestConverter
	{
		public string CreateMessage(Request request)
		{
			BaseIncomingRequest req = null;
			switch (request.Action)
			{
				case ActionType.CashIn:
					req = new IncomingCashInRequest();
					break;
				case ActionType.CashOut:
					req = new IncomingCashOutRequest();
					break;
				case ActionType.Swap:
					req = new IncomingSwapRequest();
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
			req.InitFromParameters(request.Parameters);
			return new { Action = request.Action, JsonData = req.ToJson() }.ToJson();
		}
	}
}
