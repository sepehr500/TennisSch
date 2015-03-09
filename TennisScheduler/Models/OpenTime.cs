using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TennisScheduler.Models
{
    public class OpenTime
    {

        public int Id { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        [DataType(DataType.Time)]
        public DateTime TimeOpen { get; set; }
        [DataType(DataType.Time)]
        public DateTime CloseTime { get; set; }
    }
}