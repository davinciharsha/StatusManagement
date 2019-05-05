using System;

namespace StatusManagement.API.Dtos
{
    public class Status 
    {
        public Guid StatusId { get; set; }
        public string Marshall { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
    }
}
