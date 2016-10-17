using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Models
{
    public class Request
    {
		public string Blockchain { get; set; }

		public ActionType Action { get; set; }

		public Dictionary<string, string> Parameters { get; set; }
    }

	public enum ActionType
	{
		CashIn = 0,
		CashOut = 1,
		Swap = 2,
		OrdinaryCashOut = 3,
		Transfer = 4,
		TransferAllAssetsToAddress = 5
	}



}
