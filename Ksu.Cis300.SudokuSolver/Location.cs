/* Location.cs
 * By: Max Shafer
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksu.Cis300.SudokuSolver
{
    /// <summary>
    /// A LOCATION STRUCT
    /// </summary>
    public struct Location
    {
        /// <summary>
        /// Gets an int giving the row of the puzzle location.
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// Gets an int giving the column of the puzzle location.
        /// </summary>
        public int Column { get; set; }

        /// <summary>
        /// You will need a public constructor that takes as its parameters two ints to initialize the above properties.
        /// </summary>
        /// <param name="row">the row</param>
        /// <param name="column">the column</param>
        public Location(int row, int column)
        {
            Row = row;
            Column = column;
        }


        /// <summary>
        /// ovverides the toString method to print correctly
        /// </summary>
        /// <returns>the correct string formatted</returns>
        public override string ToString()
        {
            return ("(" + Row.ToString() + ", " + Column.ToString() + ")" );
        }





    }
}
