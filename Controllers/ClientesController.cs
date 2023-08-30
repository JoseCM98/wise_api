using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using wise_api.Dto;
using wise_api.Entities;

namespace wise_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        
        private static List<Cliente> clientes = new List<Cliente>
        {
            new Cliente { id = "1", name = "John Doe", lastname = "Perez",age=10 },
            new Cliente { id = "2", name = "Jane Smith", lastname = "Lopez",age=11 },
            // Agrega más estudiantes aquí...
        };
       
        
        [HttpGet]
        public IActionResult GetClientes() {


            
            return Ok(clientes);

        }

        [HttpPost]
        public IActionResult PostClientes([FromBody] ClienteDto model)
        {
            Cliente newclient = new Cliente();

            newclient.id = clientes.Count().ToString();
            newclient.name = model.name;
            newclient.lastname=model.lastname;
            newclient.age = model.age;

            clientes.Add(newclient);
            return Ok(newclient);

        }
        [HttpPut("{id}")]
        public IActionResult PutClientes(string id,[FromBody] ClienteDto model)
        {
            var oldclient = clientes.Find(x => x.id == id);
            if(oldclient == null)
            {
                return BadRequest("No existe el cliente");
            }
            oldclient.name = model.name;
            oldclient.lastname = model.lastname;
            oldclient.age = model.age;

            return Ok(oldclient);

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClientes(string id)
        {
            var client = clientes.Find(x => x.id == id);
            if (client == null)
            {
                return BadRequest("No existe el cliente");
            }
            clientes.Remove(client);
            return Ok("Se ha eliminado el cliente");

        }
    }
}
