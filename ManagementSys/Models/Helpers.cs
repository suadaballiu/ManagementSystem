using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace ManagementSys.Models
{
    public class Helpers
    {
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(@"Data Source = DESKTOP-OMH0CJJ\SQLEXPRESS; Initial Catalog = Management; Integrated Security = True");
        }
    }
}
