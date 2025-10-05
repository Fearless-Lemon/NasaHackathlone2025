using System;
using System.Text;

namespace Hackathlone2025
{
    public class Organic_Plastic // class for all materials
    {
        
        public double biogas_val;     // amount of Biogas created from anerobic digestion
        public double chp_val;        // amount of Combined Heat and Power Created
        public double gtlf_val;       // amount of gas to liquid fuel created 
        public double digestate_val;  // amount of digestate left over
        public double liquid_val;     // amount of liquid recovered 
        public double fibre_val;      // amount of fibre recovered 

        public double syngas_val;     // 

        public string thermoprops;    // value per material is either "Thermoplastic" or "Thermosetplastic"
        public string plasticprops;   // can be Bio or Nonbio

        public double MaterialAmount; // kg of material entered in the form

        public int kindFlag;

        public Organic_Plastic()
        {
            biogas_val = 0.0;
            chp_val = 0.0;
            gtlf_val = 0.0;
            digestate_val = 0.0;
            liquid_val = 0.0;
            fibre_val = 0.0;
            syngas_val = 0.0;
            thermoprops = "Unknown";
            plasticprops = "Nonbio";
            MaterialAmount = 0.0;
            kindFlag = 0;
        }

        // Matches your usage like:
        // new Organic_Plastic(1, 0.52, 0.16, 0.36, 0.68, 0.2, 0.48, 0, "Thermoplastic", "Bio");
        public Organic_Plastic( // constructor for the organic material class
            int kindFlag,
            double biogas_val,
            double chp_val,
            double gtlf_val,
            double digestate_val,
            double liquid_val,
            double fibre_val,
            double syngas_val,
            string thermoprops,
            string plasticprops)
        {
            this.kindFlag = kindFlag;
            this.biogas_val = biogas_val;
            this.chp_val = chp_val;
            this.gtlf_val = gtlf_val;
            this.digestate_val = digestate_val;
            this.liquid_val = liquid_val;   
            this.fibre_val = fibre_val;
            this.syngas_val = syngas_val;
            this.thermoprops = thermoprops;
            this.plasticprops = plasticprops;
            this.MaterialAmount = 0.0;
        }

        public double MaterialAdder(string input) // old function from my console version
        {
          
            Console.WriteLine("<--- Material Quantity --->");
            Console.Write("Enter the amount of waste material (kg): ");
            input = Console.ReadLine();
            double wasteamount = Help.Double_Handler(input);
            this.MaterialAmount = wasteamount;
            return wasteamount;
        }

        public void Process()
        {
            string props = (plasticprops == null) ? "" : plasticprops.Trim();

            if (props.Equals("Bio", StringComparison.OrdinalIgnoreCase))
            {
                ProcessBio_Console();
            }
            else
            {
                ProcessNonBio_Console();
            }
        }

        // ----- Build a report instead of writing to the console ----------------------
        public string ProcessToString()
        {
            string props = (plasticprops == null) ? "" : plasticprops.Trim();
            if (props.Equals("Bio", StringComparison.OrdinalIgnoreCase))
            {
                return BuildBioReport();
            }
            else if(props.Equals("Nonbio", StringComparison.OrdinalIgnoreCase))
            {
                return BuildNonBioReport();
            }
            else 
            {
                return BuildRecycleReport();
            }
        }

        // ----------------- Internal console outputs -----------------
        private void ProcessBio_Console()
        {
            string report = BuildBioReport();
            Console.WriteLine(report);
        }

        private void ProcessNonBio_Console()
        {
            string report = BuildNonBioReport();
            Console.WriteLine(report);
        }

        // ----------------- shared calculators that return strings ------------------

