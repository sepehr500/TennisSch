using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TennisScheduler.Models
{
    public class Court
    {
        public int Id { get; set; }
        public byte Number { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}