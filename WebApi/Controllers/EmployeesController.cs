using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebAPI.DataConnection;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : Controller
    {
        WebApiContext Db = new WebApiContext();

        [HttpGet]
        public IActionResult GetEmployee()
        {
            return Ok(Db.Employees.ToList());
        }



        [HttpGet("id")]
        public async Task<IActionResult> Get(int id)
        {
            var emp = await Db.Employees.FirstOrDefaultAsync(m => m.Id == id);
            if (emp == null)
                return NotFound();
            return Ok(emp);

        }

        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {
            Employee obj = new Employee();

            obj.Name = employee.Name;
            obj.Email = employee.Email;
            obj.Address = employee.Address;
            Db.Employees.Add(obj);
            Db.SaveChanges();

            return Ok(obj);
        }

        [HttpPost("List")]
        public IActionResult AddList(Employee[] employees)
        {
            foreach (Employee item in employees)
            {
                Db.Employees.Add(item);
            }
            Db.SaveChanges();

            return Ok();
        }


        [HttpPut]
        public async Task<IActionResult> Put(Employee employee)
        {
            if (employee == null || employee.Id == 0)
                return BadRequest();

            var emp = await Db.Employees.FindAsync(employee.Id);
            if (emp == null)
                return NotFound();
            emp.Name = employee.Name;
            emp.Email = employee.Email;
            emp.Address = employee.Address;
            await Db.SaveChangesAsync();
            return Ok();
        }


        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            var empid = Db.Employees.Where(model => model.Id == id).FirstOrDefault();

            if (empid == null)
                return NotFound();

            Db.Employees.Remove(empid);
            await Db.SaveChangesAsync();

            return Ok();
        }
    }
}
