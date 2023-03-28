using ExploreDotnet.Core.Entities.Base;

namespace ExploreDotnet.Core.Entities
{
    public class User : Entity
    {
        public string Email { get; private set; }
        public string Password { get; private set; }
        
        public User(string email, string password)
        {
            Email = email;
            Password = password;
        }
        
        public void UpdatePassword(string password)
        {
            Password = password;
        }
        
        public void UpdateEmail(string email)
        {
            Email = email;
        }
    }
}