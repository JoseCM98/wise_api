using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using wise_api.Context;
using wise_api.Dto;
using wise_api.Entities;

namespace wise_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LineaController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly HttpClient _httpClient;

        public LineaController(DataContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;

        }
 
        [HttpGet]
        public IActionResult GetLinea()
        {


            List<Linea> lineas = _context.Linea.ToList();
            return Ok(lineas);

        }

        [HttpPost]
        public IActionResult PostLinea([FromBody] LineaDto model)
        {
            Linea linea = new Linea();

            linea.CodigoLinea = (_context.Linea.Count()+3).ToString();
            linea.IdEmpresa = model.IdEmpresa;
            linea.NombreLinea = Guid.NewGuid().ToString();
            linea.AbreviadoLinea = model.AbreviadoLinea;
            linea.PresupuestoLinea = model.PresupuestoLinea;
            linea.ActivaLinea = model.ActivaLinea;
            linea.VentaLinea = model.VentaLinea;
            linea.IdEAN13 = model.IdEAN13;
            linea.UsuariosLinea = model.UsuariosLinea;
            
            _context.Linea.Add(linea);
            _context.SaveChanges();

            return Ok(linea);

        }
        [HttpPut]
        public IActionResult PutLinea( [FromBody] Linea model)
        {

            var oldlinea = _context.Linea.Find(model.CodigoLinea);
            if (oldlinea == null)
            {
                return BadRequest("No existe el cliente");
            }
            oldlinea.IdEmpresa = model.IdEmpresa;
            oldlinea.NombreLinea = model.NombreLinea;
            oldlinea.AbreviadoLinea = model.AbreviadoLinea;
            oldlinea.PresupuestoLinea = model.PresupuestoLinea;
            oldlinea.ActivaLinea = model.ActivaLinea;
            oldlinea.VentaLinea = model.VentaLinea;
            oldlinea.IdEAN13 = model.IdEAN13;
            oldlinea.UsuariosLinea = model.UsuariosLinea;

            _context.SaveChanges();
            
            return Ok(oldlinea);

        }

        //[HttpDelete("{id}")]
        //public IActionResult DeleteClientes(string id)
        //{
        //    var client = clientes.Find(x => x.id == id);
        //    if (client == null)
        //    {
        //        return BadRequest("No existe el cliente");
        //    }
        //    clientes.Remove(client);
        //    return Ok("Se ha eliminado el cliente");

        //}


        [HttpDelete("{id}")]
        public IActionResult DeleteLinea(string id)
        {

            return Ok("Se ha eliminado el el nombre");

        }
    }
}
