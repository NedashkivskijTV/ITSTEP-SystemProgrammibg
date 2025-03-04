﻿namespace CalcRunner
{
    partial class CalcRunner
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
            this.Start = new System.Windows.Forms.Button();
            this.Stop = new System.Windows.Forms.Button();
            this.myProcess = new System.Diagnostics.Process();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(12, 12);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(182, 23);
            this.Start.TabIndex = 0;
            this.Start.Text = "Start and Wait For App Closing";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // Stop
            // 
            this.Stop.Location = new System.Drawing.Point(37, 75);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(137, 23);
            this.Stop.TabIndex = 1;
            this.Stop.Text = "Start Button";
            this.Stop.UseVisualStyleBackColor = true;
            this.Stop.Click += new System.EventHandler(this.StartAndClose_Click);
            // 
            // myProcess
            // 
            this.myProcess.StartInfo.Domain = "";
            this.myProcess.StartInfo.LoadUserProfile = false;
            this.myProcess.StartInfo.Password = null;
            this.myProcess.StartInfo.StandardErrorEncoding = null;
            this.myProcess.StartInfo.StandardOutputEncoding = null;
            this.myProcess.StartInfo.UserName = "";
            this.myProcess.SynchronizingObject = this;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(37, 104);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(137, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Close Button";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Close_Click);
            // 
            // CalcRunner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(206, 148);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Stop);
            this.Controls.Add(this.Start);
            this.Name = "CalcRunner";
            this.Text = "App runner";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.Button Stop;
        private System.Diagnostics.Process myProcess;
        private System.Windows.Forms.Button button1;
    }
}

