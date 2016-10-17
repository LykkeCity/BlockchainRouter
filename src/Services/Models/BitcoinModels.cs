using Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Models
{
	public class BaseCommandModel
	{
		public string TransactionId { get; set; }

		public virtual void InitFromParameters(Dictionary<string, string> requestParameters)
		{
			TransactionId = requestParameters.Get(() => TransactionId);
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
			MultisigAddress = requestParameters.Get(() => MultisigAddress);
			Amount = requestParameters.Get(() => Amount);
			Currency = requestParameters.Get(() => Currency);
		}
	}

	public class CashOut : BaseCommandModel
	{
		public string MultisigAddress { get; set; }
		public float Amount { get; set; }
		public string Currency { get; set; }
		public string PrivateKey { get; set; }

		public override void InitFromParameters(Dictionary<string, string> requestParameters)
		{
			base.InitFromParameters(requestParameters);
			MultisigAddress = requestParameters.Get(() => MultisigAddress);
			Amount = requestParameters.Get(() => Amount);
			Currency = requestParameters.Get(() => Currency);
			PrivateKey = requestParameters.Get(() => PrivateKey);
		}
	}

	public class OrdinaryCashOut : BaseCommandModel
	{
		public string MultisigAddress { get; set; }
		public float Amount { get; set; }
		public string Currency { get; set; }
		public string PrivateKey { get; set; }
		public string PublicWallet { get; set; }

		public override void InitFromParameters(Dictionary<string, string> requestParameters)
		{
			base.InitFromParameters(requestParameters);

			MultisigAddress = requestParameters.Get(() => MultisigAddress);
			Amount = requestParameters.Get(() => Amount);
			Currency = requestParameters.Get(() => Currency);
			PrivateKey = requestParameters.Get(() => PrivateKey);
			PublicWallet = requestParameters.Get(() => PublicWallet);
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
			SourceAddress = requestParameters.Get(() => SourceAddress);
			SourcePrivateKey = requestParameters.Get(() => SourcePrivateKey);
			DestinationAddress = requestParameters.Get(() => DestinationAddress);
			Amount = requestParameters.Get(() => Amount);
			Asset = requestParameters.Get(() => Asset);
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

			MultisigCustomer1 = requestParameters.Get(() => MultisigCustomer1);
			Amount1 = requestParameters.Get(() => Amount1);
			Asset1 = requestParameters.Get(() => Asset1);
			MultisigCustomer2 = requestParameters.Get(() => MultisigCustomer2);
			Amount2 = requestParameters.Get(() => Amount2);
			Asset2 = requestParameters.Get(() => Asset2);
		}
	}

	public class TransferAll : BaseCommandModel
	{
		public string SourceAddress { get; set; }
		public string SourcePrivateKey { get; set; }
		public string DestinationAddress { get; set; }

		public override void InitFromParameters(Dictionary<string, string> requestParameters)
		{
			base.InitFromParameters(requestParameters);
			SourceAddress = requestParameters.Get(() => SourceAddress);
			SourcePrivateKey = requestParameters.Get(() => SourcePrivateKey);
			DestinationAddress = requestParameters.Get(() => DestinationAddress);
		}
	}
}
