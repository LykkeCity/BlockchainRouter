using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Utils;

namespace Services.Models
{
	public class BaseEthRequest
	{
		public string TransactionId { get; set; }

		public virtual void InitFromParameters(Dictionary<string, string> requestParameters)
		{
			TransactionId = requestParameters.Get<string>(CommandsKeys.TransactionId);
		}
	}


	public class EthCashInRequest : BaseEthRequest
	{

		public string Coin { get; set; }
		public string To { get; set; }

		public decimal Amount { get; set; }

		public override void InitFromParameters(Dictionary<string, string> requestParameters)
		{
			base.InitFromParameters(requestParameters);
			Coin = requestParameters.Get<string>(CommandsKeys.Asset);
			To = requestParameters.Get<string>(CommandsKeys.MultisigAddress);
			Amount = requestParameters.Get<decimal>(CommandsKeys.Amount);
		}
	}

	public class EthCashOutRequest : BaseEthRequest
	{

		public string Coin { get; set; }

		public string Client { get; set; }

		public string To { get; set; }
		
		public decimal Amount { get; set; }

		public string Sign { get; set; }

		public override void InitFromParameters(Dictionary<string, string> requestParameters)
		{
			base.InitFromParameters(requestParameters);
			Coin = requestParameters.Get<string>(CommandsKeys.Asset);
			Client = requestParameters.Get<string>(CommandsKeys.MultisigAddress);
			To = requestParameters.Get<string>(CommandsKeys.To);
			Amount = requestParameters.Get<decimal>(CommandsKeys.Amount);
			//Sign = requestParameters.Get(() => Sign);
		}
	}

	public class EthSwapRequest : BaseEthRequest
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
			ClientA = requestParameters.Get<string>(CommandsKeys.MultisigAddress1);
			ClientB = requestParameters.Get<string>(CommandsKeys.MultisigAddress2);
			CoinA = requestParameters.Get<string>(CommandsKeys.Asset1);
			CoinB = requestParameters.Get<string>(CommandsKeys.Asset2);
			AmountA = requestParameters.Get<decimal>(CommandsKeys.Amount1);
			AmountB = requestParameters.Get<decimal>(CommandsKeys.Amount2);
			//SignA = requestParameters.Get(() => SignA);
			//SignB = requestParameters.Get(() => SignB);
		}
	}

	public class EthTransferRequest : BaseEthRequest
	{

		public string Coin { get; set; }

		public string From { get; set; }

		public string To { get; set; }

		public decimal Amount { get; set; }

		public string Sign { get; set; }

		public override void InitFromParameters(Dictionary<string, string> requestParameters)
		{
			base.InitFromParameters(requestParameters);
			Coin = requestParameters.Get<string>(CommandsKeys.Asset);
			From = requestParameters.Get<string>(CommandsKeys.MultisigAddress);
			To = requestParameters.Get<string>(CommandsKeys.To);
			Amount = requestParameters.Get<decimal>(CommandsKeys.Amount);
		}
	}

}
