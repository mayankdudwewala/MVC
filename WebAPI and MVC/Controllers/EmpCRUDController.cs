using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebAPI_and_MVC.Models;

namespace WebAPI_and_MVC.Controllers
{
    public class EmpCRUDController : Controller
    {
        // GET: EmpCRUD
        public ActionResult Index()
        {
            IList<Employee> empObject = null;
            HttpClient HC = new HttpClient();
            HC.BaseAddress = new Uri("https://localhost:44358/api/Employees");
            var consumeAPI = HC.GetAsync("Employees");
            consumeAPI.Wait();
            var readData = consumeAPI.Result;
            if (readData.IsSuccessStatusCode)
            {
                var displayData = readData.Content.ReadAsAsync<IList<Employee>>();
                displayData.Wait();
                empObject = displayData.Result;
            }

            return View(empObject);
        }

        public ActionResult Create()
        {
            return View("");
        }

        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            HttpClient HC = new HttpClient();
            HC.BaseAddress = new Uri("https://localhost:44358/api/Employees");
            var insertEmployee = HC.PostAsJsonAsync("Employees", employee);
            insertEmployee.Wait();

            var saveData = insertEmployee.Result;
            if (saveData.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View("Create");

        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            Employee elm = null;
            HttpClient HC = new HttpClient();
            HC.BaseAddress = new Uri("https://localhost:44358/api/");
            var consumeapi = HC.GetAsync("Employees?id=" + id.ToString());
            consumeapi.Wait();
            var readData = consumeapi.Result;
            if (readData.IsSuccessStatusCode)
            {
                var displayData = readData.Content.ReadAsAsync<Employee>();
                displayData.Wait();
                elm = displayData.Result;
            }

            return View(elm);
        }

        [HttpPut]
        public ActionResult Edit(int id)
        {
            Employee elm = null;
            HttpClient HC = new HttpClient();
            HC.BaseAddress = new Uri("https://localhost:44358/api/");
            var consumeapi = HC.GetAsync("Employees?id=" + id.ToString());
            consumeapi.Wait();
            var readData = consumeapi.Result;
            if (readData.IsSuccessStatusCode)
            {
                var displayData = readData.Content.ReadAsAsync<Employee>();
                displayData.Wait();
                elm = displayData.Result;
            }

            return View(elm);
        }
        
        public ActionResult Edit(Employee ec)
        {
            HttpClient HC = new HttpClient();
            HC.BaseAddress = new Uri("https://localhost:44358/api/Employees");
            var insertEmployee = HC.PostAsJsonAsync("Employees", ec);
            insertEmployee.Wait();

            var saveData = insertEmployee.Result;
            if (saveData.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.message = "Employee record not updated";
            }

            return View();
        }
       
        public ActionResult Delete(int id)
        {
            HttpClient HC=new HttpClient();
            HC.BaseAddress=new Uri("https://localhost:44358/api/Employees");
            var DeleteData=HC.DeleteAsync("Employees/"+id.ToString());
            DeleteData.Wait();
            var readData = DeleteData.Result;
            if (readData.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View("Index");
        }
    }
}