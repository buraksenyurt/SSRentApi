using System.Text.Json.Serialization;

namespace SSRentApi.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public int PortId { get; set; }
        [JsonIgnore]
        public Port Port { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }
        public double Range {get;set;}
        public bool IsTrending { get; set; }
        public int CategoryId { get; set; }
        [JsonIgnore]
        public Category Category { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
       
        public ICollection<Bookmark> Bookmarks { get; set; }
    }
}