using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureRepositories.Azure.Queue;
using Core;
using Core.Log;
using Core.Repositories;
using Core.Settings;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using Services.CommandSenders;
using Services.Converters;

namespace Services
{
	public static class RegisterDependency
	{
		public static void RegisterServices(this IServiceCollection services)
		{
			services.AddTransient<IRouteService, RouteService>();
			services.AddTransient<BitcoinConverter>();
			services.AddTransient<EthereumConverter>();
			services.AddTransient<BitcoinCommandSender>();
			services.AddTransient<EthereumCommandSender>();


			services.AddTransient<Func<string, IRequestConverter>>(provider => x =>
			{
				switch (x)
				{
					case Constants.BitcoinBlockchain:
						return provider.GetService<BitcoinConverter>();
					case Constants.EthereumBlockchain:
						return provider.GetService<EthereumConverter>();
					default:
						throw new Exception("Unregistered blockchain - " + x);
				}
			});

			services.AddTransient<Func<string, ICommandSender>>(provider => x =>
			{
				switch (x)
				{
					case Constants.BitcoinBlockchain:
						return provider.GetService<BitcoinCommandSender>();
					case Constants.EthereumBlockchain:
						return provider.GetService<EthereumCommandSender>();
					default:
						throw new Exception("Unregistered blockchain - " + x);
				}
			});
		}
	}
}
