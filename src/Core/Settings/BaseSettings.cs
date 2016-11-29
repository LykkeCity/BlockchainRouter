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
		public string LogsConnString { get; set; }

	    public string SharedTransactionConnString { get; set; }
	    public string SharedStorageConnString { get; set; }

        public string DictsConnString { get; set; }

        public string EthereumHandlerConnString { get; set; }
		public string BitcoinHandlerConnString { get; set; }
	}
}
