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
	    public string CreateMessage(Request request)
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
					command= new Transfer();
				    break;
				case ActionType.TransferAllAssetsToAddress:
					command = new TransferAll();
				    break;
			    default:
				    throw new ArgumentOutOfRangeException();
		    }
		    command.InitFromParameters(request.Parameters);
		    return $"{request.Action.ToString()}:{command.ToJson()}";
	    }
    }
}
