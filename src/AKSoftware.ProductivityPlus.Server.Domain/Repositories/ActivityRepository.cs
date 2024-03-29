﻿using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKSoftware.ProductivityPlus.Server.Domain.Repositories
{
	/// <summary>
	/// TODO: Handle the 404 not found and manage the error and exceptions in a better way
	/// </summary>
	public class ActivityRepository
	{

		private readonly CosmosClient _cosmosClient;
		private const string ContainerName = "activities";
		private const string DatabaseName = "productivityplus-db";
		private readonly Container _container;
		public ActivityRepository(CosmosClient cosmosClient)
		{
			_cosmosClient = cosmosClient;
			_container = _cosmosClient.GetContainer(DatabaseName, ContainerName);
		}

		public async Task<Activity> CreateAsync(string userId, Activity activity)
		{
			var response = await _container.CreateItemAsync(activity, new PartitionKey(userId));
			return response.Resource;
		}

		public async Task<Activity> UpdateGoalsAsync(string userId, Activity activity)
		{
			var patchItems = new List<PatchOperation>()
			{
				PatchOperation.Replace("/dailyGoal", activity.DailyGoal),
				PatchOperation.Replace("/weeklyGoal", activity.WeeklyGoal),
				PatchOperation.Replace("/successThreshold", activity.SuccessThreshold),
				PatchOperation.Replace("/weekendIncluded", activity.WeekendIncluded),
				PatchOperation.Replace("/modificationDate", activity.ModificationDate)
			};

			var response = await _container.PatchItemAsync<Activity>(activity.Id, new PartitionKey(userId), patchItems);
			return response.Resource;
		}

		public async Task<Activity> UpdateNameAsync(string userId, Activity activity)
		{
			var patchItems = new List<PatchOperation>()
			{
				PatchOperation.Replace("/name", activity.Name),
				PatchOperation.Replace("/modificationDate", activity.ModificationDate)
			};

			var response = await _container.PatchItemAsync<Activity>(activity.Id, new PartitionKey(userId), patchItems);
			return response.Resource;
		}

		public async Task<Activity> UpdateColorAsync(string userId, Activity activity)
		{
			var patchItems = new List<PatchOperation>()
			{
				PatchOperation.Replace("/color", activity.Color),
				PatchOperation.Replace("/modificationDate", activity.ModificationDate)
			};

			var response = await _container.PatchItemAsync<Activity>(activity.Id, new PartitionKey(userId), patchItems);
			return response.Resource;
		}

		public async Task<Activity> UpdateIconAsync(string userId, Activity activity)
		{
			var patchItems = new List<PatchOperation>()
			{
				PatchOperation.Replace("/icon", activity.Icon),
				PatchOperation.Replace("/modificationDate", activity.ModificationDate)
			};

			var response = await _container.PatchItemAsync<Activity>(activity.Id, new PartitionKey(userId), patchItems);
			return response.Resource;
		}

		public async Task<Activity> DeleteAsync(string userId, Activity activity)
		{
			var patchItems = new List<PatchOperation>()
			{
				PatchOperation.Replace("/isArchived", activity.IsArchived),
				PatchOperation.Replace("/modificationDate", activity.ModificationDate)
			};

			var response = await _container.PatchItemAsync<Activity>(activity.Id, new PartitionKey(userId), patchItems);
			return response.Resource;
		}

		public async Task<int> GetAllCount(string userId)
		{
			var query = $"SELECT VALUE COUNT(c.id) IN c WHERE c.userId = @userId and c.isArchived = false";
			var queryDefinition = new QueryDefinition(query)
				.WithParameter("@userId", userId);

			var iterator = _container.GetItemQueryIterator<int>(queryDefinition);
			var response = await iterator.ReadNextAsync();
			return response.FirstOrDefault();
		}

		public async Task<IEnumerable<Activity>> ListAsync(string userId)
		{
			var query = $"SELECT * IN c WHERE c.userId = @userId and c.isArchived = false and c.discriminator = 'Activity'";
			var queryDefinition = new QueryDefinition(query)
				.WithParameter("@userId", userId);

			var iterator = _container.GetItemQueryIterator<Activity>(queryDefinition);
			var response = await iterator.ReadNextAsync();
			return response.Resource;
		}
	}
}
