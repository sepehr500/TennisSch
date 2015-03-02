using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TennisScheduler.Models
{
    public class ClosedTime
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        //Date1 - Date2 means Closed from Date1 to Date2
        public DateTime Date1 { get; set; }
        public DateTime Date2 { get; set; }

    }
}