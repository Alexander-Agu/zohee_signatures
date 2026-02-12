using System.Reflection.Metadata;
using System.Text.Json.Serialization;

namespace ZoheeApi.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public bool HasSigned { get; set; } = false;

        public int DocumentId { get; set; }

        [JsonIgnore]
        public Documents Documents { get; set; } = new Documents();
    }
}
