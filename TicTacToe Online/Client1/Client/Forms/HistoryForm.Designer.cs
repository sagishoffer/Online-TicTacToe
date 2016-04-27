namespace Client
{
    partial class HistoryForm
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
            this.gamesCB = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nextBT = new System.Windows.Forms.Button();
            this.prevBT = new System.Windows.Forms.Button();
            this.statusLB = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // elementHost
            // 
            this.elementHost.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.elementHost.Location = new System.Drawing.Point(0, 114);
            this.elementHost.Name = "elementHost";
            this.elementHost.Size = new System.Drawing.Size(435, 270);
            this.elementHost.TabIndex = 0;
            this.elementHost.Text = "elementHost1";
            this.elementHost.Child = null;
            // 
            // gamesCB
            // 
            this.gamesCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gamesCB.FormattingEnabled = true;
            this.gamesCB.Location = new System.Drawing.Point(98, 19);
            this.gamesCB.Name = "gamesCB";
            this.gamesCB.Size = new System.Drawing.Size(302, 21);
            this.gamesCB.TabIndex = 1;
            this.gamesCB.SelectedIndexChanged += new System.EventHandler(this.gamesCB_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Choose Game:";
            // 
            // nextBT
            // 
            this.nextBT.Location = new System.Drawing.Point(348, 74);
            this.nextBT.Name = "nextBT";
            this.nextBT.Size = new System.Drawing.Size(75, 23);
            this.nextBT.TabIndex = 3;
            this.nextBT.Text = "Next Step";
            this.nextBT.UseVisualStyleBackColor = true;
            this.nextBT.Click += new System.EventHandler(this.nextBT_Click);
            // 
            // prevBT
            // 
            this.prevBT.Location = new System.Drawing.Point(12, 74);
            this.prevBT.Name = "prevBT";
            this.prevBT.Size = new System.Drawing.Size(75, 23);
            this.prevBT.TabIndex = 3;
            this.prevBT.Text = "Prev Step";
            this.prevBT.UseVisualStyleBackColor = true;
            this.prevBT.Click += new System.EventHandler(this.prevBT_Click);
            // 
            // statusLB
            // 
            this.statusLB.AllowDrop = true;
            this.statusLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.statusLB.Location = new System.Drawing.Point(93, 51);
            this.statusLB.Name = "statusLB";
            this.statusLB.Size = new System.Drawing.Size(249, 62);
            this.statusLB.TabIndex = 4;
            this.statusLB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 384);
            this.Controls.Add(this.statusLB);
            this.Controls.Add(this.prevBT);
            this.Controls.Add(this.nextBT);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gamesCB);
            this.Controls.Add(this.elementHost);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "HistoryForm";
            this.Text = "HistoryForm";
            this.Load += new System.EventHandler(this.HistoryForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost elementHost;
        private System.Windows.Forms.ComboBox gamesCB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button nextBT;
        private System.Windows.Forms.Button prevBT;
        private System.Windows.Forms.Label statusLB;
    }
}