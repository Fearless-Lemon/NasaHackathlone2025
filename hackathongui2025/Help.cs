using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
//using System.Text.RegularExpressions;

namespace Hackathlone2025
{ 
 public class Help
{
    public static int Integer_Handler(string input)
    {
        int output;

        while (!Int32.TryParse(input, out output))
        {
            Console.WriteLine("Invalid Input...");
            input = Console.ReadLine();
        }
        return output;
    }


    /*public static string String_Handler(string input)
    {
        var n = new Regex("^[A-Za-z ]+$");

        if (!n.IsMatch(input))
        {
            string error = "Please Enter a Valid Input...";
            return error;
        }
        else
        {
            return input;
        }
    }*/

    public static double Double_Handler(string input)
    {
        double output;

        while (!Double.TryParse(input, out output))
        {
            Console.WriteLine("Please Enter The Correct Value");
            input = Console.ReadLine();
        }
        return output;
    }


    public static int Decimal_Handler(string input)
    {
        if (Decimal.TryParse(input, out decimal output))
        {
            return (int)output;
        }
        else
        {
            return 0;
        }
    }

    /*public static Motorbike.BodyType Enum_Handler(string input)
    {
        Motorbike.BodyType output;

        while (!Enum.TryParse(input, true, out output))
        {
            Console.WriteLine("Please Enter a Valid Body Type(Cruiser, Touring, Sports, Naked, Adventure): ");
            input = Console.ReadLine();
        }
        return output;
    }

    public static Car.BodyType Enum_Handler1(string input)
    {
        Car.BodyType output;

        while (!Enum.TryParse(input, true, out output))
        {
            Console.WriteLine("Please Enter a Valid Body Type(Cruiser, Touring, Sports, Naked, Adventure): ");
            input = Console.ReadLine();
        }

        return output;
    }*/
}
}

