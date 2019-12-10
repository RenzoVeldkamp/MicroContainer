using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApenContainer.Apen
{
    public interface IAsyncApenProvider
    {
        Task<HashSet<AapModel>> Apen { get; }
    }
}