using System;

namespace Service.Data
{
    public class Participant
    {
        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
