using ThumbnailGrabber.Models;

namespace ThumbnailGrabber.Services
{
    public interface IUserService
    {
        Task<UserModel> Authenticate(string username, string password);
        
    }

    public class UserService : IUserService
    {
       
        private List<UserModel> _users = new List<UserModel>
    {
        new UserModel { Username = "yardillo", Password = "P@ssw0rd" }
    };

        public async Task<UserModel> Authenticate(string username, string password)
        {
            
            var user = await Task.Run(() => _users.SingleOrDefault(x => x.Username == username && x.Password == password));
           
            return user;
        }       
    }
}
