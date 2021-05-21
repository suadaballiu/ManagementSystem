using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSys.Models
{
    public class User
    {
        public int userId { get; set; }
        [Required(ErrorMessage = "Please enter username.")]
        public string username { get; set; }
        [Required(ErrorMessage = "Please enter password")]
        [DataType(DataType.Password)]
        public string password { get; set; }
        public string role { get; set; }
            }
}
