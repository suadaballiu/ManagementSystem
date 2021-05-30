using ManagementSys.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSys.Controllers
{
    public class EmployeeController : Controller
    {
        private object temp;


        // GET: EmployeeController
        [HttpGet]
      
        public ActionResult Index()
        {
            DataTable dtblEmployee= new DataTable();
            using (SqlConnection sqlCon = Helpers.GetConnection())
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM Employee", sqlCon);
                sqlDa.Fill(dtblEmployee);
            }
            List<Employee> emp = new List<Employee>();
            foreach(DataRow dr in dtblEmployee.Rows)
            {
                emp.Add(new Employee
                {
                    EmployeeID = Convert.ToString(dr["EmployeeId"]),
                    FirstName = Convert.ToString(dr["FirstName"]),
                    LastName = Convert.ToString(dr["LastName"]),
                    Birthday = Convert.ToDateTime(dr["Birthday"]),
                    Active = Convert.ToInt16(dr["Active"]),
                    Position = Convert.ToString(dr["Position"]),
                    CV = Convert.ToString(dr["CV"]),
                    IMG = Convert.ToString(dr["IMG"])


                });
            }
            TempData.Keep("Role");
            return View(emp);
        }
        [HttpGet]

        [HttpPost, ActionName("Search")]
        [ValidateAntiForgeryToken]
        public ActionResult Search(string SearchString)
        {
            string search = Request.Form["text"].ToString();
            DataTable dtblEmployee = new DataTable();
            using (SqlConnection sqlCon = Helpers.GetConnection())
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM Employee WHERE firstname LIKE '%" + SearchString + "%' OR employeeId LIKE '%" + SearchString + "%' OR position LIKE '%" + SearchString + "%'", sqlCon);
                //sqlDa.UpdateCommand.Parameters.Add(("@search", SqlDbType.Char, 50, '%' + search + '%'));
                sqlDa.Fill(dtblEmployee);
            }
            List<Employee> emp = new List<Employee>();
            foreach (DataRow dr in dtblEmployee.Rows)
            {
                emp.Add(new Employee
                {
                    EmployeeID = Convert.ToString(dr["EmployeeId"]),
                    FirstName = Convert.ToString(dr["FirstName"]),
                    LastName = Convert.ToString(dr["LastName"]),
                    Birthday = Convert.ToDateTime(dr["Birthday"]),
                    Active = Convert.ToInt16(dr["Active"]),
                    Position = Convert.ToString(dr["Position"]),
                    CV = Convert.ToString(dr["CV"]),
                    IMG = Convert.ToString(dr["IMG"])


                });
            }
            TempData.Keep("Role");
            return View(emp);
        }
        [HttpGet]
        [Route("Employee/IndexP/{pos?}")]
        public ActionResult IndexP(int pos)
        {
            DataTable dtblEmployee = new DataTable();
            using (SqlConnection sqlCon = Helpers.GetConnection())
            {
                SqlDataAdapter sqlDa = new SqlDataAdapter();
                sqlCon.Open();
                if (pos == 1)
                {
                    sqlDa = new SqlDataAdapter("SELECT * FROM Employee", sqlCon);
                }
                else
                {
                    sqlDa = new SqlDataAdapter("SELECT * FROM Employee E inner join Position P on E.employeeId=P.employeeid where depId=@depId and active=@active", sqlCon);
                    sqlDa.SelectCommand.Parameters.AddWithValue("@depId", pos);
                    sqlDa.SelectCommand.Parameters.AddWithValue("@active", 1);
                }
                
                sqlDa.Fill(dtblEmployee);
            }
            List<Employee> emp = new List<Employee>();
            foreach (DataRow dr in dtblEmployee.Rows)
            {
                emp.Add(new Employee
                {
                    EmployeeID = Convert.ToString(dr["EmployeeId"]),
                    FirstName = Convert.ToString(dr["FirstName"]),
                    LastName = Convert.ToString(dr["LastName"]),
                    Birthday = Convert.ToDateTime(dr["Birthday"]),
                    Active = Convert.ToInt16(dr["Active"]),
                    Position= Convert.ToString(dr["Position"]),
                    CV= Convert.ToString(dr["CV"]),
                    IMG=Convert.ToString(dr["IMG"])


                }) ;
            }
            TempData.Keep("Role");
            return View(emp);
        }


        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            return View(new Employee());
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee emp)
        {
            using (SqlConnection sqlCon = Helpers.GetConnection())
            {
                //string FileName = Path.GetFileNameWithoutExtension(emp.IMG);
               // var fileName = Path.GetFileName(file.FileName);
                temp =TempData["Role"];
                TempData.Keep("Role");
                //SqlCommand com1 = new SqlCommand();
                sqlCon.Open();
                string query = "INSERT INTO Employee(employeeId,firstname,lastname,birthday,active,position,img,cv) VALUES(@EmployeeId,@FirstName,@LastName,@Birthday,@Active,@Position,@IMG,@CV)";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);

                string query1 = "INSERT INTO Position(id,depId,employeeid) VALUES(@id,@depId,@employeeid)";
                SqlCommand sqlCmd1 = new SqlCommand(query1, sqlCon);
                sqlCmd.Parameters.AddWithValue("@EmployeeId", emp.EmployeeID);
                sqlCmd.Parameters.AddWithValue("@FirstName", emp.FirstName);
                sqlCmd.Parameters.AddWithValue("@LastName", emp.LastName);
                sqlCmd.Parameters.AddWithValue("@Birthday", emp.Birthday);
                sqlCmd.Parameters.AddWithValue("@Position", emp.Position);
                sqlCmd.Parameters.AddWithValue("@Active", emp.Active);
                sqlCmd.Parameters.AddWithValue("@IMG", emp.IMG.ToString());
                sqlCmd.Parameters.AddWithValue("@CV", emp.CV.ToString());
                Random r = new Random();
                sqlCmd1.Parameters.AddWithValue("@id", r.Next());
                sqlCmd1.Parameters.AddWithValue("@depId",temp);
                TempData.Keep("Role");
                sqlCmd1.Parameters.AddWithValue("@employeeid", emp.EmployeeID);
                sqlCmd.ExecuteNonQuery();
                sqlCmd1.ExecuteNonQuery();

            }
            TempData.Keep("Role");
            return Redirect("~/Employee/IndexP/"+ temp);

        }

        // GET: EmployeeController/Edit/5
        
        public IActionResult Edit(string id)
        {
           
            Employee emp = new Employee();
            DataTable dtblEmployee = new DataTable();
            temp = TempData["Role"];
            using (SqlConnection sqlCon = Helpers.GetConnection())
            {
                
                sqlCon.Open();
                string query = "SELECT * FROM Employee Where employeeId = @employeeId";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@employeeId", id);
                sqlDa.Fill(dtblEmployee);
            }
            if (dtblEmployee.Rows.Count == 1)
            {
                emp.EmployeeID = dtblEmployee.Rows[0][0].ToString();
                emp.FirstName = dtblEmployee.Rows[0][1].ToString();
                emp.LastName = dtblEmployee.Rows[0][2].ToString();
                emp.Birthday = Convert.ToDateTime(dtblEmployee.Rows[0][3].ToString());
                emp.Active = Convert.ToInt16(dtblEmployee.Rows[0][4]);
                emp.Position = dtblEmployee.Rows[0][5].ToString();
                emp.CV = dtblEmployee.Rows[0][6].ToString();
                emp.IMG = dtblEmployee.Rows[0][7].ToString();
                TempData.Keep("Role");
                return View(emp);
            }

            else
            {
                TempData.Keep("Role");
                return Redirect("~/Employee/IndexP/" + temp);
            }
           
        }


        // POST: EmployeeController/Edit/5
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public IActionResult Edit(Employee emp)
        {
            using (SqlConnection sqlCon = Helpers.GetConnection())
            {
                temp = TempData["Role"];
                sqlCon.Open();
                string query = "UPDATE Employee SET firstName = @FirstName , lastName= @LastName , birthday = @Birthday,active=@active,position=@position WHere employeeId = @EmployeeId";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@EmployeeId", emp.EmployeeID);
                sqlCmd.Parameters.AddWithValue("@FirstName", emp.FirstName);
                sqlCmd.Parameters.AddWithValue("@LastName", emp.LastName);
                sqlCmd.Parameters.AddWithValue("@Birthday", emp.Birthday);
                sqlCmd.Parameters.AddWithValue("@Position", emp.Position);
                sqlCmd.Parameters.AddWithValue("@Active", emp.Active);
                //sqlCmd.Parameters.AddWithValue("@IMG", emp.IMG.ToString());
                //sqlCmd.Parameters.AddWithValue("@CV", emp.CV.ToString());
                sqlCmd.ExecuteNonQuery();
            }
            TempData.Keep("Role");
            return Redirect("~/Employee/IndexP/" + temp);
        }

        // GET: EmployeeController/Delete/5
        public IActionResult Delete(string id)
        {
            using (SqlConnection sqlCon = Helpers.GetConnection())
            {
                sqlCon.Open();
                temp = TempData["Role"];
                
                string query1 = "DELETE FROM Position Where employeeid = @EmployeeId";
                
                SqlCommand sqlCmd1 = new SqlCommand(query1, sqlCon);
               
                sqlCmd1.Parameters.AddWithValue("@EmployeeId", id);
               
                sqlCmd1.ExecuteNonQuery();
                string query = "DELETE FROM Employee Where employeeId = @EmployeeId";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@EmployeeId", id);
                sqlCmd.ExecuteNonQuery();
            }
            TempData.Keep("Role");
            return Redirect("~/Employee/IndexP/" + temp);

        }

        [HttpPost]
        
        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
