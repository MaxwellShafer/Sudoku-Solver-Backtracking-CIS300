/* Solver.cs
 * Author: Rod Howell
 * Edited By: Max Shafer
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksu.Cis300.SudokuSolver
{
    /// <summary>
    /// Contains methods for solving a Sudoku puzzle.
    /// </summary>
    public static class Solver
    {
        /// <summary>
        /// The puzzle. Locations containing 0 are considered to be empty.
        /// </summary>
        private static PuzzleData _puzzle;

        /// <summary>
        /// Tries to backtrack.
        /// </summary>
        /// <returns>Whether the backtracking succeeded.</returns>
        private static bool TryBacktrack()
        {

            bool result;
            while(true)
            {
                if (_puzzle.Undo())
                {
                    result = false;
                    break;
                }

                if (_puzzle.TryNextValue())
                {
                    result = true;
                    break;
                }
            }

            return result;
           
        }

      

        /// <summary>
        /// Tries to solve the given puzzle. If successful, updates the puzzle to contain the
        /// solution.
        /// </summary>
        /// <param name="puzzle">The puzzle to solve.</param>
        /// <returns>Whether a solution was found.</returns>
        public static bool Solve(int[,] puzzle)
        {
            try
            {
                _puzzle = new PuzzleData(puzzle);

                while (_puzzle.EmptyLocationCount != 0)
                {
                    _puzzle.GetEmptyLocation();

                    if (_puzzle.TryNextValue())
                    {
                        // is anything neeeded here?
                    }
                    else if (TryBacktrack())
                    {
                        // is anything neeeded here?
                    }
                    else
                    {
                        return false;
                    }
                }

                return true;
            }
            catch(ArgumentException)
            {
                return false;
            }
            

        }
    }
}
