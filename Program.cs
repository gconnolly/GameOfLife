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
            var life = new HashSet<Tuple<int, int>> { 
                new Tuple<int, int> ( 1, 2 ), 
                new Tuple<int, int> ( 2, 3 ), 
                new Tuple<int, int> ( 3, 1 ), 
                new Tuple<int, int> ( 3, 2 ), 
                new Tuple<int, int> ( 3, 3 ) 
            };
      
            Console.WriteLine(life.Draw());

            while (Console.ReadLine() == "y")
            {
                life = life.Evolve();

                Console.WriteLine(life.Draw());
            }
        }
    }
}
