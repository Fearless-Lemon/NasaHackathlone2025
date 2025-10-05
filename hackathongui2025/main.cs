using System;

namespace Hackathlone2025
{
    internal class main
    {
        // 7 instances (values mirror what you provided; PVDF/Polyethylene filled as NonBio with nominal syngas)
        public Organic_Plastic PVDF = new Organic_Plastic
        {
            thermoprops = "Thermoplastic",
            plasticprops = "Nonbio",
            syngas_val = 0.25 // nominal example so pyrolysis path shows output
        };

        public Organic_Plastic Nomex = new Organic_Plastic(1, 0, 0, 0, 0, 0, 0.5, 0.5, "Thermosetplastic", "Nonbio");

        public Organic_Plastic Polyethylene = new Organic_Plastic
        {
            thermoprops = "Thermoplastic",
            plasticprops = "Nonbio",
            syngas_val = 0.30 // nominal example
        };

        public Organic_Plastic Nitrile = new Organic_Plastic(1, 0, 0, 0, 0, 0, 0, 0.18, "Thermosetplastic", "Nonbio");

        public static Organic_Plastic PLA = new Organic_Plastic(1, 0.52, 0.16, 0.36, 0.68, 0.20, 0.48, 0.00, "Thermoplastic", "Bio");
        public Organic_Plastic PHA = new Organic_Plastic(1, 1.12, 0.40, 0.73, 0.30, 0.20, 0.10, 0.00, "Thermoplastic", "Bio");
        public Organic_Plastic PBS = new Organic_Plastic(1, 0.37, 0.13, 0.24, 0.83, 0.20, 0.63, 0.00, "thermoplastic", "Bio");

        public static bool running = true;

        private void DepositAndProcess()
        {
            Console.WriteLine("\nSelect a material to deposit:");
            Console.WriteLine("1. PLA (Bio, Thermoplastic)");
            Console.WriteLine("2. PHA (Bio, Thermoplastic)");
            Console.WriteLine("3. PBS (Bio, Thermoplastic)");
            Console.WriteLine("4. PVDF (NonBio, Thermoplastic)");
            Console.WriteLine("5. Nomex (NonBio, Thermosetplastic)");
            Console.WriteLine("6. Polyethylene (NonBio, Thermoplastic)");
            Console.WriteLine("7. Nitrile (NonBio, Thermosetplastic)");
            Console.Write("Choice: ");

            string choiceStr = Console.ReadLine();
            int choice = Help.Integer_Handler(choiceStr);

            Organic_Plastic selected = null;

            switch (choice)
            {
                case 1:
                    selected = PLA;
                    break;
                case 2:
                    selected = PHA;
                    break;
                case 3:
                    selected = PBS;
                    break;
                case 4:
                    selected = PVDF;
                    break;
                case 5:
                    selected = Nomex;
                    break;
                case 6:
                    selected = Polyethylene;
                    break;
                case 7:
                    selected = Nitrile;
                    break;
                default:
                    Console.WriteLine("Invalid selection.");
                    break;
            }

            if (selected == null)
            {
                return;
            }

            selected.MaterialAdder("");
            Console.WriteLine();
            Console.WriteLine("Processing {0:0.###} kg of selected material...", selected.MaterialAmount);
            selected.Process();
        }

        private void ShowMenu()
        {
            Console.WriteLine("\n--- Waste Recycling System --->");
            Console.WriteLine("1. Deposit waste material");
            Console.WriteLine("2. View list of acceptable materials");
            Console.WriteLine("3. Exit");
            Console.Write("Enter the number for your option: ");
        }

        static void Main(string[] args)
        {
            main app = new main();
            string soption;

            while (running)
            {
                app.ShowMenu();
                soption = Console.ReadLine();
                int option = Help.Integer_Handler(soption);

                switch (option)
                {
                    case 1:
                        app.DepositAndProcess();
                        break;

                    case 2:
                        RecyclingFunctions.ViewAcceptedMaterials();
                        break;

                    case 3:
                        running = false;
                        Console.WriteLine("Exiting...");
                        break;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }
    }
}
