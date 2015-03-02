using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TennisScheduler.Models;
/* 
 HOW TO USE THIS CLASS
 1. Make a new instance of the tenniscalander class.
 2. Call the getMonth method and pass in the dbcontext and number of the month you want.
 3. This will populate the Times list which is an atribute of the Tennis Calander.
 4. The Times list now contains a massive collection of 30 min time slots for each court. Each time slot is a TimeSlot which can be seen below.
 5. You can now sort the Times list. Do this using a lambda exspression.
 */
namespace TennisScheduler.Classes
{
    public class TimeSlot
    {
        public short CourtNum { get; set; }
        public DateTime Time { get; set; }
        //True= Available False = Unavailable
        public bool Available { get; set; }
    }
    public class TennisCalender
    {
        public List<TimeSlot> Times;
        public List<TimeSlot>  getMonth(ApplicationDbContext x, int Month)
        {
            DateTime CurrentTime = new DateTime(DateTime.Now.Year , Month , 1 , 0 ,0 , 0);
            foreach (var court in x.Courts)
            {


                while (CurrentTime.Month == Month)
                {
                    if (isOpen(CurrentTime) == true && isClosed(CurrentTime) == false && isTaken(court.Number , CurrentTime) == false)
                    {

                        Times.Add(new TimeSlot { CourtNum = court.Number , Time = CurrentTime, Available = true });
                    }
                    else
                    {

                        Times.Add(new TimeSlot {CourtNum = court.Number, Time = CurrentTime, Available = false });
                    }

                    CurrentTime.AddMinutes(30);
                }
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
        private bool isTaken(int CourtNum , DateTime Time)
        {

            using(ApplicationDbContext db = new ApplicationDbContext())
	{
                var DayList = db.Reservations.Where(x => x.TimeIn.Day == Time.Day && x.Court.Number == CourtNum).ToList();

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