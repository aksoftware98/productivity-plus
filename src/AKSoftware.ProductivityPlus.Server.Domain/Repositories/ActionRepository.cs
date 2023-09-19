namespace AKSoftware.ProductivityPlus.Server.Domain.Repositories
{
	public class ActionRepository
	{
		private readonly CosmosClient _cosmosClient;
		private const string ContainerName = "activities";
		private const string DatabaseName = "productivityplus-db";
		private readonly Container _container;
		public ActionRepository(CosmosClient cosmosClient)
		{
			_cosmosClient = cosmosClient;
			_container = _cosmosClient.GetContainer(DatabaseName, ContainerName);
		}

		public async Task<ActivityAction> StartAsync(string userId, ActivityAction action)
		{
			var response = await _container.CreateItemAsync<ActivityAction>(action, new PartitionKey(userId));
			return response.Resource;
		}

		public async Task<ActivityAction> StopAsync(string userId, Activity activity, ActivityAction action)
		{
			var patchItems = new List<PatchOperation>()
			{
				PatchOperation.Replace("/modificationDate", action.ModificationDate)
			};

			if (activity.IsTrackableAction)
			{
				var index = action.Flows!.Count - 1;
				var lastFlow = action.Flows!.Last();
				patchItems.Add(PatchOperation.Replace("/endDate", action.EndDate));
				patchItems.Add(PatchOperation.Replace($"/flows/{index}/isRunning", false));
				patchItems.Add(PatchOperation.Replace($"/flows/{index}/endDate", lastFlow.EndDate));
				patchItems.Add(PatchOperation.Replace($"/flows/{index}/duration", lastFlow.Duration));
			}
			else
			{
				patchItems.Add(PatchOperation.Replace("/actionTime", action.ActionTime));
			}

			var response = await _container.PatchItemAsync<ActivityAction>(action.Id, new PartitionKey(userId), patchItems);
			return response.Resource;
		}

		public async Task DeleteAsync(string userId, string actionId)
		{
			await _container.DeleteItemAsync<ActivityAction>(actionId, new PartitionKey(userId));
		}	

		public async Task<ActivityAction> GetAsync(string userId, string actionId)
		{
			var response = await _container.ReadItemAsync<ActivityAction>(actionId, new PartitionKey(userId));
			return response.Resource;
		}

		public async Task<ActivityAction> PauseAsync(string userId, ActivityAction action)
		{
			if (action.Flows == null)
				throw new InvalidInputException("The action is not trackable");

			var index = action.Flows.Count - 1;
			var lastFlow = action.Flows.Last();
			var patchItems = new List<PatchOperation>()
			{
				PatchOperation.Replace("/modificationDate", action.ModificationDate),
				PatchOperation.Replace($"/flows/{index}/isRunning", false),
				PatchOperation.Replace($"/flows/{index}/endDate", lastFlow.EndDate),
				PatchOperation.Replace($"/flows/{index}/duration", lastFlow.Duration)
			};

			var response = await _container.PatchItemAsync<ActivityAction>(action.Id, new PartitionKey(userId), patchItems);
			return response.Resource;
		}

		public async Task<ActivityAction> ResumeAsync(string userId, ActivityAction action)
		{
			if (action.Flows == null)
				throw new InvalidInputException("The action is not trackable");

			var patchItems = new List<PatchOperation>()
			{
				PatchOperation.Replace("/modificationDate", action.ModificationDate),
				PatchOperation.Add("/flows/-", action.Flows!.Last())
			};

			var response = await _container.PatchItemAsync<ActivityAction>(action.Id, new PartitionKey(userId), patchItems);
			return response.Resource;
		}

		public async Task<IEnumerable<ActivityAction>> ListAsync(string userId, DateOnly dateOnly)
		{
			var query = $"SELECT * IN c WHERE c.userId = @userId and c.discriminator = 'ActivityAction' and Substring(ToString(c.startDate), 0, 10) = @date";
			var queryDefinition = new QueryDefinition(query)
				.WithParameter("@userId", userId)
				.WithParameter("@date", dateOnly.ToString());

			var iterator = _container.GetItemQueryIterator<ActivityAction>(queryDefinition);
			var response = await iterator.ReadNextAsync();
			return response.Resource;
		}
	}
}
