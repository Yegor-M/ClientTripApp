using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreIntro2.Dto
{
    public class PostAssignClientRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public String Telephone { get; set; }
        public String Pesel { get; set; }
        public int IdTrip { get; set; }
        public String TripName { get; set; }
        public String PaymentDate { get; set; }

    }
}