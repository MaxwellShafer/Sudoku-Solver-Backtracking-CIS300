/* SolverTests.cs
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
    /// Unit tests for the Solver class.
    /// </summary>
    [TestFixture]
    public class SolverTests
    {
        /// <summary>
        /// Tests that the Solve method correctly solves the given 4x4 puzzle.
        /// </summary>
        [Test, Timeout(1000), Category("A: 4x4 Puzzles")]
        public void Test4x4A()
        {
            int[,] puzzle =
            {
                { 2, 0, 0, 0 },
                { 4, 3, 2, 1 },
                { 3, 0, 0, 0 },
                { 1, 4, 3, 2 }
            };
            int[,] goal =
            {
                { 2, 1, 4, 3 },
                { 4, 3, 2, 1 },
                { 3, 2, 1, 4 },
                { 1, 4, 3, 2 }
            };
            bool result = Solver.Solve(puzzle);
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.True);
                Common.ComparePuzzles(puzzle, goal);
            });
        }

        /// <summary>
        /// Tests that the Solve method correctly solves the given 4x4 puzzle.
        /// </summary>
        [Test, Timeout(1000), Category("A: 4x4 Puzzles")]
        public void Test4x4B()
        {
            int[,] puzzle =
            {
                { 0, 4, 0, 0 },
                { 2, 0, 0, 4 },
                { 0, 2, 4, 0 },
                { 0, 0, 1, 2 },
            };
            int[,] goal =
            {
                { 3, 4, 2, 1 },
                { 2, 1, 3, 4 },
                { 1, 2, 4, 3 },
                { 4, 3, 1, 2 }
            };
            bool result = Solver.Solve(puzzle);
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.True);
                Common.ComparePuzzles(puzzle, goal);
            });
        }

        /// <summary>
        /// Tests that the Solve method returns false for the given unsolvable puzzle.
        /// </summary>
        [Test, Timeout(1000), Category("A: 4x4 Puzzles")]
        public void Test4x4Unsolvable()
        {
            int[,] puzzle =
            {
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 1, 0, 0, 0 },
                { 0, 2, 3, 4 }
            };
            Assert.That(Solver.Solve(puzzle), Is.False);
        }

        /// <summary>
        /// Tests that the Solve method correctly solves the given 9x9 puzzle.
        /// </summary>
        [Test, Timeout(1000), Category("B: 9x9 Puzzles")]
        public void Test9x9A()
        {
            int[,] puzzle =
            {
                { 0, 0, 0, 8, 1, 0, 0, 0, 0 },
                { 0, 2, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 1, 0, 9, 0, 0, 7, 0, 0 },
                { 0, 7, 0, 0, 2, 5, 0, 9, 3 },
                { 4, 0, 2, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 5, 0, 0 },
                { 0, 9, 7, 5, 0, 0, 0, 0, 0 },
                { 5, 6, 3, 0, 0, 0, 0, 0, 4 },
                { 0, 0, 0, 0, 0, 0, 6, 8, 0 },
            };
            int[,] goal =
            {
                { 9, 3, 4, 8, 1, 7, 2, 5, 6 },
                { 7, 2, 8, 6, 5, 3, 4, 1, 9 },
                { 6, 1, 5, 9, 4, 2, 7, 3, 8 },
                { 1, 7, 6, 4, 2, 5, 8, 9, 3 },
                { 4, 5, 2, 3, 9, 8, 1, 6, 7 },
                { 3, 8, 9, 1, 7, 6, 5, 4, 2 },
                { 8, 9, 7, 5, 6, 4, 3, 2, 1 },
                { 5, 6, 3, 2, 8, 1, 9, 7, 4 },
                { 2, 4, 1, 7, 3, 9, 6, 8, 5 }
            };
            bool result = Solver.Solve(puzzle);
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.True);
                Common.ComparePuzzles(puzzle, goal);
            });
        }

        /// <summary>
        /// Tests that the Solve method correctly solves the given 9x9 puzzle.
        /// </summary>
        [Test, Timeout(1000), Category("B: 9x9 Puzzles")]
        public void Test9x9B()
        {
            int[,] puzzle =
            {
                { 5, 0, 0, 0, 6, 3, 4, 0, 0 },
                { 0, 0, 0, 7, 0, 0, 0, 0, 0 },
                { 1, 0, 0, 0, 5, 0, 8, 3, 0 },
                { 0, 0, 0, 0, 1, 8, 0, 0, 7 },
                { 0, 0, 6, 9, 0, 0, 0, 0, 0 },
                { 0, 4, 3, 0, 0, 0, 9, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 7, 0, 0, 2, 0 },
                { 3, 2, 0, 6, 4, 0, 5, 0, 0 }
            };
            int[,] goal =
            {
                { 5, 9, 8, 1, 6, 3, 4, 7, 2 },
                { 6, 3, 2, 7, 8, 4, 1, 5, 9 },
                { 1, 7, 4, 2, 5, 9, 8, 3, 6 },
                { 2, 5, 9, 4, 1, 8, 3, 6, 7 },
                { 8, 1, 6, 9, 3, 7, 2, 4, 5 },
                { 7, 4, 3, 5, 2, 6, 9, 8, 1 },
                { 4, 6, 5, 8, 9, 2, 7, 1, 3 },
                { 9, 8, 1, 3, 7, 5, 6, 2, 4 },
                { 3, 2, 7, 6, 4, 1, 5, 9, 8 }
            };
            bool result = Solver.Solve(puzzle);
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.True);
                Common.ComparePuzzles(puzzle, goal);
            });
        }

        /// <summary>
        /// Tests that the Solve method solves the given 9x9 puzzle in a reasonable amount of
        /// time.
        /// </summary>
        [Test, Timeout(1000), Category("B: 9x9 Puzzles")]
        public void Test9x9C()
        {
            int[,] puzzle =
            {
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 2, 3, 4, 5, 6, 7, 8, 9 }
            };
            Assert.That(Solver.Solve(puzzle), Is.True);
        }

        /// <summary>
        /// Tests that a puzzle with duplicate values in a row will cause the Solve method to
        /// return false.
        /// </summary>
        [Test, Timeout(1000), Category("C: Bad Puzzles")]
        public void TestDuplicateInRow()
        {
            int[,] puzzle =
            {
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 4, 0, 4 }
            };
            Assert.That(Solver.Solve(puzzle), Is.False);
        }

        /// <summary>
        /// Tests that a puzzle with duplicate values in a block will cause the Solve method to
        /// return false.
        /// </summary>
        [Test, Timeout(1000), Category("C: Bad Puzzles")]
        public void TestDuplicateInBlock()
        {
            int[,] puzzle =
            {
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 2, 0 },
                { 0, 4, 0, 2 }
            };
            Assert.That(Solver.Solve(puzzle), Is.False);
        }

        /// <summary>
        /// Tests that a puzzle with duplicate values in a column will cause the Solve method to
        /// return false.
        /// </summary>
        [Test, Timeout(1000), Category("C: Bad Puzzles")]
        public void TestDuplicateInColumn()
        {
            int[,] puzzle =
            {
                { 1, 2, 3, 0, 0, 0, 0, 0, 0 },
                { 4, 5, 0, 6, 7, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 6, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 7, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 }
            };
            Assert.That(Solver.Solve(puzzle), Is.False);
        }


    }
}
