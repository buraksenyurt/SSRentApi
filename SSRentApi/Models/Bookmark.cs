using System.Text.Json.Serialization;

namespace SSRentApi.Models
{
    public class Bookmark
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public int UserId { get; set; }
        public int VehicleId { get; set; }
        [JsonIgnore]
        public Vehicle Vehicle { get; set; }
    }
}