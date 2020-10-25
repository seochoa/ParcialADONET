using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entidad;
using Logica;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Parcial.models;

namespace Parcial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApoyoController : ControllerBase
    {
        private readonly ApoyoService apoyoService;
        public IConfiguration Configuration { get; }

        public ApoyoController(IConfiguration configuration)
        {
            Configuration = configuration;
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            apoyoService = new ApoyoService(connectionString);
        }

        [HttpGet]
        public IEnumerable<ApoyoViewModel> Gets()
        {
            var personas = apoyoService.ConsultarTodos().Select(p=> new ApoyoViewModel(p));
            return personas;
        }
        
        [HttpPost]
        public ActionResult<ApoyoViewModel> Post(ApoyoInputModel apoyoInput)
        {
            Apoyo apoyo = MapearApoyo(apoyoInput);
            var response = apoyoService.Guardar(apoyo);
            if (response.Error) 
            {
                return BadRequest(response.Mensaje);
            }
            return Ok(response.Apoyo);
        }

        private Apoyo MapearApoyo(ApoyoInputModel apoyoInput)
        {
            var apoyo = new Apoyo
            {
                Persona = apoyoInput.Persona,
                Tipoapoyo = apoyoInput.Tipoapoyo,
                Vrapoyo = apoyoInput.Vrapoyo,
            };
            return apoyo;
        }
    }
}