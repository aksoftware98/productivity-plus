using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKSoftware.ProductivityPlus.Server.Domain.Models;

public class UserProfile : Entity
{

    public UserProfile()
    {
        FirstName = string.Empty;
		LastName = string.Empty;
		Email = string.Empty;
		DisplayName = string.Empty;
		AvatarUrl = string.Empty;
		UserId = string.Empty;
		DailyGoal = new();
		WeeklyGoal = new();
		Discriminator = "UserProfile";
    }

    [JsonProperty("firstName")]
	public string FirstName { get; private set; }

	[JsonProperty("lastName")]
	public string LastName { get; private set; }

	[JsonProperty("email")]
	public string Email { get; private set; }

	[JsonProperty("displayName")]
	public string DisplayName { get; private set; }

	[JsonProperty("avatarUrl")]
	public string UserId { get; private set; }

	[JsonProperty("avatarUrl")]
	public string AvatarUrl { get; private set; }

	[JsonProperty("dailyGoal")]
	public Goal DailyGoal { get; private set; }

	[JsonProperty("weeklyGoal")]
	public Goal WeeklyGoal { get; private set; }
	// TODO: Add other metadata like social media links, etc.

	public static UserProfile Create(string firstName, string lastName, string email, string displayName, string avatarUrl)
	{
		if (string.IsNullOrEmpty(firstName))
			throw new ArgumentNullException(nameof(firstName));
		if (string.IsNullOrEmpty(lastName))
			throw new ArgumentNullException(nameof(lastName));
		if (string.IsNullOrEmpty(email))
			throw new ArgumentNullException(nameof(email));
		if (string.IsNullOrEmpty(displayName))
			throw new ArgumentNullException(nameof(displayName));
		if (string.IsNullOrEmpty(avatarUrl))
			avatarUrl = "https://www.gravatar.com/a"; // TODO: Define a default avatar link

		return new UserProfile
		{
			FirstName = firstName,
			LastName = lastName,
			Email = email,
			DisplayName = displayName,
			AvatarUrl = avatarUrl
		};
	}

	public void SetDisplayName(string displayName)
	{
		if (string.IsNullOrEmpty(displayName))
			throw new ArgumentNullException(nameof(displayName));

		DisplayName = displayName;
		ModificationDate = DateTime.UtcNow;
	}

	public void SetAvatarUrl(string avatarUrl)
	{
		if (string.IsNullOrEmpty(avatarUrl))
			throw new ArgumentNullException(nameof(avatarUrl));

		AvatarUrl = avatarUrl;
		ModificationDate = DateTime.UtcNow;
	}

	public void SetName(string firstName, string lastName)
	{
		if (string.IsNullOrEmpty(firstName))
			throw new ArgumentNullException(nameof(firstName));
		if (string.IsNullOrEmpty(lastName))
			throw new ArgumentNullException(nameof(lastName));

		FirstName = firstName;
		LastName = lastName;
		ModificationDate = DateTime.UtcNow;
	}	

	public void SetDailyGoal(int workingMinutes, int learningMinutes, int meditationSessions)
	{
		if (workingMinutes < 0 || learningMinutes < 0 || meditationSessions < 0)
			throw new ArgumentException("The goal values cannot be negative");
		if (workingMinutes + learningMinutes + meditationSessions > 60 * 24)
			throw new ArgumentException("The goal values cannot be more than 24 hours");

		DailyGoal.SetGoal(workingMinutes, learningMinutes, meditationSessions);
		ModificationDate = DateTime.UtcNow;
	}

	public void SetWeeklyGoal(int workingMinutes, int learningMinutes, int meditationSessions)
	{
		if (workingMinutes < 0 || learningMinutes < 0 || meditationSessions < 0)
			throw new ArgumentException("The goal values cannot be negative");
		if (workingMinutes + learningMinutes + meditationSessions > 60 * 24 * 7)
			throw new ArgumentException("The goal values cannot be more than 7 days");

		DailyGoal.SetGoal(workingMinutes, learningMinutes, meditationSessions);
		ModificationDate = DateTime.UtcNow;
	}


}

public class Goal
{
    public int WorkingMinutes { get; private set; }

	public int LearningMinutes { get; private set; }

	public int MeditationSessions { get; private set; }

    public Goal()
    {
		WorkingMinutes = 0;
		LearningMinutes = 0;
		MeditationSessions = 0;
    }

	public void SetGoal(int workingMinutes, int learningMinutes, int meditationSessions)
	{
		WorkingMinutes = workingMinutes;
		LearningMinutes = learningMinutes;
		MeditationSessions = meditationSessions;
	}
}
