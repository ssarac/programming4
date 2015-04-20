namespace windows_form_01.View
{
    partial class DifficultyEditor
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
            this.widthLabel = new System.Windows.Forms.Label();
            this.heightLabel = new System.Windows.Forms.Label();
            this.minesLabel = new System.Windows.Forms.Label();
            this.heightText = new System.Windows.Forms.TextBox();
            this.widthText = new System.Windows.Forms.TextBox();
            this.minesText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // widthLabel
            // 
            this.widthLabel.AutoSize = true;
            this.widthLabel.Location = new System.Drawing.Point(6, 10);
            this.widthLabel.Name = "widthLabel";
            this.widthLabel.Size = new System.Drawing.Size(38, 13);
            this.widthLabel.TabIndex = 0;
            this.widthLabel.Text = "Width:";
            // 
            // heightLabel
            // 
            this.heightLabel.AutoSize = true;
            this.heightLabel.Location = new System.Drawing.Point(3, 36);
            this.heightLabel.Name = "heightLabel";
            this.heightLabel.Size = new System.Drawing.Size(41, 13);
            this.heightLabel.TabIndex = 1;
            this.heightLabel.Text = "Height:";
            // 
            // minesLabel
            // 
            this.minesLabel.AutoSize = true;
            this.minesLabel.Location = new System.Drawing.Point(6, 62);
            this.minesLabel.Name = "minesLabel";
            this.minesLabel.Size = new System.Drawing.Size(38, 13);
            this.minesLabel.TabIndex = 2;
            this.minesLabel.Text = "Mines:";
            // 
            // heightText
            // 
            this.heightText.Location = new System.Drawing.Point(50, 33);
            this.heightText.Name = "heightText";
            this.heightText.Size = new System.Drawing.Size(52, 20);
            this.heightText.TabIndex = 3;
            // 
            // widthText
            // 
            this.widthText.Location = new System.Drawing.Point(50, 7);
            this.widthText.Name = "widthText";
            this.widthText.Size = new System.Drawing.Size(52, 20);
            this.widthText.TabIndex = 5;
            // 
            // minesText
            // 
            this.minesText.Location = new System.Drawing.Point(50, 59);
            this.minesText.Name = "minesText";
            this.minesText.Size = new System.Drawing.Size(52, 20);
            this.minesText.TabIndex = 6;
            // 
            // DifficultyEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.minesText);
            this.Controls.Add(this.widthText);
            this.Controls.Add(this.heightText);
            this.Controls.Add(this.minesLabel);
            this.Controls.Add(this.heightLabel);
            this.Controls.Add(this.widthLabel);
            this.Name = "DifficultyEditor";
            this.Size = new System.Drawing.Size(110, 86);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label widthLabel;
        private System.Windows.Forms.Label heightLabel;
        private System.Windows.Forms.Label minesLabel;
        private System.Windows.Forms.TextBox heightText;
        private System.Windows.Forms.TextBox widthText;
        private System.Windows.Forms.TextBox minesText;
    }
}
