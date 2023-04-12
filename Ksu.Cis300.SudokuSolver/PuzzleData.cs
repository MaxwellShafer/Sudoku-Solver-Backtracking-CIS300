/* PuzzleData.cs
 * By: Max Shafer
 */

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;

namespace Ksu.Cis300.SudokuSolver
{
    public class PuzzleData
    {
        /// <summary>
        /// The number of rows and columns in the puzzle.
        /// </summary>
        private int _size;

        /// <summary>
        /// The number of rows and columns in a block.
        /// </summary>
        private int _blockSize;

        /// <summary>
        /// A DoublyLinkedListCell<int>[ , ] to store the header cells for each unused value list, 
        /// as described in Section 3.1. Doubly-Linked Lists.
        /// </summary>
        private DoublyLinkedListCell<int>[,] _headerCells;

        /// <summary>
        /// A DoublyLinkedListCell<int>[ , , ] to act as the value cell store, as described in Section 3.1. Doubly-Linked Lists.
        /// </summary>
        private DoublyLinkedListCell<int>[,,] _valueCells;

        /// <summary>
        /// A DoublyLinkedListCell<Location>[ , ] to store the available location lists for the rows, as described in Section 3.1. Doubly-Linked Lists.
        /// </summary>
        private DoublyLinkedListCell<Location>[,] _availableRowLocations;

        /// <summary>
        ///A DoublyLinkedListCell<Location>[ , , ] to act as the location cell store for the above row lists, as described in Section 3.1. Doubly-Linked Lists.
        /// </summary>
        private DoublyLinkedListCell<Location>[,,] _rowCellStore;

        /// <summary>
        /// A DoublyLinkedListCell<Location>[ , ] to store the available location lists for the columns, as described in Section 3.1. Doubly-Linked Lists.
        /// </summary>
        private DoublyLinkedListCell<Location>[,] _availableColumnLocations;

        /// <summary>
        ///A DoublyLinkedListCell<Location>[ , , ] to act as the location cell store for the above row lists, as described in Section 3.1. Doubly-Linked Lists.
        /// </summary>
        private DoublyLinkedListCell<Location>[,,] _columnCellStore;

        /// <summary>
        /// A DoublyLinkedListCell<Location>[ , , ] to store the available location lists for the  blocks, as described in Section 3.1. 
        /// </summary>
        private DoublyLinkedListCell<Location>[,,] _avaliableBlockLocations;

        /// <summary>
        /// A DoublyLinkedListCell<Location>[ , , ] to act as the location cell store for the above block above lists, as described in Section 3.1. Doubly-Linked Lists.
        /// </summary>
        private DoublyLinkedListCell<Location>[,,] _blockCellStore;


        /// <summary>
        /// A MinPriorityQueue to store the empty locations in the puzzle. This field should be initialized to a new instance.
        /// </summary>
        private MinPriorityQueue _priorityQueue = new MinPriorityQueue();


        /// <summary>
        /// A Stack<AssignmentData> to store the history of assignments of values to puzzle 
        /// location that have been made by the program. 
        /// This field should be initialized to a new instance. When this stack is nonempty, 
        /// we will consider the Location property in its top element to be the active location.
        /// </summary>
        private Stack<AssignmentData> _stack = new Stack<AssignmentData>();

        /// <summary>
        /// Puzzle: Gets an int[ , ] giving the puzzle. This property will replace the _puzzle field in the Solver class. It should use the default implementation.
        /// </summary>
        public int[,] Puzzle { get; set; }



        /// <summary>
        /// gets the number of empty locations
        /// </summary>
        public int EmptyLocationCount
        {
            get
            {
                int emptyLocations = 0;
                foreach (int i in Puzzle) // check to ensure iterate correctly
                {
                    if (i == 0)
                    {
                        emptyLocations++;
                    }

                }
                return emptyLocations;
                // Gets an int giving the number of empty locations in the puzzle.
            }
        }

        /// <summary>
        /// Removes the given cell from its list without changing its Previous or Next.
        /// Assumes cell is non-null and properly linked into a list.
        /// </summary>
        /// <param name="cell">The cell to remove.</param>
        private static void Remove<T>(DoublyLinkedListCell<T> cell)
        {
            cell.Previous.Next = cell.Next;
            cell.Next.Previous = cell.Previous;
        }


