using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DierenHok.Communication
{
    public class GetDierenResponse : BaseMessage
    {
        public GetDierenResponse()
        {
            Dieren = new Collection<Dier>();
        }

        public ICollection<Dier> Dieren { get; private set; }
    }
}