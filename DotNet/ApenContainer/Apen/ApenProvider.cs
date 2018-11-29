using System.Collections.Generic;

namespace ApenContainer.Apen
{
    public static class ApenProvider
    {
        static ApenProvider()
        {
            Apen = new HashSet<AapModel> {
                new AapModel {
                    Id=0,
                    Naam = "Bokito",
                    Soort= ApenSoort.Gorilla.ToString()
                },
                new AapModel {
                    Id = 1,
                    Naam = "Johannes",
                    Soort= ApenSoort.Sim.ToString()
                }
            };
        }

        public static HashSet<AapModel> Apen { get; private set; }
    }
}
