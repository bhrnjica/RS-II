using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPISvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SecureController : ControllerBase
    {
        /// <summary>
        /// Testiranje generiranog Tokena. Ukoliko je token koji je generiran od strane Identity servera ispravan, ova metoda će se pozvati.
        /// Pristup ovoj metodi ima samo autentificirani user koji imaju isravno generiran token
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}