using Rebus.Logging;

namespace ApenHok
{
    class RebusProgram
    {
        const LogLevel MinimumLogLevel = LogLevel.Debug;
        const string ConnectionString = "amqp://admin:admin@localhost:5672/";
        const string InputQueueName = "inpoetkjoe";

        static void RebusMain(string[] args)
        {
            /*
            //Console.WriteLine("Hello World!");

            // we have the container in a variable, but you would probably stash it in a static field somewhere
            using (var publisher = new BuiltinHandlerActivator())
            using (var subscriber = new BuiltinHandlerActivator())
            {
                ConfigureSubscriber(subscriber, InputQueueName);

                var publisherBus = Configure.With(publisher)
                      //.Transport(t => t.UseRabbitMq("amqp://admin:admin@localhost:5672/", "inpoetkjoe")) //("host=localhost;username=admin;password=admin", "inpoetkjoe"))
                      .Logging(l => l.ColoredConsole(MinimumLogLevel))
                      .Transport(t => t.UseRabbitMq(ConnectionString, InputQueueName)
                            .InputQueueOptions(c =>
                            {
                                c.SetAutoDelete(true);
                                c.SetDurable(false);
                            })
                        )
                      .Start();
*/
            /*
            var busMessage = new BusMessage { CorrelationId = Guid.NewGuid(), Success = false, ErrorMessage = "no error yet" };

            Console.WriteLine($"Sending message with correlation id {busMessage.CorrelationId}");

            Task.Run(async () => await publisherBus.Send(busMessage)).Wait();
            */
            /*
            //Console.WriteLine($"Sending message with correlation id {busMessage.CorrelationId}");
            //publisherBus.Send(busMessage).Wait();

            var busMessage2 = new BusMessage { CorrelationId = Guid.NewGuid(), Success = true, ErrorMessage = "no 2 errors yet!!" };
            Console.WriteLine($"Sending message with correlation id {busMessage2.CorrelationId}");
            publisherBus.SendLocal(busMessage2).Wait();

            Console.WriteLine("### Press enter to quit ####");
            Console.ReadLine();
        } //< always dispose bus when your app quits - here done via the container adapter
    */

            /*
            using (var publisher = new BuiltinHandlerActivator())
            using (var subscriber1 = new BuiltinHandlerActivator())
            using (var subscriber2 = new BuiltinHandlerActivator())
            using (var subscriber3 = new BuiltinHandlerActivator())
            {
                ConfigureSubscriber(subscriber1, "endpoint1");
                ConfigureSubscriber(subscriber2, "endpoint2");
                ConfigureSubscriber(subscriber3, "endpoint3");

                subscriber1.Bus.Advanced.Topics.Subscribe("mercedes.#").Wait();
                subscriber2.Bus.Advanced.Topics.Subscribe("mercedes.bmw.#").Wait();
                subscriber3.Bus.Advanced.Topics.Subscribe("mercedes.bmw.vw").Wait();

                var publisherBus = Configure.With(publisher)
                    .Logging(l => l.ColoredConsole(MinimumLogLevel))
                    .Transport(t => t.UseRabbitMqAsOneWayClient(ConnectionString).InputQueueOptions(c =>
                    {
                        c.SetAutoDelete(true);
                        c.SetDurable(false);
                    }))
                    //.Transport(t => t.UseRabbitMq(ConnectionString, "inpoetkjoe").InputQueueOptions(c => c.SetAutoDelete(true)))
                    .Start();

                var topicsApi = publisherBus.Advanced.Topics;

                topicsApi.Publish("mercedes.bmw.vw", "This one should be received by all!").Wait();
                topicsApi.Publish("mercedes.bmw.mazda", "This one should be received by 1 & 2").Wait();
                topicsApi.Publish("mercedes.honda", "This one should be received by 1").Wait();

                Console.WriteLine("Press ENTER to quit");
                Console.ReadLine();
            }
            */
            /* }

             static void ConfigureSubscriber(BuiltinHandlerActivator activator, string inputQueueName)
             {
                 //activator.Handle<BusMessage>(message => new GetApenMessageHandler().Handle(message));

                 //{
                 //    Console.WriteLine("{0} {2} => '{1}'", message.CorrelationId, inputQueueName, message.ErrorMessage);
                 //});

                 Configure.With(activator)
                     .Logging(l => l.ColoredConsole(MinimumLogLevel))
                     .Routing(r => r.TypeBased().Map<BusMessage>(InputQueueName))
                     .Transport(t => t.UseRabbitMq(ConnectionString, inputQueueName)
                                      .InputQueueOptions(c =>
                                      {
                                          c.SetDurable(false);
                                          c.SetAutoDelete(true);
                                      })
                                      )
                     .Start();
             }

             private static GetApenMessageHandler Factory()
             {
                 return new GetApenMessageHandler();
             */
        }
    }
}
