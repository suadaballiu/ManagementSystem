using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSys.Models
{
    public class Employee
    {
        [DisplayName("Employee Id")]
        public string EmployeeID { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        [Range(0,1)]
        public int Active { get; set; }
        [DataType(DataType.Text)]
        public string Position { get; set; }
        public string CV { get; set; }
        public string IMG { get; set; }

    }
}
