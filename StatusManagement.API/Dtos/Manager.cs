using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatusManagement.API.Dtos
{
    public class Manager
    {
        public Guid ManagerId { get; set; }
        public string Name { get; set; }
    }
}
