using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TennisScheduler.Models
{
    public class OpenTime
    {

        public int Id { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public DateTime TimeOpen { get; set; }
        public DateTime CloseTime { get; set; }
    }
}