        /// <summary>
        /// Restores the given cell to the location between its Previous and its Next.
        /// The cell and its Previous and Next are assumed to be non-null.
        /// </summary>
        /// <param name="cell">The cell to restore</param>
        private static void Restore<T>(DoublyLinkedListCell<T> cell)
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
        private static void InsertBefore<T>(DoublyLinkedListCell<T> cell, DoublyLinkedListCell<T> next)
        {
            cell.Previous = next.Previous;
            cell.Next = next;
            Restore(cell);
        }


        /// <summary>
        /// Gets a circular doubly-linked list containing all the values for the puzzle.
        /// </summary>

        private void GetAllValues(int row, int column)
        {

            DoublyLinkedListCell<int> header = new DoublyLinkedListCell<int>();
            header.Data = 0;

            _headerCells[row, column] = header;
            _valueCells[row, column, 0] = header;

            for (int i = 1; i <= _size; i++)
            {
                DoublyLinkedListCell<int> cell = new DoublyLinkedListCell<int>();
                cell.Data = i;
                InsertBefore(cell, header);


                //At the end of the loop, store cell in the value cell store. 

                // WHAT DO I DO HERE ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

                _valueCells[row, column, i] = cell; // THIS?!?!?

                //Then create a DoublyLinkedListCell<Location> and store within it the
                //location described by the parameters to this method.

                DoublyLinkedListCell<Location> rowLocs = new DoublyLinkedListCell<Location>();
                rowLocs.Data = new Location(row, column);


                //Insert this cell at the end of the available locations
                //list for the row containing this location and the value i.
                _availableRowLocations[row, i] = rowLocs;

                //Also store this cell in the location cell store for this list.

                //In a similar way, add cells to record that the value i is available in the row and
                //block containing the location described by the parameters to this method.
                //Thus, you will construct a total of three 

                DoublyLinkedListCell<Location> columnLocs = new DoublyLinkedListCell<Location>();
                columnLocs.Data = new Location(row, column);

                _availableColumnLocations[column, i] = columnLocs;

                DoublyLinkedListCell<Location> blockLocs = new DoublyLinkedListCell<Location>();
                blockLocs.Data = new Location(row, column);


                _avaliableBlockLocations[row / _blockSize, column / _blockSize, i] = blockLocs;
            }

            if (Puzzle[row, column] == 0)
            {
                _priorityQueue.Add(_size, new Location(row, column));
            }
        }


        /// <summary>
        /// A constructor for the puzzleData class
        /// </summary>
        /// <param name="puzzle">the puzzle</param>
        public PuzzleData(int[,] puzzle)
        {
            Puzzle = puzzle;

            //Initialize the int fields based on the size of this array 
            _size = puzzle.GetLength(0);
            _blockSize = (int)Math.Sqrt(_size);

            //Initialize each of the arrays to a new array of the appropriate size.


            _headerCells = new DoublyLinkedListCell<int>[_size, _size];
            _valueCells = new DoublyLinkedListCell<int>[_size, _size, _size + 1]; // check later


            _availableRowLocations = new DoublyLinkedListCell<Location>[_size, _size + 1];
            _availableColumnLocations = new DoublyLinkedListCell<Location>[_size, _size + 1];
            _avaliableBlockLocations = new DoublyLinkedListCell<Location>[_blockSize, _blockSize, _size + 1];

            _rowCellStore = new DoublyLinkedListCell<Location>[_size, _size, _size + 1];
            _columnCellStore = new DoublyLinkedListCell<Location>[_size, _size, _size + 1];
            _blockCellStore = new DoublyLinkedListCell<Location>[_blockSize, _blockSize, _size + 1];


            //Initialize each of the array locations in the three arrays of available location lists to new header cells. (Note that these header cells will not be stored in the location cell stores.)

            for (int i = 0; i < _size; i++)
            {
                _availableRowLocations[i, 0] = new DoublyLinkedListCell<Location>();
                _availableRowLocations[i, 0] = new DoublyLinkedListCell<Location>();
            }

            for (int i = 0; i < _blockSize; i++)
            {
                for (int j = 0; j < _blockSize; j++)
                {
                    _avaliableBlockLocations[i, j, 0] = new DoublyLinkedListCell<Location>();
                }
            }

            //Initialize all of the lists and the min-priority queue by calling the above method for each puzzle location.
            //After this is done, the doubly-linked lists will have been initialized as if the puzzle had no initial values,
            //and the min-priority queue will contain all locations that are actually empty in the puzzle.
            //Each priority in the queue will be as if all values were available for that location.
            for( int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    GetAllValues(i, j);
                }
            }

