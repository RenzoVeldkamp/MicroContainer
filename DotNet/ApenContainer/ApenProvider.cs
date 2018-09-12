using System.Collections.Generic;

namespace ApenContainer
{
    public static class ApenProvider
    {
        static ApenProvider()
        {
            Apen = new HashSet<Aap> {
                new Aap {
                    Id=0,
                    Naam = "Bokito",
                    Soort= ApenSoort.Gorilla.ToString()
                },
                new Aap {
                    Id = 1,
                    Naam = "Johannes",
                    Soort= ApenSoort.Sim.ToString()
                }
            };
        }

        public static HashSet<Aap> Apen { get; private set; }
    }
}
