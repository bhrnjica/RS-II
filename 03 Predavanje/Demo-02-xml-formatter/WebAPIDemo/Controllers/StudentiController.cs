using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentiController : ControllerBase
    {
        // GET: api/Studenti
        [HttpGet]
        [Produces("application/xml")]
        public IEnumerable<string> Get()
        {
            return new string[] { "Student1", "Student2" };
        }

        // GET: api/Studenti/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return $"Student {id}";
        }

        // POST: api/Studenti
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Studenti/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