        public string BuildRecycleReport() // function to create a report for recyclable material when rbtn is pressed
        {
            
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("=== Waste Recycling Results ===");
            sb.AppendLine(string.Format("Material:      {0} / {1}", thermoprops, plasticprops));
            sb.AppendLine(string.Format("Input (kg):    {0:0.###}", MaterialAmount));
            sb.AppendLine(string.Format($"Recycling Output (kg): {MaterialAmount}kg Of Recycled Pellets"));

            return sb.ToString();
        }
        private string BuildBioReport() // function to create report in form if the material is bio
        {
          
            const double P_FROM_FIBRE_FACTOR = 0.30;   // 30% of fibre mass to phosphates
            const double K_FROM_FIBRE_FACTOR = 0.25;   // 25% of fibre mass to potash
            const double N_FROM_LIQUID_FACTOR = 0.20;  // 20% of liquid mass to nitrogen

            double biogas = MaterialAmount * biogas_val;            //multiplies the amount entered by the values to output the real total amount
            double chp = MaterialAmount * chp_val;                  //multiplies the amount entered by the values to output the real total amount
            double gtl = MaterialAmount * gtlf_val;                 //multiplies the amount entered by the values to output the real total amount
            double digestate = MaterialAmount * digestate_val;      //multiplies the amount entered by the values to output the real total amount
            double liquid = MaterialAmount * liquid_val;            //multiplies the amount entered by the values to output the real total amount
            double fibre = MaterialAmount * fibre_val;              //multiplies the amount entered by the values to output the real total amount

            double pRecovered = fibre * P_FROM_FIBRE_FACTOR;
            double kRecovered = fibre * K_FROM_FIBRE_FACTOR;
            double nRecovered = liquid * N_FROM_LIQUID_FACTOR;

            StringBuilder sb = new StringBuilder();  // displays all of the data in the report in the format below
            sb.AppendLine("=== Anaerobic Digestion Results (BIO) ===");
            sb.AppendLine(string.Format("Material:      {0} / {1}", thermoprops, plasticprops));
            sb.AppendLine(string.Format("Input (kg):    {0:0.###}", MaterialAmount));
            sb.AppendLine(string.Format("Biogas:        {0:0.###} kg Of Total Biogas Created Through Anerobic Digestion", biogas));
            sb.AppendLine(string.Format("CHP:           {0:0.###}kW Energy Recovered from Biogas", chp));
            sb.AppendLine(string.Format("GTL Fuel:      {0:0.###} kg Of Total Gas to Liquid Fuel Created Through Biogas", gtl));
            sb.AppendLine(string.Format("Digestate:     {0:0.###} kg Of Total Digestate Left After Anerobic Digestion", digestate));
            sb.AppendLine(string.Format("  ├─ Liquid:   {0:0.###} kg Of Total Liquid Recovered Through Seperation Of The Digestate", liquid));
            sb.AppendLine(string.Format("  │   └─ N:    {0:0.###} kg Of Total Nitrogen Recovered From Nutient Recovery Process", nRecovered));
            sb.AppendLine(string.Format("  └─ Fibre:    {0:0.###} kg Of Total Fiber Recovered Through Anerobic Digestion", fibre));
            sb.AppendLine(string.Format("      ├─ P:    {0:0.###} kg Of Phosphate Recovered From Waste", pRecovered));
            sb.AppendLine(string.Format("      └─ K:    {0:0.###} kg Of Potash Recovered From Waste", kRecovered));
            return sb.ToString();
        }

        private string BuildNonBioReport()  // function to create a report for Nonbio material
        {
            
            const double H2_FRACTION_OF_SYNGAS = 0.10; // assume 10% of syngas = hydrogen
            const double CHP_FROM_SYNGAS = 0.60;       // syngas gives 0.6 of combined heat and power
            const double CHP_FROM_H2 = 0.80;           // hydrogen gives 0.8 of combined heat and power

            double syngas = MaterialAmount * syngas_val;          //multiplies the amount entered by the values to output the real total amount
            double hydrogen = syngas * H2_FRACTION_OF_SYNGAS;     //multiplies the amount entered by the values to output the real total amount
            double chpFromSyngas = syngas * CHP_FROM_SYNGAS;      //multiplies the amount entered by the values to output the real total amount
            double chpFromH2 = hydrogen * CHP_FROM_H2;            //multiplies the amount entered by the values to output the real total amount
            double chpTotal = chpFromSyngas + chpFromH2;          //multiplies the amount entered by the values to output the real total amount

            StringBuilder sb = new StringBuilder();     // creates a report in the format specified below
            sb.AppendLine("=== Pyrolysis Results (NON-BIO) ===");
            sb.AppendLine(string.Format("Material:       {0} / {1}", thermoprops, plasticprops));
            sb.AppendLine(string.Format("Input (kg):     {0:0.###}", MaterialAmount));
            sb.AppendLine(string.Format("Syngas:         {0:0.###} kg Of Created Syngas through Pyrolysis", syngas));
            sb.AppendLine(string.Format("Hydrogen (est): {0:0.###} kg Of Hydrogen Extracted From Syngas", hydrogen));
            sb.AppendLine(string.Format("CHP from gas:   {0:0.###} kW Energy Created from Syngas", chpFromSyngas));
            sb.AppendLine(string.Format("CHP from H2:    {0:0.###} kW Energy Created From Hydrogen", chpFromH2));
            sb.AppendLine(string.Format("CHP (total):    {0:0.###} kW Energy Created From All Gases", chpTotal));
            return sb.ToString();
        }
    }
}
