using ApiReservas.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Linq;

namespace ApiReservas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservasController : ControllerBase
    {
        private IRepository repository;
        public ReservasController(IRepository repo) => repository = repo;

        [HttpGet]
        public IEnumerable<Reserva> Get() => repository.Reservas;

        [HttpGet("{id}")]
        public Reserva Get(int id) => repository[id];

        [HttpPost]
        public IActionResult Post([FromBody] Reserva res)
        {
            return Ok(repository.AddReserva(new Reserva
            {
                Nome = res.Nome,
                InicioLocacao = res.InicioLocacao,
                FimLocacao = res.FimLocacao
            }));
        }

        [HttpPut]
        public IActionResult Put([FromForm] Reserva res)
        {
      
            return Ok(repository.UpdateReserva(res));
        }

        [HttpPatch("{id}")]
        public StatusCodeResult Patch(int id, [FromForm]JsonPatchDocument<Reserva> patch)
        {
            Reserva res = Get(id);
            if (res != null)
            {
                patch.ApplyTo(res);
                return Ok();
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public void Delete(int id) => repository.DeleteReserva(id);

        
    }
}