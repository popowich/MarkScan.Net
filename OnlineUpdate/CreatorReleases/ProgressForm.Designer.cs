namespace CreatorReleases
{
    partial class ProgressForm
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
            this.processProgressBar = new System.Windows.Forms.ProgressBar();
            this.prcTextBox = new System.Windows.Forms.TextBox();
            this.fileTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // processProgressBar
            // 
            this.processProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.processProgressBar.Location = new System.Drawing.Point(13, 38);
            this.processProgressBar.Margin = new System.Windows.Forms.Padding(4);
            this.processProgressBar.Name = "processProgressBar";
            this.processProgressBar.Size = new System.Drawing.Size(398, 19);
            this.processProgressBar.TabIndex = 0;
            // 
            // prcTextBox
            // 
            this.prcTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.prcTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.prcTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.prcTextBox.Location = new System.Drawing.Point(313, 16);
            this.prcTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.prcTextBox.Name = "prcTextBox";
            this.prcTextBox.Size = new System.Drawing.Size(97, 15);
            this.prcTextBox.TabIndex = 1;
            this.prcTextBox.Text = "0 %";
            this.prcTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // fileTextBox
            // 
            this.fileTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.fileTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.fileTextBox.Location = new System.Drawing.Point(13, 16);
            this.fileTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.fileTextBox.Name = "fileTextBox";
            this.fileTextBox.Size = new System.Drawing.Size(300, 15);
            this.fileTextBox.TabIndex = 2;
            this.fileTextBox.Text = ". . .";
            // 
            // ProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 69);
            this.Controls.Add(this.fileTextBox);
            this.Controls.Add(this.prcTextBox);
            this.Controls.Add(this.processProgressBar);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ProgressForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Прогресс выполнения";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar processProgressBar;
        private System.Windows.Forms.TextBox prcTextBox;
        private System.Windows.Forms.TextBox fileTextBox;
    }
}