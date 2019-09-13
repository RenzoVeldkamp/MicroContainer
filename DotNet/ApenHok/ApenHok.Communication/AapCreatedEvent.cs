using System;
using System.Collections.Generic;
using System.Text;

namespace ApenHok.Communication
{
    public class AapCreated: BaseMessage
    {
        public Aap CreatedAap { get; set; } 
    }
}
