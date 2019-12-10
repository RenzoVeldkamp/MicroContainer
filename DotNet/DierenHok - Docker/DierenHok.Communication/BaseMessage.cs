﻿using System;

namespace DierenHok.Communication
{
    public class BaseMessage
    {
        public Guid CorrelationId { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}