namespace Core
{
	public class Constants
	{
		/// <summary>
		/// Used to change table and queue names in testing enviroment
		/// </summary>
		public static string StoragePrefix { get; set; } = "";

		/// <summary>
		/// Used to send commands to bitcoin blockchain
		/// </summary>
		public const string BitcoinQueue = "intransactions";

		public const string EthereumQueue = "ethereum-coin-request-queue";

		public const string EmailNotifierQueue = "emailsqueue";		

		/// <summary>
		/// Used to receive transaction requests from external services
		/// </summary>
		public const string RouterIncomeQueue = "router-income-queue";

	

	}
}
