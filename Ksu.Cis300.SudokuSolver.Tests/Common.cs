/* Common.cs
 * Author: Rod Howell
 */
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksu.Cis300.SudokuSolver.Tests
{
    /// <summary>
    /// Contains a method used by both PuzzleDataTests and SolverTests.
    /// </summary>
    public static class Common
    {
        /// <summary>
        /// Tests that each entry in result is the same as in goal.
        /// </summary>
        /// <param name="result">The puzzle to test.</param>
        /// <param name="goal">The expected result.</param>
        public static void ComparePuzzles(int[,] result, int[,] goal)
        {
            for (int i = 0; i < goal.GetLength(0); i++)
            {
                for (int j = 0; j < goal.GetLength(1); j++)
                {
                    Assert.That(result[i, j], Is.EqualTo(goal[i, j]),
                        $"Row {i}, column {j} is wrong.");
                }
            }
        }

    }
}
