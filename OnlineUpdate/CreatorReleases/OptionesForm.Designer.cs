namespace CreatorReleases
{
    partial class OptionesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionesForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.acceptToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.cancelToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.currDirProducedyAppTextBox = new System.Windows.Forms.TextBox();
            this.selectcurrDirProducedyAppButton = new System.Windows.Forms.Button();
            this.selectBildReleasesDirButton = new System.Windows.Forms.Button();
            this.bildReleasesDirTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.isCompressCheckBox = new System.Windows.Forms.CheckBox();
            this.nameCreateDirReleasTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ignorePatternsDataGridView = new System.Windows.Forms.DataGridView();
            this.ignorePatternsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.permitsPatternsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.publicationDirFtpTextBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.passwordFtpTextBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.loginFtptextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.portFtpTextBox = new System.Windows.Forms.TextBox();
            this.urlFtpTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.appSettingsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.patternDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patternDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.testButton = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ignorePatternsDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ignorePatternsBindingSource)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.permitsPatternsBindingSource)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.appSettingsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.acceptToolStripButton,
            this.cancelToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(721, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // acceptToolStripButton
            // 
            this.acceptToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("acceptToolStripButton.Image")));
            this.acceptToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.acceptToolStripButton.Name = "acceptToolStripButton";
            this.acceptToolStripButton.Size = new System.Drawing.Size(100, 22);
            this.acceptToolStripButton.Text = "Применить";
            this.acceptToolStripButton.Click += new System.EventHandler(this.acceptToolStripButton_Click);
            // 
            // cancelToolStripButton
            // 
            this.cancelToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("cancelToolStripButton.Image")));
            this.cancelToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cancelToolStripButton.Name = "cancelToolStripButton";
            this.cancelToolStripButton.Size = new System.Drawing.Size(76, 22);
            this.cancelToolStripButton.Text = "Отмена";
            this.cancelToolStripButton.Click += new System.EventHandler(this.cancelToolStripButton_Click);
            // 
            // currDirProducedyAppTextBox
            // 
            this.currDirProducedyAppTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.currDirProducedyAppTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.appSettingsBindingSource, "CurrDirSourceFilesApp", true));
            this.currDirProducedyAppTextBox.Location = new System.Drawing.Point(7, 25);
            this.currDirProducedyAppTextBox.Name = "currDirProducedyAppTextBox";
            this.currDirProducedyAppTextBox.Size = new System.Drawing.Size(641, 22);
            this.currDirProducedyAppTextBox.TabIndex = 2;
            // 
            // selectcurrDirProducedyAppButton
            // 
            this.selectcurrDirProducedyAppButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectcurrDirProducedyAppButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.selectcurrDirProducedyAppButton.Location = new System.Drawing.Point(654, 25);
            this.selectcurrDirProducedyAppButton.Name = "selectcurrDirProducedyAppButton";
            this.selectcurrDirProducedyAppButton.Size = new System.Drawing.Size(52, 22);
            this.selectcurrDirProducedyAppButton.TabIndex = 3;
            this.selectcurrDirProducedyAppButton.Text = ". . .";
            this.selectcurrDirProducedyAppButton.UseVisualStyleBackColor = true;
            this.selectcurrDirProducedyAppButton.Click += new System.EventHandler(this.selectcurrDirProducedyAppButton_Click);
            // 
            // selectBildReleasesDirButton
            // 
            this.selectBildReleasesDirButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectBildReleasesDirButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.selectBildReleasesDirButton.Location = new System.Drawing.Point(651, 32);
            this.selectBildReleasesDirButton.Name = "selectBildReleasesDirButton";
            this.selectBildReleasesDirButton.Size = new System.Drawing.Size(52, 22);
            this.selectBildReleasesDirButton.TabIndex = 6;
            this.selectBildReleasesDirButton.Text = ". . .";
            this.selectBildReleasesDirButton.UseVisualStyleBackColor = true;
            this.selectBildReleasesDirButton.Click += new System.EventHandler(this.selectBildReleasesDirButton_Click);
            // 
            // bildReleasesDirTextBox
            // 
            this.bildReleasesDirTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bildReleasesDirTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.appSettingsBindingSource, "BildReleasesDir", true));
            this.bildReleasesDirTextBox.Location = new System.Drawing.Point(124, 32);
            this.bildReleasesDirTextBox.Name = "bildReleasesDirTextBox";
            this.bildReleasesDirTextBox.Size = new System.Drawing.Size(524, 22);
            this.bildReleasesDirTextBox.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Общий каталог:";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.isCompressCheckBox);
            this.panel1.Controls.Add(this.nameCreateDirReleasTextBox);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.bildReleasesDirTextBox);
            this.panel1.Controls.Add(this.selectBildReleasesDirButton);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(5, 306);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(712, 93);
            this.panel1.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Georgia", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(4, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "Сборка релизов";
            // 
            // isCompressCheckBox
            // 
            this.isCompressCheckBox.AutoSize = true;
            this.isCompressCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.appSettingsBindingSource, "IsCompressFiles", true));
            this.isCompressCheckBox.Location = new System.Drawing.Point(374, 61);
            this.isCompressCheckBox.Name = "isCompressCheckBox";
            this.isCompressCheckBox.Size = new System.Drawing.Size(182, 20);
            this.isCompressCheckBox.TabIndex = 9;
            this.isCompressCheckBox.Text = "Сжимать файлы релиза";
            this.isCompressCheckBox.UseVisualStyleBackColor = true;
            // 
            // nameCreateDirReleasTextBox
            // 
            this.nameCreateDirReleasTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.appSettingsBindingSource, "NameCreateDirReleas", true));
            this.nameCreateDirReleasTextBox.Location = new System.Drawing.Point(124, 60);
            this.nameCreateDirReleasTextBox.Name = "nameCreateDirReleasTextBox";
            this.nameCreateDirReleasTextBox.Size = new System.Drawing.Size(243, 22);
            this.nameCreateDirReleasTextBox.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Префикс релиза:";
            // 
            // ignorePatternsDataGridView
            // 
            this.ignorePatternsDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ignorePatternsDataGridView.AutoGenerateColumns = false;
            this.ignorePatternsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ignorePatternsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.patternDataGridViewTextBoxColumn});
            this.ignorePatternsDataGridView.DataSource = this.ignorePatternsBindingSource;
            this.ignorePatternsDataGridView.EnableHeadersVisualStyles = false;
            this.ignorePatternsDataGridView.Location = new System.Drawing.Point(7, 72);
            this.ignorePatternsDataGridView.Name = "ignorePatternsDataGridView";
            this.ignorePatternsDataGridView.RowHeadersWidth = 15;
            this.ignorePatternsDataGridView.Size = new System.Drawing.Size(357, 203);
            this.ignorePatternsDataGridView.TabIndex = 8;
            // 
            // ignorePatternsBindingSource
            // 
            this.ignorePatternsBindingSource.AllowNew = true;
            this.ignorePatternsBindingSource.DataMember = "IgnorePatterns";
            this.ignorePatternsBindingSource.DataSource = this.appSettingsBindingSource;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.ignorePatternsDataGridView);
            this.panel2.Controls.Add(this.currDirProducedyAppTextBox);
            this.panel2.Controls.Add(this.selectcurrDirProducedyAppButton);
            this.panel2.Location = new System.Drawing.Point(5, 25);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(712, 278);
            this.panel2.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(370, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(140, 16);
            this.label6.TabIndex = 11;
            this.label6.Text = "Фильтр разрешения";
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.patternDataGridViewTextBoxColumn1});
            this.dataGridView1.DataSource = this.permitsPatternsBindingSource;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(371, 72);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 15;
            this.dataGridView1.Size = new System.Drawing.Size(341, 203);
            this.dataGridView1.TabIndex = 10;
            // 
            // permitsPatternsBindingSource
            // 
            this.permitsPatternsBindingSource.DataMember = "PermitsPatterns";
            this.permitsPatternsBindingSource.DataSource = this.appSettingsBindingSource;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(162, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "Фильтр игнорирования";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Georgia", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(4, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(145, 16);
            this.label4.TabIndex = 2;
            this.label4.Text = "Источник файлов";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.testButton);
            this.panel3.Controls.Add(this.publicationDirFtpTextBox);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.passwordFtpTextBox);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.loginFtptextBox);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.portFtpTextBox);
            this.panel3.Controls.Add(this.urlFtpTextBox);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Location = new System.Drawing.Point(5, 405);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(712, 93);
            this.panel3.TabIndex = 10;
            // 
            // publicationDirFtpTextBox
            // 
            this.publicationDirFtpTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.publicationDirFtpTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.appSettingsBindingSource, "PublicationDirFtp", true));
            this.publicationDirFtpTextBox.Location = new System.Drawing.Point(86, 55);
            this.publicationDirFtpTextBox.Name = "publicationDirFtpTextBox";
            this.publicationDirFtpTextBox.Size = new System.Drawing.Size(227, 22);
            this.publicationDirFtpTextBox.TabIndex = 20;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(18, 58);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(67, 16);
            this.label12.TabIndex = 19;
            this.label12.Text = "Каталог:";
            // 
            // passwordFtpTextBox
            // 
            this.passwordFtpTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.passwordFtpTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.appSettingsBindingSource, "PasswordFtp", true));
            this.passwordFtpTextBox.Location = new System.Drawing.Point(377, 55);
            this.passwordFtpTextBox.Name = "passwordFtpTextBox";
            this.passwordFtpTextBox.PasswordChar = '*';
            this.passwordFtpTextBox.Size = new System.Drawing.Size(179, 22);
            this.passwordFtpTextBox.TabIndex = 18;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(316, 58);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(61, 16);
            this.label11.TabIndex = 17;
            this.label11.Text = "Пароль:";
            // 
            // loginFtptextBox
            // 
            this.loginFtptextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loginFtptextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.appSettingsBindingSource, "LoginFtp", true));
            this.loginFtptextBox.Location = new System.Drawing.Point(377, 27);
            this.loginFtptextBox.Name = "loginFtptextBox";
            this.loginFtptextBox.Size = new System.Drawing.Size(179, 22);
            this.loginFtptextBox.TabIndex = 16;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(318, 30);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 16);
            this.label10.TabIndex = 15;
            this.label10.Text = "Логин:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(241, 30);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(12, 16);
            this.label9.TabIndex = 14;
            this.label9.Text = ":";
            // 
            // portFtpTextBox
            // 
            this.portFtpTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.portFtpTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.appSettingsBindingSource, "PortFtp", true));
            this.portFtpTextBox.Location = new System.Drawing.Point(253, 27);
            this.portFtpTextBox.Name = "portFtpTextBox";
            this.portFtpTextBox.Size = new System.Drawing.Size(60, 22);
            this.portFtpTextBox.TabIndex = 13;
            // 
            // urlFtpTextBox
            // 
            this.urlFtpTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.urlFtpTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.appSettingsBindingSource, "UrlFtp", true));
            this.urlFtpTextBox.Location = new System.Drawing.Point(86, 27);
            this.urlFtpTextBox.Name = "urlFtpTextBox";
            this.urlFtpTextBox.Size = new System.Drawing.Size(153, 22);
            this.urlFtpTextBox.TabIndex = 12;
            this.urlFtpTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.urlFtpTextBox_Validating);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(2, 28);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 16);
            this.label8.TabIndex = 11;
            this.label8.Text = "FTP сервер:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Georgia", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(3, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(157, 16);
            this.label7.TabIndex = 11;
            this.label7.Text = "Публикация релиза";
            // 
            // appSettingsBindingSource
            // 
            this.appSettingsBindingSource.DataSource = typeof(CreatorReleases.AppSettings);
            // 
            // patternDataGridViewTextBoxColumn1
            // 
            this.patternDataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.patternDataGridViewTextBoxColumn1.DataPropertyName = "Pattern";
            this.patternDataGridViewTextBoxColumn1.HeaderText = "Шаблон";
            this.patternDataGridViewTextBoxColumn1.Name = "patternDataGridViewTextBoxColumn1";
            // 
            // patternDataGridViewTextBoxColumn
            // 
            this.patternDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.patternDataGridViewTextBoxColumn.DataPropertyName = "Pattern";
            this.patternDataGridViewTextBoxColumn.HeaderText = "Шаблон";
            this.patternDataGridViewTextBoxColumn.Name = "patternDataGridViewTextBoxColumn";
            // 
            // testButton
            // 
            this.testButton.Location = new System.Drawing.Point(562, 26);
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(141, 51);
            this.testButton.TabIndex = 21;
            this.testButton.Text = "Test";
            this.testButton.UseVisualStyleBackColor = true;
            this.testButton.Click += new System.EventHandler(this.testButton_Click);
            // 
            // OptionesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 500);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Georgia", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "OptionesForm";
            this.Text = "Настройки";
            this.Load += new System.EventHandler(this.OptionesForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ignorePatternsDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ignorePatternsBindingSource)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.permitsPatternsBindingSource)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.appSettingsBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.TextBox currDirProducedyAppTextBox;
        private System.Windows.Forms.Button selectcurrDirProducedyAppButton;
        private System.Windows.Forms.Button selectBildReleasesDirButton;
        private System.Windows.Forms.TextBox bildReleasesDirTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox nameCreateDirReleasTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox isCompressCheckBox;
        private System.Windows.Forms.ToolStripButton acceptToolStripButton;
        private System.Windows.Forms.ToolStripButton cancelToolStripButton;
        private System.Windows.Forms.DataGridView ignorePatternsDataGridView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.BindingSource ignorePatternsBindingSource;
        private System.Windows.Forms.BindingSource appSettingsBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn patternDataGridViewTextBoxColumn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox portFtpTextBox;
        private System.Windows.Forms.TextBox urlFtpTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox loginFtptextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox passwordFtpTextBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox publicationDirFtpTextBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DataGridViewTextBoxColumn patternDataGridViewTextBoxColumn1;
        private System.Windows.Forms.BindingSource permitsPatternsBindingSource;
        private System.Windows.Forms.Button testButton;
    }
}