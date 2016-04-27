namespace Client
{
    partial class ChampForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChampForm));
            this.nameChampTB = new System.Windows.Forms.TextBox();
            this.cityChampTB = new System.Windows.Forms.TextBox();
            this.dateChampDP = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.saveChampBT = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.openBT = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // nameChampTB
            // 
            this.nameChampTB.Dock = System.Windows.Forms.DockStyle.Left;
            this.nameChampTB.Location = new System.Drawing.Point(69, 3);
            this.nameChampTB.Name = "nameChampTB";
            this.nameChampTB.Size = new System.Drawing.Size(150, 20);
            this.nameChampTB.TabIndex = 4;
            this.nameChampTB.Validating += new System.ComponentModel.CancelEventHandler(this.nameChampTB_Validating);
            // 
            // cityChampTB
            // 
            this.cityChampTB.Dock = System.Windows.Forms.DockStyle.Left;
            this.cityChampTB.Location = new System.Drawing.Point(69, 38);
            this.cityChampTB.Name = "cityChampTB";
            this.cityChampTB.Size = new System.Drawing.Size(150, 20);
            this.cityChampTB.TabIndex = 4;
            this.cityChampTB.Validating += new System.ComponentModel.CancelEventHandler(this.cityChampTB_Validating);
            // 
            // dateChampDP
            // 
            this.dateChampDP.Dock = System.Windows.Forms.DockStyle.Left;
            this.dateChampDP.Location = new System.Drawing.Point(69, 73);
            this.dateChampDP.Name = "dateChampDP";
            this.dateChampDP.Size = new System.Drawing.Size(150, 20);
            this.dateChampDP.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name *";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(3, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "City";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(3, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Date *";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Location = new System.Drawing.Point(3, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Image";
            // 
            // saveChampBT
            // 
            this.saveChampBT.Location = new System.Drawing.Point(116, 290);
            this.saveChampBT.Name = "saveChampBT";
            this.saveChampBT.Size = new System.Drawing.Size(75, 23);
            this.saveChampBT.TabIndex = 1;
            this.saveChampBT.Text = "Save";
            this.saveChampBT.UseVisualStyleBackColor = true;
            this.saveChampBT.Click += new System.EventHandler(this.saveChampBT_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.23944F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 76.76057F));
            this.tableLayoutPanel2.Controls.Add(this.cityChampTB, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.nameChampTB, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.dateChampDP, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.panel1, 1, 3);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(12, 31);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(284, 237);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox);
            this.panel1.Controls.Add(this.openBT);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(69, 108);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(212, 126);
            this.panel1.TabIndex = 6;
            // 
            // pictureBox
            // 
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox.Image")));
            this.pictureBox.InitialImage = null;
            this.pictureBox.Location = new System.Drawing.Point(0, 3);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(150, 116);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox.TabIndex = 1;
            this.pictureBox.TabStop = false;
            // 
            // openBT
            // 
            this.openBT.Location = new System.Drawing.Point(156, 45);
            this.openBT.Name = "openBT";
            this.openBT.Size = new System.Drawing.Size(46, 23);
            this.openBT.TabIndex = 0;
            this.openBT.Text = "Open";
            this.openBT.UseVisualStyleBackColor = true;
            this.openBT.Click += new System.EventHandler(this.openBT_Click);
            // 
            // ChampForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 325);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.saveChampBT);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ChampForm";
            this.Text = "ChampForm";
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox nameChampTB;
        private System.Windows.Forms.TextBox cityChampTB;
        private System.Windows.Forms.DateTimePicker dateChampDP;
        private System.Windows.Forms.Button saveChampBT;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button openBT;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}