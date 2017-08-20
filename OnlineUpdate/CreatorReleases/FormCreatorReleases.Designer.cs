namespace CreatorReleases
{
    partial class FormCreatorReleases
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCreatorReleases));
            this.currentVersionTextBox = new System.Windows.Forms.TextBox();
            this.appSettingsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.preparationBildtoolStripButton = new System.Windows.Forms.ToolStripButton();
            this.bildToolStripButton = new System.Windows.Forms.ToolStripSplitButton();
            this.создатьИОпубликоватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.optionesToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.label2 = new System.Windows.Forms.Label();
            this.lastVersionTextBox = new System.Windows.Forms.TextBox();
            this.logRichTextBox = new System.Windows.Forms.RichTextBox();
            this.sourceFilesListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.игнорироватьФайлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.taskProgressBar = new System.Windows.Forms.ProgressBar();
            this.prcTextBox = new System.Windows.Forms.TextBox();
            this.fileTextBox = new System.Windows.Forms.TextBox();
            this.progressPanel = new System.Windows.Forms.Panel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.descriptionesRichTextBox = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.appSettingsBindingSource)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.progressPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // currentVersionTextBox
            // 
            this.currentVersionTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.appSettingsBindingSource, "CurrVersione", true));
            this.currentVersionTextBox.Location = new System.Drawing.Point(258, 4);
            this.currentVersionTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.currentVersionTextBox.Name = "currentVersionTextBox";
            this.currentVersionTextBox.Size = new System.Drawing.Size(64, 22);
            this.currentVersionTextBox.TabIndex = 0;
            // 
            // appSettingsBindingSource
            // 
            this.appSettingsBindingSource.DataSource = typeof(CreatorReleases.AppSettings);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(170, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Тек. версия:";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.preparationBildtoolStripButton,
            this.bildToolStripButton,
            this.toolStripButton1,
            this.toolStripSeparator1,
            this.optionesToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(781, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // preparationBildtoolStripButton
            // 
            this.preparationBildtoolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("preparationBildtoolStripButton.Image")));
            this.preparationBildtoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.preparationBildtoolStripButton.Name = "preparationBildtoolStripButton";
            this.preparationBildtoolStripButton.Size = new System.Drawing.Size(109, 22);
            this.preparationBildtoolStripButton.Text = "Подготовить";
            this.preparationBildtoolStripButton.Click += new System.EventHandler(this.preparationBildtoolStripButton_Click);
            // 
            // bildToolStripButton
            // 
            this.bildToolStripButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.создатьИОпубликоватьToolStripMenuItem});
            this.bildToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("bildToolStripButton.Image")));
            this.bildToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bildToolStripButton.Name = "bildToolStripButton";
            this.bildToolStripButton.Size = new System.Drawing.Size(90, 22);
            this.bildToolStripButton.Text = "Создать";
            this.bildToolStripButton.ButtonClick += new System.EventHandler(this.bildToolStripButton_ButtonClick);
            // 
            // создатьИОпубликоватьToolStripMenuItem
            // 
            this.создатьИОпубликоватьToolStripMenuItem.Name = "создатьИОпубликоватьToolStripMenuItem";
            this.создатьИОпубликоватьToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.создатьИОпубликоватьToolStripMenuItem.Text = "Создать и опубликовать";
            this.создатьИОпубликоватьToolStripMenuItem.Click += new System.EventHandler(this.создатьИОпубликоватьToolStripMenuItem_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(118, 22);
            this.toolStripButton1.Text = "Опубликовать";
            this.toolStripButton1.Click += new System.EventHandler(this.publicationToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // optionesToolStripButton
            // 
            this.optionesToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("optionesToolStripButton.Image")));
            this.optionesToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.optionesToolStripButton.Name = "optionesToolStripButton";
            this.optionesToolStripButton.Size = new System.Drawing.Size(96, 22);
            this.optionesToolStripButton.Text = "Настройки";
            this.optionesToolStripButton.Click += new System.EventHandler(this.optionesToolStripButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 7);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(97, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Пред. версия:";
            // 
            // lastVersionTextBox
            // 
            this.lastVersionTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.appSettingsBindingSource, "LastVersione", true));
            this.lastVersionTextBox.Location = new System.Drawing.Point(102, 4);
            this.lastVersionTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.lastVersionTextBox.Name = "lastVersionTextBox";
            this.lastVersionTextBox.Size = new System.Drawing.Size(64, 22);
            this.lastVersionTextBox.TabIndex = 4;
            // 
            // logRichTextBox
            // 
            this.logRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logRichTextBox.Location = new System.Drawing.Point(0, 478);
            this.logRichTextBox.Name = "logRichTextBox";
            this.logRichTextBox.Size = new System.Drawing.Size(779, 86);
            this.logRichTextBox.TabIndex = 6;
            this.logRichTextBox.Text = "";
            // 
            // sourceFilesListView
            // 
            this.sourceFilesListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sourceFilesListView.CheckBoxes = true;
            this.sourceFilesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader3,
            this.columnHeader2});
            this.sourceFilesListView.ContextMenuStrip = this.contextMenuStrip1;
            this.sourceFilesListView.FullRowSelect = true;
            this.sourceFilesListView.GridLines = true;
            this.sourceFilesListView.Location = new System.Drawing.Point(159, 141);
            this.sourceFilesListView.Name = "sourceFilesListView";
            this.sourceFilesListView.Size = new System.Drawing.Size(619, 331);
            this.sourceFilesListView.TabIndex = 7;
            this.sourceFilesListView.UseCompatibleStateImageBehavior = false;
            this.sourceFilesListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Файл";
            this.columnHeader1.Width = 396;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Размер, Кб";
            this.columnHeader3.Width = 103;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "MD5";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 238;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.игнорироватьФайлToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(186, 26);
            // 
            // игнорироватьФайлToolStripMenuItem
            // 
            this.игнорироватьФайлToolStripMenuItem.Name = "игнорироватьФайлToolStripMenuItem";
            this.игнорироватьФайлToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.игнорироватьФайлToolStripMenuItem.Text = "Игнорировать файл";
            // 
            // taskProgressBar
            // 
            this.taskProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.taskProgressBar.Location = new System.Drawing.Point(5, 88);
            this.taskProgressBar.Name = "taskProgressBar";
            this.taskProgressBar.Size = new System.Drawing.Size(767, 17);
            this.taskProgressBar.TabIndex = 8;
            // 
            // prcTextBox
            // 
            this.prcTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.prcTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.prcTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.prcTextBox.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.prcTextBox.Location = new System.Drawing.Point(680, 66);
            this.prcTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.prcTextBox.Multiline = true;
            this.prcTextBox.Name = "prcTextBox";
            this.prcTextBox.Size = new System.Drawing.Size(91, 20);
            this.prcTextBox.TabIndex = 9;
            this.prcTextBox.Text = "0 %";
            this.prcTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // fileTextBox
            // 
            this.fileTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.fileTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.fileTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.fileTextBox.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.fileTextBox.Location = new System.Drawing.Point(8, 66);
            this.fileTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.fileTextBox.Multiline = true;
            this.fileTextBox.Name = "fileTextBox";
            this.fileTextBox.Size = new System.Drawing.Size(294, 20);
            this.fileTextBox.TabIndex = 10;
            this.fileTextBox.Text = ". . .";
            // 
            // progressPanel
            // 
            this.progressPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressPanel.Controls.Add(this.descriptionesRichTextBox);
            this.progressPanel.Controls.Add(this.currentVersionTextBox);
            this.progressPanel.Controls.Add(this.label1);
            this.progressPanel.Controls.Add(this.fileTextBox);
            this.progressPanel.Controls.Add(this.lastVersionTextBox);
            this.progressPanel.Controls.Add(this.prcTextBox);
            this.progressPanel.Controls.Add(this.label2);
            this.progressPanel.Controls.Add(this.taskProgressBar);
            this.progressPanel.Location = new System.Drawing.Point(3, 27);
            this.progressPanel.Name = "progressPanel";
            this.progressPanel.Size = new System.Drawing.Size(775, 108);
            this.progressPanel.TabIndex = 11;
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeView1.Location = new System.Drawing.Point(3, 141);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(150, 331);
            this.treeView1.TabIndex = 12;
            // 
            // descriptionesRichTextBox
            // 
            this.descriptionesRichTextBox.Location = new System.Drawing.Point(343, 5);
            this.descriptionesRichTextBox.Name = "descriptionesRichTextBox";
            this.descriptionesRichTextBox.Size = new System.Drawing.Size(423, 60);
            this.descriptionesRichTextBox.TabIndex = 11;
            this.descriptionesRichTextBox.Text = "";
            // 
            // FormCreatorReleases
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(781, 564);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.progressPanel);
            this.Controls.Add(this.sourceFilesListView);
            this.Controls.Add(this.logRichTextBox);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Georgia", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormCreatorReleases";
            this.Text = "Создание релиза";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormCreatorReleases_FormClosing);
            this.Load += new System.EventHandler(this.FormCreatorReleases_Load);
            ((System.ComponentModel.ISupportInitialize)(this.appSettingsBindingSource)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.progressPanel.ResumeLayout(false);
            this.progressPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox currentVersionTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton preparationBildtoolStripButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox lastVersionTextBox;
        private System.Windows.Forms.ToolStripButton optionesToolStripButton;
        private System.Windows.Forms.RichTextBox logRichTextBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ListView sourceFilesListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem игнорироватьФайлToolStripMenuItem;
        private System.Windows.Forms.BindingSource appSettingsBindingSource;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ProgressBar taskProgressBar;
        private System.Windows.Forms.TextBox prcTextBox;
        private System.Windows.Forms.TextBox fileTextBox;
        private System.Windows.Forms.ToolStripSplitButton bildToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem создатьИОпубликоватьToolStripMenuItem;
        private System.Windows.Forms.Panel progressPanel;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.RichTextBox descriptionesRichTextBox;
    }
}

