﻿using System.Collections.Generic;
using BasicAuthWebApi.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasicAuthWebApi.Controllers
{
    // All methods in this controller requires authorization, except Get().
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SecureValuesController : ControllerBase
    {
        private string[] values = { "value 1", "value 2", "value 3" };

        // GET: api/SecureValues
        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return values;
        }

        // GET: api/SecureValues/5
        [HttpGet("{id}", Name = "Get")]
        [Authorize(Roles = Role.Admin)]
        public string Get(int id)
        {
            return values[id];
        }

        // POST: api/SecureValues
        [HttpPost]
        
        public string Post([FromBody] string value)
        {
            return "Only logged user can access it!";
        }

        // PUT: api/SecureValues/5
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
