using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadixSort
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] arr = { 29, 72, 98, 13, 87, 66, 52, 51, 36 };
            int[] LSD = new int[arr.Length];

            int insertIndex = -1;
            bool insertFlag = false;
            int toInsertValue = 0;
            int toInsertLSD = 0;

            int baseNum = 1;

            bool sorted = false; // should find a way to get what the most number of digits are

            while(!sorted) 
            {
                // get the LSD (Least Significant Digit) of the array based on baseNum
                // the LSD starts from the once digit, followed by the tens, followed by the hundreds and so on
                sorted = true;
                for (int x = 0; x < arr.Length; x++)
                {
                    LSD[x] = arr[x] / baseNum % 10;
                    // if all LSD are 0, this means that the sorting algorithm, if coded correctly, would have sorted
                    // everything by now
                    if (LSD[x] > 0)
                        sorted = false;
                }

                // this just breaks the while loop if all LSD are 0
                if (sorted)
                    break;

                //foreach (int a in LSD)
                //    Console.Write(a + "\t");
                //Console.WriteLine();

                // we will be performing the insertion sort
                // basing everything in the LSD array but reflecting the changes in both
                // the LSD array and the number array
                Console.WriteLine("The number array to be sorted in base {0}", baseNum);
                foreach (int a in arr)
                    Console.Write(a + "\t");
                Console.WriteLine();

                // beginning the insertion sort...
                // this is just regular insertion sort but instead of comparing values, we are comparing LSD
                for(int x = 1; x < arr.Length; x++)
                {
                    insertIndex = -1;
                    insertFlag = false;

                    for (int y = 0; y < x; y++)
                    {
                        Console.WriteLine("Base {0} : Comparing {1} and {2}", baseNum, LSD[x], LSD[y]);
                        for(int a = 0; a < arr.Length; a++)
                        {
                            if (a == x)
                                Console.ForegroundColor = ConsoleColor.Red;
                            else if (a == y)
                                Console.ForegroundColor = ConsoleColor.Blue;
                            else
                                Console.ResetColor();

                            Console.Write(arr[a] + "\t");
                        }
                        Console.ResetColor();
                        Console.WriteLine();
                        if (LSD[x] < LSD[y])
                        {
                            insertFlag = true;
                            insertIndex = y;
                            break;
                        }
                    }

                    if(insertFlag)
                    {
                        toInsertValue = arr[x];
                        toInsertLSD = LSD[x];
                        arr[x] = -1;
                        LSD[x] = -1;

                        Console.WriteLine("Base {0} : Removing {1}.", baseNum, toInsertValue);
                        for (int a = 0; a < arr.Length; a++)
                        {
                            if (arr[a] == -1)
                            {
                                Console.BackgroundColor = ConsoleColor.Red;
                                Console.Write("  \t");
                            }
                            else
                            {
                                Console.ResetColor();
                                Console.Write(arr[a] + "\t");
                            }
                        }
                        Console.ResetColor();
                        Console.WriteLine();

                        for (int y = x - 1; y >= insertIndex; y--)
                        {
                            arr[y + 1] = arr[y];
                            LSD[y + 1] = LSD[y];
                            arr[y] = -1;
                            LSD[y] = -1;

                            Console.WriteLine("Base {0} : Moving {1}.", baseNum, arr[y+1]);
                            for (int a = 0; a < arr.Length; a++)
                            {
                                if (arr[a] == -1)
                                {
                                    Console.BackgroundColor = ConsoleColor.Red;
                                    Console.Write("  \t");
                                }
                                else if (a == y + 1)
                                {
                                    Console.ResetColor();
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.Write(arr[a] + "\t");
                                }
                                else
                                {
                                    Console.ResetColor();
                                    Console.Write(arr[a] + "\t");
                                }
                            }
                            Console.ResetColor();
                            Console.WriteLine();
                        }

                        arr[insertIndex] = toInsertValue;
                        LSD[insertIndex] = toInsertLSD;
                    }
                    Console.ReadKey();
                }

                Console.WriteLine("The Sorted Number array base {0}", baseNum);
                foreach (int a in arr)
                    Console.Write(a + "\t");
                Console.WriteLine();
                Console.ReadKey();
                Console.Clear();


                baseNum *= 10;
            }
            Console.ReadKey();
        }
    }
}
