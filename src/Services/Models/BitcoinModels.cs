using Core.Utils;
using System.Collections.Generic;

namespace Services.Models
{
	public class BaseCommandModel
	{
		public string TransactionId { get; set; }

		public virtual void InitFromParameters(Dictionary<string, string> requestParameters)
		{
			TransactionId = requestParameters.Get<string>(CommandsKeys.TransactionId);
		}
	}

	public class CashIn : BaseCommandModel
	{
		public string MultisigAddress { get; set; }
		public float Amount { get; set; }
		public string Currency { get; set; }

		public override void InitFromParameters(Dictionary<string, string> requestParameters)
		{
			base.InitFromParameters(requestParameters);
			MultisigAddress = requestParameters.Get<string>(CommandsKeys.MultisigAddress);
			Amount = requestParameters.Get<float>(CommandsKeys.Amount);
			Currency = requestParameters.Get<string>(CommandsKeys.Asset);
		}
	}

	public class CashOut : BaseCommandModel
	{
		public string AddressFrom { get; set; }
        public string AddressTo { get; set; }
        public float Amount { get; set; }
		public string Currency { get; set; }

		public override void InitFromParameters(Dictionary<string, string> requestParameters)
		{
			base.InitFromParameters(requestParameters);
            AddressFrom = requestParameters.Get<string>(CommandsKeys.MultisigAddress);
		    AddressTo = requestParameters.Get<string>(CommandsKeys.To);
			Amount = requestParameters.Get<float>(CommandsKeys.Amount);
			Currency = requestParameters.Get<string>(CommandsKeys.Asset);
		}
	}

	public class Transfer : BaseCommandModel
	{
		public string SourceAddress { get; set; }
		public string SourcePrivateKey { get; set; }
		public string DestinationAddress { get; set; }
		public float Amount { get; set; }
		public string Asset { get; set; }
		public override void InitFromParameters(Dictionary<string, string> requestParameters)
		{
			base.InitFromParameters(requestParameters);
			SourceAddress = requestParameters.Get<string>(CommandsKeys.MultisigAddress);
			//SourcePrivateKey = requestParameters.Get(() => SourcePrivateKey);
			DestinationAddress = requestParameters.Get<string>(CommandsKeys.To);
			Amount = requestParameters.Get<float>(CommandsKeys.Amount);
			Asset = requestParameters.Get<string>(CommandsKeys.Asset);
		}
	}

	public class Swap : BaseCommandModel
	{
		public string MultisigCustomer1 { get; set; }
		public float Amount1 { get; set; }
		public string Asset1 { get; set; }
		public string MultisigCustomer2 { get; set; }
		public float Amount2 { get; set; }
		public string Asset2 { get; set; }

		public override void InitFromParameters(Dictionary<string, string> requestParameters)
		{
			base.InitFromParameters(requestParameters);

			MultisigCustomer1 = requestParameters.Get<string>(CommandsKeys.MultisigAddress1);
			Amount1 = requestParameters.Get<float>(CommandsKeys.Amount1);
			Asset1 = requestParameters.Get<string>(CommandsKeys.Asset1);
			MultisigCustomer2 = requestParameters.Get<string>(CommandsKeys.MultisigAddress2);
			Amount2 = requestParameters.Get<float>(CommandsKeys.Amount2);
			Asset2 = requestParameters.Get<string>(CommandsKeys.Asset2);
		}
	}	
}
