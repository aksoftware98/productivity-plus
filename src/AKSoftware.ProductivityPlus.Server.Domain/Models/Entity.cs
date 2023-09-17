using Newtonsoft.Json;

namespace AKSoftware.ProductivityPlus.Server.Domain.Models
{
    public class Entity
    {

        public Entity()
        {
            Id = Guid.NewGuid().ToString();
            CreationDate = DateTimeOffset.UtcNow;
            ModificationDate = DateTimeOffset.UtcNow;
            Discriminator = string.Empty;
        }

        [JsonProperty("id")]
        public string Id { get; private set; }

        [JsonProperty("creationDate")]
        public DateTimeOffset CreationDate { get; protected set; }

        [JsonProperty("modificationDate")]
        public DateTimeOffset ModificationDate { get; protected set; }

        [JsonProperty("discriminator")]
        public string Discriminator { get; protected set; }


    }
}
