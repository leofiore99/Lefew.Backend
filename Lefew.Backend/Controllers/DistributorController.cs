using System.Collections.Generic;
using System.Threading.Tasks;
using Lefew.Application.InputModels;
using Lefew.Application.Interfaces;
using Lefew.Domain.Models;
using Lefew.RestApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Lefew.Backend.Controllers
{
    [Route("api/[controller]")]
    public class DistributorController : BaseApiController
    {
        private IDistributorAppService _appService { get; }

        public DistributorController(IDistributorAppService appService)
        {
            _appService = appService;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Distributor input)
        {
            var c = await _appService.Add(input);
            return Created("Get", c.Id, c);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        // POST api/values
        [HttpPost("Process")]
        public async Task<IActionResult> Process([FromBody]ProcessInputModel input)
        {
            var c = await _appService.Process(input);
            return Created("Get", c.Id, c);
        }
    }
}
