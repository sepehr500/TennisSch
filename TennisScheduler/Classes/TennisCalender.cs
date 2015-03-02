using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TennisScheduler.Models;

namespace TennisScheduler.Classes
{
    public class TimeSlot
    {
        public DateTime Time { get; set; }
        public bool Available { get; set; }
    }
    public class TennisCalender
    {
        private List<TimeSlot> Times;
        public List<TimeSlot>  GetMonth(ApplicationDbContext x, int Month)
        {
            DateTime CurrentTime = new DateTime(DateTime.Now.Year , Month , 1 , 0 ,0 , 0);
            while (CurrentTime.Month == Month )
	{
                if (isOpen(CurrentTime) == true && isClosed(CurrentTime) == false && isTaken(CurrentTime) == false)
	{

                    Times.Add(new TimeSlot{Time = CurrentTime , Available = true});
	}
                else
	{

                    Times.Add(new TimeSlot{Time = CurrentTime , Available = false});
	}

                CurrentTime.AddMinutes(30);
	}
            return this.Times;



        }
        //Is it open?
        private bool isOpen(DateTime Time)
        {
            using(ApplicationDbContext db = new ApplicationDbContext())
	{
                var DayList = db.OpenTimes.Where(x => x.DayOfWeek == Time.DayOfWeek).ToList();
                var Day = DayList.First();

                
                if (Time.TimeOfDay >= Day.TimeOpen.TimeOfDay && Time.TimeOfDay <= Day.CloseTime.TimeOfDay)
	            {
		                return true;
	            }
                else
	            {
                    return false;
	            }
	}
        }
        //Is it closed?
        private bool isClosed(DateTime Time)
        {

            using(ApplicationDbContext db = new ApplicationDbContext())
	{
                var DayList = db.ClosedTimes.Where(x => x.Date1.Day == Time.Day).ToList();

                foreach (var item in DayList)
	{

	
                
                if (Time.TimeOfDay >= item.Date1.TimeOfDay && Time.TimeOfDay <= item.Date2.TimeOfDay)
	            {
		                return false;
	            }
                
                }
                return true;
	}

        }
        //Is it taken?
        private bool isTaken(DateTime Time)
        {

            using(ApplicationDbContext db = new ApplicationDbContext())
	{
                var DayList = db.Reservations.Where(x => x.TimeIn.Day == Time.Day).ToList();

                foreach (var item in DayList)
	{

	
                
                if (Time.TimeOfDay >= item.TimeIn.TimeOfDay && Time.TimeOfDay <= item.TimeOut.TimeOfDay)
	            {
		                return true;
	            }
                
                }
                return false;
	}

        }

    }
}