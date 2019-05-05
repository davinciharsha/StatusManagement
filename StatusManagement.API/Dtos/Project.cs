using System;

namespace StatusManagement.API.Dtos
{
    public class Project  
    {
        public Guid ProjectId { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Venue { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
 