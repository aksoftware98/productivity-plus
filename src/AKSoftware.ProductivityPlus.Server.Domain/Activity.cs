using AKSoftware.ProductivityPlus.Server.Domain.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AKSoftware.ProductivityPlus.Server.Domain
{

	/// <summary>
	/// Represents an action that can be done by the user. The action can be work, meditation, study, exercises, etc.
	/// </summary>
	public class Activity : Entity
	{

		public Activity()
		{
			Name = string.Empty;
			Category = string.Empty;
			CreationDate = DateTimeOffset.UtcNow;
			ModificationDate = DateTimeOffset.UtcNow;
			UserId = string.Empty;
		}

		[JsonProperty("name")]
		public string Name { get; private set; }

		[JsonProperty("description")]
		public string? Description { get; private set; }

		[JsonProperty("icon")]
		public string? Icon { get; private set; }

		[JsonProperty("color")]
		public string? Color { get; private set; }

		[JsonProperty("dailyGoal")]
		public int DailyGoal { get; private set; }

		[JsonProperty("successThreshold")]
		public float SuccessThreshold { get; private set; }

		[JsonProperty("weeklyGoal")]
		public int WeeklyGoal { get; private set; }

		[JsonProperty("weekendIncluded")]
		public bool WeekendIncluded { get; private set; }

		[JsonProperty("isArchived")]
		public bool IsArchived { get; private set; }

		[JsonProperty("category")]
		public string Category { get; private set; }

		[JsonProperty("isTrackableAction")]
		public bool IsTrackableAction { get; private set; }

		[JsonProperty("allowParallelActions")]
		public bool AllowParallelActions { get; private set; }

		[JsonProperty("userId")]
		public string UserId { get; private set; }

		public static Activity Create(string name, bool isTrackableAction, string userId, bool allowParallelActions, string category, int dailyGoal, float successThreshold, int weeklyGoal, bool weekendIncluded, string? description = null, string? icon = null, string? color = null)
		{

			var activity = new Activity
			{
				Name = name,
				Category = category,
				DailyGoal = dailyGoal,
				IsTrackableAction = isTrackableAction,
				AllowParallelActions = allowParallelActions,
				SuccessThreshold = successThreshold,
				WeeklyGoal = weeklyGoal,
				WeekendIncluded = weekendIncluded,
				Description = description,
				Icon = icon,
				Color = color,
				UserId = userId
			};

			return activity;
		}

		public void SetColor(string color)
		{
			if (string.IsNullOrWhiteSpace(color))
				throw new InvalidInputException("The color can not be null or empty");
			// Validate the color value 
			if (color.Length != 7 || !color.StartsWith("#"))
				throw new InvalidInputException("The color must be in the format #FFFFFF");

			if (color.Equals(Color))
				return; // No changes
			Color = color;
			ModificationDate = DateTimeOffset.UtcNow;
		}

		public void SetIcon(string icon)
		{
			if (string.IsNullOrWhiteSpace(icon))
				throw new InvalidInputException("The icon can not be null or empty");

			if (icon.Equals(Icon))
				return;
			Icon = icon;
			ModificationDate = DateTimeOffset.UtcNow;
		}

		public void SetDescription(string? description)
		{
			if (string.Equals(description, Description))
				return;
			Description = description;
			ModificationDate = DateTimeOffset.UtcNow;
		}

		public void SetName(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
				throw new InvalidInputException("The name can not be null or empty");

			if (string.Equals(name, Name))
				return;
			Name = name;
			ModificationDate = DateTimeOffset.UtcNow;
		}

		public void SetGoals(int dailyGoal, int weeklyGoal, bool weekendIncluded, float successThreshold)
		{
			// Validate the goals 
			if (dailyGoal < 1)
				throw new InvalidValueException("The daily goal must be greater than 0");

			if (weeklyGoal < 1)
				throw new InvalidValueException("The weekly goal must be greater than 0");

			if (successThreshold < 0 || successThreshold > 100)
				throw new InvalidValueException("The success threshold must be between 0 and 100");

			if (dailyGoal.Equals(DailyGoal) && weeklyGoal.Equals(WeeklyGoal) && weekendIncluded.Equals(WeekendIncluded) && successThreshold.Equals(SuccessThreshold))
				return;

			DailyGoal = dailyGoal;
			WeeklyGoal = weeklyGoal;
			WeekendIncluded = weekendIncluded;
			SuccessThreshold = successThreshold;
			ModificationDate = DateTimeOffset.UtcNow;
		}

		public void Archive(bool archive)
		{
			if (archive.Equals(IsArchived))
				return;

			IsArchived = archive;
			ModificationDate = DateTimeOffset.UtcNow;
		}
	}

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
