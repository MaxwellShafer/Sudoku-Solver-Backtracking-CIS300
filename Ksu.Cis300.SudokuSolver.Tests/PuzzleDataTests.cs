/* PuzzleDataTests.cs
 * Author: Rod Howell
 */
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksu.Cis300.SudokuSolver.Tests
{
    /// <summary>
    /// Unit tests for PuzzleData.
    /// </summary>
    [TestFixture]
    public class PuzzleDataTests
    {
        /// <summary>
        /// Tests that the constructor correctly initializes the puzzle.
        /// </summary>
        [Test, Timeout(1000), Category("A: Constructor Tests")]
        public void TestPuzzleInitialize()
        {
            int[,] puzzle =
            {
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 1, 3, 4 }
            };
            int[,] goal =
            {
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 1, 3, 4 }
            };
            PuzzleData pd = new PuzzleData(puzzle);
            Assert.Multiple(() => Common.ComparePuzzles(pd.Puzzle, goal));
        }

        /// <summary>
        /// Tests that EmptyLocationCount is correct initially.
        /// </summary>
        [Test, Timeout(1000), Category("A: Constructor Tests")]
        public void TestInitialEmptyCount()
        {
            int[,] puzzle =
            {
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 1, 3, 4 }
            };
            PuzzleData pd = new PuzzleData(puzzle);
            Assert.That(pd.EmptyLocationCount, Is.EqualTo(13));
        }

        /// <summary>
        /// Tests that a puzzle with duplicate values in a row will cause the constructor to
        /// throw an ArgumentException.
        /// </summary>
        [Test, Timeout(1000), Category("A: Constructor Tests")]
        public void TestDuplicateInRow()
        {
            int[,] puzzle =
            {
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 4, 0, 4 }
            };
            Assert.That(() => new PuzzleData(puzzle), Throws.InstanceOf<ArgumentException>());
        }

        /// <summary>
        /// Tests that a puzzle with duplicate values in a block will cause the constructor to
        /// throw an ArgumentException.
        /// </summary>
        [Test, Timeout(1000), Category("A: Constructor Tests")]
        public void TestDuplicateInBlock()
        {
            int[,] puzzle =
            {
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 2, 0 },
                { 0, 4, 0, 2 }
            };
            Assert.That(() => new PuzzleData(puzzle), Throws.InstanceOf<ArgumentException>());
        }

        /// <summary>
        /// Tests that a puzzle with duplicate values in a column will cause the constructor to
        /// throw an ArgumentException.
        /// </summary>
        [Test, Timeout(1000), Category("A: Constructor Tests")]
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
            Assert.That(() => new PuzzleData(puzzle), Throws.InstanceOf<ArgumentException>());
        }

        /// <summary>
        /// Tests that GetEmptyLocation gets the correct location and that
        /// TryNextValue sets it to the only available value.
        /// </summary>
        [Test, Timeout(1000), Category("B: TryNextValue Tests")]
        public void Test1ValueAvailable()
        {
            int[,] puzzle =
            {
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 1, 3, 4 }
            };
            int[,] goal =
            {
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 2, 1, 3, 4 }
            };
            PuzzleData pd = new PuzzleData(puzzle);
            pd.GetEmptyLocation();

            // The following should place 2 in (3, 0).
            bool success = pd.TryNextValue();
            Assert.Multiple(() =>
            {
                Assert.That(success, Is.True);
                Common.ComparePuzzles(puzzle, goal);
            });
        }

        /// <summary>
        /// Tests that TryNextValue returns false and makes no changes to the puzzle
        /// when the active location has no values available.
        /// </summary>
        [Test, Timeout(1000), Category("B: TryNextValue Tests")]
        public void TestNoValueAvailable()
        {
            int[,] puzzle =
            {
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 2, 0, 0, 0 },
                { 0, 1, 3, 4 }
            };
            int[,] goal =
            {
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 2, 0, 0, 0 },
                { 0, 1, 3, 4 }
            };
            PuzzleData pd = new PuzzleData(puzzle);
            pd.GetEmptyLocation();
            bool success = pd.TryNextValue();
            Assert.Multiple(() =>
            {
                Assert.That(success, Is.False);
                Common.ComparePuzzles(puzzle, goal);
            });
        }

        /// <summary>
        /// Tests that TryNextValue returns false after all values have been tried.
        /// </summary>
        [Test, Timeout(1000), Category("B: TryNextValue Tests")]
        public void TestValuesExhausted()
        {
            int[,] puzzle =
            {
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 1, 3, 4 }
            };
            int[,] goal =
            {
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 2, 1, 3, 4 }
            };
            PuzzleData pd = new PuzzleData(puzzle);
            pd.GetEmptyLocation();

            // The following should place 2 in (3, 0).
            pd.TryNextValue();
            bool success = pd.TryNextValue();
            Assert.Multiple(() =>
            {
                Assert.That(success, Is.False);
                Common.ComparePuzzles(puzzle, goal);
            });
        }

        /// <summary>
        /// Tests that getting a new empty location without filling the last one throws
        /// an InvalidOperationException.
        /// </summary>
        [Test, Timeout(1000), Category("B: TryNextValue Tests")]
        public void TestEmptyActiveLocation()
        {
            int[,] puzzle =
            {
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 1, 3, 4 }
            };
            PuzzleData pd = new PuzzleData(puzzle);
            pd.GetEmptyLocation();
            Assert.That(() => pd.GetEmptyLocation(), Throws.InstanceOf<InvalidOperationException>());
        }

        /// <summary>
        /// Tests that trying a value without an active location throws an InvalidOperationException.
        /// </summary>
        [Test, Timeout(1000), Category("B: TryNextValue Tests")]
        public void TestNoActiveLocation()
        {
            int[,] puzzle =
            {
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 1, 3, 4 }
            };
            PuzzleData pd = new PuzzleData(puzzle);
            Assert.That(() => pd.TryNextValue(), Throws.InstanceOf<InvalidOperationException>());
        }

        /// <summary>
        /// Tests that trying to get an empty location when all are filled throws an
        /// InvalidOperationException.
        /// </summary>
        [Test, Timeout(1000), Category("C: GetEmptyLocationTests")]
        public void TestGetEmptyNoneAvailable()
        {
            int[,] puzzle =
            {
                { 1, 2, 3, 4 },
                { 4, 3, 2, 1 },
                { 3, 4, 1, 2 },
                { 2, 1, 4, 3 }
            };
            PuzzleData pd = new PuzzleData(puzzle);
            Assert.That(() => pd.GetEmptyLocation(), Throws.InstanceOf<InvalidOperationException>());
        }

        /// <summary>
        /// Tests that getting a second empty location gets the correct location.
        /// </summary>
        [Test, Timeout(1000), Category("C: GetEmptyLocationTests")]
        public void Test2GetEmpty()
        {
            int[,] puzzle =
            {
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 4, 6, 0, 0, 0, 0, 0, 0, 2 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 3 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 4 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 5 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 6 },
                { 0, 0, 0, 0, 0, 0, 5, 0, 7 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 8 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 9 }
            };
            int[,] goal =
            {
                { 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                { 4, 6, 0, 0, 0, 0, 7, 0, 2 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 3 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 4 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 5 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 6 },
                { 0, 0, 0, 0, 0, 0, 5, 0, 7 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 8 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 9 }
            };
            PuzzleData pd = new PuzzleData(puzzle);
            pd.GetEmptyLocation();

            // The following should place 1 in (0, 8).
            pd.TryNextValue();
            pd.GetEmptyLocation();

            // The following should place 7 in (1, 6).
            bool success = pd.TryNextValue();
            Assert.Multiple(() =>
            {
                Assert.That(success, Is.True);
                Common.ComparePuzzles(puzzle, goal);
            });
        }

        [Test, Timeout(1000), Category("C: GetEmptyLocationTests")]
        public void Test2GetEmpty2ndValue()
        {
            int[,] puzzle =
            {
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 4, 6, 0, 0, 0, 0, 0, 0, 2 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 3 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 4 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 5 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 6 },
                { 0, 0, 0, 0, 0, 0, 5, 0, 7 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 8 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 9 }
            };
            int[,] goal =
            {
                { 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                { 4, 6, 0, 0, 0, 0, 8, 0, 2 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 3 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 4 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 5 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 6 },
                { 0, 0, 0, 0, 0, 0, 5, 0, 7 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 8 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 9 }
            };
            PuzzleData pd = new PuzzleData(puzzle);
            pd.GetEmptyLocation();

            // The following should place 1 in (0, 8).
            pd.TryNextValue();
            pd.GetEmptyLocation();

            // The following should place 7 in (1, 6).
            pd.TryNextValue();

            // The following should place 8 in (1, 6).
            bool success = pd.TryNextValue();
            Assert.Multiple(() =>
            {
                Assert.That(success, Is.True);
                Common.ComparePuzzles(puzzle, goal);
            });
        }

        /// <summary>
        /// Tests that undoing without an active location throws an InvalidOperationException.
        /// </summary>
        [Test, Timeout(1000), Category("D: Undo Tests")]
        public void TestUndoNoActive()
        {
            int[,] puzzle =
            {
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 1, 3, 4 }
            };
            PuzzleData pd = new PuzzleData(puzzle);
            Assert.That(() => pd.Undo(), Throws.InstanceOf<InvalidOperationException>());
        }

        /// <summary>
        /// Tests a single undo.
        /// </summary>
        [Test, Timeout(1000), Category("D: Undo Tests")]
        public void TestUndo()
        {
            int[,] puzzle =
            {
                { 1, 2, 3, 0, 0, 0, 0, 0, 0 },
                { 4, 5, 0, 6, 7, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 6, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 }
            };
            int[,] goal =
            {
                { 1, 2, 3, 0, 0, 0, 0, 0, 0 },
                { 4, 5, 9, 6, 7, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 6, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 }
            };
            PuzzleData pd = new PuzzleData(puzzle);
            pd.GetEmptyLocation();

            // The following should place 8 in (1, 2).
            pd.TryNextValue();
            pd.GetEmptyLocation();

            // The following should place 7 in (2, 0).
            pd.TryNextValue();

            // The following should place 0 in (2, 0).
            pd.Undo();

            // The following should place 9 in (1, 2).
            bool success = pd.TryNextValue();
            Assert.Multiple(() =>
            {
                Assert.That(success, Is.True);
                Common.ComparePuzzles(pd.Puzzle, goal);
            });
        }

        /// <summary>
        /// Tests two undos, yielding no active location.
        /// </summary>
        [Test, Timeout(1000), Category("D: Undo Tests")]
        public void TestUndo2()
        {
            int[,] puzzle =
            {
                { 1, 2, 3, 0, 0, 0, 0, 0, 0 },
                { 4, 5, 0, 6, 7, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 6, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 }
            };
            int[,] goal =
            {
                { 1, 2, 3, 0, 0, 0, 0, 0, 0 },
                { 4, 5, 0, 6, 7, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 6, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 }
            };
            PuzzleData pd = new PuzzleData(puzzle);
            pd.GetEmptyLocation();

            // The following should place 8 in (1, 2).
            pd.TryNextValue();
            pd.GetEmptyLocation();

            // The following should place 7 in (2, 0).
            pd.TryNextValue();

            // The following should place 0 in (2, 0).
            pd.Undo();

            // The following should place 0 in (1, 2).
            bool success = pd.Undo();
            Assert.Multiple(() =>
            {
                Assert.That(success, Is.False);
                Common.ComparePuzzles(pd.Puzzle, goal);
            });
        }

        /// <summary>
        /// Tests a single undo, followed by next empty location.
        /// </summary>
        [Test, Timeout(1000), Category("D: Undo Tests")]
        public void TestUndoNext()
        {
            int[,] puzzle =
            {
                { 1, 2, 3, 0, 0, 0, 0, 0, 0 },
                { 4, 5, 0, 6, 7, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 6, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 }
            };
            int[,] goal =
            {
                { 1, 2, 3, 0, 0, 0, 0, 0, 0 },
                { 4, 5, 9, 6, 7, 0, 0, 0, 0 },
                { 7, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 6, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 }
            };
            PuzzleData pd = new PuzzleData(puzzle);
            pd.GetEmptyLocation();

            // The following should place 8 in (1, 2).
            pd.TryNextValue();
            pd.GetEmptyLocation();

            // The following should place 7 in (2, 0).
            pd.TryNextValue();

            // The following should place 0 in (2, 0).
            pd.Undo();

            // The following should place 9 in (1, 2).
            pd.TryNextValue();
            pd.GetEmptyLocation();

            // The following should place 7 in (2, 0).
            bool success = pd.TryNextValue();

            Assert.Multiple(() =>
            {
                Assert.That(success, Is.True);
                Common.ComparePuzzles(pd.Puzzle, goal);
            });
        }
    }
}
