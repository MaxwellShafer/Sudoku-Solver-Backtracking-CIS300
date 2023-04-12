/* AssignmentData.cs
 * By: Max Shafer
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksu.Cis300.SudokuSolver
{
    public struct AssignmentData
    {

        /// <summary>
        ///  Gets a Location giving the puzzle location at which the assignment was made.
        /// </summary>
        public Location Location { get; set; }

        /// <summary>
        ///  Gets an int giving the priority in the MinPriorityQueue of the Location when it was removed from the queue.
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// Gets a Stack<Location> giving the puzzle locations where doubly-linked list cells were removed as a result of this assignment.
        /// </summary>
        public Stack<Location> MadeUnavaliable { get; set; }

        /// <summary>
        /// ou will need a public constructor taking as its parameters a Location and an int to use to initialize the first two properties above. Initialize the stack to an empty stack.
        /// </summary>
        /// <param name="loc">the location</param>
        /// <param name="priority">the prioirty</param>
        public AssignmentData(Location loc, int priority)
        {
            Location = loc;
            Priority = priority;
            MadeUnavaliable = new Stack<Location>();
        }




    }
}
