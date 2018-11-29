using System.Collections.Generic;

namespace ApenContainer.Apen
{
    public interface IApenProvider
    {
        HashSet<AapModel> Apen { get; }
    }
}