using System;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Controllers.Dto;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class SessionController : ControllerBase
    {
        [HttpPost]
        [Route(nameof(CreateSession))]
        public IActionResult CreateSession()
        {
            return Ok(new {SessionId = Guid.NewGuid()});
        }

        [HttpPost]
        [Route(nameof(Vote))]
        public IActionResult Vote([FromBody] VoteDto voteDto)
        {
            return Ok(new { });
        }
        
        [HttpPost]
        [Route(nameof(Clear))]
        public IActionResult Clear(string sessionId)
        {
            return Ok(new { });
        }
    }
}