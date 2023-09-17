using Newtonsoft.Json;

namespace AKSoftware.ProductivityPlus.Server.Domain
{
	public class ActionFlow
	{

		public ActionFlow()
		{
			Id = Guid.NewGuid().ToString();
			IsRunning = true;
			StartDate = DateTimeOffset.UtcNow;
		}

		[JsonProperty("id")]
		public string Id { get; private set; }

		[JsonProperty("isRunning")]
		public bool IsRunning { get; private set; }

		[JsonProperty("startDate")]
		public DateTimeOffset StartDate { get; private set; }

		[JsonProperty("endDate")]
		public DateTimeOffset? EndDate { get; private set; }

		[JsonProperty("totalTime")]
		[JsonIgnore]
		public TimeSpan? TotalTime { get; private set; }

		public string? Duration { get; private set; }

		public void Stop()
		{
			if (!IsRunning)
				return;

			IsRunning = false;
			EndDate = DateTimeOffset.UtcNow;
			TotalTime = EndDate - StartDate;
			Duration = TotalTime?.ToString(@"hh\:mm\:ss");
		}

		public static ActionFlow Create()
		{
			return new ActionFlow();
		}
	}
}
