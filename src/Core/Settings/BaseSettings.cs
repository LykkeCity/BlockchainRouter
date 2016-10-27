using System;
using System.Collections.Generic;
using System.Numerics;

namespace Core.Settings
{
	public interface IBaseSettings
	{
		DbSettings Db { get; set; }				
	}

	public class BaseSettings : IBaseSettings
	{
		public DbSettings Db { get; set; }		
	}

	public class DbSettings
	{
		public string DataConnString { get; set; }
		public string LogsConnString { get; set; }


		public string ExchangeQueueConnString { get; set; }
		public string EthereumNotificationsConnString { get; set; }
		public string BitcoinConnectionString { get; set; }
	}
}
