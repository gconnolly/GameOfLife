using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    public class Universe
    {
        private readonly int[][] grid;

        public Universe(int width, int height)
        {
            //Initialize grid to all dead cells
            grid = new int[height][];

            for (var i = 0; i < height; i++)
            {
                var row = new int[width];

                for (var j = 0; j < width; j++)
                {
                    row[j] = 0;
                }

                grid[i] = row;
            }
        }

        /// <summary>
        /// Initialize the universe with living cells
        /// </summary>
        /// <param name="initallyLivingCells"></param>
        public void LetThereBeLife(int[,] initallyLivingCells)
        {
            ScortchTheUniverse();

            //Let there be life!
            var length = initallyLivingCells.GetLength(0);
            for (var i = 0; i < length; i++)
            {
                grid[initallyLivingCells[i, 0]][initallyLivingCells[i, 1]] = 1;
            }
        }

        /// <summary>
        /// Kill all cells
        /// </summary>
        public void ScortchTheUniverse()
        {
            foreach (var row in grid)
            {
                for (var i = 0; i < row.Length; i++)
                {
                    row[i] = 0;
                }
            }
        }

        /// <summary>
        /// Advance the state of the universe by
        /// determining the fate of each cell
        /// </summary>
        public void Evolve()
        {
            //Inspect each cell for its fate
            var changes = new List<Tuple<int, int, int>>();
            for (var y = 0; y < grid.Length; y++)
            {
                for (var x = 0; x < grid[y].Length; x++)
                {
                    var fate = DetermineFateOfCell(x, y);
                    if (grid[y][x] != fate)
                    {
                        changes.Add(new Tuple<int, int, int>(y, x, fate));
                    }
                }
            }

            //Process Changes
            foreach (var change in changes)
            {
                grid[change.Item1][change.Item2] = change.Item3;
            }
        }

        /// <summary>
        /// Output the universe in an array format
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string universe = string.Empty;

            foreach (var row in grid)
            {
                universe += String.Join(" ", row) + Environment.NewLine;
            }

            return universe;
        }

        /// <summary>
        /// Evaluate if a cell is going to live or die based on the
        /// state of its current state and the state of its neighbors
        /// </summary>
        private int DetermineFateOfCell(int x, int y)
        {
            int livingNeighbors = GetNumberOfNeighbors(x, y);

            if (IsCellAlive(x, y) == 1)
            {
                return (livingNeighbors < 2 || livingNeighbors > 3) ? 0 : 1;
            }
            else
            {
                return livingNeighbors == 3 ? 1 : 0;
            }
        }

        /// <summary>
        /// Count the number of living neighbors
        /// Inspecting them in a clockwise fashion
        /// </summary>
        private int GetNumberOfNeighbors(int x, int y)
        {
            int count = 0;

            count += IsCellAlive(x - 1, y - 1);
            count += IsCellAlive(x, y - 1);
            count += IsCellAlive(x + 1, y - 1);
            count += IsCellAlive(x + 1, y);
            count += IsCellAlive(x + 1, y + 1);
            count += IsCellAlive(x, y + 1);
            count += IsCellAlive(x - 1, y + 1);
            count += IsCellAlive(x - 1, y);

            return count;
        }

        /// <summary>
        /// Determine is a cell is alive or dead
        /// </summary>
        private int IsCellAlive(int x, int y)
        {
            // First check to see if it is a valid coordinate
            // if not, it is effectively dead
            if (x < 0 || y < 0 || x >= grid[0].Length || y >= grid.Length)
            {
                return 0;
            }
            else
            {
                return grid[y][x];
            }
        }
    }
}
