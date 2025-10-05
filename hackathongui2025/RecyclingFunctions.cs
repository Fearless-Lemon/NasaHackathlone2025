using System;

namespace Hackathlone2025
{
    public static class RecyclingFunctions
    {
        public static void ViewAcceptedMaterials() // displays the accepted materials, aloong with their thermoproperties and bioproperties
        {// not in use since the windows form can display them all as options 
            Console.WriteLine("\n--- Accepted Materials ---");
            Console.WriteLine("1. PLA            (Bio, Thermoplastic)");
            Console.WriteLine("2. PHA            (Bio, Thermoplastic)");
            Console.WriteLine("3. PBS            (Bio, Thermoplastic)");
            Console.WriteLine("4. PVDF           (NonBio, Thermoplastic)");
            Console.WriteLine("5. Nomex          (NonBio, Thermosetplastic)");
            Console.WriteLine("6. Polyethylene   (NonBio, Thermoplastic)");
            Console.WriteLine("7. Nitrile        (NonBio, Thermosetplastic)\n");
        }
    }
}
