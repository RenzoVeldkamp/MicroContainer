using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ApenHok.Communication
{
    public class GetApenResponse : BaseMessage
    {
        public GetApenResponse()
        {
            Apen = new Collection<Aap>();
        }

        public ICollection<Aap> Apen { get; private set; }
    }
}