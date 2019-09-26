using Dierentuin.DierenProvider;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Dierentuin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DierenController : ControllerBase
    {
        private readonly IDierenProvider DierenProvider;

        public DierenController(IDierenProvider dierenProvider)
        {
            DierenProvider = dierenProvider;
        }

        // GET: api/Dieren
        [HttpGet]
        public IEnumerable<DierModel> Get()
        {
            var dieren = DierenProvider.Dieren;
            return dieren;
        }

        // GET: api/Dieren/5
        [HttpGet("{id}", Name = "Get")]
        public DierModel Get(int id)
        {
            return DierenProvider.Dieren.FirstOrDefault(a => a.Id == id);
        }

        // POST: api/Dieren
        public ActionResult Post([FromBody] DierModel value)
        {
            HashSet<DierModel> dieren = DierenProvider.Dieren;
            value.Id = dieren.Count;
            dieren.Add(value);

            return Created("https://localhost:44389", value);
        }

        // PUT: api/Dieren/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] DierModel value)
        {
            HashSet<DierModel> dieren = DierenProvider.Dieren;
            var dierToUpdate = dieren.FirstOrDefault(a => a.Id == id);
            dierToUpdate.Naam = value.Naam;
            dierToUpdate.Soort = value.Soort;

            return NoContent();
        }

        // DELETE: api/Dieren/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            HashSet<DierModel> dieren = DierenProvider.Dieren;
            var dierToDelete = dieren.FirstOrDefault(a => a.Id == id);
            dieren.Remove(dierToDelete);

            return Ok(dierToDelete);
        }
    }
}
