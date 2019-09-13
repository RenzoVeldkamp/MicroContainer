using System;
using System.Collections.Generic;
using System.Text;

namespace ApenHok.Communication
{
    public class CreateAap : BaseMessage
    {
        public Aap AapToCreate { get; set; } 
    }
}
