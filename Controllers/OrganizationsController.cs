using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Services.Contracts;


namespace seffaflikBackend.Controllers  
{

    [ApiController]
    [Route("[controller]")]

    public class OrganizationsController : ControllerBase
    {
       
        private readonly IEpiasService _epiasService;
       

        public OrganizationsController(IEpiasService epiasService)
        {
            _epiasService = epiasService;
           
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrganization()
        { 
            var res= await _epiasService.GetAllOrganizations();
            return Ok(res);

        }
       


     






    }
}
