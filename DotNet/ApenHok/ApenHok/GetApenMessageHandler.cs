using System;
using System.Threading.Tasks;

namespace ApenHok
{
    public class GetApenMessageHandler : Rebus.Handlers.IHandleMessages<BusMessage>
    {
        public Task Handle(BusMessage message)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"****** Received bus message with CorrelationId {message.CorrelationId} and message {message.ErrorMessage} ******");
            });
        }
    }
}
