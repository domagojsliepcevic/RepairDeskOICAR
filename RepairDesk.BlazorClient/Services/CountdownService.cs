using System.Timers;

namespace RepairDesk.BlazorClient.Services
{
	public class CountdownService
	{
		public TimeSpan TimeLeft { get; private set; } = new TimeSpan(2, 0, 0, 0);

		public event Action OnTimeUpdated;

		private System.Timers.Timer timer;

		public CountdownService()
		{
			timer = new System.Timers.Timer(1000);
			timer.Elapsed += OnTimerElapsed;
			timer.Start();
		}

		private void OnTimerElapsed(Object source, ElapsedEventArgs e)
		{
			if (TimeLeft.TotalSeconds > 0)
			{
				TimeLeft = TimeLeft.Add(TimeSpan.FromSeconds(-1));
				OnTimeUpdated?.Invoke();
			}
			else
			{
				timer.Stop();
			}
		}

		public void Dispose()
		{
			timer?.Dispose();
		}
	}

}
