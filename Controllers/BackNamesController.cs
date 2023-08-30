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
        public IActionResult PostBackNames(List<BackNamesDto> model)
        {
            List<BackNames> names = new List<BackNames>();

            
            _context.BackNames.AddRange(names);
            _context.SaveChanges();
            return Ok(names);

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
        //[HttpPut("{id}")]
        //public IActionResult PutBackNames(string id, [FromBody] ClienteDto model)
        //{
            

        //    return Ok("Editado");

        //}

        //[HttpDelete("{id}")]
        //public IActionResult DeleteBakcNames(string id)
        //{
           
        //    return Ok("Se ha eliminado el el nombre");

        //}
    }
}
