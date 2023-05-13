using System.Text.Json.Serialization;

namespace ThumbnailGrabber.Models
{
    public class UserModel
    {       
        public string Username { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
    }
}
