using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TennisScheduler.Models;
/* 
 HOW TO USE THIS CLASS
 1. Make a new instance of the tenniscalander class.
 2. Call the getMonth method and pass in the number of the month you want.
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
        public string Type { get; set; }
        public int Id { get; set; }
    }
    public class TennisCalender
    {
        public List<TimeSlot> Times;
        public List<TimeSlot>  getMonth(int Month)
        {
            using (ApplicationDbContext x = new ApplicationDbContext())
            {


                this.Times = new List<TimeSlot>();
                DateTime CurrentTime = new DateTime(DateTime.Now.Year, Month, 1, 0, 0, 0);
                foreach (var court in x.Courts)
                {


                    while (CurrentTime.Month == Month)
                    {
                        if (isOpen(CurrentTime) == true && isClosed(CurrentTime) == false && isTaken(court.Number, CurrentTime) == false)
                        {

                            Times.Add(new TimeSlot { CourtNum = court.Number, Time = CurrentTime, Available = true , Type = "Open"});
                        }
                        else
                        {
                            if (isClosed(CurrentTime))
                            {
                            Times.Add(new TimeSlot { CourtNum = court.Number, Time = CurrentTime, Available = false , Type = "Closed"});
                               
                                
                            }
                            else
                            {
                            Times.Add(new TimeSlot { CourtNum = court.Number, Time = CurrentTime, Available = false , Type = "Reserved"});

                            }

                        }

                       CurrentTime = CurrentTime.AddMinutes(30);
                    }
                }
            }
            return this.Times;



        }
        public List<TimeSlot> getDay(int Month, int Day , int Year)
        {
            using (ApplicationDbContext x = new ApplicationDbContext())
            {

                var DayOfWeek = x.OpenTimes.Where(Thing => Thing.DayOfWeek ==   new DateTime(Year, Month, Day, 0, 0, 0).DayOfWeek);
                var OpenTime = DayOfWeek.First();

                this.Times = new List<TimeSlot>();
                foreach (var court in x.Courts)
                {

                DateTime CurrentTime = new DateTime(Year , Month, Day, OpenTime.TimeOpen.Hour, OpenTime.TimeOpen.Minute, 0);
                

                    //While we have not yet reached close time
                    while (CurrentTime.TimeOfDay <= OpenTime.CloseTime.TimeOfDay)
                    {
                        if (isOpen(CurrentTime) == true && isClosed(CurrentTime) == false && isTaken(court.Number, CurrentTime) == false)
                        {

                            Times.Add(new TimeSlot { CourtNum = court.Number, Time = CurrentTime, Available = true , Type = "Open"});
                        }
                        else
                        {
                            if (isClosed(CurrentTime))
                            {
                                ClosedTime which = isClosedWhich(CurrentTime);
                                Times.Add(new TimeSlot { CourtNum = court.Number, Time = CurrentTime, Available = false, Type = "Closed" , Id = which.Id});


                            }
                            else
                            {
                                Reservation which = isTakenWhich(court.Number , CurrentTime);
                                Times.Add(new TimeSlot { CourtNum = court.Number, Time = CurrentTime, Available = false, Type = "Reservation" , Id = which.Id });

                            }
                        }

                        CurrentTime = CurrentTime.AddMinutes(30);
                    }

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
        if (db.ClosedTimes.Count() == 0)
        {
            return false;
        }
                var DayList = new List<ClosedTime>();
                //var DayList = db.ClosedTimes.Where(x => x.Covered(Time) == true).ToList();
                foreach (var item in db.ClosedTimes.ToList())
                {
                    if (item.Covered(Time))
                    {
                        DayList.Add(item);
                    }
                }
                foreach (var item in DayList)
	{

	
                
                if (Time.TimeOfDay >= item.Date1.TimeOfDay && Time.TimeOfDay <= item.Date2.TimeOfDay)
	            {
		                return true;
	            }
                
                }
                return false;
	}

        }
        //Which one is closing it?
        private ClosedTime isClosedWhich(DateTime Time)
        {

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (db.ClosedTimes.Count() == 0)
                {
                    return null;
                }
                var DayList = new List<ClosedTime>();
                //var DayList = db.ClosedTimes.Where(x => x.Covered(Time) == true).ToList();
                foreach (var item in db.ClosedTimes.ToList())
                {
                    if (item.Covered(Time))
                    {
                        DayList.Add(item);
                    }
                }
                foreach (var item in DayList)
                {



                    if (Time.TimeOfDay >= item.Date1.TimeOfDay && Time.TimeOfDay <= item.Date2.TimeOfDay)
                    {
                        return item;
                    }

                }
                return null;
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

	
                
                if (Time.TimeOfDay >= item.TimeIn.TimeOfDay && Time.TimeOfDay < item.TimeOut.TimeOfDay)
	            {
		                return true;
	            }
                
                }
                return false;
	}

        }
        private Reservation isTakenWhich(int CourtNum, DateTime Time)
        {

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var DayList = db.Reservations.Where(x => x.TimeIn.Day == Time.Day && x.Court.Number == CourtNum).ToList();

                foreach (var item in DayList)
                {



                    if (Time.TimeOfDay >= item.TimeIn.TimeOfDay && Time.TimeOfDay < item.TimeOut.TimeOfDay)
                    {
                        return item;
                    }

                }
                return null;
            }

        }
    }
}