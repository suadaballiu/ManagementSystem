using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSys.Models
{
    public class Department
    {
        [DisplayName("Department Id")]
        public int DepartmentID { get; set; }
        [DisplayName("Department Name")]
        public string DepartmentName { get; set; }
        public string Description { get; set; }
    }
}
