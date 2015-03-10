using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TennisScheduler.Models
{
    public class Reservation 
    {
        public enum UserType
        {
            Student,
            Staff,
            Community,
            ClubTeam,
            Lessons,
            PE,
            Tourny,
            Varsity,
            Camp,
            Other

        }
        public int Id { get; set; }
        public string FirstName   { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string ContactInfo { get; set; }
        [DataType(DataType.Time)]
        public DateTime TimeIn { get; set; }
        public UserType TypeUser { get; set; }

        public int CourtId { get; set; }
        
        //Subtract Time out from time in to get playtime
        [DataType(DataType.Time)]
        public DateTime TimeOut { get; set; }
        public byte NumberOfPlayers { get; set; }
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public virtual Court Court { get; set; }
        


    }
}