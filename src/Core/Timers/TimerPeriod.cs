﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Log;
using Core.Timers.Interfaces;

namespace Core.Timers
{
	// Таймер, который исполняет метод Execute через определенный интервал после окончания исполнения метода Execute
	public abstract class TimerPeriod : IStarter, ITimerCommand
	{
		private readonly string _componentName;
		private readonly int _periodMs;
		private readonly ILog _log;
		private bool _finished = false;


		protected TimerPeriod(string componentName, int periodMs, ILog log)
		{
			_componentName = componentName;

			_periodMs = periodMs;
			_log = log;
		}

		public bool Working { get; set; }

		private void LogFatalError(Exception exception)
		{
			try
			{
				_log.WriteFatalError(_componentName, "Loop", "", exception).Wait();
			}
			catch (Exception)
			{
			}
		}

		public abstract Task Execute();

		private async Task ThreadMethod()
		{
			_finished = false;
			while (Working)
			{
				try
				{
					await Execute();
				}
				catch (Exception exception)
				{
					LogFatalError(exception);
				}
				await Task.Delay(_periodMs);
			}
			_finished = true;
		}

		public virtual void Start()
		{

			if (Working)
				return;

			Working = true;
			Task.Run(async () => { await ThreadMethod(); });

		}

		public virtual async Task Stop()
		{
			if (!Working) return;
			Working = false;
			while (!_finished)
				await Task.Delay(50);
		}

		public string GetComponentName()
		{
			return _componentName;
		}
	}
}
