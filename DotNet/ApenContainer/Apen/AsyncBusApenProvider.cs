using ApenHok.Communication;
using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApenContainer.Apen
{
    public class AsyncBusApenProvider : IAsyncApenProvider
    {
        private readonly IBus bus;

        public AsyncBusApenProvider(IBus bus)
        {
            this.bus = bus;

            if (!this.bus.IsConnected) throw new InvalidOperationException("Bus not connected!!!");
        }

        public Task<HashSet<AapModel>> Apen => GetApen();

        private async Task<HashSet<AapModel>> GetApen()
        {
            HashSet<AapModel> apen = new HashSet<AapModel>();
            GetApenResponse response = new GetApenResponse();

            try
            {
                response = await bus.RequestAsync<GetApenRequest, GetApenResponse>(new GetApenRequest { RequestId = Guid.NewGuid() });

                if (response.Success)
                {
                    apen = response.Apen.Select(aap => ConvertToAapModel(aap)).ToHashSet();

                    //bus.Publish()
                }
            }
            catch (Exception ex)
            {
                // uh oh.....
                Console.WriteLine($"****** Exception occurred: {ex.GetType().FullName}");
                Console.WriteLine($"****** Exception message: {ex.Message}");
            }

            return apen;
        }

        private AapModel ConvertToAapModel(Aap aap)
        {
            return new AapModel
            {
                Id = aap.Id,
                Naam = aap.AapNaam,
                Soort = Enum.GetName(typeof(ApenSoort), aap.Soort)
            };
        }
    }
}
