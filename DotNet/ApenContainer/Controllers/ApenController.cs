using ApenContainer.Apen;
using EasyNetQ;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using AHC = ApenHok.Communication;

namespace ApenContainer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApenController : ControllerBase
    {
        private readonly IBus bus;

        public ApenController(IBus bus)
        {
            this.bus = bus;

            if (!this.bus.IsConnected) throw new InvalidOperationException("Bus not connected!!!");
        }

        // GET: api/Apen
        [HttpGet]
        public IEnumerable<Aap> Get()
        {
            //return ApenProvider.Apen;

            /**/
            AHC.GetApenResponse response = GetApen();

            if (response.Success)
                return response.Apen.Select(aap => ConvertToAapModel(aap));

            return Enumerable.Empty<Aap>();
            /**/
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

        private AHC.GetApenResponse GetApen()
        {
            AHC.GetApenResponse response = new AHC.GetApenResponse();

            try
            {
                response = bus.Request<AHC.GetApenRequest, AHC.GetApenResponse>(new AHC.GetApenRequest { RequestId = Guid.NewGuid() });
            }
            catch (Exception ex)
            {
                // uh oh.....
                Console.WriteLine($"Exception occurred: {ex.GetType().FullName}");
                Console.WriteLine($"Exception message: {ex.Message}");
            }

            return response;
        }

        private Aap ConvertToAapModel(AHC.Aap aap)
        {
            return new Aap
            {
                Id = aap.Id,
                Naam = aap.AapNaam,
                Soort = Enum.GetName(typeof(AHC.ApenSoort), aap.Soort)
            };
        }
    }
}
