﻿using System;
using System.Threading.Tasks;
using ApplicationServices;
using Microsoft.AspNetCore.Mvc;
using Ports;
using WebApplication.Controllers.Dto;
using static System.Guid;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class SessionController : ControllerBase
    {
        private readonly IStore _store;

        public SessionController(IStore store)
        {
            _store = store;
        }
        
        [HttpPost]
        [Route(nameof(Create))]
        public async Task<IActionResult> Create()
        {
            var newSessionId = NewGuid();
            await new CreateSessionHandler(_store).Execute(new CreateSession(newSessionId));
            return Ok(new {SessionId = newSessionId});
        }

        [HttpPost]
        [Route(nameof(Vote))]
        public async Task<IActionResult> Vote([FromBody] VoteDto voteDto)
        {
            await new VoteHandler(_store).Execute(voteDto.ToVote());
            return Ok(new { });
        }
        
        [HttpPost]
        [Route(nameof(Clear))]
        public async Task<IActionResult> Clear(Guid sessionId)
        {
            await new ClearSessionHandler(_store).Execute(new ClearSession(sessionId));
            return Ok(new { });
        }
    }
}