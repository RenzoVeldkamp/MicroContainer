using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dierentuin.Dieren
{
    public class AsyncDierenProvider : IAsyncDierenProvider
    {
        private readonly IBus bus;

        public AsyncDierenProvider(IBus bus)
        {
            this.bus = bus;

            if (!this.bus.IsConnected) throw new InvalidOperationException("Bus not connected!!!");
        }

        public Task<HashSet<DierModel>> Dieren => GetDieren();

        private async Task<HashSet<DierModel>> GetDieren()
        {
            HashSet<DierModel> dieren = new HashSet<DierModel>();
            GetDierenResponse response = new GetDierenResponse();

            try
            {
                response = await bus.RequestAsync<GetDierenRequest, GetDierenResponse>(new GetDierenRequest { RequestId = Guid.NewGuid() });

                if (response.Success)
                {
                    dieren = response.Dieren.Select(dier => ConvertToDierModel(dier)).ToHashSet();

                    //bus.Publish()
                }
            }
            catch (Exception ex)
            {
                // uh oh.....
                Console.WriteLine($"****** Exception occurred: {ex.GetType().FullName}");
                Console.WriteLine($"****** Exception message: {ex.Message}");
            }

            return dieren;
        }

        private DierModel ConvertToDierModel(Dier dier)
        {
            return new DierModel
            {
                Id = dier.Id,
                Naam = dier.Naam,
                Soort = Enum.GetName(typeof(DierenSoort), dier.Soort)
            };
        }
    }
}
