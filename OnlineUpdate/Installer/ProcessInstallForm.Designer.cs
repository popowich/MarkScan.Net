namespace Installer
{
    partial class ProcessInstallForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessInstallForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.descriptionesTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.processProgressBar = new System.Windows.Forms.ProgressBar();
            this.installedFileLabel = new System.Windows.Forms.Label();
            this.statePictureBox = new System.Windows.Forms.PictureBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.descStateTextBox = new System.Windows.Forms.TextBox();
            this.progressPrcTextBox = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.descriptionesTextBox);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Font = new System.Drawing.Font("Georgia", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.panel1.Location = new System.Drawing.Point(3, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(456, 0);
            this.panel1.TabIndex = 2;
            // 
            // descriptionesTextBox
            // 
            this.descriptionesTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionesTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.descriptionesTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.descriptionesTextBox.Font = new System.Drawing.Font("Georgia", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.descriptionesTextBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.descriptionesTextBox.Location = new System.Drawing.Point(16, 19);
            this.descriptionesTextBox.Multiline = true;
            this.descriptionesTextBox.Name = "descriptionesTextBox";
            this.descriptionesTextBox.ReadOnly = true;
            this.descriptionesTextBox.Size = new System.Drawing.Size(429, 0);
            this.descriptionesTextBox.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Georgia", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.Color.DarkGreen;
            this.label3.Location = new System.Drawing.Point(2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(147, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "Описание изменений:";
            // 
            // processProgressBar
            // 
            this.processProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.processProgressBar.Location = new System.Drawing.Point(5, 32);
            this.processProgressBar.Name = "processProgressBar";
            this.processProgressBar.Size = new System.Drawing.Size(413, 15);
            this.processProgressBar.TabIndex = 3;
            // 
            // installedFileLabel
            // 
            this.installedFileLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.installedFileLabel.AutoSize = true;
            this.installedFileLabel.Font = new System.Drawing.Font("Georgia", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.installedFileLabel.Location = new System.Drawing.Point(7, 14);
            this.installedFileLabel.Name = "installedFileLabel";
            this.installedFileLabel.Size = new System.Drawing.Size(25, 14);
            this.installedFileLabel.TabIndex = 2;
            this.installedFileLabel.Text = ". . .";
            // 
            // statePictureBox
            // 
            this.statePictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.statePictureBox.Image = ((System.Drawing.Image)(resources.GetObject("statePictureBox.Image")));
            this.statePictureBox.Location = new System.Drawing.Point(426, 2);
            this.statePictureBox.Name = "statePictureBox";
            this.statePictureBox.Size = new System.Drawing.Size(32, 32);
            this.statePictureBox.TabIndex = 4;
            this.statePictureBox.TabStop = false;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "1423281420_clean.png");
            this.imageList1.Images.SetKeyName(1, "1423281431_17-32.png");
            this.imageList1.Images.SetKeyName(2, "1423281328_Play All.png");
            // 
            // descStateTextBox
            // 
            this.descStateTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.descStateTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.descStateTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.descStateTextBox.ForeColor = System.Drawing.Color.DarkGreen;
            this.descStateTextBox.Location = new System.Drawing.Point(232, 10);
            this.descStateTextBox.Multiline = true;
            this.descStateTextBox.Name = "descStateTextBox";
            this.descStateTextBox.Size = new System.Drawing.Size(186, 17);
            this.descStateTextBox.TabIndex = 5;
            this.descStateTextBox.Text = "Подготовка";
            this.descStateTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // progressPrcTextBox
            // 
            this.progressPrcTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.progressPrcTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.progressPrcTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.progressPrcTextBox.Location = new System.Drawing.Point(410, 30);
            this.progressPrcTextBox.Name = "progressPrcTextBox";
            this.progressPrcTextBox.ReadOnly = true;
            this.progressPrcTextBox.Size = new System.Drawing.Size(46, 15);
            this.progressPrcTextBox.TabIndex = 6;
            this.progressPrcTextBox.Text = "0 %";
            this.progressPrcTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ProcessInstallForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 53);
            this.Controls.Add(this.processProgressBar);
            this.Controls.Add(this.progressPrcTextBox);
            this.Controls.Add(this.descStateTextBox);
            this.Controls.Add(this.statePictureBox);
            this.Controls.Add(this.installedFileLabel);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Georgia", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ProcessInstallForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Установка обновлений";
            this.Shown += new System.EventHandler(this.ProcessInstallForm_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statePictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ProgressBar processProgressBar;
        private System.Windows.Forms.Label installedFileLabel;
        private System.Windows.Forms.PictureBox statePictureBox;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TextBox descStateTextBox;
        private System.Windows.Forms.TextBox descriptionesTextBox;
        private System.Windows.Forms.TextBox progressPrcTextBox;
        private System.Windows.Forms.Label label3;
    }
}