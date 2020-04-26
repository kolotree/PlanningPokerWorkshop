using System;
using ApplicationServices;

namespace WebApplication.Controllers.Dto
{
    public sealed class VoteDto
    {
        public Guid SessionId { get; set; }
        public string Username { get; set; }
        
        public int Points { get; set; }

        public Vote ToVote() => new Vote(
            SessionId,
            Username,
            Points);
    }
}