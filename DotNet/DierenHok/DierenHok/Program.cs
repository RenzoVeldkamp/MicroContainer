using EasyNetQ;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DierenHok.Communication;
using DierenHok.Data;
using Newtonsoft.Json;
using System.Linq;

namespace DierenHok
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting DierenHok Service...");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddInMemoryCollection();

            IConfiguration configuration = builder.Build();

            string rabbitConnectionString = configuration.GetValue<string>("Konijn");
            var bus = RabbitHutch.CreateBus(rabbitConnectionString);

            DierenProvider.VolumeName = configuration.GetValue<string>("VolumeName");

            SubscribeToBus(bus);

            Seed();
        }

        private static void SubscribeToBus(IBus bus)
        {
            // 'RPC style' handler: request - response
            bus.RespondAsync<GetDierenRequest, GetDierenResponse>(HandleGetDierenRequestAsync);

            // Subscribe to event (without topic)
            bus.SubscribeAsync<DierCreated>("DierCreatedSubscription", HandleDierCreatedAsync);

            // Set up receive queue for handling a command
            bus.Receive<CreateDier>("CreateDierQueue", HandleCreateDierAsync);
        }

        /* Alternate options: with lots of configuration */
        /* private static void SubscribeToBus(IBus bus)
        {
            // 'RPC style' handler: request - response
            bus.RespondAsync<GetDierenRequest, GetDierenResponse>(HandleGetDierenRequestAsync, (config) => {
                config.WithPrefetchCount(5);
                config.WithQueueName("RPCqueue");
            });

            // Subscribe to event (with or without topic)
            bus.SubscribeAsync<DierCreated>("supskripsjun", HandleDierCreatedAsync, (config) => {
                config.WithAutoDelete(true);
                config.AsExclusive();
                config.WithQueueName("kjoeneem");
                config.WithPrefetchCount(10);
                config.WithTopic("toppique");
            });

            // Set up receive queue for handling a command
            bus.Receive<CreateDier>("CreateDierQueue", HandleCreateDierAsync, (config) => {
                config.AsExclusive();
                config.WithPrefetchCount(15);
                config.WithPriority(5);
            });
        } */

        private static Task<GetDierenResponse> HandleGetDierenRequestAsync(GetDierenRequest request)
        {
            GetDierenResponse response = new GetDierenResponse
            {
                CorrelationId = request.RequestId,
                Success = request.RequestId != Guid.Empty
            };

            if (response.Success)
            {
                foreach (var dier in DierenProvider.GetDieren())
                    response.Dieren.Add(dier);
            }

            return Task.FromResult(response);
        }

        private static Task HandleDierCreatedAsync(DierCreated dierCreated)
        {
            Console.WriteLine($"Dier {dierCreated.CreatedDier.Naam} was created: {JsonConvert.SerializeObject(dierCreated)}");
            // some business logic
            return Task.CompletedTask;
        }

        private static Task<bool> HandleCreateDierAsync(CreateDier command)
        {
            bool successful = false;

            if (command.DierToCreate != null)
            {
                Console.WriteLine($"Creating animal {command.DierToCreate.Naam}: {JsonConvert.SerializeObject(command.DierToCreate)}");
                successful = DierenProvider.AddDier(command.DierToCreate);
            }

            return Task.FromResult(successful);
        }

        private static void Seed()
        {
            Console.WriteLine("Starting Seed...");
            IList<Dier> dieren = DierenProvider.GetDieren().ToList();

            if (!dieren.Any())
            {
                Console.WriteLine("Writing Seed data...");

                var dier0 = DierenProvider.GetDier(0);
                if (dier0 == null) DierenProvider.AddDier(new Dier { Id = 0, Naam = "Bokito", Soort = DierenSoort.Primaat });

                var dier1 = DierenProvider.GetDier(1);
                if (dier1 == null) DierenProvider.AddDier(new Dier { Id = 1, Naam = "King Kong", Soort = DierenSoort.Ongedefinieerd });

                var dier2 = DierenProvider.GetDier(2);
                if (dier2 == null) DierenProvider.AddDier(new Dier { Id = 2, Naam = "Johannes", Soort = DierenSoort.Sim });
            }
            else
            {
                Console.WriteLine("Seed data was already written...");
            }
        }
    }
}
