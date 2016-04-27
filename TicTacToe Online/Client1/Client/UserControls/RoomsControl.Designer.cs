namespace Client
{
    partial class RoomsControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.RoomsPanel = new System.Windows.Forms.SplitContainer();
            this.boardsListBox = new System.Windows.Forms.ListBox();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.playerRB = new System.Windows.Forms.RadioButton();
            this.computerRB = new System.Windows.Forms.RadioButton();
            this.playBT = new System.Windows.Forms.Button();
            this.descPanel = new System.Windows.Forms.TableLayoutPanel();
            this.boardNameLB = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.boardSizeLB = new System.Windows.Forms.Label();
            this.numberOfPlayersLB = new System.Windows.Forms.Label();
            this.descLB = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.RoomsPanel)).BeginInit();
            this.RoomsPanel.Panel1.SuspendLayout();
            this.RoomsPanel.Panel2.SuspendLayout();
            this.RoomsPanel.SuspendLayout();
            this.groupBox.SuspendLayout();
            this.descPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // RoomsPanel
            // 
            this.RoomsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RoomsPanel.Location = new System.Drawing.Point(0, 0);
            this.RoomsPanel.Name = "RoomsPanel";
            // 
            // RoomsPanel.Panel1
            // 
            this.RoomsPanel.Panel1.Controls.Add(this.boardsListBox);
            this.RoomsPanel.Panel1.Controls.Add(this.groupBox);
            this.RoomsPanel.Panel1.Padding = new System.Windows.Forms.Padding(5);
            // 
            // RoomsPanel.Panel2
            // 
            this.RoomsPanel.Panel2.Controls.Add(this.playBT);
            this.RoomsPanel.Panel2.Controls.Add(this.descPanel);
            this.RoomsPanel.Size = new System.Drawing.Size(500, 370);
            this.RoomsPanel.SplitterDistance = 166;
            this.RoomsPanel.TabIndex = 8;
            // 
            // boardsListBox
            // 
            this.boardsListBox.FormattingEnabled = true;
            this.boardsListBox.Location = new System.Drawing.Point(8, 89);
            this.boardsListBox.Name = "boardsListBox";
            this.boardsListBox.Size = new System.Drawing.Size(140, 238);
            this.boardsListBox.TabIndex = 3;
            this.boardsListBox.SelectedValueChanged += new System.EventHandler(this.boardsListBox_SelectedValueChanged);
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.playerRB);
            this.groupBox.Controls.Add(this.computerRB);
            this.groupBox.Location = new System.Drawing.Point(8, 18);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(140, 65);
            this.groupBox.TabIndex = 2;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Game Mode";
            // 
            // playerRB
            // 
            this.playerRB.AutoSize = true;
            this.playerRB.Location = new System.Drawing.Point(6, 42);
            this.playerRB.Name = "playerRB";
            this.playerRB.Size = new System.Drawing.Size(77, 17);
            this.playerRB.TabIndex = 2;
            this.playerRB.TabStop = true;
            this.playerRB.Text = "vs. Player2";
            this.playerRB.UseVisualStyleBackColor = true;
            this.playerRB.CheckedChanged += new System.EventHandler(this.getBoardsListBox);
            // 
            // computerRB
            // 
            this.computerRB.AutoSize = true;
            this.computerRB.Checked = true;
            this.computerRB.Location = new System.Drawing.Point(6, 19);
            this.computerRB.Name = "computerRB";
            this.computerRB.Size = new System.Drawing.Size(87, 17);
            this.computerRB.TabIndex = 1;
            this.computerRB.TabStop = true;
            this.computerRB.Text = "vs. Computer";
            this.computerRB.UseVisualStyleBackColor = true;
            this.computerRB.CheckedChanged += new System.EventHandler(this.getBoardsListBox);
            // 
            // playBT
            // 
            this.playBT.Location = new System.Drawing.Point(131, 265);
            this.playBT.Name = "playBT";
            this.playBT.Size = new System.Drawing.Size(75, 23);
            this.playBT.TabIndex = 2;
            this.playBT.Text = "Enter Room";
            this.playBT.UseVisualStyleBackColor = true;
            this.playBT.Click += new System.EventHandler(this.playBT_Click);
            // 
            // descPanel
            // 
            this.descPanel.ColumnCount = 2;
            this.descPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.descPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.descPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.descPanel.Controls.Add(this.boardNameLB, 1, 0);
            this.descPanel.Controls.Add(this.label3, 0, 0);
            this.descPanel.Controls.Add(this.label4, 0, 3);
            this.descPanel.Controls.Add(this.label1, 0, 1);
            this.descPanel.Controls.Add(this.label2, 0, 2);
            this.descPanel.Controls.Add(this.boardSizeLB, 1, 1);
            this.descPanel.Controls.Add(this.numberOfPlayersLB, 1, 2);
            this.descPanel.Controls.Add(this.descLB, 1, 3);
            this.descPanel.Location = new System.Drawing.Point(51, 61);
            this.descPanel.Name = "descPanel";
            this.descPanel.RowCount = 4;
            this.descPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.descPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.descPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.descPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.descPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.descPanel.Size = new System.Drawing.Size(233, 176);
            this.descPanel.TabIndex = 1;
            // 
            // boardNameLB
            // 
            this.boardNameLB.AutoSize = true;
            this.boardNameLB.Location = new System.Drawing.Point(119, 0);
            this.boardNameLB.Name = "boardNameLB";
            this.boardNameLB.Size = new System.Drawing.Size(69, 13);
            this.boardNameLB.TabIndex = 0;
            this.boardNameLB.Text = "Board Name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Board Name:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 132);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Description:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Board Size:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Number of Players:";
            // 
            // boardSizeLB
            // 
            this.boardSizeLB.AutoSize = true;
            this.boardSizeLB.Location = new System.Drawing.Point(119, 44);
            this.boardSizeLB.Name = "boardSizeLB";
            this.boardSizeLB.Size = new System.Drawing.Size(61, 13);
            this.boardSizeLB.TabIndex = 0;
            this.boardSizeLB.Text = "Board Size:";
            // 
            // numberOfPlayersLB
            // 
            this.numberOfPlayersLB.AutoSize = true;
            this.numberOfPlayersLB.Location = new System.Drawing.Point(119, 88);
            this.numberOfPlayersLB.Name = "numberOfPlayersLB";
            this.numberOfPlayersLB.Size = new System.Drawing.Size(96, 13);
            this.numberOfPlayersLB.TabIndex = 0;
            this.numberOfPlayersLB.Text = "Number of Players:";
            // 
            // descLB
            // 
            this.descLB.AutoSize = true;
            this.descLB.Location = new System.Drawing.Point(119, 132);
            this.descLB.Name = "descLB";
            this.descLB.Size = new System.Drawing.Size(63, 13);
            this.descLB.TabIndex = 0;
            this.descLB.Text = "Description:";
            // 
            // RoomsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.RoomsPanel);
            this.Name = "RoomsControl";
            this.Size = new System.Drawing.Size(500, 370);
            this.RoomsPanel.Panel1.ResumeLayout(false);
            this.RoomsPanel.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RoomsPanel)).EndInit();
            this.RoomsPanel.ResumeLayout(false);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.descPanel.ResumeLayout(false);
            this.descPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer RoomsPanel;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.RadioButton playerRB;
        private System.Windows.Forms.RadioButton computerRB;
        public System.Windows.Forms.Button playBT;
        private System.Windows.Forms.TableLayoutPanel descPanel;
        public System.Windows.Forms.Label boardNameLB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label boardSizeLB;
        public System.Windows.Forms.Label numberOfPlayersLB;
        public System.Windows.Forms.Label descLB;
        private System.Windows.Forms.ListBox boardsListBox;
    }
}
