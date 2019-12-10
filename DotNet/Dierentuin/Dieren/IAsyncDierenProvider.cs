using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dierentuin.Dieren
{
    public interface IAsyncDierenProvider
    {
        Task<HashSet<DierModel>> Dieren { get; }
    }
}