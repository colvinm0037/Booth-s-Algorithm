using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoothsAlgorithm
{
    class Algorithm
    {
        /**
         * Use Booth's Algortihm to multiply two signed 8 bit integers together.
         * Shows every step of the process and gives final answer in binary and decimal.
         *
         */
        static void Main(string[] args)
        {
            while (true)
            {
                int mcand, multi;
                                
                // Get input from user and validate input
                Console.WriteLine("Enter 1st number (Multiplicand): ");
                bool resultMcand = Int32.TryParse(Console.ReadLine(), out mcand);

                Console.WriteLine("Enter 2nd number (Multiplier): ");
                bool resultMulti = Int32.TryParse(Console.ReadLine(), out multi);

                if (!resultMcand || !resultMulti || mcand > 127 || multi < -127 || mcand > 127 || mcand < -127)
                {
                    Console.WriteLine("\nPlease enter values within -127 and 127.\n");
                    continue;
                }

                // Convert to binary strings
                string multiplicand = Convert.ToString(mcand, 2);
                string multiplier = Convert.ToString(multi, 2);

                // Format binary strings to length 8
                multiplicand = format(multiplicand, 8);
                multiplier = format(multiplier, 8);

                Console.WriteLine("Multiplicand: " + multiplicand);
                Console.WriteLine("Multiplier:   " + multiplier + "\n");

                // Format product with leading 0's and extra 0 at end
                var product = "00000000" + multiplier + "0";

                // Initialization iteration
                Console.WriteLine("Iteration          Step          Multicand        Product");
                Console.WriteLine("    0            Initialize       " + multiplicand + "       " + product);

                string opCode = "";
                string step = "";

                // Remaining Iterations
                for (int iteration = 1; iteration < 9; iteration++)
                {
                    opCode = product.Substring(product.Length - 2);
                    int upperProduct = Convert.ToInt32(product.Substring(0, 8), 2); // The left half of the product
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

                    // Arithmatic Shift Right
                    product = ASR(product, 1);

                    Console.WriteLine("    " + iteration + "            " + "ASR 1" + "            " + multiplicand + "       " + product);
                }

                // Print the answer in Binary
                product = product.Substring(0, 16); // Remove the extra bit on the end
                Console.WriteLine("\nFinal Answer: " + product);

                // Print the answer in decimal
                if (product.Substring(0, 1).Equals("0"))
                {
                    // If 1st bit is zero then product is positive
                    Console.WriteLine("Final Answer: " + Convert.ToInt32(product, 2));
                }
                else if (!product.Contains("0"))
                {
                    // If product is only 1's then product is -1
                    Console.WriteLine("Final Answer: -1");
                }
                else
                {
                    // Otherwise product is negative and value needs to be calculated by hand
                    string positivePortion = product.Substring(product.IndexOf("0"), 16 - product.IndexOf("0"));
                    int value = Convert.ToInt32(positivePortion, 2) - (int)Math.Pow(2, positivePortion.Length);
                    Console.WriteLine("Final Answer: " + value);
                }

                Console.WriteLine("Try again? [y/n]");
                string input = Console.ReadLine();
                if (input.ToUpper().Equals("N"))
                {
                    break;
                }
            }
        }

        // Formats a binary string to exactly 'length' characters by padding with 0's
        private static string format(String str, int desiredLength)
        {            
            StringBuilder builder = new StringBuilder(str);
            
            if (str.Length < desiredLength)
            {
                int padding = desiredLength - str.Length;
                for (int i = 0; i < padding; i++)
                {
                    builder.Insert(0, "0");
                }
                return builder.ToString();
            }
            else
            {
                return str.Substring(str.Length - desiredLength);
            }
        }

        // Performs an Arithmatic Shift Right on a binary string
        private static string ASR(string str, int shiftAmount)
        {            
            StringBuilder builder = new StringBuilder(str.Substring(0, str.Length - shiftAmount));
            for (int i = 0; i < shiftAmount; i++)
            {
                if (str.Substring(0, 1).Equals("0"))
                {
                    builder.Insert(0, "0");
                }
                else
                {
                    builder.Insert(0, "1");
                }
            }
            return builder.ToString();
        }
    }
}