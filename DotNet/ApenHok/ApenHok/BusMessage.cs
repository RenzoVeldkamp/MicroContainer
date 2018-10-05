using System;

namespace ApenHok
{
    public class BusMessage
    {
        public Guid CorrelationId { get; set; }

        public bool Success { get; set; }

        public string ErrorMessage { get; set; }
    }
}