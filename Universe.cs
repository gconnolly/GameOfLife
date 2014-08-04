using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    /// <summary>
    /// Brute Force Implementation of the 'Game Of Life'
    /// </summary>
    public class Universe
    {
        private readonly List<IList<int>> grid;

        public Universe(int width, int height)
        {
            grid = new List<IList<int>>();

            for (var i = 0; i < height; i++)
            {
                var row = new List<int>();

                for (var j = 0; j < width; j++)
                {
                    row.Add(0);
                }

                grid.Add(row);
            }
        }

        /// <summary>
        /// Initialize the universe with living cells
        /// </summary>
        /// <param name="initallyLivingCells"></param>
        public void LetThereBeLife(IEnumerable<Tuple<int, int>> initallyLivingCells)
        {
            ScortchTheUniverse();

            //Let there be life!
            foreach (var cell in initallyLivingCells)
            {
                grid[cell.Item1][cell.Item2] = 1;
            }
        }

        /// <summary>
        /// Kill all cells
        /// </summary>
        public void ScortchTheUniverse()
        {
            foreach (var row in grid)
            {
                for (var i = 0; i < row.Count; i++)
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
            for (var y = 0; y < grid.Count; y++)
            {
                for (var x = 0; x < grid[y].Count; x++)
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
        /// Evaluate if a cell is going to live or die based on the
        /// state of its current state and the state of its neighbors
        /// </summary>
        private int DetermineFateOfCell(int x, int y)
        {
            var cell = new Cell(x, y, grid);

            int livingNeighbors = cell.GetNumberOfNeighbors();

            if (cell.IsAlive)
            {
                return (livingNeighbors < 2 || livingNeighbors > 3) ? 0 : 1;
            }
            else
            {
                return livingNeighbors == 3 ? 1 : 0;
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

        #region Nested Type: Cell

        public class Cell
        {
            private readonly List<IList<int>> grid;
            private readonly int x;
            private readonly int y;

            public Cell(int x, int y, List<IList<int>> grid)
            {
                this.x = x;
                this.y = y;
                this.grid = grid;
            }

            public bool IsAlive
            {
                get
                {
                    return grid[y][x] == 1;
                }
            }

            public int GetNumberOfNeighbors()
            {
                int count = 0;

                count += IsCellAlive(x - 1, y - 1, grid);
                count += IsCellAlive(x, y - 1, grid);
                count += IsCellAlive(x + 1, y - 1, grid);
                count += IsCellAlive(x + 1, y, grid);
                count += IsCellAlive(x + 1, y + 1, grid);
                count += IsCellAlive(x, y + 1, grid);
                count += IsCellAlive(x - 1, y + 1, grid);
                count += IsCellAlive(x - 1, y, grid);

                return count;
            }

            private int IsCellAlive(int x, int y, List<IList<int>> grid)
            {
                if (x < 0 || y < 0 || x >= grid[0].Count || y >= grid.Count)
                {
                    return 0;
                }
                else
                {
                    return grid[y][x];
                }
            }
        }

        #endregion
    }
}
