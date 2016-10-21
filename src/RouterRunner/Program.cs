using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Core.Settings;
using RoutingJob;

namespace TransactionHandlerRunner
{
	public class Program
	{
		static JobApp JobApp { get; set; }

		public static void Main(string[] args)
		{
			Console.Clear();
			Console.Title = "Blockchain Router Job - Ver. " + Microsoft.Extensions.PlatformAbstractions.PlatformServices.Default.Application.ApplicationVersion;

			var settings = GetSettings();
			if (settings == null)
			{
				Console.WriteLine("Press any key to exit...");
				Console.ReadKey();
				return;
			}

			try
			{
				CheckSettings(settings);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				Console.WriteLine("Press any key to exit...");
				Console.ReadKey();
				return;
			}

			Console.WriteLine("Settings checked!");

			try
			{
				JobApp = new JobApp();
				JobApp.Run(settings);
			}
			catch (Exception e)
			{
				Console.WriteLine("cannot start jobs! Exception: " + e.Message);
				Console.WriteLine("Press any key to exit...");
				Console.ReadKey();
				return;
			}

			Console.WriteLine("Web job started");
			Console.WriteLine("Utc time: " + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));

			Console.WriteLine("Press 'q' to quit.");


			while (Console.ReadLine() != "q") continue;
			JobApp.Stop().Wait();
		}

		static BaseSettings GetSettings()
		{
			var settingsData = ReadSettingsFile();

			if (string.IsNullOrWhiteSpace(settingsData))
			{
				Console.WriteLine("Please, provide generalsettings.json file");
				return null;
			}

			BaseSettings settings = GeneralSettingsReader.ReadSettingsFromData<BaseSettings>(settingsData);

			return settings;
		}

		static string ReadSettingsFile()
		{
			try
			{
#if DEBUG
				return File.ReadAllText(@"..\..\settings\generalsettings.json");
#else
				return File.ReadAllText("generalsettings.json");
#endif
			}
			catch (Exception e)
			{
				return null;
			}
		}

		static void CheckSettings(BaseSettings settings)
		{
			if (string.IsNullOrWhiteSpace(settings.Db?.DataConnString))
				throw new Exception("DataConnString is missing");
			if (string.IsNullOrWhiteSpace(settings.Db?.LogsConnString))
				throw new Exception("LogsConnString is missing");
			if (string.IsNullOrWhiteSpace(settings.Db?.ExchangeQueueConnString))
				throw new Exception("ExchangeQueueConnString is missing");
			if (string.IsNullOrWhiteSpace(settings.Db?.EthereumNotificationsConnString))
				throw new Exception("EthereumNotificationsConnString is missing");
		}


	}
}
