using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TennisScheduler.Models
{
    public class Reservation 
    {
        public int Id { get; set; }
        public string FirstName   { get; set; }
        public string LastName { get; set; }
        public string ContactInfo { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime TimeOut { get; set; }

        public byte NumberOfPlayers { get; set; }
        public decimal Price { get; set; }
        public virtual Court CourtId { get; set; }


    }
}