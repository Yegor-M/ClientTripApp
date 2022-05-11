using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreIntro2.Dto
{
    public class GetTripResponse
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int MaxPeople { get; set; }
        public ICollection<CountryDto> Countries { get; set; }
        public ICollection<ClientDto> Clients { get; set; }
    }

    public class CountryDto
    {
        public string Name { get; set; }
    }

    public class ClientDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
