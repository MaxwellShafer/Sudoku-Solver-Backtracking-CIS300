namespace Ksu.Cis300.SudokuSolver
{
    partial class UserInterface
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.uxMenuBar = new System.Windows.Forms.MenuStrip();
            this.uxPuzzleMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.uxNew4By4 = new System.Windows.Forms.ToolStripMenuItem();
            this.uxNew9By9 = new System.Windows.Forms.ToolStripMenuItem();
            this.uxSolve = new System.Windows.Forms.ToolStripMenuItem();
            this.uxBoardPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.uxMenuBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // uxMenuBar
            // 
            this.uxMenuBar.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.uxMenuBar.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.uxMenuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uxPuzzleMenu});
            this.uxMenuBar.Location = new System.Drawing.Point(0, 0);
            this.uxMenuBar.Name = "uxMenuBar";
            this.uxMenuBar.Size = new System.Drawing.Size(800, 33);
            this.uxMenuBar.TabIndex = 0;
            this.uxMenuBar.Text = "menuStrip1";
            // 
            // uxPuzzleMenu
            // 
            this.uxPuzzleMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uxNew4By4,
            this.uxNew9By9,
            this.uxSolve});
            this.uxPuzzleMenu.Name = "uxPuzzleMenu";
            this.uxPuzzleMenu.Size = new System.Drawing.Size(77, 29);
            this.uxPuzzleMenu.Text = "Puzzle";
            // 
            // uxNew4By4
            // 
            this.uxNew4By4.Name = "uxNew4By4";
            this.uxNew4By4.Size = new System.Drawing.Size(182, 34);
            this.uxNew4By4.Text = "New 4x4";
            this.uxNew4By4.Click += new System.EventHandler(this.uxNew4By4_Click);
            // 
            // uxNew9By9
            // 
            this.uxNew9By9.Name = "uxNew9By9";
            this.uxNew9By9.Size = new System.Drawing.Size(182, 34);
            this.uxNew9By9.Text = "New 9x9";
            this.uxNew9By9.Click += new System.EventHandler(this.uxNew9By9_Click);
            // 
            // uxSolve
            // 
            this.uxSolve.Name = "uxSolve";
            this.uxSolve.Size = new System.Drawing.Size(182, 34);
            this.uxSolve.Text = "Solve";
            this.uxSolve.Click += new System.EventHandler(this.uxSolve_Click);
            // 
            // uxBoardPanel
            // 
            this.uxBoardPanel.AutoSize = true;
            this.uxBoardPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.uxBoardPanel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.uxBoardPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.uxBoardPanel.Location = new System.Drawing.Point(12, 36);
            this.uxBoardPanel.MinimumSize = new System.Drawing.Size(50, 50);
            this.uxBoardPanel.Name = "uxBoardPanel";
            this.uxBoardPanel.Size = new System.Drawing.Size(50, 50);
            this.uxBoardPanel.TabIndex = 1;
            this.uxBoardPanel.WrapContents = false;
            // 
            // UserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.uxBoardPanel);
            this.Controls.Add(this.uxMenuBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MainMenuStrip = this.uxMenuBar;
            this.MaximizeBox = false;
            this.Name = "UserInterface";
            this.Text = "Sudoku Solver";
            this.Load += new System.EventHandler(this.UserInterface_Load);
            this.uxMenuBar.ResumeLayout(false);
            this.uxMenuBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip uxMenuBar;
        private System.Windows.Forms.ToolStripMenuItem uxPuzzleMenu;
        private System.Windows.Forms.ToolStripMenuItem uxNew4By4;
        private System.Windows.Forms.ToolStripMenuItem uxNew9By9;
        private System.Windows.Forms.ToolStripMenuItem uxSolve;
        private System.Windows.Forms.FlowLayoutPanel uxBoardPanel;
    }
}

