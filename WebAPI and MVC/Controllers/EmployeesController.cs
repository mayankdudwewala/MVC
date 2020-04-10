using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI_and_MVC.Models;

namespace WebAPI_and_MVC.Controllers
{
    public class EmployeesController : ApiController
    {
        EmployeesModelEntities emp = new EmployeesModelEntities();

        public IHttpActionResult getEmpList()
        {
            var empList = emp.Employees.ToList();
            return Ok(empList);
        }
        [HttpPost]
        public IHttpActionResult createEmp(Employee employee)
        {
            emp.Employees.Add(employee);
            emp.SaveChanges();
            return Ok();
        }
        [HttpGet]
        public IHttpActionResult getEmp(int id)
        {
            EmpListViewModel elm = new EmpListViewModel();
            elm = emp.Employees.Where(x => x.EmployeeID == id).Select(x => new EmpListViewModel()
            {
                EmployeeID = x.EmployeeID,
                Name = x.Name,
                Position = x.Position,
                Age = x.Age,
                Salary = x.Salary,
            }).FirstOrDefault<EmpListViewModel>();
            if (elm == null)
            {
                return NotFound();
            }

            return Ok(elm);
        }

        [HttpPut]
        public IHttpActionResult putEmp(Employee putemp)
        {
            var updateData = emp.Employees.Where(x => x.EmployeeID == putemp.EmployeeID).FirstOrDefault<Employee>();
            if (updateData != null)
            {
                updateData.EmployeeID = putemp.EmployeeID;
                updateData.Name = putemp.Name;
                updateData.Position = putemp.Position;
                updateData.Age = putemp.Age;
                updateData.Salary = putemp.Salary;
                emp.SaveChanges();
            }
            else
            {
                return NotFound();
            }

            return Ok();
        }
        [HttpDelete]
        public IHttpActionResult empDelete(int id)
        {
            var deleteData = emp.Employees.Where(x => x.EmployeeID == id).FirstOrDefault();
            emp.Entry(deleteData).State = System.Data.Entity.EntityState.Deleted;
            emp.SaveChanges();
            return Ok();
        }
    }
}
