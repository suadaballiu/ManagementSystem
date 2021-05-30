using ManagementSys.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


namespace ManagementSys.Controllers
{
    public class LoginController : Controller
    {


        public IActionResult Privacy()
        {
            return View();
        }

        SqlConnection con = Helpers.GetConnection();
        SqlCommand com = new SqlCommand();
        SqlCommand com1 = new SqlCommand();
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            SignOut();
            TempData["Role"] = 0;
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult Authorize(ManagementSys.Models.User user)
        {

            con.Open();
            com.Connection = con;
            com1.Connection = con;
            com1.CommandText = "select role from [User] where username='" + user.username + "'and password='" + user.password + "'";
            com.CommandText = "select username from [User] where username='" + user.username + "'and password='" + user.password + "'";
            string userName = (string)com.ExecuteScalar();
            string role = (string)com1.ExecuteScalar();
            
            if (!String.IsNullOrEmpty(userName)&& role == "superadmin")
            {

                TempData["Role"] = "1";
                TempData.Keep("Role");
                return Redirect("/Employee/Index");
            }
            else if (!String.IsNullOrEmpty(userName) && role == "hradmin")
            {
                TempData["Role"] = "2";
                TempData.Keep("Role");

                return Redirect("/Employee/IndexP/2");
            }
            else if (!String.IsNullOrEmpty(userName) && role == "financeadmin")
            {
                TempData["Role"] = "3";
                TempData.Keep("Role");
                return Redirect("~/Employee/IndexP/3");
            }
            else if (!String.IsNullOrEmpty(userName) && role == "itadmin")
            {
                TempData["Role"] = "4";
                TempData.Keep("Role");
                return Redirect("~/Employee/IndexP/4");
            }
            else
            {
                TempData["Message"] = "Login failed.User name or password doesn't exist.";

                con.Close();
                TempData.Keep("Role");
                return Redirect("~/Home/Index");
            }

        }
    }
}


