using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            var universe = new Universe(10, 10);

            universe.LetThereBeLife(new[,] { { 1, 2 }, { 2, 3 }, { 3, 1 }, { 3, 2 }, { 3, 3 } });

            Console.WriteLine(universe.ToString());

            while (Console.ReadLine() == "y")
            {
                universe.Evolve();

                Console.WriteLine(universe.ToString());
            }
        }
    }
}
