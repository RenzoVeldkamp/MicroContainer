using ApenHok.Communication;
using ApenHok.Data;
using EasyNetQ;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ApenHok
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting ApenHok Service...");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddInMemoryCollection();

            IConfiguration configuration = builder.Build();

            string rabbitConnectionString = configuration.GetValue<string>("Konijn");
            var bus = RabbitHutch.CreateBus(rabbitConnectionString);

            SubscribeBus(bus);

            Seed();
        }

        private static void SubscribeBus(IBus bus) => bus.Respond<GetApenRequest, GetApenResponse>(HandleGetApenRequest);

        private static GetApenResponse HandleGetApenRequest(GetApenRequest request)
        {
            GetApenResponse response = new GetApenResponse { CorrelationId = request.RequestId };

            if (request.RequestId != Guid.Empty)
            {
                foreach (var aap in ApenProvider.GetApen()) response.Apen.Add(aap);
                response.Success = true;
            }
            else
            {
                response.ErrorMessage = "Received request withour correlation identifier";
            }

            return response;
        }

        private static void Seed()
        {
            Console.WriteLine("Starting Seed...");
            IList<Aap> apen = ApenHok.Data.ApenProvider.GetApen().ToList();

            if (!apen.Any())
            {
                Console.WriteLine("Writing Seed data...");

                var aap0 = ApenProvider.GetAap(0);
                if (aap0 == null) ApenProvider.AddAap(new Aap { Id = 0, AapNaam = "Bokito", Soort = ApenSoort.Gorilla });

                var aap1 = ApenProvider.GetAap(1);
                if (aap1 == null) ApenProvider.AddAap(new Aap { Id = 1, AapNaam = "King Kong", Soort = ApenSoort.Primaat });

                var aap2 = ApenProvider.GetAap(2);
                if (aap2 == null) ApenProvider.AddAap(new Aap { Id = 2, AapNaam = "Johannes", Soort = ApenSoort.Sim });
            }
        }
    }
}
