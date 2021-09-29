using Microsoft.AspNetCore.Mvc;
using PlatformDemo.Models;

namespace PlatformDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Reading all the projects.");
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok($"Reading project #{id}.");
        }

        //[HttpGet]
        //[Route("/api/projects/{pid}/tickets")]
        //public IActionResult GetProjectTicket(int pId, [FromQuery] int tId)
        //{
        //    if (tId == 0)
        //        return Ok($"Reading all the tickets belong to project #{pId}.");

        //    return Ok($"Reading project #{pId}, ticket #{tId}.");
        //}

        [HttpGet]
        [Route("/api/projects/{pid}/tickets")]
        public IActionResult GetProjectTicket([FromQuery]Ticket ticket)
        {
            if (ticket == null)
                return BadRequest("Parameters are not provided properly!");

            if (ticket.TicketId == 0)
                return Ok($"Reading all the tickets belong to project #{ticket.ProjectId}.");

            return Ok($"Reading project #{ticket.ProjectId}, ticket #{ticket.TicketId}, title: {ticket.Title}, description: {ticket.Description}.");
        }


        [HttpPost]
        public IActionResult Post()
        {
            return Ok("Creating project");
        }

        [HttpPut]
        public IActionResult Put()
        {
            return Ok("Updating a project.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok($"Deleting project #{id}.");
        }
    }
}