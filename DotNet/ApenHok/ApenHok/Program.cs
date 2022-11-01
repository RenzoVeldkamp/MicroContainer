using ApenHok.Communication;
using ApenHok.Data;
using EasyNetQ;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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

            ApenProvider.VolumeName = configuration.GetValue<string>("VolumeName");
            //SubscribeBus(bus);

            SubscribeToBus(bus);

            Seed();
        }

        private static void SubscribeBus(IBus bus) => bus.RespondAsync<GetApenRequest, GetApenResponse>(HandleGetApenRequestAsync);

        private static void SubscribeToBus(IBus bus)
        {
            // 'RPC style' handler: request - response
            bus.Rpc.RespondAsync<GetApenRequest, GetApenResponse>(HandleGetApenRequestAsync);

            // Subscribe to event (without topic)
            bus.PubSub.SubscribeAsync<AapCreated>("DierCreatedSubscription", HandleAapCreatedAsync);

            // Set up receive queue for handling a command
            bus.SendReceive.Receive<CreateAap>("CreateDierQueue", HandleCreateAapAsync);
        }

        /* Alternate options: with lots of configuration */
        /* private static void SubscribeToBus(IBus bus)
        {
            // 'RPC style' handler: request - response
            bus.Rpc.RespondAsync<GetApenRequest, GetApenResponse>(HandleGetApenRequestAsync, (config) => {
                config.WithPrefetchCount(5);
                config.WithQueueName("RPCqueue");
            });

            // Subscribe to event (with or without topic)
            bus.PubSub.SubscribeAsync<AapCreated>("supskripsjun", HandleAapCreatedAsync, (config) => {
                config.WithAutoDelete(true);
                config.AsExclusive();
                config.WithQueueName("kjoeneem");
                config.WithPrefetchCount(10);
                config.WithTopic("toppique");
            });

            // Set up receive queue for handling a command
            bus.SendReceive.Receive<CreateAap>("CreateAapQueue", HandleCreateAapAsync, (config) => {
                config.AsExclusive();
                config.WithPrefetchCount(15);
                config.WithPriority(5);
            });
        } */

        /*private static Task<GetApenResponse> HandleGetApenRequestAsync(GetApenRequest request)
        {
            GetApenResponse response = new GetApenResponse { CorrelationId = request.RequestId };

            if (request.RequestId != Guid.Empty)
            {
                foreach (var aap in ApenProvider.GetApen()) response.Apen.Add(aap);
                response.Success = true;
            }
            else
            {
                response.ErrorMessage = "Received request without correlation identifier";
            }

            return Task.FromResult(response);
        }*/

        private static Task<GetApenResponse> HandleGetApenRequestAsync(GetApenRequest request)
        {
            GetApenResponse response = new GetApenResponse
            {
                CorrelationId = request.RequestId,
                Success = request.RequestId != Guid.Empty
            };

            if (response.Success)
            {
                foreach (var aap in ApenProvider.GetApen())
                    response.Apen.Add(aap);
            }

            return Task.FromResult(response);
        }

        private static Task HandleAapCreatedAsync(AapCreated aapCreated)
        {
            Console.WriteLine($"Aap {aapCreated.CreatedAap.AapNaam} was created: {JsonConvert.SerializeObject(aapCreated)}");
            // some business logic
            return Task.CompletedTask;
        }

        private static Task<bool> HandleCreateAapAsync(CreateAap command)
        {
            bool successful = false;

            if (command.AapToCreate != null)
            {
                Console.WriteLine($"Creating animal {command.AapToCreate.AapNaam}: {JsonConvert.SerializeObject(command.AapToCreate)}");
                successful = ApenProvider.AddAap(command.AapToCreate);
            }

            return Task.FromResult(successful);
        }

        /*
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
                response.ErrorMessage = "Received request without correlation identifier";
            }

            return response;
        }
        */

        private static void Seed()
        {
            Console.WriteLine("Starting Seed...");
            IList<Aap> apen = ApenProvider.GetApen().ToList();

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
            else
            {
                Console.WriteLine("Seed data was already written...");
            }
        }
    }
}
