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
			BaseEthRequest req = null;
			switch (request.Action)
			{
				case ActionType.CashIn:
					req = new EthCashInRequest();
					break;
				case ActionType.CashOut:
					req = new EthCashOutRequest();
					break;
				case ActionType.Swap:
					req = new EthSwapRequest();
					break;
				case ActionType.Transfer:
					req = new EthTransferRequest();
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
			req.InitFromParameters(request.Parameters);
			return new { Action = request.Action.ToString(), JsonData = req.ToJson() }.ToJson();
		}
	}
}
