using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    public static class GameOfLifeExtensions
    {
        /// <summary>
        /// Advance the state of the universe by
        /// determining the fate of each cell
        /// </summary>
        public static HashSet<Tuple<int, int>> Evolve(this HashSet<Tuple<int, int>> livingCells)
        {
            var cellsToInspect = new HashSet<Tuple<int, int>>();
            var result = new HashSet<Tuple<int, int>>();

            //Find all cells adjacent to living cells to be inspected
            foreach (var cell in livingCells)
            {
                new List<Tuple<int, int>> {
                    new Tuple<int, int> ( cell.Item1, cell.Item2 ),
                    new Tuple<int, int> ( cell.Item1, cell.Item2 - 1 ),
                    new Tuple<int, int> ( cell.Item1, cell.Item2 + 1 ),
                    new Tuple<int, int> ( cell.Item1 + 1, cell.Item2 - 1 ),
                    new Tuple<int, int> ( cell.Item1 + 1, cell.Item2 + 1 ),
                    new Tuple<int, int> ( cell.Item1 + 1, cell.Item2 ),
                    new Tuple<int, int> ( cell.Item1 - 1, cell.Item2 - 1 ),
                    new Tuple<int, int> ( cell.Item1 - 1, cell.Item2 + 1 ),
                    new Tuple<int, int> ( cell.Item1 - 1, cell.Item2 ),
                }.ForEach(c => cellsToInspect.Add(c));
            }

            //Determine the new living state of each cell
            foreach (var cell in cellsToInspect)
            {
                if (livingCells.DetermineFateOfCell(cell))
                {
                    result.Add(cell);
                }
            }

            return result;
        }

        /// <summary>
        /// Output the universe in a grid format
        /// </summary>
        /// <returns></returns>
        public static string Draw(this HashSet<Tuple<int, int>> livingCells)
        {
            //Determine the bounding box for displaying the
            //universe
            var maxX = livingCells.Max(c => c.Item1);
            var minX = livingCells.Min(c => c.Item1);
            var maxY = livingCells.Max(c => c.Item2);
            var minY = livingCells.Min(c => c.Item2);

            //Print a grid that only displays live cells within
            //the bounding box
            return Enumerable.Range(minY, maxY - minY + 1)
                    .Aggregate(String.Empty, (grid, y) =>
                        grid + Enumerable.Range(minX, maxX - minX + 1)
                            .Aggregate(String.Empty, (line, x) => line + (livingCells.Contains(new Tuple<int, int>(x, y)) ? "*" : " ")) + Environment.NewLine);
        }

        /// <summary>
        /// Evaluate if a cell is going to live or die based on the
        /// state of its current state and the state of its neighbors
        /// </summary>
        public static bool DetermineFateOfCell(this HashSet<Tuple<int, int>> livingCells, Tuple<int, int> cell)
        {
            int livingNeighbors = livingCells.GetNumberOfNeighbors(cell);

            return livingCells.Contains(cell) ? 
                (livingNeighbors == 2 || livingNeighbors == 3)
                : livingNeighbors == 3;
        }

        /// <summary>
        /// Count the number of living neighbors
        /// Inspecting them in a clockwise fashion
        /// </summary>
        public static int GetNumberOfNeighbors(this HashSet<Tuple<int, int>> livingCells, Tuple<int, int> cell)
        {
            return new List<Tuple<int, int>> {
                    new Tuple<int, int> ( cell.Item1, cell.Item2 - 1 ),
                    new Tuple<int, int> ( cell.Item1, cell.Item2 + 1 ),
                    new Tuple<int, int> ( cell.Item1 + 1, cell.Item2 - 1 ),
                    new Tuple<int, int> ( cell.Item1 + 1, cell.Item2 + 1 ),
                    new Tuple<int, int> ( cell.Item1 + 1, cell.Item2 ),
                    new Tuple<int, int> ( cell.Item1 - 1, cell.Item2 - 1 ),
                    new Tuple<int, int> ( cell.Item1 - 1, cell.Item2 + 1 ),
                    new Tuple<int, int> ( cell.Item1 - 1, cell.Item2 ),
                }.Sum(c => livingCells.Contains(c) ? 1 : 0);
        }
    }
}
