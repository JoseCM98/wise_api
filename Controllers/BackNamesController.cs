using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using wise_api.Context;
using wise_api.Dto;
using wise_api.Entities;

namespace wise_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BackNamesController : ControllerBase
    {
        private readonly DataContext _context;


        public BackNamesController(DataContext context)
        {
            _context = context;

        }

        [HttpGet]
        public IActionResult GetBackNames()
        {


            List<BackNames> names = _context.BackNames.ToList();

            return Ok(names);

        }

        [HttpPost]
        public IActionResult PostBackNames(BackNamesDto model)
        {
            BackNames back = new BackNames();
            back.Id= Guid.NewGuid().ToString();
            back.Name= model.Name;
  
            
            _context.BackNames.Add(back);
            _context.SaveChanges();
            return Ok("Se ha agragado a : "+back.Name);

        }
        [HttpPost("Server")]
        public IActionResult PostBackNamesInServer(List<BackNamesStateDto> namesState)
        {
            List<BackNames> names = _context.BackNames.ToList();
            List<BackNamesHistory> historyList = new List<BackNamesHistory>();
            string fecha = DateTime.Now.ToString();
            for(int i = 0;i<namesState.Count();i++)
            {
                foreach(var name in names)
                {
                    if(name.Name == namesState[i].Name)
                    {
                        BackNamesHistory history = new BackNamesHistory();
                        history.Id = Guid.NewGuid().ToString();
                        history.Name = name.Name;
                        history.State = 1;
                        history.Hour = fecha;
                        historyList.Add(history);
                    }
                }
            }
            _context.BackNamesHistories.AddRange(historyList);
            _context.SaveChanges();
            return Ok(historyList);

        }
        [HttpPut("{id}")]
        public IActionResult PutBackNames(string id, [FromBody] BackNamesDto model)
        {

            BackNames oldback = _context.BackNames.Find(id);
           if (oldback == null)
            {
                return BadRequest("No existe el nombre");
            }

            oldback.Name = model.Name;

            _context.SaveChanges();
            return Ok("Se ha cambiado el nombre a : " + oldback.Name);
            

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBakcNames(string id)
        {

            BackNames oldback = _context.BackNames.Find(id);
            if (oldback == null)
            {
                return BadRequest("No existe el nombre");
            }

            _context.BackNames.Remove(oldback);
            _context.SaveChanges();
            return Ok("Se ha eliminado a : " + oldback.Name);


        }
    }
}
