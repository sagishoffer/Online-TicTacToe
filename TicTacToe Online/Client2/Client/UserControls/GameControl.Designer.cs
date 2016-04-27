namespace Client
{
    partial class GameControl
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
            this.boardElementHost = new System.Windows.Forms.Integration.ElementHost();
            this.leaveBT = new System.Windows.Forms.Button();
            this.rematchBT = new System.Windows.Forms.Button();
            this.infoLB = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // boardElementHost
            // 
            this.boardElementHost.Location = new System.Drawing.Point(0, 41);
            this.boardElementHost.Name = "boardElementHost";
            this.boardElementHost.Size = new System.Drawing.Size(500, 279);
            this.boardElementHost.TabIndex = 3;
            this.boardElementHost.Text = "elementHost1";
            this.boardElementHost.Child = null;
            // 
            // leaveBT
            // 
            this.leaveBT.Location = new System.Drawing.Point(296, 326);
            this.leaveBT.Name = "leaveBT";
            this.leaveBT.Size = new System.Drawing.Size(90, 23);
            this.leaveBT.TabIndex = 8;
            this.leaveBT.Text = "Leave Game";
            this.leaveBT.UseVisualStyleBackColor = true;
            this.leaveBT.Visible = false;
            this.leaveBT.Click += new System.EventHandler(this.leaveBT_Click);
            // 
            // rematchBT
            // 
            this.rematchBT.Location = new System.Drawing.Point(118, 326);
            this.rematchBT.Name = "rematchBT";
            this.rematchBT.Size = new System.Drawing.Size(86, 23);
            this.rematchBT.TabIndex = 7;
            this.rematchBT.Text = "Rematch";
            this.rematchBT.UseVisualStyleBackColor = true;
            this.rematchBT.Visible = false;
            this.rematchBT.Click += new System.EventHandler(this.rematchBT_Click);
            // 
            // infoLB
            // 
            this.infoLB.Dock = System.Windows.Forms.DockStyle.Top;
            this.infoLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.infoLB.Location = new System.Drawing.Point(0, 0);
            this.infoLB.Name = "infoLB";
            this.infoLB.Size = new System.Drawing.Size(500, 47);
            this.infoLB.TabIndex = 9;
            this.infoLB.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // GameControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.infoLB);
            this.Controls.Add(this.leaveBT);
            this.Controls.Add(this.rematchBT);
            this.Controls.Add(this.boardElementHost);
            this.Name = "GameControl";
            this.Size = new System.Drawing.Size(500, 370);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost boardElementHost;
        private System.Windows.Forms.Button leaveBT;
        private System.Windows.Forms.Button rematchBT;
        private System.Windows.Forms.Label infoLB;
    }
}
