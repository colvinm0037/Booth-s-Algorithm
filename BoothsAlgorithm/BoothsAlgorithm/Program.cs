using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoothsAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                // Read in input and format to binary strings of length 8
                Console.WriteLine("Enter 1st number (Multiplicand): ");
                int mcand = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter 2nd number (Multiplier): ");
                int multi = Convert.ToInt32(Console.ReadLine());

                var multiplicand = Convert.ToString(mcand, 2);
                var multiplier = Convert.ToString(multi, 2);

                multiplicand = format(multiplicand, 8);
                multiplier = format(multiplier, 8);

                Console.WriteLine("Multiplicand: " + multiplicand);
                Console.WriteLine("Multiplier:   " + multiplier + "\n");

                // Format product with leading 0's and extra 0 at end
                var product = "00000000" + multiplier + "0";

                // Initialize iteration
                Console.WriteLine("Iteration          Step          Multicand        Product");
                Console.WriteLine("    0            Initialize       " + multiplicand + "       " + product);

                var opCode = "";
                String step = "";

                // Remaining Iteration
                for (int iteration = 1; iteration < 9; iteration++)
                {
                    opCode = product.Substring(product.Length - 2);
                    int upperProduct = Convert.ToInt32(product.Substring(0, 8), 2);
                    step = "No Op        ";

                    if (opCode.Equals("10"))
                    {
                        // Prod -= Mcand
                        upperProduct -= Convert.ToInt32(multiplicand, 2);
                        product = format(Convert.ToString(upperProduct, 2), 8) + product.Substring(product.Length - 9);
                        step = "Prod -= MCand";
                    }
                    else if (opCode.Equals("01"))
                    {
                        // prod += Mcand
                        upperProduct += Convert.ToInt32(multiplicand, 2);
                        product = format(Convert.ToString(upperProduct, 2), 8) + product.Substring(product.Length - 9);
                        step = "Prod += MCand";
                    }

                    Console.WriteLine("    " + iteration + "            " + step + "    " + multiplicand + "       " + product);

                    // Right Shift
                    product = ASR_1(product);

                    Console.WriteLine("    " + iteration + "            " + "ASR 1" + "            " + multiplicand + "       " + product);
                }

                Console.WriteLine("\nFinal Answer: " + product.Substring(0, 16));


                if (product.Substring(0, 1).Equals("0"))
                {
                    Console.WriteLine("Final Answer: " + Convert.ToInt32(product.Substring(0, 16), 2));
                }
                else if (!product.Substring(0, 16).Contains("0"))
                {
                    Console.WriteLine("Final Answer: -1");
                }
                else
                {
                    String result = product.Substring(product.IndexOf("0"), 16 - product.IndexOf("0"));
                    int value = Convert.ToInt32(result, 2) - (int)Math.Pow(2, result.Length);
                    Console.WriteLine("Final Answer: " + value);
                }

                Console.WriteLine("Try again? [y/n]");
                String input = Console.ReadLine();
                if (input.ToUpper().Equals("N"))
                {
                    break;
                }
            }
        }

        // Formats a binary string to exactly 'length' characters by padding with 0's
        static String format(String str, int length)
        {
            String newString = str;
            if (str.Length < length)
            {
                int padding = length - str.Length;
                for (int i = 0; i < padding; i++)
                {
                    newString = "0" + newString;
                }
                return newString;
            }
            else
            {
                return str.Substring(str.Length - length);
            }
        }

        static String ASR_1(String str)
        {
            String result = str.Substring(0, str.Length - 1);
            if (result.Substring(0, 1).Equals("0"))
            {
                result = "0" + result;
            }
            else
            {
                result = "1" + result;
            }

            return result;
        }
    }
}
