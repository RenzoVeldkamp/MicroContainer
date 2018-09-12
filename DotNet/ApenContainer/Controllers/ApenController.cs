using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ApenContainer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApenController : ControllerBase
    {
        // GET: api/Apen
        [HttpGet]
        public IEnumerable<Aap> Get()
        {
            return ApenProvider.Apen;
        }

        // GET: api/Apen/5
        [HttpGet("{id}", Name = "Get")]
        public Aap Get(int id)
        {
            return ApenProvider.Apen.FirstOrDefault(a => a.Id == id);
        }

        // POST: api/Apen
        [HttpPost]
        public IActionResult Post([FromBody] Aap value)
        {
            value.Id = ApenProvider.Apen.Count;
            ApenProvider.Apen.Add(value);

            return Created("https://localhost:44389", value);
        }

        // PUT: api/Apen/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Aap value)
        {
            var aapToUpdate = ApenProvider.Apen.FirstOrDefault(a => a.Id == id);
            aapToUpdate.Naam = value.Naam;
            aapToUpdate.Soort = value.Soort;
            return NoContent();
        }

        // DELETE: api/Apen/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aapToDelete = ApenProvider.Apen.FirstOrDefault(a => a.Id == id);
            ApenProvider.Apen.Remove(aapToDelete);
            return Ok(aapToDelete);
        }
    }
}
