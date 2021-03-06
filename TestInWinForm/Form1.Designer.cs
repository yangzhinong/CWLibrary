
namespace TestInWinForm
{
    partial class Form1
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
            this.BtnPlay = new System.Windows.Forms.Button();
            this.LblPlayingTip = new System.Windows.Forms.Label();
            this.Txt1 = new System.Windows.Forms.TextBox();
            this.TxtDotLine = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // BtnPlay
            // 
            this.BtnPlay.Location = new System.Drawing.Point(21, 121);
            this.BtnPlay.Name = "BtnPlay";
            this.BtnPlay.Size = new System.Drawing.Size(75, 23);
            this.BtnPlay.TabIndex = 0;
            this.BtnPlay.Text = "播放";
            this.BtnPlay.UseVisualStyleBackColor = true;
            this.BtnPlay.Click += new System.EventHandler(this.BtnPlay_Click);
            // 
            // LblPlayingTip
            // 
            this.LblPlayingTip.AutoSize = true;
            this.LblPlayingTip.Location = new System.Drawing.Point(19, 9);
            this.LblPlayingTip.Name = "LblPlayingTip";
            this.LblPlayingTip.Size = new System.Drawing.Size(107, 12);
            this.LblPlayingTip.TabIndex = 1;
            this.LblPlayingTip.Text = "正在播放第*个字符";
            // 
            // Txt1
            // 
            this.Txt1.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Txt1.Location = new System.Drawing.Point(21, 28);
            this.Txt1.Name = "Txt1";
            this.Txt1.Size = new System.Drawing.Size(420, 26);
            this.Txt1.TabIndex = 2;
            this.Txt1.Text = "CW 1234 5678";
            // 
            // TxtDotLine
            // 
            this.TxtDotLine.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TxtDotLine.Location = new System.Drawing.Point(21, 69);
            this.TxtDotLine.Name = "TxtDotLine";
            this.TxtDotLine.Size = new System.Drawing.Size(944, 26);
            this.TxtDotLine.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(987, 450);
            this.Controls.Add(this.TxtDotLine);
            this.Controls.Add(this.Txt1);
            this.Controls.Add(this.LblPlayingTip);
            this.Controls.Add(this.BtnPlay);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnPlay;
        private System.Windows.Forms.Label LblPlayingTip;
        private System.Windows.Forms.TextBox Txt1;
        private System.Windows.Forms.TextBox TxtDotLine;
    }
}