            RemoveFixedValues(puzzle);


        }

        /// <summary>
        /// removes the fixed values in the puzzle
        /// </summary>
        /// <param name="puzzle">the puzzle</param>
        /// <exception cref="ArgumentException"></exception>
        private void RemoveFixedValues(int[,] puzzle)
        {
            int fixedValue;
            int blockRow;
            int blockColumn;

            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    if (puzzle[i, j] != 0)
                    {
                        fixedValue = puzzle[i, j];

                        if (_valueCells[i, j, fixedValue].Previous == _valueCells[i, j, fixedValue])
                        {
                            
                            
                            RemoveFromCellStore(i, j, fixedValue);


                            blockRow = i / _blockSize;
                            blockColumn = j / _blockSize;

                            for (int k = 0; k < _size; k++)
                            {
                                RemoveFromCellStore(i, k, fixedValue);
                            }

                            for (int k = blockRow * _blockSize; k < _blockSize; k++)
                            {
                                for (int l = blockColumn * _blockSize; l < _blockSize; l++)
                                {
                                    RemoveFromCellStore(k, l, fixedValue);
                                }
                            }
                        }
                        else
                        {
                            throw new ArgumentException();
                        }
                    }
                }

            }

        }

        /// <summary>
        /// implements step b in 5.5.7
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="fixedValue"></param>
        private void RemoveFromCellStore(int row, int column, int fixedValue)
        {
            Remove(_valueCells[row, column, fixedValue]);

            _priorityQueue.DecreasePriority(new Location(row, column));

            Remove(_rowCellStore[row, column, fixedValue]);
            Remove(_columnCellStore[row, column, fixedValue]);
            Remove(_blockCellStore[row / _blockSize, column / _blockSize, fixedValue]);
        }

        /// <summary>
        /// removes all cells but the header cell on a linked list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="header">the headercell</param>
        private void RemoveFullLinkedList<T>(DoublyLinkedListCell<T> header)
        {
            DoublyLinkedListCell<T> cell = header;
            while (cell.Next != header)
            {
                cell = cell.Next;                       // CHEK LOGIC LATER
                Remove(cell);
            }
        }

        /// <summary>
        /// gets the next epmty location and puts it on the stack
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public void GetEmptyLocation()
        {
            if (_priorityQueue.Count == 0)
            {
                throw new InvalidOperationException();
            }

            Location topLoc = _stack.Peek().Location;

            if (_stack.Count > 0 && Puzzle[topLoc.Row, topLoc.Column] != 0)
            {
                throw new InvalidOperationException();
            }

            int priority = _priorityQueue.MinimumPriority;
            Location activeLoc = _priorityQueue.RemoveMinimumPriority();


            AssignmentData assignmentData = new AssignmentData(activeLoc, priority);
            _stack.Push(assignmentData);

            //remove cell from avalible lists

            RemoveFullLinkedList(_headerCells[activeLoc.Row, activeLoc.Column]);

            int currentValue = Puzzle[activeLoc.Row, activeLoc.Column];
            for (int i = 0; i < _size; i++)
            {


                RemoveFullLinkedList(_availableRowLocations[activeLoc.Row, currentValue]);
                RemoveFullLinkedList(_availableColumnLocations[activeLoc.Column, currentValue]);
                RemoveFullLinkedList(_avaliableBlockLocations[activeLoc.Row, activeLoc.Column, currentValue]);


            }
        }


        /// <summary>
        /// tries the next value
        /// </summary>
        /// <returns>if there was a value to try</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public bool TryNextValue()
        {
            if (_stack.Count > 0)
            {
                Location activeLoc = _stack.Peek().Location;
                int currentValue = Puzzle[activeLoc.Row, activeLoc.Column];
                DoublyLinkedListCell<int> cell = _headerCells[activeLoc.Row, activeLoc.Column];

                DoublyLinkedListCell<int> header = cell;

                if (cell.Next == cell)//If this cell is the last cell in its list, return false, as there are no more values to try
                {
                    return false;
                }
                else //Otherwise, get the next cell, and get the Stack<Location> from the top of the Stack<AssignmentData>. 
                {
                    cell = cell.Next;
                    Stack<Location> madeUnavaliable = _stack.Peek().MadeUnavaliable;

                    while (madeUnavaliable.Count > 0)
                    {
                        Location loc = madeUnavaliable.Pop();

                        RestoreFromCellStore(loc.Row, loc.Column, currentValue);
                    }

                    Puzzle[activeLoc.Row, activeLoc.Column] = cell.Data; //once the above loop finishes, set the active puzzle location to the value in the value cell obtained above (i.e., the next unused value). 



                    RemoveFullLinkedList(_headerCells[activeLoc.Row, activeLoc.Column]);



                    RemoveFullLinkedList(_availableRowLocations[activeLoc.Row, currentValue]);
                    RemoveFullLinkedList(_availableColumnLocations[activeLoc.Column, currentValue]);
                    RemoveFullLinkedList(_avaliableBlockLocations[activeLoc.Row, activeLoc.Column, currentValue]);

                    madeUnavaliable.Push(activeLoc);


                    return true;
                }
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// implements step 3 in 5.5.9
        /// </summary>
        /// <param name="row">the row</param>
        /// <param name="column">the column</param>
        /// <param name="fixedValue">the value to store</param>
        private void RestoreFromCellStore(int row, int column, int fixedValue)
        {
            Restore(_valueCells[row, column, fixedValue]);

            Restore(_rowCellStore[row, column, fixedValue]);
            Restore(_columnCellStore[row, column, fixedValue]);
            Restore(_blockCellStore[row / _blockSize, column / _blockSize, fixedValue]);

            _priorityQueue.IncreasePriority(new Location(row, column));
        }


        /// <summary>
        /// a method to undo the last edits
        /// </summary>
        /// <returns>if it was able to undo</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public bool Undo()
        {
            if (_stack.Count > 0) // First, make sure there is an active location - if not, throw an InvalidOperationException.
            {
                AssignmentData assignmentData = _stack.Pop();
                Location activeLoc = assignmentData.Location;

                int currentValue = Puzzle[activeLoc.Row, activeLoc.Column]; // and retrieve the value from the puzzle location stored in this AssignmentData.
                Puzzle[activeLoc.Row, activeLoc.Column] = 0;

                Stack<Location> madeUnavaliable = assignmentData.MadeUnavaliable;

                while (madeUnavaliable.Count > 0) // Then follow the same process as in Step 3 of Section 5.5.9. A public TryNextValue method to restore any cells that were removed when the last value was assigned.
                {
                    Location loc = madeUnavaliable.Pop();

                    RestoreFromCellStore(loc.Row, loc.Column, currentValue);
                }

                DoublyLinkedListCell<int> header = _headerCells[activeLoc.Row, activeLoc.Column];
                DoublyLinkedListCell<int> cell = header;

                while (cell.Previous != header) //ou will then need to iterate backwards through the unused values list for the previously-active location
                {
                    cell = cell.Previous;
                    int fixedValue = cell.Data;

                    // For each value cell in this list, restore the three location cells corresponding to the previously-active location and the value the current value cell.

                    Restore(_valueCells[activeLoc.Row, activeLoc.Column, fixedValue]);
                    Restore(_rowCellStore[activeLoc.Row, activeLoc.Column, fixedValue]);
                    Restore(_columnCellStore[activeLoc.Row, activeLoc.Column, fixedValue]);
                    Restore(_blockCellStore[activeLoc.Row / _blockSize, activeLoc.Column / _blockSize, fixedValue]);
                }
                //Finally, add the previously-active location back to the min-priority queue with its original priority
                //(taken from the AssignmentData), and return whether there is still an active location.

                _priorityQueue.Add(assignmentData.Priority, activeLoc);

                if (_stack.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                throw new InvalidOperationException();
            }
        }



    }

}
