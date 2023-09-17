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
}
