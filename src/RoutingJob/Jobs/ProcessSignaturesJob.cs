using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Log;
using Core.Timers;
using Services;

namespace RoutingJob.Jobs
{
	public class ProcessSignaturesJob : TimerPeriod
	{
		private readonly ISignatureService _signatureService;
		public const int PeriodSeconds = 2;

		public ProcessSignaturesJob(ISignatureService signatureService, ILog log) : base("ProcessSignaturesJob", PeriodSeconds * 1000, log)
		{
			_signatureService = signatureService;
		}

		public override async Task Execute()
		{
			while (Working && await _signatureService.ProcessNextSignedRequest())
			{

			}
		}
	}
}
