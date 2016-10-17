using System;
using System.Collections.Generic;
using System.Numerics;

namespace Core.Settings
{
	public interface IBaseSettings
	{
		DbSettings Db { get; set; }
		
		int MinTransactionConfirmaionLevel { get; set; }
		string ApiUrl { get; set; }
	}

	public class BaseSettings : IBaseSettings
	{
		public DbSettings Db { get; set; }

		public int MinTransactionConfirmaionLevel { get; set; } = 1;
		public string ApiUrl { get; set; }
	}

	public class DbSettings
	{
		public string DataConnString { get; set; }
		public string LogsConnString { get; set; }


		public string ExchangeQueueConnString { get; set; }
		public string EthereumNotificationsConnString { get; set; }
	}
}
