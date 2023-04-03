﻿/* DoublyLinkedListCell.cs
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
    /// A single cell of a doubly-linked list of ints.
    /// </summary>
    public class DoublyLinkedListCell
    {
        /// <summary>
        /// Gets or sets the data stored in this cell.
        /// </summary>
        public int Data { get; set; }

        /// <summary>
        /// Gets or sets the previous cell in the list.
        /// </summary>
        public DoublyLinkedListCell Previous { get; set; }

        /// <summary>
        /// Gets or sets the next cell in the list.
        /// </summary>
        public DoublyLinkedListCell Next { get; set; }

        /// <summary>
        /// Constructs a new cells that forms a circular list by itself.
        /// </summary>
        public DoublyLinkedListCell() 
        {
            Previous = this;
            Next = this;
        }
    }
}
