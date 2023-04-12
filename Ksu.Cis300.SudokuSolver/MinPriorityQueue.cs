/* MinPriorityQueue.cs
 * By: Max Shafer
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Ksu.Cis300.SudokuSolver
{
    /// <summary>
    /// Note that this class should not be generic. 
    /// It needs two private fields, two public properties, a public constructor, 
    /// four public methods, and at least three private methods. The fields, properties,
    /// constructor, and public methods are described in what follows. You will need to decide
    /// how to break the code into at least three additional private methods.
    /// </summary>
    public class MinPriorityQueue
    {
        /// <summary>
        /// giving the binary heap as described in Section 3.2. A Min-Priority Queue.
        /// </summary>
        List<KeyValuePair<int, Location>> _list = new List<KeyValuePair<int, Location>>();

        /// <summary>
        /// mapping each location to its index in the above list.
        /// </summary>
        Dictionary<Location, int> _dictionary = new Dictionary<Location, int>();

        /// <summary>
        /// Gets an int giving the number of Locations in the queue. 
        /// Note that because the list contains a sentinel element, 
        /// its number of elements will not be the number of elements in the queue; 
        /// however, the number of key-value pairs in the dictionary will be.
        /// </summary>
        public int Count { get => _dictionary.Count;}


        /// <summary>
        /// //Gets an int giving the minimum priority of any Location in the queue.
        ///  If the queue is empty, it should throw an InvalidOperationException.
        /// </summary>
        public int MinimumPriority 
        {
            get {
                if (_list.Count > 1)
                {
                    return _list[1].Key; //Gets an int giving the minimum priority of any Location in the queue.
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }        
        }

        /// <summary>
        /// You will need a public constructor that takes no parameters.
        /// It should add the sentinel to the list of key-value pairs. 
        /// This sentinel should be a pair whose key is int.MinValue 
        /// (the smallest possible int value) and whose value is the default.
        /// </summary>
        public MinPriorityQueue()
        {
            _list.Add(new KeyValuePair<int, Location>(int.MinValue, default));
        }



        /// <summary>
        /// Swaps the two index positions, while updating the dict
        /// </summary>
        /// <param name="index1">the first index loc</param>
        /// <param name="index2">the second index loc</param>
        private void Swap(int index1, int index2)
        {
            KeyValuePair<int, Location> temp = _list[index1];


            _list[index1] = _list[index2];
            _dictionary[_list[index2].Value] = index1;
            _list[index2] = temp;
            _dictionary[temp.Value] = index1;

        }

        /// <summary>
        /// This method should take as its parameters an int giving the priority of the element to add
        /// and a Location giving the element to add. It should return nothing. It is responsible for adding 
        /// the given element and priority to the queue as outlined in Section 3.2.1. Adding an element and priority.
        /// Be sure to update the dictionary any time you add to the list or move any of its elements.
        /// </summary>
        /// <param name="priority">the priority</param>
        /// <param name="loc">the location</param>
        public void Add(int priority, Location loc)
        {
            KeyValuePair<int, Location> kvp = new KeyValuePair<int, Location>(priority, loc);

            _list.Add(kvp);
            _dictionary.Add(loc, _list.Count -1 ); // just added -1 ****

            //suffle up
            SiftUp(_list.Count -1);

        }

        /// <summary>
        /// sifts the given position up
        /// </summary>
        /// <param name="i">the given position</param>
        private void SiftUp(int i)
        {
            int currentIndex = i;
            int priority = _list[currentIndex].Key;

            int parentIndex =  currentIndex/ 2;

            while (_list[parentIndex].Key > priority)
            {
                Swap(parentIndex, currentIndex);

                /*
                if (parentIndex * 2 + 1 < _list.Count) // if there are both kids
                {
                    if( _list[parentIndex * 2].Key > _list[parentIndex *2 + 1].Key) //fix prios if off
                    {
                        Swap(parentIndex * 2, parentIndex * 2 + 1);
                    }
                }
                */

                SiftUp(parentIndex);

                currentIndex = parentIndex;
                parentIndex = currentIndex / 2;
            }
        }

        /// <summary>
        /// shuffles down starting at loc i
        /// </summary>
        /// <param name="i">start pos</param>
        private void SiftDown(int i)
        {
            if(_list.Count > 1)
            {
                int currentIndex = i;
                int priority = _list[currentIndex].Key;
                int leftParentIndex = (2 * currentIndex);
                int rightParentIndex = (2 * currentIndex) + 1;

                for (int j = 1; (j * 2) < _list.Count; j++) 
                {
                    if(currentIndex * 2 + 1 < _list.Count)
                    {
                        if (_list[leftParentIndex].Key < _list[rightParentIndex].Key)
                        {
                            if (priority > _list[leftParentIndex].Key)
                            {
                                Swap(currentIndex, leftParentIndex);
                                currentIndex = leftParentIndex;
                            }
                        }
                        else
                        {
                            if (priority > _list[rightParentIndex].Key)
                            {
                                Swap(currentIndex, rightParentIndex);
                                currentIndex = rightParentIndex;
                            }
                        }
                    }
                    else if( currentIndex * 2 <= _list.Count)
                    {

                        if (priority > _list[leftParentIndex].Key)
                        {
                            Swap(currentIndex, leftParentIndex);
                            currentIndex = leftParentIndex;
                        }
                    }
                   
                }
            }
            
        }

        /// <summary>
        /// This method should take no parameters and return the Location removed. Follow the outline given in Section 3.2.2. Removing an element with minimum priority. If the queue is empty, throw an InvalidOperationException. Use the list's RemoveAt method to remove its last location (its Remove method searches the list for the given element, and is therefore too inefficient).
        /// </summary>
        /// <returns>the location removed</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public Location RemoveMinimumPriority()
        {
            if (_list.Count > 1)
            {
                KeyValuePair<int, Location> kvp = _list[_list.Count-1];
                KeyValuePair<int, Location> temp = _list[1];

                _list[1] = kvp; //Instead, we replace the key-value pair at the root with the one in the last location

                _dictionary[kvp.Value] = 1;

                _list.RemoveAt(_list.Count - 1 ); //nd remove the last location. 

                _dictionary.Remove(temp.Value);     /// NOT SURE IF NEEDED JUST TRYING TO KEEP UPDATING DICTIONARY


                // Shuffle down
                SiftDown(1);

                return temp.Value;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// decreases thepriority
        /// </summary>
        /// <param name="loc">which location to decrease</param>
        public void DecreasePriority(Location loc)
        {
            if (_dictionary.TryGetValue(loc, out int index))
            {
                _list[index] = new KeyValuePair<int, Location>(_list[index].Key - 1, _list[index].Value);
                SiftUp(index);
            }
            
        }

        /// <summary>
        /// increases the priority
        /// </summary>
        /// <param name="loc">the loc to increase</param>
        public void IncreasePriority(Location loc)
        {
            if (_dictionary.TryGetValue(loc, out int index))
            {
                _list[index] = new KeyValuePair<int, Location>(_list[index].Key + 1, _list[index].Value);
                SiftDown(index);
            }

        }


    }
}
