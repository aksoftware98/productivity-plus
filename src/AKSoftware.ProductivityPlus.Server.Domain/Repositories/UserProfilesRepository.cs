namespace AKSoftware.ProductivityPlus.Server.Domain.Repositories
{
	public class UserProfilesRepository
	{

		private readonly CosmosClient _cosmosClient;
		private const string ContainerName = "activities";
		private const string DatabaseName = "productivityplus-db";
		private readonly Container _container;
		public UserProfilesRepository(CosmosClient cosmosClient)
		{
			_cosmosClient = cosmosClient;
			_container = _cosmosClient.GetContainer(DatabaseName, ContainerName);
		}

		public async Task<UserProfile> CreateAsync(string userId, UserProfile userProfile)
		{
			var response = await _container.CreateItemAsync(userProfile, new PartitionKey(userId));
			return response.Resource;
		}

		public async Task<UserProfile?> GetByUserIdAsync(string userId)
		{
			string query = $"SELECT * FROM c WHERE c.userId = @userId and c.discriminator = 'UserProfile'";
			var queryDefinition = new QueryDefinition(query)
				.WithParameter("@userId", userId);

			var iterator = _container.GetItemQueryIterator<UserProfile>(queryDefinition);
			var response = await iterator.ReadNextAsync();
			return response.Resource.FirstOrDefault();
		}

		public async Task<UserProfile> UpdateDisplayNameAsync(string userId, UserProfile userProfile)
		{
			var patchItems = new List<PatchOperation>()
			{
				PatchOperation.Replace("/displayName", userProfile.DisplayName),
				PatchOperation.Replace("/modificationDate", userProfile.ModificationDate)
			};

			var response = await _container.PatchItemAsync<UserProfile>(userProfile.Id, new PartitionKey(userId), patchItems);
			return response.Resource;
		}

		public async Task<UserProfile> UpdateAvatarAsync(string userId, UserProfile userProfile)
		{
			var patchItems = new List<PatchOperation>()
			{
				PatchOperation.Replace("/avatarUrl", userProfile.AvatarUrl),
				PatchOperation.Replace("/modificationDate", userProfile.ModificationDate)
			};

			var response = await _container.PatchItemAsync<UserProfile>(userProfile.Id, new PartitionKey(userId), patchItems);
			return response.Resource;
		}

		public async Task<UserProfile> UpdateNameAsync(string userId, UserProfile userProfile)
		{
			var patchItems = new List<PatchOperation>()
			{
				PatchOperation.Replace("/firstName", userProfile.FirstName),
				PatchOperation.Replace("/lastName", userProfile.LastName),
				PatchOperation.Replace("/modificationDate", userProfile.ModificationDate)
			};

			var response = await _container.PatchItemAsync<UserProfile>(userProfile.Id, new PartitionKey(userId), patchItems);
			return response.Resource;
		}

		public async Task<UserProfile> UpdateDailyGoalAsync(string userId, UserProfile userProfile)
		{
			var patchItems = new List<PatchOperation>()
			{
				PatchOperation.Replace("/dailyGoal/workingMinutes", userProfile.DailyGoal.WorkingMinutes),
				PatchOperation.Replace("/dailyGoal/learningMinutes", userProfile.DailyGoal.LearningMinutes),
				PatchOperation.Replace("/dailyGoal/mediationSessions", userProfile.DailyGoal.MeditationSessions),
				PatchOperation.Replace("/modificationDate", userProfile.ModificationDate)
			};

			var response = await _container.PatchItemAsync<UserProfile>(userProfile.Id, new PartitionKey(userId), patchItems);
			return response.Resource;
		}

		public async Task<UserProfile> UpdateWeeklyGoalAsync(string userId, UserProfile userProfile)
		{
			var patchItems = new List<PatchOperation>()
			{
				PatchOperation.Replace("/weeklyGoal/workingMinutes", userProfile.WeeklyGoal.WorkingMinutes),
				PatchOperation.Replace("/weeklyGoal/learningMinutes", userProfile.WeeklyGoal.LearningMinutes),
				PatchOperation.Replace("/weeklyGoal/mediationSessions", userProfile.WeeklyGoal.MeditationSessions),
				PatchOperation.Replace("/modificationDate", userProfile.ModificationDate)
			};

			var response = await _container.PatchItemAsync<UserProfile>(userProfile.Id, new PartitionKey(userId), patchItems);
			return response.Resource;
		}

	}
}
