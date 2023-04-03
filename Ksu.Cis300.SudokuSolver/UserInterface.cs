/* UserInterface.cs
 * Author: Rod Howell
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ksu.Cis300.SudokuSolver
{
    /// <summary>
    /// A GUI for a Sudoku solver.
    /// </summary>
    public partial class UserInterface : Form
    {
        /// <summary>
        /// The number of rows in a block for a small puzzle.
        /// </summary>
        private const int _smallBlockSize = 2;

        /// <summary>
        /// The number of rows in a block for a large puzzle.
        /// </summary>
        private const int _largeBlockSize = 3;

        /// <summary>
        /// The space around each block.
        /// </summary>
        private const int _blockMargin = 1;

        /// <summary>
        /// The font size for the cells in the puzzle
        /// </summary>
        private const int _fontSize = 18;

        /// <summary>
        /// The font to use for boldface.
        /// </summary>
        private readonly Font _boldFont = new Font(FontFamily.GenericSerif, _fontSize, FontStyle.Bold);

        /// <summary>
        /// The font to use for normal face.
        /// </summary>
        private readonly Font _normalFont = new Font(FontFamily.GenericSerif, _fontSize);

        /// <summary>
        /// The cells on the board.
        /// </summary>
        private TextBox[,] _cells;

        /// <summary>
        /// The puzzle. Locations containing 0 are treated as being empty.
        /// </summary>
        private int[,] _puzzle;

        /// <summary>
        /// Constructs the GUI.
        /// </summary>
        public UserInterface()
        {
            InitializeComponent();
            NewPuzzle(_largeBlockSize);
        }

        /// <summary>
        /// Handles a TextChanged event on a cell.
        /// </summary>
        /// <param name="sender">The cell signaling the event.</param>
        /// <param name="e"></param>
        private void CellTextChanged(object sender, EventArgs e)
        {
            TextBox b = (TextBox)sender;
            string text = b.Text;
            int i = b.Name[0] - '0';
            int j = b.Name[1] - '0';
            if (text.Length == 0)
            {
                _puzzle[i, j] = 0;
            }
            else if (text.Length == 1 && text[0] >= '1' && text[0] <= _puzzle.GetLength(0) + '0')
            {
                _puzzle[i, j] = Convert.ToInt32(text);
            }
            else if (_puzzle[i, j] == 0)
            {
                _cells[i, j].Text = "";
            }
            else
            {
                _cells[i, j].Text = _puzzle[i, j].ToString();
            }
        }

        /// <summary>
        /// Adds the panels to the form.
        /// </summary>
        /// <param name="blockSize">The number of rows in a block.</param>
        private void AddPanels(int blockSize)
        {
            uxBoardPanel.Controls.Clear();
            for (int i = 0; i < blockSize; i++)
            {
                FlowLayoutPanel row = new FlowLayoutPanel();
                row.AutoSize = true;
                row.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                row.WrapContents = false;
                row.Margin = new Padding(0);
                uxBoardPanel.Controls.Add(row);
                for (int j = 0; j < blockSize; j++)
                {
                    FlowLayoutPanel p = new FlowLayoutPanel();
                    p.Margin = new Padding(_blockMargin);
                    row.Controls.Add(p);
                }
            }
        }

        /// <summary>
        /// Add the cells to the GUI and to _cells, and initializes _puzzle.
        /// Assumes the block panels have been added to the GUI.
        /// </summary>
        /// <param name="blockSize">The number of rows in a block.</param>
        private void AddCells(int blockSize)
        {
            int boardSize = blockSize * blockSize;
            _cells = new TextBox[boardSize, boardSize];
            _puzzle = new int[boardSize, boardSize];
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    _cells[i, j] = new TextBox();
                    _cells[i, j].Font = _boldFont;
                    _cells[i, j].Width = _cells[i, j].Height;
                    _cells[i, j].Margin = new Padding(0);
                    _cells[i, j].TextAlign = HorizontalAlignment.Center;
                    _cells[i, j].Name = i.ToString() + j.ToString();
                    _cells[i, j].TextChanged += new EventHandler(CellTextChanged);
                    uxBoardPanel.Controls[i / blockSize].Controls[j / blockSize].Controls.Add(_cells[i, j]);
                }
            }
        }

        /// <summary>
        /// Resizes the block panels appropriately.
        /// Assumes the block panels have been filled and added to the GUI.
        /// </summary>
        /// <param name="blockSize">The number of rows in a block.</param>
        private void ResizePanels(int blockSize)
        {
            foreach (Control row in uxBoardPanel.Controls)
            {
                foreach (Control block in row.Controls)
                {
                    int size = blockSize * block.Controls[0].Height;
                    block.Size = new Size(size, size);
                }
            }
        }

        /// <summary>
        /// Sets up a new puzzle with the given number of rows in each block.
        /// </summary>
        /// <param name="blockSize">The number of rows in each block.</param>
        private void NewPuzzle(int blockSize)
        {
            uxBoardPanel.Visible = false;
            AddPanels(blockSize);
            AddCells(blockSize);
            ResizePanels(blockSize);
            uxSolve.Enabled = true;
            uxBoardPanel.Enabled = true;
            uxBoardPanel.Visible = true;
        }

        /// <summary>
        /// Handles a Click event on the "New 4x4" menu item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxNew4By4_Click(object sender, EventArgs e)
        {
            NewPuzzle(_smallBlockSize);
        }

        /// <summary>
        /// Handles a Click event on the "New 9x9" menu item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxNew9By9_Click(object sender, EventArgs e)
        {
            NewPuzzle(_largeBlockSize);
        }

        /// <summary>
        /// Places a solution in the GUI.
        /// </summary>
        private void FillSolution()
        {
            for (int i = 0; i < _puzzle.GetLength(0); i++)
            {
                for (int j = 0; j < _puzzle.GetLength(1); j++)
                {
                    if (_cells[i, j].Text == "")
                    {
                        _cells[i, j].Text = _puzzle[i, j].ToString();
                        _cells[i, j].Font = _normalFont;
                    }
                }
            }
        }

        /// <summary>
        /// Handles a Click event on the "Solve" menu item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxSolve_Click(object sender, EventArgs e)
        {
            if (!Solver.Solve(_puzzle))
            {
                MessageBox.Show("No solution found.");
            }
            else
            {
                FillSolution();
            }
            uxSolve.Enabled = false;
            uxBoardPanel.Enabled = false;
        }
    }
}
