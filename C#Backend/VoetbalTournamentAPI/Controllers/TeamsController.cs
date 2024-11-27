using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using VoetbalTournamentAPI.Model;

namespace VoetbalTournamentAPI.Controllers
{
    [ApiController]
    [Route("/teams")]
    public class TeamsController : ControllerBase
    {
        [HttpGet]
        public List<Team> GetTeams()
        {
            return new List<Team>();
        }

        [HttpGet("{id}")]
        public Team GetTeamById(int id)
        {
            return new Team { Id = id };
        }
    }

}
