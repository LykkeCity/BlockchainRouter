using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Utils;

namespace Services.Models
{
	public class BaseIncomingRequest
	{
		public Guid TransactionId { get; set; }

		public virtual void InitFromParameters(Dictionary<string, string> requestParameters)
		{
			TransactionId = requestParameters.Get(() => TransactionId);
		}
	}


	public class IncomingCashInRequest : BaseIncomingRequest
	{

		public string Coin { get; set; }
		public string To { get; set; }

		public decimal Amount { get; set; }

		public override void InitFromParameters(Dictionary<string, string> requestParameters)
		{
			base.InitFromParameters(requestParameters);
			Coin = requestParameters.Get(() => Coin);
			To = requestParameters.Get(() => To);
			Amount = requestParameters.Get(() => Amount);
		}
	}

	public class IncomingCashOutRequest : BaseIncomingRequest
	{

		public string Coin { get; set; }

		public string Client { get; set; }

		public string To { get; set; }

		public decimal Amount { get; set; }

		public string Sign { get; set; }

		public override void InitFromParameters(Dictionary<string, string> requestParameters)
		{
			base.InitFromParameters(requestParameters);
			Coin = requestParameters.Get(() => Coin);
			Client = requestParameters.Get(() => Client);
			To = requestParameters.Get(() => To);
			Amount = requestParameters.Get(() => Amount);
			Sign = requestParameters.Get(() => Sign);
		}
	}

	public class IncomingSwapRequest : BaseIncomingRequest
	{		
		public string ClientA { get; set; }

		public string ClientB { get; set; }

		public string CoinA { get; set; }

		public string CoinB { get; set; }

		public decimal AmountA { get; set; }
		public decimal AmountB { get; set; }

		public string SignA { get; set; }
		public string SignB { get; set; }

		public override void InitFromParameters(Dictionary<string, string> requestParameters)
		{
			base.InitFromParameters(requestParameters);
			ClientA = requestParameters.Get(() => ClientA);
			ClientB = requestParameters.Get(() => ClientB);
			CoinA = requestParameters.Get(() => CoinA);
			CoinB = requestParameters.Get(() => CoinB);
			AmountA = requestParameters.Get(() => AmountA);
			AmountB = requestParameters.Get(() => AmountB);
			SignA = requestParameters.Get(() => SignA);
			SignB = requestParameters.Get(() => SignB);
		}
	}

}
