using System;

namespace ApenHok.Communication
{
    public class BaseResponse
    {
        public Guid CorrelationId { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}