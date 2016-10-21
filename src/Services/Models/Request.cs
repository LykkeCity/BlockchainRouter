using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Models
{
    public class Request
    {
		public ActionType Action { get; set; }

		public Dictionary<string, string> Parameters { get; set; }
    }

	public enum ActionType
	{
		CashIn,
		CashOut,
		Swap,
		OrdinaryCashOut,
		Transfer	
	}
}
