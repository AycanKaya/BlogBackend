using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Services.Contracts;

namespace seffaflikBackend.Controllers
{

    [ApiController]
    [Route("[controller]")]

    public class BulletinController : ControllerBase
    {
       private readonly IEpiasService _service;


        public BulletinController(IEpiasService service)
        {
            _service = service;
        }


      

        [HttpGet("Weekly")]
        public async Task<IActionResult> GetWeeklyBulletin()
        {
            var response = await _service.GetBulletinsWeekly();
            return Ok(response);

        }

        [HttpGet("Monthly")]
        public async Task<IActionResult> GetMonthlyBulletin()
        {
            var response = await _service.GetBulletinsMonthly();
            return Ok(response);

        }

        [HttpGet("Yearly")]
        public async Task<IActionResult> GetYearlyBulletin()
        {
            var response = await _service.GetBulletinsYearly();
            return Ok(response);
        }

        [HttpGet("{Date}")]
        public async Task<IActionResult> GetBulletinNaturalgasDaily(string date)
        {

            var response = await _service.GetBulletinNaturalgasDaily(date);
            return Ok(response);
        }





    }
}
