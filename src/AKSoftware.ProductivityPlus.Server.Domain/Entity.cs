using Newtonsoft.Json;

namespace AKSoftware.ProductivityPlus.Server.Domain
{
	public class Entity
	{

        public Entity()
        {
			Id = Guid.NewGuid().ToString();
        }

        [JsonProperty("id")]
		public string Id { get; private set; }

		[JsonProperty("creationDate")]
		public DateTimeOffset CreationDate { get; protected set; }

		[JsonProperty("modificationDate")]
		public DateTimeOffset ModificationDate { get; protected set; }


	}
}
