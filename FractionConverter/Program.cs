using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FractionConverter;

namespace FractionConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter the decimal number");
            string input = Console.ReadLine();
            Decimal inputDecimal;
            if (Decimal.TryParse(input, out inputDecimal))
            {
                string fraction = Converter.Convert(inputDecimal);
                Console.WriteLine(fraction);
            }
            else
            {
                Console.WriteLine("The input is not decimal number! ");
            }
            Console.WriteLine("Please enter any key to eixt");
            Console.ReadLine();
        }
    }
}
