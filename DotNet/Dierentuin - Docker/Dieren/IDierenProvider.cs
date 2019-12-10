using System.Collections.Generic;

namespace Dierentuin.Dieren
{
    public interface IDierenProvider
    {
        HashSet<DierModel> Dieren { get; }
    }
}