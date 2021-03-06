﻿using System;
using System.Threading.Tasks;
using Core.Settings;
using Microsoft.Extensions.DependencyInjection;
using RoutingJob.Config;
using RoutingJob.Jobs;

namespace RoutingJob
{
	public class JobApp
	{
		public IServiceProvider Services { get; set; }

		public void Run(IBaseSettings settings)
		{
			IServiceCollection collection = new ServiceCollection();
			collection.InitJobDependencies(settings);

			Services = collection.BuildServiceProvider();

			// start monitoring
			Services.GetService<MonitoringJob>().Start();
			Services.GetService<RouteJob>().Start();
			Services.GetService<ProcessSignaturesJob>().Start();
		}

		public async Task Stop()
		{
			await Services.GetService<MonitoringJob>().Stop();
			await Services.GetService<RouteJob>().Stop();
			await Services.GetService<ProcessSignaturesJob>().Stop();
		}
	}
}
