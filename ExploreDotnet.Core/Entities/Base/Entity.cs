using System;

namespace ExploreDotnet.Core.Entities.Base
{
    public abstract class Entity
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}