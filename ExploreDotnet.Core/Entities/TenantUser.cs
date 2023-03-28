using ExploreDotnet.Core.Entities.Base;

namespace ExploreDotnet.Core.Entities
{
    public class TenantUser : Entity
    {
        public long TenantId { get; private set; }
        public Tenant Tenant { get; private set; }
        public long UserId { get; private set; }
        public User User { get; private set; }
        public string Role { get; private set; }
        
        public TenantUser(long tenantId, long userId, string role)
        {
            TenantId = tenantId;
            UserId = userId;
            Role = role;
        }
        
        public void UpdateRole(string role)
        {
            Role = role;
        }
    }
}