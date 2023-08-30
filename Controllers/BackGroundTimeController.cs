using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using wise_api.Context;
using wise_api.Dto;
using wise_api.Entities;

namespace wise_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BackGroundTimeController : ControllerBase
    {
        private readonly DataContext _context;

        public BackGroundTimeController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetClientes()
        {


            BackGroundTime newback = new BackGroundTime();
            newback.Id = Guid.NewGuid().ToString();
            
            newback.Hora = DateTime.Now.ToString();
            newback.Iteracion = _context.BackGroundTime.Count().ToString();

            _context.BackGroundTime.Add(newback);
            _context.SaveChanges();

            return Ok(newback);
        }

        [HttpPost]
        public IActionResult PostClientes([FromBody] BackGroundTime model)
        {
            BackGroundTime newback = new BackGroundTime();
            newback.Id = Guid.NewGuid().ToString(); 
            newback.Hora = DateTime.Now.ToString();
            newback.Iteracion = _context.BackGroundTime.Count().ToString();

            _context.BackGroundTime.Add(newback);
            _context.SaveChanges();

            return Ok(newback);

        }
        //[HttpPut]
        //public IActionResult PutClientes([FromBody] Linea model)
        //{
        //    var oldlinea = _context.Linea.Find(model.CodigoLinea);
        //    if (oldlinea == null)
        //    {
        //        return BadRequest("No existe el cliente");
        //    }
        //    oldlinea.IdEmpresa = model.IdEmpresa;
        //    oldlinea.NombreLinea = model.NombreLinea;
        //    oldlinea.AbreviadoLinea = model.AbreviadoLinea;
        //    oldlinea.PresupuestoLinea = model.PresupuestoLinea;
        //    oldlinea.ActivaLinea = model.ActivaLinea;
        //    oldlinea.VentaLinea = model.VentaLinea;
        //    oldlinea.IdEAN13 = model.IdEAN13;
        //    oldlinea.UsuariosLinea = model.UsuariosLinea;

        //    _context.SaveChanges();
        //    return Ok(oldlinea);

        //}
    }
}
