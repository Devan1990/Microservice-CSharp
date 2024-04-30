using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CompetencyFramework.API.Controllers
{
    
    [ApiController]
    [Route("api/v1/[controller]")]
    
    public class UserIdentityController : ControllerBase
    {
      
        private readonly IConfiguration _configuration;
        public UserIdentityController(IConfiguration configuration)
        {
           
             _configuration = configuration;
        }

        [HttpPost("GetToken")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> GetToken()
        {
            var result = "";
            return Ok(result);
        }

        
    }
}
