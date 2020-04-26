using System;

namespace WebApplication.Controllers.Dto
{
    public sealed class VoteDto
    {
        public Guid SessionId { get; set; }
        public string Username { get; set; }
    }
}