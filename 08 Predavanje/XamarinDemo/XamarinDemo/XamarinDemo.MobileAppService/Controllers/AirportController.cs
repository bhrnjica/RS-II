using System;
using Microsoft.AspNetCore.Mvc;
using SharedModels;
using XamarinDemo.Models;

namespace XamarinDemo.Controllers
{
    [Route("api/[controller]")]
    public class AirportController : Controller
    {

        private readonly IAirportRepository AirportRepository;

        public AirportController(IAirportRepository airportRepository)
        {
            AirportRepository = airportRepository;
        }

        [HttpGet]
        public IActionResult List()
        {
            return Ok(AirportRepository.GetAll());
        }

        [HttpGet("{id}")]
        public  Airport GetItem(string id)
        {
            var item = AirportRepository.Get(id);
            return item;
            //if (item == null)
            //    return NotFound();
            //return Ok();
        }
    }
}
