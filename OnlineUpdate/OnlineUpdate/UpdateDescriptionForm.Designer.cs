namespace OnlineUpdate
{
    partial class UpdateDescriptionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateDescriptionForm));
            this.updateVersionLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.descriptionRichTextBox = new System.Windows.Forms.RichTextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.instalNowButton = new System.Windows.Forms.Button();
            this.remindLaterButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // updateVersionLabel
            // 
            this.updateVersionLabel.AutoSize = true;
            this.updateVersionLabel.Font = new System.Drawing.Font("Georgia", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.updateVersionLabel.Location = new System.Drawing.Point(235, 17);
            this.updateVersionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.updateVersionLabel.Name = "updateVersionLabel";
            this.updateVersionLabel.Size = new System.Drawing.Size(56, 18);
            this.updateVersionLabel.TabIndex = 4;
            this.updateVersionLabel.Text = "0.0.0.0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Georgia", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.DarkGreen;
            this.label1.Location = new System.Drawing.Point(50, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(188, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Доступна новая версия:";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.descriptionRichTextBox);
            this.panel1.Font = new System.Drawing.Font("Georgia", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.panel1.Location = new System.Drawing.Point(8, 51);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(473, 110);
            this.panel1.TabIndex = 5;
            // 
            // descriptionRichTextBox
            // 
            this.descriptionRichTextBox.BackColor = System.Drawing.Color.White;
            this.descriptionRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.descriptionRichTextBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.descriptionRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.descriptionRichTextBox.Location = new System.Drawing.Point(0, 0);
            this.descriptionRichTextBox.Name = "descriptionRichTextBox";
            this.descriptionRichTextBox.ReadOnly = true;
            this.descriptionRichTextBox.Size = new System.Drawing.Size(471, 108);
            this.descriptionRichTextBox.TabIndex = 3;
            this.descriptionRichTextBox.Text = "";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(8, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // instalNowButton
            // 
            this.instalNowButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.instalNowButton.Location = new System.Drawing.Point(200, 168);
            this.instalNowButton.Name = "instalNowButton";
            this.instalNowButton.Size = new System.Drawing.Size(137, 23);
            this.instalNowButton.TabIndex = 7;
            this.instalNowButton.Text = "Установить сейчас";
            this.instalNowButton.UseVisualStyleBackColor = true;
            this.instalNowButton.Click += new System.EventHandler(this.instalNowButton_Click);
            // 
            // remindLaterButton
            // 
            this.remindLaterButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.remindLaterButton.Location = new System.Drawing.Point(344, 168);
            this.remindLaterButton.Name = "remindLaterButton";
            this.remindLaterButton.Size = new System.Drawing.Size(137, 23);
            this.remindLaterButton.TabIndex = 8;
            this.remindLaterButton.Text = "Напомнить позже";
            this.remindLaterButton.UseVisualStyleBackColor = true;
            this.remindLaterButton.Click += new System.EventHandler(this.remindLaterButton_Click);
            // 
            // UpdateDescriptionForm
            // 
            this.AcceptButton = this.instalNowButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 196);
            this.Controls.Add(this.remindLaterButton);
            this.Controls.Add(this.instalNowButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.updateVersionLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "UpdateDescriptionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Обновление";
            this.TopMost = true;
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label updateVersionLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button instalNowButton;
        private System.Windows.Forms.Button remindLaterButton;
        private System.Windows.Forms.RichTextBox descriptionRichTextBox;

    }
}