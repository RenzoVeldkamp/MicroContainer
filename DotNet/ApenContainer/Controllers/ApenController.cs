using ApenContainer.Apen;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApenContainer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApenController : ControllerBase
    {
        private readonly IApenProvider ApenProvider;

        public ApenController(IApenProvider apenProvider)
        {
            ApenProvider = apenProvider;
        }

        // GET: api/Apen
        [HttpGet]
        public IEnumerable<AapModel> Get()
        {
            var apen = ApenProvider.Apen;
            return apen;
        }

        // GET: api/Apen/5
        [HttpGet("{id}", Name = "Get")]
        public AapModel Get(int id)
        {
            return ApenProvider.Apen.FirstOrDefault(a => a.Id == id);
        }

        // POST: api/Apen
        public ActionResult Post([FromBody] AapModel value)
        {
            HashSet<AapModel> apen = ApenProvider.Apen;
            value.Id = apen.Count;
            apen.Add(value);

            return Created("https://localhost:44389", value);
        }

        // PUT: api/Apen/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] AapModel value)
        {
            HashSet<AapModel> apen = ApenProvider.Apen;
            var aapToUpdate = apen.FirstOrDefault(a => a.Id == id);
            aapToUpdate.Naam = value.Naam;
            aapToUpdate.Soort = value.Soort;

            return NoContent();
        }

        // DELETE: api/Apen/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            HashSet<AapModel> apen = ApenProvider.Apen;
            var aapToDelete = apen.FirstOrDefault(a => a.Id == id);
            apen.Remove(aapToDelete);

            return Ok(aapToDelete);
        }

        /* Async variant*/
        /*
        private readonly IAsyncApenProvider ApenProvider;

        public ApenController(IAsyncApenProvider apenProvider)
        {
            ApenProvider = apenProvider;
        }

        // GET: api/Apen
        [HttpGet]
        public async Task<IEnumerable<AapModel>> Get()
        {
            var apen = await ApenProvider.Apen;
            return apen;
        }

        // GET: api/Apen/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<AapModel> Get(int id)
        {
            return (await ApenProvider.Apen).FirstOrDefault(a => a.Id == id);
        }

        // POST: api/Apen
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AapModel value)
        {
            HashSet<AapModel> apen = await ApenProvider.Apen;
            value.Id = apen.Count;
            apen.Add(value);

            return Created("https://localhost:44389", value);
        }

        // PUT: api/Apen/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AapModel value)
        {
            HashSet<AapModel> apen = await ApenProvider.Apen;
            var aapToUpdate = apen.FirstOrDefault(a => a.Id == id);
            aapToUpdate.Naam = value.Naam;
            aapToUpdate.Soort = value.Soort;

            return NoContent();
        }

        // DELETE: api/Apen/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            HashSet<AapModel> apen = await ApenProvider.Apen;
            var aapToDelete = apen.FirstOrDefault(a => a.Id == id);
            apen.Remove(aapToDelete);

            return Ok(aapToDelete);
        }
        */
    }
}
