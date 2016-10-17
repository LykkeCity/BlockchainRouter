using AzureRepositories;
using Core.Settings;
using Microsoft.Extensions.DependencyInjection;
using RoutingJob.Jobs;
using Services;

namespace RoutingJob.Config
{
	public static class RegisterDepencency
	{
		public static void InitJobDependencies(this IServiceCollection collection, IBaseSettings settings)
		{
			collection.AddSingleton(settings);

			collection.RegisterAzureLogs(settings, "TransactionHandler");
			collection.RegisterAzureStorages(settings);
			collection.RegisterAzureQueues(settings);

			collection.RegisterServices();

			RegisterJobs(collection);
		}

		private static void RegisterJobs(IServiceCollection collection)
		{
		
			collection.AddSingleton<MonitoringJob>();		
		}
	}
}
