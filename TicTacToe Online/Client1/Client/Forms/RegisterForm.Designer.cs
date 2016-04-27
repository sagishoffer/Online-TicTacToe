namespace Client
{
    partial class registerForm
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
            this.regBT = new System.Windows.Forms.Button();
            this.userNameLB = new System.Windows.Forms.Label();
            this.firstNameLB = new System.Windows.Forms.Label();
            this.LastNameLB = new System.Windows.Forms.Label();
            this.userNameTB = new System.Windows.Forms.TextBox();
            this.firstNameTB = new System.Windows.Forms.TextBox();
            this.LastNameTB = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.adviserCheckedList = new System.Windows.Forms.CheckedListBox();
            this.rankLB = new System.Windows.Forms.Label();
            this.rankCB = new System.Windows.Forms.ComboBox();
            this.champsCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.editChampBT = new System.Windows.Forms.Button();
            this.addChampBT = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // regBT
            // 
            this.regBT.Location = new System.Drawing.Point(235, 280);
            this.regBT.Name = "regBT";
            this.regBT.Size = new System.Drawing.Size(75, 23);
            this.regBT.TabIndex = 0;
            this.regBT.Text = "Register";
            this.regBT.UseVisualStyleBackColor = true;
            this.regBT.Click += new System.EventHandler(this.regBT_Click);
            // 
            // userNameLB
            // 
            this.userNameLB.AutoSize = true;
            this.userNameLB.Dock = System.Windows.Forms.DockStyle.Top;
            this.userNameLB.Location = new System.Drawing.Point(3, 0);
            this.userNameLB.Name = "userNameLB";
            this.userNameLB.Size = new System.Drawing.Size(68, 13);
            this.userNameLB.TabIndex = 1;
            this.userNameLB.Text = "User Name *";
            // 
            // firstNameLB
            // 
            this.firstNameLB.AutoSize = true;
            this.firstNameLB.Dock = System.Windows.Forms.DockStyle.Top;
            this.firstNameLB.Location = new System.Drawing.Point(3, 35);
            this.firstNameLB.Name = "firstNameLB";
            this.firstNameLB.Size = new System.Drawing.Size(68, 13);
            this.firstNameLB.TabIndex = 2;
            this.firstNameLB.Text = "First Name *";
            // 
            // LastNameLB
            // 
            this.LastNameLB.AutoSize = true;
            this.LastNameLB.Dock = System.Windows.Forms.DockStyle.Top;
            this.LastNameLB.Location = new System.Drawing.Point(3, 70);
            this.LastNameLB.Name = "LastNameLB";
            this.LastNameLB.Size = new System.Drawing.Size(68, 13);
            this.LastNameLB.TabIndex = 3;
            this.LastNameLB.Text = "Last Name";
            // 
            // userNameTB
            // 
            this.userNameTB.Dock = System.Windows.Forms.DockStyle.Left;
            this.userNameTB.Location = new System.Drawing.Point(77, 3);
            this.userNameTB.Name = "userNameTB";
            this.userNameTB.Size = new System.Drawing.Size(132, 20);
            this.userNameTB.TabIndex = 4;
            this.userNameTB.Validating += new System.ComponentModel.CancelEventHandler(this.userNameTB_Validating);
            // 
            // firstNameTB
            // 
            this.firstNameTB.Dock = System.Windows.Forms.DockStyle.Left;
            this.firstNameTB.Location = new System.Drawing.Point(77, 38);
            this.firstNameTB.Name = "firstNameTB";
            this.firstNameTB.Size = new System.Drawing.Size(132, 20);
            this.firstNameTB.TabIndex = 5;
            this.firstNameTB.Validating += new System.ComponentModel.CancelEventHandler(this.firstNameTB_Validating);
            // 
            // LastNameTB
            // 
            this.LastNameTB.Dock = System.Windows.Forms.DockStyle.Left;
            this.LastNameTB.Location = new System.Drawing.Point(77, 73);
            this.LastNameTB.Name = "LastNameTB";
            this.LastNameTB.Size = new System.Drawing.Size(132, 20);
            this.LastNameTB.TabIndex = 6;
            this.LastNameTB.Validating += new System.ComponentModel.CancelEventHandler(this.LastNameTB_Validating);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.32787F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.67213F));
            this.tableLayoutPanel1.Controls.Add(this.userNameLB, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.LastNameTB, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.firstNameLB, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.LastNameLB, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.adviserCheckedList, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.firstNameTB, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.userNameTB, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.rankLB, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.rankCB, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 23);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.67F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(244, 212);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(3, 140);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Advisers";
            // 
            // adviserCheckedList
            // 
            this.adviserCheckedList.Dock = System.Windows.Forms.DockStyle.Left;
            this.adviserCheckedList.FormattingEnabled = true;
            this.adviserCheckedList.HorizontalScrollbar = true;
            this.adviserCheckedList.Location = new System.Drawing.Point(77, 143);
            this.adviserCheckedList.Name = "adviserCheckedList";
            this.adviserCheckedList.Size = new System.Drawing.Size(132, 66);
            this.adviserCheckedList.TabIndex = 8;
            // 
            // rankLB
            // 
            this.rankLB.AutoSize = true;
            this.rankLB.Dock = System.Windows.Forms.DockStyle.Top;
            this.rankLB.Location = new System.Drawing.Point(3, 105);
            this.rankLB.Name = "rankLB";
            this.rankLB.Size = new System.Drawing.Size(68, 13);
            this.rankLB.TabIndex = 9;
            this.rankLB.Text = "Rank";
            // 
            // rankCB
            // 
            this.rankCB.Dock = System.Windows.Forms.DockStyle.Left;
            this.rankCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rankCB.FormattingEnabled = true;
            this.rankCB.Items.AddRange(new object[] {
            "Begginer",
            "Normal",
            "Professional"});
            this.rankCB.Location = new System.Drawing.Point(77, 108);
            this.rankCB.Name = "rankCB";
            this.rankCB.Size = new System.Drawing.Size(132, 21);
            this.rankCB.TabIndex = 10;
            // 
            // champsCheckedListBox
            // 
            this.champsCheckedListBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.champsCheckedListBox.FormattingEnabled = true;
            this.champsCheckedListBox.HorizontalScrollbar = true;
            this.champsCheckedListBox.Location = new System.Drawing.Point(3, 23);
            this.champsCheckedListBox.Name = "champsCheckedListBox";
            this.champsCheckedListBox.Size = new System.Drawing.Size(188, 209);
            this.champsCheckedListBox.TabIndex = 8;
            // 
            // editChampBT
            // 
            this.editChampBT.Location = new System.Drawing.Point(197, 73);
            this.editChampBT.Name = "editChampBT";
            this.editChampBT.Size = new System.Drawing.Size(35, 23);
            this.editChampBT.TabIndex = 9;
            this.editChampBT.Text = "Edit";
            this.editChampBT.UseVisualStyleBackColor = true;
            this.editChampBT.Click += new System.EventHandler(this.editChampBT_Click);
            // 
            // addChampBT
            // 
            this.addChampBT.Location = new System.Drawing.Point(197, 108);
            this.addChampBT.Name = "addChampBT";
            this.addChampBT.Size = new System.Drawing.Size(35, 23);
            this.addChampBT.TabIndex = 9;
            this.addChampBT.Text = "Add";
            this.addChampBT.UseVisualStyleBackColor = true;
            this.addChampBT.Click += new System.EventHandler(this.addChampBT_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.champsCheckedListBox);
            this.groupBox1.Controls.Add(this.addChampBT);
            this.groupBox1.Controls.Add(this.editChampBT);
            this.groupBox1.Location = new System.Drawing.Point(279, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.groupBox1.Size = new System.Drawing.Size(238, 235);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Champions";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel1);
            this.groupBox2.Location = new System.Drawing.Point(12, 25);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.groupBox2.Size = new System.Drawing.Size(250, 238);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Details";
            // 
            // registerForm
            // 
            this.AcceptButton = this.regBT;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 315);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.regBT);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "registerForm";
            this.Text = "Register";
            this.Load += new System.EventHandler(this.registerForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button regBT;
        private System.Windows.Forms.Label userNameLB;
        private System.Windows.Forms.Label firstNameLB;
        private System.Windows.Forms.Label LastNameLB;
        private System.Windows.Forms.TextBox userNameTB;
        private System.Windows.Forms.TextBox firstNameTB;
        private System.Windows.Forms.TextBox LastNameTB;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckedListBox adviserCheckedList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox champsCheckedListBox;
        private System.Windows.Forms.Button editChampBT;
        private System.Windows.Forms.Button addChampBT;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label rankLB;
        private System.Windows.Forms.ComboBox rankCB;
    }
}