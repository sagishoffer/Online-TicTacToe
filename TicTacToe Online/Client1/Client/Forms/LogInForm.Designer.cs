namespace Client
{
    partial class LogInForm
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
            this.userNameTB = new System.Windows.Forms.TextBox();
            this.userNameLB = new System.Windows.Forms.Label();
            this.loginBT = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // userNameTB
            // 
            this.userNameTB.Location = new System.Drawing.Point(144, 40);
            this.userNameTB.Name = "userNameTB";
            this.userNameTB.Size = new System.Drawing.Size(100, 20);
            this.userNameTB.TabIndex = 11;
            this.userNameTB.Validating += new System.ComponentModel.CancelEventHandler(this.userNameTB_Validating);
            // 
            // userNameLB
            // 
            this.userNameLB.AutoSize = true;
            this.userNameLB.Location = new System.Drawing.Point(43, 43);
            this.userNameLB.Name = "userNameLB";
            this.userNameLB.Size = new System.Drawing.Size(67, 13);
            this.userNameLB.TabIndex = 8;
            this.userNameLB.Text = "User Name *";
            // 
            // loginBT
            // 
            this.loginBT.Location = new System.Drawing.Point(100, 99);
            this.loginBT.Name = "loginBT";
            this.loginBT.Size = new System.Drawing.Size(75, 23);
            this.loginBT.TabIndex = 7;
            this.loginBT.Text = "Log In";
            this.loginBT.UseVisualStyleBackColor = true;
            this.loginBT.Click += new System.EventHandler(this.logInBT_Click);
            // 
            // LogInForm
            // 
            this.AcceptButton = this.loginBT;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 147);
            this.Controls.Add(this.userNameTB);
            this.Controls.Add(this.userNameLB);
            this.Controls.Add(this.loginBT);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "LogInForm";
            this.Text = "Log In";
            this.Load += new System.EventHandler(this.LogInForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox userNameTB;
        private System.Windows.Forms.Label userNameLB;
        private System.Windows.Forms.Button loginBT;
    }
}