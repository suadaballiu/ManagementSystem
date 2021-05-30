using ManagementSys.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSys.Controllers
{
    public class DepartmentController : Controller
    {
        // GET: DepartmentController
        public IActionResult Index()
        {
            DataTable dtblDepartment = new DataTable();
            using (SqlConnection sqlCon = Helpers.GetConnection())
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM Department", sqlCon);
                sqlDa.Fill(dtblDepartment);
            }
            List<Department> dep = new List<Department>();
            foreach (DataRow dr in dtblDepartment.Rows)
            {
                dep.Add(new Department
                {
                    DepartmentID = Convert.ToInt32(dr["depId"]),
                    DepartmentName = Convert.ToString(dr["depName"]),
                    Description = Convert.ToString(dr["description"])
                   
                });
            }
            TempData.Keep("Role");
            return View(dep);
        }

        // GET: DepartmentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DepartmentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DepartmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: DepartmentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DepartmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: DepartmentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DepartmentController/Delete/5
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
