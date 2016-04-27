namespace Client
{
    partial class QueryForm
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
            this.elementHost = new System.Windows.Forms.Integration.ElementHost();
            this.queryCB = new System.Windows.Forms.ComboBox();
            this.filterCB = new System.Windows.Forms.ComboBox();
            this.filterLB = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.saveBT = new System.Windows.Forms.Button();
            this.delBT = new System.Windows.Forms.Button();
            this.delOpGroup = new System.Windows.Forms.GroupBox();
            this.oneRowRB = new System.Windows.Forms.RadioButton();
            this.multiRowRB = new System.Windows.Forms.RadioButton();
            this.queryOpGroup = new System.Windows.Forms.GroupBox();
            this.queryDescLB = new System.Windows.Forms.Label();
            this.delayCB = new System.Windows.Forms.CheckBox();
            this.delOpGroup.SuspendLayout();
            this.queryOpGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // elementHost
            // 
            this.elementHost.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.elementHost.Location = new System.Drawing.Point(0, 163);
            this.elementHost.Name = "elementHost";
            this.elementHost.Size = new System.Drawing.Size(599, 328);
            this.elementHost.TabIndex = 0;
            this.elementHost.Text = "elementHost";
            this.elementHost.Child = null;
            // 
            // queryCB
            // 
            this.queryCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.queryCB.FormattingEnabled = true;
            this.queryCB.Items.AddRange(new object[] {
            "Q1",
            "Q2",
            "Q3",
            "Q4",
            "Q5",
            "Q6",
            "Q7",
            "Q8",
            "Q9",
            "Q10"});
            this.queryCB.Location = new System.Drawing.Point(63, 19);
            this.queryCB.Name = "queryCB";
            this.queryCB.Size = new System.Drawing.Size(225, 21);
            this.queryCB.TabIndex = 1;
            this.queryCB.SelectedIndexChanged += new System.EventHandler(this.queryCB_SelectedIndexChanged);
            // 
            // filterCB
            // 
            this.filterCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filterCB.Enabled = false;
            this.filterCB.FormattingEnabled = true;
            this.filterCB.Location = new System.Drawing.Point(63, 46);
            this.filterCB.Name = "filterCB";
            this.filterCB.Size = new System.Drawing.Size(225, 21);
            this.filterCB.TabIndex = 1;
            this.filterCB.SelectedIndexChanged += new System.EventHandler(this.filterCB_SelectedIndexChanged);
            // 
            // filterLB
            // 
            this.filterLB.AutoSize = true;
            this.filterLB.Location = new System.Drawing.Point(19, 50);
            this.filterLB.Name = "filterLB";
            this.filterLB.Size = new System.Drawing.Size(32, 13);
            this.filterLB.TabIndex = 2;
            this.filterLB.Text = "Filter:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Query:";
            // 
            // saveBT
            // 
            this.saveBT.Location = new System.Drawing.Point(498, 55);
            this.saveBT.Name = "saveBT";
            this.saveBT.Size = new System.Drawing.Size(75, 23);
            this.saveBT.TabIndex = 3;
            this.saveBT.Text = "Save";
            this.saveBT.UseVisualStyleBackColor = true;
            this.saveBT.Click += new System.EventHandler(this.saveBT_Click);
            // 
            // delBT
            // 
            this.delBT.Location = new System.Drawing.Point(498, 84);
            this.delBT.Name = "delBT";
            this.delBT.Size = new System.Drawing.Size(75, 23);
            this.delBT.TabIndex = 3;
            this.delBT.Text = "Delete";
            this.delBT.UseVisualStyleBackColor = true;
            this.delBT.Click += new System.EventHandler(this.delBT_Click);
            // 
            // delOpGroup
            // 
            this.delOpGroup.Controls.Add(this.oneRowRB);
            this.delOpGroup.Controls.Add(this.multiRowRB);
            this.delOpGroup.Location = new System.Drawing.Point(341, 34);
            this.delOpGroup.Name = "delOpGroup";
            this.delOpGroup.Size = new System.Drawing.Size(145, 73);
            this.delOpGroup.TabIndex = 4;
            this.delOpGroup.TabStop = false;
            this.delOpGroup.Text = "Delete Options";
            // 
            // oneRowRB
            // 
            this.oneRowRB.AutoSize = true;
            this.oneRowRB.Checked = true;
            this.oneRowRB.Location = new System.Drawing.Point(24, 21);
            this.oneRowRB.Name = "oneRowRB";
            this.oneRowRB.Size = new System.Drawing.Size(70, 17);
            this.oneRowRB.TabIndex = 1;
            this.oneRowRB.TabStop = true;
            this.oneRowRB.Text = "One Row";
            this.oneRowRB.UseVisualStyleBackColor = true;
            this.oneRowRB.CheckedChanged += new System.EventHandler(this.oneRowRB_CheckedChanged);
            // 
            // multiRowRB
            // 
            this.multiRowRB.AutoSize = true;
            this.multiRowRB.Location = new System.Drawing.Point(24, 48);
            this.multiRowRB.Name = "multiRowRB";
            this.multiRowRB.Size = new System.Drawing.Size(72, 17);
            this.multiRowRB.TabIndex = 0;
            this.multiRowRB.TabStop = true;
            this.multiRowRB.Text = "Multi Row";
            this.multiRowRB.UseVisualStyleBackColor = true;
            this.multiRowRB.CheckedChanged += new System.EventHandler(this.multiRowRB_CheckedChanged);
            // 
            // queryOpGroup
            // 
            this.queryOpGroup.Controls.Add(this.queryCB);
            this.queryOpGroup.Controls.Add(this.filterCB);
            this.queryOpGroup.Controls.Add(this.label2);
            this.queryOpGroup.Controls.Add(this.filterLB);
            this.queryOpGroup.Location = new System.Drawing.Point(31, 33);
            this.queryOpGroup.Name = "queryOpGroup";
            this.queryOpGroup.Size = new System.Drawing.Size(299, 74);
            this.queryOpGroup.TabIndex = 5;
            this.queryOpGroup.TabStop = false;
            this.queryOpGroup.Text = "Query Options";
            // 
            // queryDescLB
            // 
            this.queryDescLB.Font = new System.Drawing.Font("Arial", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.queryDescLB.Location = new System.Drawing.Point(37, 125);
            this.queryDescLB.Name = "queryDescLB";
            this.queryDescLB.Size = new System.Drawing.Size(536, 23);
            this.queryDescLB.TabIndex = 6;
            this.queryDescLB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // delayCB
            // 
            this.delayCB.AutoSize = true;
            this.delayCB.Location = new System.Drawing.Point(509, 32);
            this.delayCB.Name = "delayCB";
            this.delayCB.Size = new System.Drawing.Size(53, 17);
            this.delayCB.TabIndex = 7;
            this.delayCB.Text = "Delay";
            this.delayCB.UseVisualStyleBackColor = true;
            // 
            // QueryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 491);
            this.Controls.Add(this.delayCB);
            this.Controls.Add(this.queryDescLB);
            this.Controls.Add(this.queryOpGroup);
            this.Controls.Add(this.delOpGroup);
            this.Controls.Add(this.delBT);
            this.Controls.Add(this.saveBT);
            this.Controls.Add(this.elementHost);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "QueryForm";
            this.Text = "QueryForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.QueryForm_FormClosing);
            this.Load += new System.EventHandler(this.QueryForm_Load);
            this.delOpGroup.ResumeLayout(false);
            this.delOpGroup.PerformLayout();
            this.queryOpGroup.ResumeLayout(false);
            this.queryOpGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost elementHost;
        private System.Windows.Forms.ComboBox queryCB;
        private System.Windows.Forms.ComboBox filterCB;
        private System.Windows.Forms.Label filterLB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button saveBT;
        private System.Windows.Forms.Button delBT;
        private System.Windows.Forms.GroupBox delOpGroup;
        private System.Windows.Forms.RadioButton oneRowRB;
        private System.Windows.Forms.RadioButton multiRowRB;
        private System.Windows.Forms.GroupBox queryOpGroup;
        private System.Windows.Forms.Label queryDescLB;
        private System.Windows.Forms.CheckBox delayCB;



    }
}