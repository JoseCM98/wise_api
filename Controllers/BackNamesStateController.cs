using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Linq;
using wise_api.Context;
using wise_api.Dto;
using wise_api.Entities;

namespace wise_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BackNamesStateController : ControllerBase
    {
        private readonly DataContext _context;
      

        public BackNamesStateController(DataContext context)
        {
            _context = context;

        }

        [HttpGet]
        public IActionResult GetBackNamesState()
        {


            List<BackNamesState> names = _context.BackNamesState.ToList();

            return Ok(names);

        }

        [HttpPost]
        public IActionResult PostBackNamesState([FromBody] BackNamesStateDto model)
        {
           
                BackNamesState backState = new BackNamesState();
                backState.Id = Guid.NewGuid().ToString();
                backState.Name = model.Name;
                backState.State = 0;
            _context.BackNamesState.Add(backState);
            _context.SaveChanges();
            return Ok(backState);

        }

        [HttpPost("Update")]
        public IActionResult UpadateBackNamesState([FromBody] List<BackNamesHistory> model)
        {
            List<BackNamesState> namesState = _context.BackNamesState.ToList();
          
            for(int i = 0; i < model.Count(); i++)
            {
                foreach(var item in namesState)
                {
                    if(item.Name == model[i].Name)
                    {
                        item.State = 1;
                        Console.WriteLine(item.Name);
                    }
                }
            }

            _context.SaveChanges();
            return Ok(namesState);
        

        }
        //[HttpPut("{id}")]
        //public IActionResult PutBackNamesState(string id, [FromBody] ClienteDto model)
        //{


        //    return Ok("Editado");

        //}

        //[HttpDelete("{id}")]
        //public IActionResult DeleteBakcNamesStaate(string id)
        //{

        //    return Ok("Se ha eliminado el el nombre");

        //}
    }
}
