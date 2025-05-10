namespace Proiect_Paoo
{
    partial class AdminTable
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelMain = new System.Windows.Forms.Panel();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.columnIdOrar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnNume = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnGen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnSala = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnTeatru = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnData = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnOra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnOras = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.addButton = new System.Windows.Forms.Button();
            this.editButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.refreshButton = new System.Windows.Forms.Button();
            this.btImport = new System.Windows.Forms.Button();
            this.btExport = new System.Windows.Forms.Button();
            this.comboBoxOras = new System.Windows.Forms.ComboBox();
            this.panelForm = new System.Windows.Forms.Panel();
            this.tableLayoutForm = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.labelNume = new System.Windows.Forms.Label();
            this.comboBoxSpectacol = new System.Windows.Forms.ComboBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.textBoxOra = new System.Windows.Forms.DateTimePicker();
            this.labelOra = new System.Windows.Forms.Label();
            this.labelData = new System.Windows.Forms.Label();
            this.dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
            this.labelSala = new System.Windows.Forms.Label();
            this.labelTeatru = new System.Windows.Forms.Label();
            this.comboBoxTeatru = new System.Windows.Forms.ComboBox();
            this.comboBoxSala = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.documentationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.buttonPanel.SuspendLayout();
            this.panelForm.SuspendLayout();
            this.tableLayoutForm.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.dataGridView);
            this.panelMain.Controls.Add(this.buttonPanel);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 28);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(897, 521);
            this.panelMain.TabIndex = 0;
            // 
            // dataGridView
            // 
            this.dataGridView.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(84)))), ((int)(((byte)(150)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnIdOrar,
            this.columnNume,
            this.columnGen,
            this.columnSala,
            this.columnTeatru,
            this.columnData,
            this.columnOra,
            this.columnOras});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView.GridColor = System.Drawing.Color.LightGray;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersWidth = 51;
            this.dataGridView.Size = new System.Drawing.Size(897, 450);
            this.dataGridView.TabIndex = 0;
            // 
            // columnIdOrar
            // 
            this.columnIdOrar.HeaderText = "ID Orar";
            this.columnIdOrar.MinimumWidth = 6;
            this.columnIdOrar.Name = "columnIdOrar";
            this.columnIdOrar.Width = 80;
            // 
            // columnNume
            // 
            this.columnNume.HeaderText = "Nume";
            this.columnNume.MinimumWidth = 6;
            this.columnNume.Name = "columnNume";
            this.columnNume.Width = 150;
            // 
            // columnGen
            // 
            this.columnGen.FillWeight = 120F;
            this.columnGen.HeaderText = "Gen";
            this.columnGen.MinimumWidth = 6;
            this.columnGen.Name = "columnGen";
            this.columnGen.Width = 125;
            // 
            // columnSala
            // 
            this.columnSala.HeaderText = "Sala";
            this.columnSala.MinimumWidth = 6;
            this.columnSala.Name = "columnSala";
            this.columnSala.Width = 120;
            // 
            // columnTeatru
            // 
            this.columnTeatru.HeaderText = "Teatru";
            this.columnTeatru.MinimumWidth = 6;
            this.columnTeatru.Name = "columnTeatru";
            this.columnTeatru.Width = 150;
            // 
            // columnData
            // 
            this.columnData.HeaderText = "Data";
            this.columnData.MinimumWidth = 6;
            this.columnData.Name = "columnData";
            this.columnData.Width = 125;
            // 
            // columnOra
            // 
            this.columnOra.HeaderText = "Ora";
            this.columnOra.MinimumWidth = 6;
            this.columnOra.Name = "columnOra";
            this.columnOra.Width = 120;
            // 
            // columnOras
            // 
            this.columnOras.HeaderText = "Oraș";
            this.columnOras.MinimumWidth = 6;
            this.columnOras.Name = "columnOras";
            this.columnOras.Width = 150;
            // 
            // buttonPanel
            // 
            this.buttonPanel.BackColor = System.Drawing.Color.PeachPuff;
            this.buttonPanel.Controls.Add(this.addButton);
            this.buttonPanel.Controls.Add(this.editButton);
            this.buttonPanel.Controls.Add(this.deleteButton);
            this.buttonPanel.Controls.Add(this.refreshButton);
            this.buttonPanel.Controls.Add(this.btImport);
            this.buttonPanel.Controls.Add(this.btExport);
            this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonPanel.Location = new System.Drawing.Point(0, 449);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(897, 72);
            this.buttonPanel.TabIndex = 1;
            // 
            // addButton
            // 
            this.addButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(150)))), ((int)(((byte)(84)))));
            this.addButton.FlatAppearance.BorderSize = 0;
            this.addButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.addButton.ForeColor = System.Drawing.Color.White;
            this.addButton.Location = new System.Drawing.Point(3, 3);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(120, 40);
            this.addButton.TabIndex = 0;
            this.addButton.Text = "Adaugă";
            this.toolTip.SetToolTip(this.addButton, "Adaugă un nou spectacol în orar.");
            this.addButton.UseVisualStyleBackColor = false;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // editButton
            // 
            this.editButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(84)))), ((int)(((byte)(150)))));
            this.editButton.FlatAppearance.BorderSize = 0;
            this.editButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.editButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.editButton.ForeColor = System.Drawing.Color.White;
            this.editButton.Location = new System.Drawing.Point(129, 3);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(120, 40);
            this.editButton.TabIndex = 1;
            this.editButton.Text = "Editează";
            this.toolTip.SetToolTip(this.editButton, "Editează spectacolul selectat.");
            this.editButton.UseVisualStyleBackColor = false;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.deleteButton.FlatAppearance.BorderSize = 0;
            this.deleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.deleteButton.ForeColor = System.Drawing.Color.White;
            this.deleteButton.Location = new System.Drawing.Point(255, 3);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(120, 40);
            this.deleteButton.TabIndex = 2;
            this.deleteButton.Text = "Șterge";
            this.toolTip.SetToolTip(this.deleteButton, "Șterge spectacolul selectat.");
            this.deleteButton.UseVisualStyleBackColor = false;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // refreshButton
            // 
            this.refreshButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(84)))), ((int)(((byte)(150)))));
            this.refreshButton.FlatAppearance.BorderSize = 0;
            this.refreshButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.refreshButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.refreshButton.ForeColor = System.Drawing.Color.White;
            this.refreshButton.Location = new System.Drawing.Point(381, 3);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(150, 40);
            this.refreshButton.TabIndex = 3;
            this.refreshButton.Text = "Reîmprospătează";
            this.toolTip.SetToolTip(this.refreshButton, "Reîmprospătează lista de spectacole.");
            this.refreshButton.UseVisualStyleBackColor = false;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // btImport
            // 
            this.btImport.BackColor = System.Drawing.Color.Orange;
            this.btImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btImport.ForeColor = System.Drawing.Color.White;
            this.btImport.Location = new System.Drawing.Point(537, 3);
            this.btImport.Name = "btImport";
            this.btImport.Size = new System.Drawing.Size(150, 40);
            this.btImport.TabIndex = 4;
            this.btImport.Text = "Import Excel";
            this.btImport.UseVisualStyleBackColor = false;
            this.btImport.Click += new System.EventHandler(this.btImport_Click);
            // 
            // btExport
            // 
            this.btExport.BackColor = System.Drawing.Color.LimeGreen;
            this.btExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btExport.ForeColor = System.Drawing.Color.White;
            this.btExport.Location = new System.Drawing.Point(693, 3);
            this.btExport.Name = "btExport";
            this.btExport.Size = new System.Drawing.Size(150, 40);
            this.btExport.TabIndex = 5;
            this.btExport.Text = "Export Excel";
            this.btExport.UseVisualStyleBackColor = false;
            this.btExport.Click += new System.EventHandler(this.btExport_Click);
            // 
            // comboBoxOras
            // 
            this.comboBoxOras.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOras.Location = new System.Drawing.Point(92, 53);
            this.comboBoxOras.Name = "comboBoxOras";
            this.comboBoxOras.Size = new System.Drawing.Size(273, 24);
            this.comboBoxOras.TabIndex = 0;
            this.comboBoxOras.SelectedIndexChanged += new System.EventHandler(this.ComboBoxOras_SelectedIndexChanged);
            // 
            // panelForm
            // 
            this.panelForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelForm.Controls.Add(this.tableLayoutForm);
            this.panelForm.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelForm.Location = new System.Drawing.Point(897, 28);
            this.panelForm.Name = "panelForm";
            this.panelForm.Padding = new System.Windows.Forms.Padding(10);
            this.panelForm.Size = new System.Drawing.Size(400, 521);
            this.panelForm.TabIndex = 2;
            // 
            // tableLayoutForm
            // 
            this.tableLayoutForm.BackColor = System.Drawing.Color.MistyRose;
            this.tableLayoutForm.ColumnCount = 2;
            this.tableLayoutForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.10526F));
            this.tableLayoutForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.89474F));
            this.tableLayoutForm.Controls.Add(this.label1, 0, 1);
            this.tableLayoutForm.Controls.Add(this.labelNume, 0, 0);
            this.tableLayoutForm.Controls.Add(this.comboBoxSpectacol, 1, 0);
            this.tableLayoutForm.Controls.Add(this.cancelButton, 1, 6);
            this.tableLayoutForm.Controls.Add(this.saveButton, 0, 6);
            this.tableLayoutForm.Controls.Add(this.textBoxOra, 1, 5);
            this.tableLayoutForm.Controls.Add(this.labelOra, 0, 5);
            this.tableLayoutForm.Controls.Add(this.labelData, 0, 4);
            this.tableLayoutForm.Controls.Add(this.dateTimePickerDate, 1, 4);
            this.tableLayoutForm.Controls.Add(this.labelSala, 0, 3);
            this.tableLayoutForm.Controls.Add(this.labelTeatru, 0, 2);
            this.tableLayoutForm.Controls.Add(this.comboBoxTeatru, 1, 2);
            this.tableLayoutForm.Controls.Add(this.comboBoxSala, 1, 3);
            this.tableLayoutForm.Controls.Add(this.comboBoxOras, 1, 1);
            this.tableLayoutForm.Controls.Add(this.linkLabel1, 1, 7);
            this.tableLayoutForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutForm.Location = new System.Drawing.Point(10, 10);
            this.tableLayoutForm.Name = "tableLayoutForm";
            this.tableLayoutForm.Padding = new System.Windows.Forms.Padding(10);
            this.tableLayoutForm.RowCount = 8;
            this.tableLayoutForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 11F));
            this.tableLayoutForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutForm.Size = new System.Drawing.Size(378, 499);
            this.tableLayoutForm.TabIndex = 0;
            this.tableLayoutForm.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutForm_Paint);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(13, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 23);
            this.label1.TabIndex = 12;
            this.label1.Text = "Oras";
            // 
            // labelNume
            // 
            this.labelNume.Location = new System.Drawing.Point(13, 10);
            this.labelNume.Name = "labelNume";
            this.labelNume.Size = new System.Drawing.Size(73, 23);
            this.labelNume.TabIndex = 0;
            this.labelNume.Text = "Spectacol";
            this.labelNume.Click += new System.EventHandler(this.labelNume_Click);
            // 
            // comboBoxSpectacol
            // 
            this.comboBoxSpectacol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxSpectacol.Location = new System.Drawing.Point(92, 13);
            this.comboBoxSpectacol.Name = "comboBoxSpectacol";
            this.comboBoxSpectacol.Size = new System.Drawing.Size(273, 24);
            this.comboBoxSpectacol.TabIndex = 1;
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.cancelButton.ForeColor = System.Drawing.Color.White;
            this.cancelButton.Location = new System.Drawing.Point(92, 273);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 35);
            this.cancelButton.TabIndex = 11;
            this.cancelButton.Text = "Anulează";
            this.toolTip.SetToolTip(this.cancelButton, "Anulează modificările.");
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(150)))), ((int)(((byte)(84)))));
            this.saveButton.FlatAppearance.BorderSize = 0;
            this.saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.saveButton.ForeColor = System.Drawing.Color.White;
            this.saveButton.Location = new System.Drawing.Point(13, 273);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(73, 35);
            this.saveButton.TabIndex = 10;
            this.saveButton.Text = "Salvează";
            this.toolTip.SetToolTip(this.saveButton, "Salvează modificările.");
            this.saveButton.UseVisualStyleBackColor = false;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // textBoxOra
            // 
            this.textBoxOra.CustomFormat = "HH:mm";
            this.textBoxOra.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxOra.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.textBoxOra.Location = new System.Drawing.Point(92, 213);
            this.textBoxOra.Name = "textBoxOra";
            this.textBoxOra.ShowUpDown = true;
            this.textBoxOra.Size = new System.Drawing.Size(273, 22);
            this.textBoxOra.TabIndex = 9;
            // 
            // labelOra
            // 
            this.labelOra.Location = new System.Drawing.Point(13, 210);
            this.labelOra.Name = "labelOra";
            this.labelOra.Size = new System.Drawing.Size(73, 23);
            this.labelOra.TabIndex = 8;
            this.labelOra.Text = "Oră";
            // 
            // labelData
            // 
            this.labelData.Location = new System.Drawing.Point(13, 170);
            this.labelData.Name = "labelData";
            this.labelData.Size = new System.Drawing.Size(73, 23);
            this.labelData.TabIndex = 6;
            this.labelData.Text = "Dată";
            // 
            // dateTimePickerDate
            // 
            this.dateTimePickerDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dateTimePickerDate.Location = new System.Drawing.Point(92, 173);
            this.dateTimePickerDate.Name = "dateTimePickerDate";
            this.dateTimePickerDate.Size = new System.Drawing.Size(273, 22);
            this.dateTimePickerDate.TabIndex = 7;
            // 
            // labelSala
            // 
            this.labelSala.Location = new System.Drawing.Point(13, 130);
            this.labelSala.Name = "labelSala";
            this.labelSala.Size = new System.Drawing.Size(73, 23);
            this.labelSala.TabIndex = 2;
            this.labelSala.Text = "Sală";
            // 
            // labelTeatru
            // 
            this.labelTeatru.Location = new System.Drawing.Point(13, 90);
            this.labelTeatru.Name = "labelTeatru";
            this.labelTeatru.Size = new System.Drawing.Size(73, 23);
            this.labelTeatru.TabIndex = 4;
            this.labelTeatru.Text = "Teatru";
            // 
            // comboBoxTeatru
            // 
            this.comboBoxTeatru.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxTeatru.Location = new System.Drawing.Point(92, 93);
            this.comboBoxTeatru.Name = "comboBoxTeatru";
            this.comboBoxTeatru.Size = new System.Drawing.Size(273, 24);
            this.comboBoxTeatru.TabIndex = 5;
            this.comboBoxTeatru.SelectedIndexChanged += new System.EventHandler(this.comboBoxTeatru_SelectedIndexChanged_1);
            // 
            // comboBoxSala
            // 
            this.comboBoxSala.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxSala.Location = new System.Drawing.Point(92, 133);
            this.comboBoxSala.Name = "comboBoxSala";
            this.comboBoxSala.Size = new System.Drawing.Size(273, 24);
            this.comboBoxSala.TabIndex = 3;
            this.comboBoxSala.SelectedIndexChanged += new System.EventHandler(this.comboBoxSala_SelectedIndexChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1297, 28);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(60, 24);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // mainToolStripMenuItem
            // 
            this.mainToolStripMenuItem.Name = "mainToolStripMenuItem";
            this.mainToolStripMenuItem.Size = new System.Drawing.Size(125, 26);
            this.mainToolStripMenuItem.Text = "Main";
            this.mainToolStripMenuItem.Click += new System.EventHandler(this.mainToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.documentationToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(195, 26);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // documentationToolStripMenuItem
            // 
            this.documentationToolStripMenuItem.Name = "documentationToolStripMenuItem";
            this.documentationToolStripMenuItem.Size = new System.Drawing.Size(195, 26);
            this.documentationToolStripMenuItem.Text = "Documentation";
            this.documentationToolStripMenuItem.Click += new System.EventHandler(this.documentationToolStripMenuItem_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(92, 321);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(232, 16);
            this.linkLabel1.TabIndex = 13;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Vrei sa bagi spectacol Nou? Click aici";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // AdminTable
            // 
            this.ClientSize = new System.Drawing.Size(1297, 549);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelForm);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "AdminTable";
            this.Text = "Admin Table";
            this.Load += new System.EventHandler(this.AdminTable_Load);
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.buttonPanel.ResumeLayout(false);
            this.panelForm.ResumeLayout(false);
            this.tableLayoutForm.ResumeLayout(false);
            this.tableLayoutForm.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.FlowLayoutPanel buttonPanel;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.Panel panelForm;
        private System.Windows.Forms.TableLayoutPanel tableLayoutForm;
        private System.Windows.Forms.Label labelNume;
        private System.Windows.Forms.ComboBox comboBoxSpectacol;
        private System.Windows.Forms.Label labelSala;
        private System.Windows.Forms.ComboBox comboBoxSala;
        private System.Windows.Forms.Label labelTeatru;
        private System.Windows.Forms.ComboBox comboBoxTeatru;
        private System.Windows.Forms.Label labelData;
        private System.Windows.Forms.DateTimePicker dateTimePickerDate;
        private System.Windows.Forms.Label labelOra;
        private System.Windows.Forms.DateTimePicker textBoxOra;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ComboBox comboBoxOras;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnIdOrar;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnNume;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnGen;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSala;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnTeatru;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnData;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnOra;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnOras;
        private System.Windows.Forms.Button btImport;
        private System.Windows.Forms.Button btExport;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem documentationToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}