using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TennisScheduler.Models
{
    public enum Repeat
    {
        OneTime,
        Weekly,
        Monthly
    }
    public class ClosedTime
    {

        public int Id { get; set; }
        [Display(Name = "How Often?")]
        public Repeat Repeat{ get; set; }
        [Display(Name = "Event Name")]
        public string EventName { get; set; }
        //Date1 - Date2 means Closed from Date1 to Date2
        [DataType(DataType.DateTime)]
        public DateTime Date1 { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Date2 { get; set; }

        public bool Covered(DateTime x)
        {
            //If one time and the dates match
            if (this.Repeat == Repeat.OneTime && this.Date1.Date == x.Date)
            {
                return true;
            }
            //If weekly and days of weeks match
            if (this.Repeat == Repeat.Weekly && this.Date1.DayOfWeek == x.DayOfWeek)
            {
                return true;
            }
            //If monthly and days of the month match
            if (this.Repeat == Repeat.Monthly && this.Date1.Day == x.Day)
            {
                return true;
            }
            return false;
        }

    }
}