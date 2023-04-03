/* Solver.cs
 * Author: Rod Howell
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
        private static int[,] _puzzle;

        /// <summary>
        /// The number of rows and columns in the puzzle.
        /// </summary>
        private static int _size;

        /// <summary>
        /// The number of rows and columns in a block.
        /// </summary>
        private static int _blockSize;

        /// <summary>
        /// Element [j, k] indicates whether column j contains k for k > 0.
        /// Elements [j, 0] are unused.
        /// </summary>
        private static bool[,] _columnContains;

        /// <summary>
        /// Element [i, j, k] indicates whether block [i, j] contains k for k > 0.
        /// Elements [i, j, 0] are unused.
        /// </summary>
        private static bool[,,] _blockContains;

        /// <summary>
        /// Contains the column indicies of the empty locations for each row.
        /// </summary>
        private static DoublyLinkedListCell[] _emptyLocations;

        /// <summary>
        /// Contains the unused values for each row.
        /// </summary>
        private static DoublyLinkedListCell[] _unusedValues;

        /// <summary>
        /// Contains the cells removed from _unusedValues.
        /// </summary>
        private static Stack<DoublyLinkedListCell> _usedValues;

        /// <summary>
        /// The current row being filled.
        /// </summary>
        private static int _currentRow;

        /// <summary>
        /// The cell in _emptyLocations that represents the next location to be filled.
        /// </summary>
        private static DoublyLinkedListCell _currentLocation;

        /// <summary>
        /// The cell containing the next available value for the current row.
        /// If there are no more available values, it will be the header cell for the current row.
        /// </summary>
        private static DoublyLinkedListCell _nextValue;

        /// <summary>
        /// Removes the given cell from its list without changing its Previous or Next.
        /// Assumes cell is non-null and properly linked into a list.
        /// </summary>
        /// <param name="cell">The cell to remove.</param>
        private static void Remove(DoublyLinkedListCell cell)
        {
            cell.Previous.Next = cell.Next;
            cell.Next.Previous = cell.Previous;
        }

        /// <summary>
        /// Restores the given cell to the location between its Previous and its Next.
        /// The cell and its Previous and Next are assumed to be non-null.
        /// </summary>
        /// <param name="cell">The cell to restore</param>
        private static void Restore(DoublyLinkedListCell cell)
        {
            cell.Next.Previous = cell;
            cell.Previous.Next = cell;
        }

        /// <summary>
        /// Inserts the given cell before the given previous cell.
        /// Both cells and next.Previous are assumed to be non-null.
        /// </summary>
        /// <param name="cell">The cell to insert.</param>
        /// <param name="prev">The cell to insert before.</param>
        private static void InsertBefore(DoublyLinkedListCell cell, DoublyLinkedListCell next)
        {
            cell.Previous = next.Previous;
            cell.Next = next;
            Restore(cell);
        }

        /// <summary>
        /// Gets a circular doubly-linked list containing all the values for the puzzle.
        /// </summary>
        /// <returns>The header cell for the list of all values.</returns>
        private static DoublyLinkedListCell GetAllValues()
        {
            DoublyLinkedListCell header = new DoublyLinkedListCell();
            for (int i = 1; i <= _size; i++)
            {
                DoublyLinkedListCell cell = new DoublyLinkedListCell();
                cell.Data = i;
                InsertBefore(cell, header);
            }
            return header;
        }

        /// <summary>
        /// Removes the cell containing the given value 
        /// </summary>
        /// <param name="value">The value to remove.</param>
        /// <param name="header">The header cell of the list.</param>
        /// <returns>Whether the value was found.</returns>
        private static bool RemoveValue(int value, DoublyLinkedListCell header)
        {
            DoublyLinkedListCell cell = header.Next;
            while (cell != header)
            {
                if (cell.Data == value)
                {
                    Remove(cell);
                    return true;
                }
                cell = cell.Next;
            }
            return false;
        }

        /// <summary>
        /// Records that the column and block containing the given location contain the given 
        /// value.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="col">The column.</param>
        /// <param name="value">The value.</param>
        /// <param name="present">Indicates whether the value is present.</param>
        private static void RecordValue(int row, int col, int value, bool present)
        {
            _columnContains[col, value] = present;
            _blockContains[row / _blockSize, col / _blockSize, value] = present;
        }

        /// <summary>
        /// Determines whether the given location is in a column and block that does not already
        /// contain the given value.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="col">The column.</param>
        /// <param name="value">The value.</param>
        /// <returns>Whether row and col describe a location that does not already contain 
        /// value.</returns>
        private static bool CanPlace(int row, int col, int value)
        {
            return !_columnContains[col, value] && 
                !_blockContains[row / _blockSize, col / _blockSize, value];
        }

        /// <summary>
        /// Initializes the given row of the data structures.
        /// </summary>
        /// <param name="i">The row to initialize,</param>
        /// <returns>Whether this row contains no duplicates of values, either within the
        /// row or within a column or block.</returns>
        private static bool InitializeRow(int i)
        {
            _emptyLocations[i] = new DoublyLinkedListCell();
            _unusedValues[i] = GetAllValues();
            for (int j = 0; j < _size; j++)
            {
                if (_puzzle[i, j] == 0)
                {
                    DoublyLinkedListCell cell = new DoublyLinkedListCell();
                    cell.Data = j;
                    InsertBefore(cell, _emptyLocations[i]);
                }
                else
                {
                    if (!CanPlace(i, j, _puzzle[i, j]) ||
                        !RemoveValue(_puzzle[i, j], _unusedValues[i]))
                    {
                        return false;
                    }
                    RecordValue(i, j, _puzzle[i, j], true);
                }
            }
            return true;
        }

        /// <summary>
        /// Initializes the data structures for the given puzzle, and verifies that the puzzle
        /// contains no duplicate values in any row, column, or block.
        /// </summary>
        /// <param name="puzzle">The puzzle.</param>
        /// <returns>Whether the puzzle is without duplicate values in rows, columns, or blocks.</returns>
        private static bool InitializeDataStructures(int[,] puzzle)
        {
            _puzzle = puzzle;
            _size = puzzle.GetLength(0);
            _blockSize = (int)Math.Sqrt(_size);
            _emptyLocations = new DoublyLinkedListCell[_size];
            _unusedValues = new DoublyLinkedListCell[_size];
            _columnContains = new bool[_size, _size + 1];
            _blockContains = new bool[_blockSize, _blockSize, _size + 1];
            _usedValues = new Stack<DoublyLinkedListCell>();
            for (int i = 0; i < _size; i++)
            {
                if (!InitializeRow(i))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Tries to advance the current row to the next row.
        /// </summary>
        /// <returns>Whether there was a next row.</returns>
        private static bool TryAdvanceRow()
        {
            _currentRow++;
            if (_currentRow == _size)
            {
                return false;
            }
            _currentLocation = _emptyLocations[_currentRow].Next;
            _nextValue = _unusedValues[_currentRow].Next;
            return true;
        }

        /// <summary>
        /// Tries to backtrack.
        /// </summary>
        /// <returns>Whether the backtracking succeeded.</returns>
        private static bool TryBacktrack()
        {
            while (_usedValues.Count > 0)
            {
                if (_currentLocation.Previous == _emptyLocations[_currentRow])
                {
                    _currentRow--;
                    _currentLocation = _emptyLocations[_currentRow];
                }
                else
                {
                    _currentLocation = _currentLocation.Previous;
                    int col = _currentLocation.Data;
                    _puzzle[_currentRow, col] = 0;
                    DoublyLinkedListCell cell = _usedValues.Pop();
                    RecordValue(_currentRow, col, cell.Data, false);
                    Restore(cell);
                    if (cell.Next != _unusedValues[_currentRow])
                    {
                        _nextValue = cell.Next;
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Tries to use the next value in the current location.
        /// </summary>
        private static void UseNextValue()
        {
            if (CanPlace(_currentRow, _currentLocation.Data, _nextValue.Data))
            {
                DoublyLinkedListCell cell = _nextValue;
                _usedValues.Push(cell);
                Remove(cell);
                _puzzle[_currentRow, _currentLocation.Data] = cell.Data;
                RecordValue(_currentRow, _currentLocation.Data, cell.Data, true);
                _nextValue = _unusedValues[_currentRow].Next;
                _currentLocation = _currentLocation.Next;
            }
            else
            {
                _nextValue = _nextValue.Next;
            }
        }

        /// <summary>
        /// Tries to solve the given puzzle. If successful, updates the puzzle to contain the
        /// solution.
        /// </summary>
        /// <param name="puzzle">The puzzle to solve.</param>
        /// <returns>Whether a solution was found.</returns>
        public static bool Solve(int[,] puzzle)
        {
            if (!InitializeDataStructures(puzzle))
            {
                return false;
            }
            _currentRow = -1;
            TryAdvanceRow();
            while (true)
            {
                if (_currentLocation == _emptyLocations[_currentRow])
                {
                    if (!TryAdvanceRow())
                    {
                        return true;
                    }
                }
                else if (_nextValue == _unusedValues[_currentRow])
                {
                    if (!TryBacktrack())
                    {
                        return false;
                    }
                }
                else 
                {
                    UseNextValue();
                }
            }
        }
    }
}
