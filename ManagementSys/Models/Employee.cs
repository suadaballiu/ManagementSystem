using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSys.Models
{
    public class Employee
    {
        public string EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public int Active { get; set; }
        [DataType(DataType.Text)]
        public string Position { get; set; }
        public string CV { get; set; }
        public string IMG { get; set; }

    }
}
