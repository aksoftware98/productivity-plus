using AKSoftware.ProductivityPlus.Server.Domain.Exceptions;
using Newtonsoft.Json;

namespace AKSoftware.ProductivityPlus.Server.Domain
{
	/// <summary>
	/// Represents an action for a specific activity, it can be a work session for a work activity, or a meditation session for a meditation activity, etc.
	/// </summary>
	public class Action : Entity
	{

		public Action()
		{
			ActivityId = string.Empty;
			UserId = string.Empty;
			CreationDate = DateTimeOffset.UtcNow;
			ModificationDate = DateTimeOffset.UtcNow;
		}

		[JsonProperty("description")]
		public string? Description { get; private set; }

		[JsonProperty("startDate")]
		public DateTimeOffset? StartDate { get; private set; }

		[JsonProperty("endDate")]
		public DateTimeOffset? EndDate { get; private set; }

		[JsonProperty("actionTime")]
		public DateTimeOffset? ActionTime { get; private set; }

		[JsonProperty("activityId")]
		public string ActivityId { get; private set; }

		[JsonProperty("flows")]
		public List<ActionFlow>? Flows { get; private set; }

		[JsonProperty("userId")]
		public string UserId { get; set; }

		public bool IsFinished => EndDate != null || ActionTime != null;

		public static Action Create(Activity activity, string userId, string? description = null)
		{
			if (activity == null)
				throw new ArgumentNullException(nameof(activity));
			if (string.IsNullOrWhiteSpace(userId))
				throw new InvalidInputException("The user id can not be null or empty");

			var action = new Action
			{
				ActivityId = activity.Id,
				Description = description,
				UserId = userId,
				Flows = activity.IsTrackableAction ? new List<ActionFlow>() : null,
				StartDate = activity.IsTrackableAction ? DateTimeOffset.UtcNow : null,
				EndDate = activity.IsTrackableAction ? DateTimeOffset.UtcNow : null,
				ActionTime = activity.IsTrackableAction ? null : DateTimeOffset.UtcNow
			};

			return action;
		}

		public void SetDescription(string? description)
		{
			if (string.Equals(description, Description))
				return;

			Description = description;
		}

		public void Pause()
		{
			bool isTrackable = Flows != null;
			if (!isTrackable)
				throw new DomainException("The action is not trackable");

			var lastFlow = Flows?.LastOrDefault();
			if (lastFlow == null)
				throw new DomainException("The action does not have any flow");

			if (!lastFlow.IsRunning)
				return;

			lastFlow.Stop();
			ModificationDate = DateTimeOffset.UtcNow;
		}

		public void Resume()
		{
			bool isTrackable = Flows != null;
			if (!isTrackable)
				throw new DomainException("The action is not trackable");

			var lastFlow = Flows?.LastOrDefault();
			if (lastFlow == null)
				throw new DomainException("The action does not have any flow");

			if (lastFlow.IsRunning)
				return;

			var newFlow = ActionFlow.Create();
			Flows?.Add(newFlow);
			ModificationDate = DateTimeOffset.UtcNow;
		}

		public void Stop()
		{
			bool isTrackable = Flows != null;
			if (!isTrackable)
				throw new DomainException("The action is not trackable");

			var lastFlow = Flows?.LastOrDefault();
			if (lastFlow == null)
				throw new DomainException("The action does not have any flow");

			if (!lastFlow.IsRunning)
				return;

			lastFlow.Stop();
			EndDate = DateTimeOffset.UtcNow;
			ModificationDate = DateTimeOffset.UtcNow;
		}

		public void DoAction()
		{
			bool isTrackable = Flows != null;
			if (isTrackable)
				throw new DomainException("The action is trackable and can be marked using the time tracker feature");

			if (ActionTime != null)
				throw new DomainException("The action has already been marked");

			ActionTime = DateTimeOffset.UtcNow;
			ModificationDate = DateTimeOffset.UtcNow;
		}
	}
}
