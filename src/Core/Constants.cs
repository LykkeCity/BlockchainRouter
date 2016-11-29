namespace Core
{
	public class Constants
	{
	    public const string EthereumBlockchain = "ethereum";
	    public const string BitcoinBlockchain = "bitcoin";

		/// <summary>
		/// Used to change table and queue names in testing enviroment
		/// </summary>
		public static string StoragePrefix { get; set; } = "";

		/// <summary>
		/// Used to send commands to bitcoin blockchain
		/// </summary>
		public const string BitcoinQueue = "bitcoin-coin-request-queue";

		public const string EthereumQueue = "ethereum-coin-request-queue";

		public const string EmailNotifierQueue = "emailsqueue";		

		/// <summary>
		/// Used to receive transaction requests from external services
		/// </summary>
		public const string RouterIncomeQueue = "router-income-queue";

		public const string RouterSignedRequestQueue = "router-signed-request-queue";

		public const string EthereumSignedRequestQueue = "ethereum-signed-request-queue";
		public const string BitcoinSignedRequestQueue = "bitcoin-signed-request-queue";


        public const string CoinTable = "Dictionaries";
		public const string MonitoringTable = "MonitoringTable";
	}
}
