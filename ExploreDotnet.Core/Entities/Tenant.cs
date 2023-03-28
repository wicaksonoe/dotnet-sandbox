using ExploreDotnet.Core.Entities.Base;

namespace ExploreDotnet.Core.Entities
{
    public class Tenant : Entity
    {
        public string Name { get; private set; }

        public Tenant(string name)
        {
            Name = name;
        }

        public void UpdateName(string name)
        {
            Name = name;
        }
    }
}