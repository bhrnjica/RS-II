using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_EFCore.Models;
using WebAPI_EFCore.Repositories;

namespace WebAPI_EFCore.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IDataRepository<Employee> m_dataRepository;

        public EmployeeController(IDataRepository<Employee> dataRepository)
        {
            m_dataRepository = dataRepository;
        }

        // GET: api/Employee
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Employee> employees = m_dataRepository.GetAll();
            return Ok(employees);
        }

        // GET: api/Employee/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(long id)
        {
            Employee employee = m_dataRepository.Get(id);

            if (employee == null)
            {
                return NotFound("The Employee record couldn't be found.");
            }

            return Ok(employee);
        }

        // POST: api/Employee
        [HttpPost]
        public IActionResult Post([FromBody] Employee employee)
        {
            if (employee == null)
            {
                return BadRequest("Employee is null.");
            }

            m_dataRepository.Add(employee);
            return CreatedAtRoute(
                  "Get",
                  new { Id = employee.EmployeeId },
                  employee);
        }

        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] Employee employee)
        {
            if (employee == null)
            {
                return BadRequest("Employee is null.");
            }

            Employee employeeToUpdate = m_dataRepository.Get(id);
            if (employeeToUpdate == null)
            {
                return NotFound("The Employee record couldn't be found.");
            }

            m_dataRepository.Update(employeeToUpdate, employee);
            return NoContent();
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            Employee employee = m_dataRepository.Get(id);
            if (employee == null)
            {
                return NotFound("The Employee record couldn't be found.");
            }

            m_dataRepository.Delete(employee);
            return NoContent();
        }
    }
}

