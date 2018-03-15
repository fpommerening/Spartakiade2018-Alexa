using System;

namespace Function.Models
{
    public class Appointment
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public string Type { get; set; }

        public DateTime CreateOn { get; set; }

        public string[] Participant { get; set; }
    }
}
