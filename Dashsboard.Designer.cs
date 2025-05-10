namespace Proiect_Paoo
{
    partial class Dashboard
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelMain = new System.Windows.Forms.Panel();
            this.dataGridViewSpectacole = new System.Windows.Forms.DataGridView();
            this.buttonSterge = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.buttonExport = new System.Windows.Forms.Button();
            this.butonImport = new System.Windows.Forms.Button();
            this.buttonAddSpectacol = new System.Windows.Forms.Button();
            this.panelForm = new System.Windows.Forms.Panel();
            this.spectacolPanel = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.comboBoxRegizor = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dataGridViewDirectors = new System.Windows.Forms.DataGridView();
            this.textBoxNume = new System.Windows.Forms.TextBox();
            this.textBoxPrenume = new System.Windows.Forms.TextBox();
            this.dateTimePickerDataNasterii = new System.Windows.Forms.DateTimePicker();
            this.buttonAddDirector = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ButtonSaveEdit = new System.Windows.Forms.Button();
            this.comboBoxOras = new System.Windows.Forms.ComboBox();
            this.comboBoxTeatru = new System.Windows.Forms.ComboBox();
            this.comboBoxSala = new System.Windows.Forms.ComboBox();
            this.comboBoxSpectacol = new System.Windows.Forms.ComboBox();
            this.dateTimePickerData = new System.Windows.Forms.DateTimePicker();
            this.textBoxOra = new System.Windows.Forms.TextBox();
            this.buttonAddOrar = new System.Windows.Forms.Button();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSpectacole)).BeginInit();
            this.panelForm.SuspendLayout();
            this.spectacolPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDirectors)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.LightCoral;
            this.panelMain.Controls.Add(this.button2);
            this.panelMain.Controls.Add(this.dataGridViewSpectacole);
            this.panelMain.Controls.Add(this.buttonSterge);
            this.panelMain.Controls.Add(this.buttonEdit);
            this.panelMain.Controls.Add(this.buttonRefresh);
            this.panelMain.Controls.Add(this.buttonExport);
            this.panelMain.Controls.Add(this.butonImport);
            this.panelMain.Controls.Add(this.buttonAddSpectacol);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1305, 365);
            this.panelMain.TabIndex = 0;
            this.panelMain.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMain_Paint);
            // 
            // dataGridViewSpectacole
            // 
            this.dataGridViewSpectacole.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewSpectacole.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSpectacole.Location = new System.Drawing.Point(15, 12);
            this.dataGridViewSpectacole.Name = "dataGridViewSpectacole";
            this.dataGridViewSpectacole.RowHeadersWidth = 51;
            this.dataGridViewSpectacole.Size = new System.Drawing.Size(927, 319);
            this.dataGridViewSpectacole.TabIndex = 0;
            this.dataGridViewSpectacole.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewSpectacole_CellContentClick);
            // 
            // buttonSterge
            // 
            this.buttonSterge.Location = new System.Drawing.Point(1045, 8);
            this.buttonSterge.Name = "buttonSterge";
            this.buttonSterge.Size = new System.Drawing.Size(150, 40);
            this.buttonSterge.TabIndex = 1;
            this.buttonSterge.Text = "Șterge Spectacol";
            this.buttonSterge.UseVisualStyleBackColor = true;
            this.buttonSterge.Click += new System.EventHandler(this.ButtonSterge_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.Location = new System.Drawing.Point(1045, 58);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(150, 40);
            this.buttonEdit.TabIndex = 2;
            this.buttonEdit.Text = "Editează";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.ButtonEdit_Click);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Location = new System.Drawing.Point(1045, 108);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(150, 40);
            this.buttonRefresh.TabIndex = 3;
            this.buttonRefresh.Text = "Reîmprospătează";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.ButtonRefresh_Click);
            // 
            // buttonExport
            // 
            this.buttonExport.Location = new System.Drawing.Point(1045, 158);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(150, 40);
            this.buttonExport.TabIndex = 4;
            this.buttonExport.Text = "Exportă în Excel";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.ButtonExport_Click);
            // 
            // butonImport
            // 
            this.butonImport.Location = new System.Drawing.Point(1045, 208);
            this.butonImport.Name = "butonImport";
            this.butonImport.Size = new System.Drawing.Size(150, 40);
            this.butonImport.TabIndex = 5;
            this.butonImport.Text = "Importă din Excel";
            this.butonImport.UseVisualStyleBackColor = true;
            this.butonImport.Click += new System.EventHandler(this.ButtonImport_Click);
            // 
            // buttonAddSpectacol
            // 
            this.buttonAddSpectacol.Location = new System.Drawing.Point(1045, 258);
            this.buttonAddSpectacol.Name = "buttonAddSpectacol";
            this.buttonAddSpectacol.Size = new System.Drawing.Size(150, 40);
            this.buttonAddSpectacol.TabIndex = 6;
            this.buttonAddSpectacol.Text = "Adaugă Orar";
            this.buttonAddSpectacol.UseVisualStyleBackColor = true;
            this.buttonAddSpectacol.Click += new System.EventHandler(this.ButtonAddSpectacol_Click);
            // 
            // panelForm
            // 
            this.panelForm.BackColor = System.Drawing.Color.IndianRed;
            this.panelForm.Controls.Add(this.panel1);
            this.panelForm.Controls.Add(this.spectacolPanel);
            this.panelForm.Controls.Add(this.dataGridViewDirectors);
            this.panelForm.Controls.Add(this.textBoxNume);
            this.panelForm.Controls.Add(this.textBoxPrenume);
            this.panelForm.Controls.Add(this.dateTimePickerDataNasterii);
            this.panelForm.Controls.Add(this.buttonAddDirector);
            this.panelForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelForm.Location = new System.Drawing.Point(0, 365);
            this.panelForm.Name = "panelForm";
            this.panelForm.Size = new System.Drawing.Size(1305, 300);
            this.panelForm.TabIndex = 1;
            // 
            // spectacolPanel
            // 
            this.spectacolPanel.BackColor = System.Drawing.Color.Salmon;
            this.spectacolPanel.Controls.Add(this.button1);
            this.spectacolPanel.Controls.Add(this.label11);
            this.spectacolPanel.Controls.Add(this.comboBoxRegizor);
            this.spectacolPanel.Controls.Add(this.label10);
            this.spectacolPanel.Controls.Add(this.textBox2);
            this.spectacolPanel.Controls.Add(this.label9);
            this.spectacolPanel.Controls.Add(this.numericUpDown1);
            this.spectacolPanel.Controls.Add(this.label8);
            this.spectacolPanel.Controls.Add(this.comboBox2);
            this.spectacolPanel.Controls.Add(this.textBox1);
            this.spectacolPanel.Controls.Add(this.label7);
            this.spectacolPanel.Location = new System.Drawing.Point(660, 0);
            this.spectacolPanel.Name = "spectacolPanel";
            this.spectacolPanel.Size = new System.Drawing.Size(645, 308);
            this.spectacolPanel.TabIndex = 14;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(450, 240);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(160, 36);
            this.button1.TabIndex = 10;
            this.button1.Text = "Adauga Spectacol";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(448, 68);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(54, 16);
            this.label11.TabIndex = 9;
            this.label11.Text = "Regizor";
            // 
            // comboBoxRegizor
            // 
            this.comboBoxRegizor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRegizor.FormattingEnabled = true;
            this.comboBoxRegizor.Location = new System.Drawing.Point(451, 92);
            this.comboBoxRegizor.Name = "comboBoxRegizor";
            this.comboBoxRegizor.Size = new System.Drawing.Size(159, 24);
            this.comboBoxRegizor.TabIndex = 8;
            this.comboBoxRegizor.SelectedIndexChanged += new System.EventHandler(this.comboBoxRegizor_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(242, 165);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(66, 16);
            this.label10.TabIndex = 7;
            this.label10.Text = "Descriere";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(245, 184);
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBox2.Size = new System.Drawing.Size(162, 22);
            this.textBox2.TabIndex = 6;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(242, 68);
            this.label9.Name = "label9";
            this.label9.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label9.Size = new System.Drawing.Size(62, 16);
            this.label9.TabIndex = 5;
            this.label9.Text = "Pret Pers";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(245, 92);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(162, 22);
            this.numericUpDown1.TabIndex = 4;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(27, 163);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 16);
            this.label8.TabIndex = 3;
            this.label8.Text = "Gen";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(30, 182);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(174, 24);
            this.comboBox2.TabIndex = 2;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(26, 94);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(178, 22);
            this.textBox1.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 71);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 16);
            this.label7.TabIndex = 0;
            this.label7.Text = "Spectacol";
            // 
            // dataGridViewDirectors
            // 
            this.dataGridViewDirectors.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewDirectors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDirectors.Location = new System.Drawing.Point(10, 430);
            this.dataGridViewDirectors.Name = "dataGridViewDirectors";
            this.dataGridViewDirectors.RowHeadersWidth = 51;
            this.dataGridViewDirectors.Size = new System.Drawing.Size(800, 200);
            this.dataGridViewDirectors.TabIndex = 15;
            // 
            // textBoxNume
            // 
            this.textBoxNume.Location = new System.Drawing.Point(850, 430);
            this.textBoxNume.Name = "textBoxNume";
            this.textBoxNume.Size = new System.Drawing.Size(200, 22);
            this.textBoxNume.TabIndex = 16;
            // 
            // textBoxPrenume
            // 
            this.textBoxPrenume.Location = new System.Drawing.Point(850, 460);
            this.textBoxPrenume.Name = "textBoxPrenume";
            this.textBoxPrenume.Size = new System.Drawing.Size(200, 22);
            this.textBoxPrenume.TabIndex = 17;
            // 
            // dateTimePickerDataNasterii
            // 
            this.dateTimePickerDataNasterii.Location = new System.Drawing.Point(850, 490);
            this.dateTimePickerDataNasterii.Name = "dateTimePickerDataNasterii";
            this.dateTimePickerDataNasterii.Size = new System.Drawing.Size(200, 22);
            this.dateTimePickerDataNasterii.TabIndex = 18;
            // 
            // buttonAddDirector
            // 
            this.buttonAddDirector.Location = new System.Drawing.Point(850, 520);
            this.buttonAddDirector.Name = "buttonAddDirector";
            this.buttonAddDirector.Size = new System.Drawing.Size(200, 30);
            this.buttonAddDirector.TabIndex = 19;
            this.buttonAddDirector.Text = "Add Director";
            this.buttonAddDirector.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1045, 304);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(150, 40);
            this.button2.TabIndex = 7;
            this.button2.Text = "Adaugă Spectacol";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.ButtonSaveEdit);
            this.panel1.Controls.Add(this.comboBoxOras);
            this.panel1.Controls.Add(this.comboBoxTeatru);
            this.panel1.Controls.Add(this.comboBoxSala);
            this.panel1.Controls.Add(this.comboBoxSpectacol);
            this.panel1.Controls.Add(this.dateTimePickerData);
            this.panel1.Controls.Add(this.textBoxOra);
            this.panel1.Controls.Add(this.buttonAddOrar);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(677, 297);
            this.panel1.TabIndex = 20;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(453, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 16);
            this.label6.TabIndex = 27;
            this.label6.Text = "Ora";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(453, 132);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 16);
            this.label5.TabIndex = 26;
            this.label5.Text = "Data";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(226, 134);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 16);
            this.label4.TabIndex = 25;
            this.label4.Text = "Spectacol";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(226, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 16);
            this.label3.TabIndex = 24;
            this.label3.Text = "Sala";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 16);
            this.label2.TabIndex = 22;
            this.label2.Text = "Teatru";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 16);
            this.label1.TabIndex = 20;
            this.label1.Text = "Oras";
            // 
            // ButtonSaveEdit
            // 
            this.ButtonSaveEdit.Location = new System.Drawing.Point(479, 236);
            this.ButtonSaveEdit.Name = "ButtonSaveEdit";
            this.ButtonSaveEdit.Size = new System.Drawing.Size(166, 40);
            this.ButtonSaveEdit.TabIndex = 23;
            this.ButtonSaveEdit.Text = "Salvează Modificările";
            this.ButtonSaveEdit.UseVisualStyleBackColor = true;
            this.ButtonSaveEdit.Click += new System.EventHandler(this.ButtonSaveEdit_Click_1);
            // 
            // comboBoxOras
            // 
            this.comboBoxOras.FormattingEnabled = true;
            this.comboBoxOras.Location = new System.Drawing.Point(15, 57);
            this.comboBoxOras.Name = "comboBoxOras";
            this.comboBoxOras.Size = new System.Drawing.Size(180, 24);
            this.comboBoxOras.TabIndex = 14;
            // 
            // comboBoxTeatru
            // 
            this.comboBoxTeatru.FormattingEnabled = true;
            this.comboBoxTeatru.Location = new System.Drawing.Point(15, 149);
            this.comboBoxTeatru.Name = "comboBoxTeatru";
            this.comboBoxTeatru.Size = new System.Drawing.Size(180, 24);
            this.comboBoxTeatru.TabIndex = 15;
            // 
            // comboBoxSala
            // 
            this.comboBoxSala.FormattingEnabled = true;
            this.comboBoxSala.Location = new System.Drawing.Point(229, 55);
            this.comboBoxSala.Name = "comboBoxSala";
            this.comboBoxSala.Size = new System.Drawing.Size(180, 24);
            this.comboBoxSala.TabIndex = 16;
            // 
            // comboBoxSpectacol
            // 
            this.comboBoxSpectacol.FormattingEnabled = true;
            this.comboBoxSpectacol.Location = new System.Drawing.Point(229, 153);
            this.comboBoxSpectacol.Name = "comboBoxSpectacol";
            this.comboBoxSpectacol.Size = new System.Drawing.Size(180, 24);
            this.comboBoxSpectacol.TabIndex = 17;
            // 
            // dateTimePickerData
            // 
            this.dateTimePickerData.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerData.Location = new System.Drawing.Point(456, 151);
            this.dateTimePickerData.Name = "dateTimePickerData";
            this.dateTimePickerData.Size = new System.Drawing.Size(180, 22);
            this.dateTimePickerData.TabIndex = 18;
            // 
            // textBoxOra
            // 
            this.textBoxOra.Location = new System.Drawing.Point(456, 55);
            this.textBoxOra.Name = "textBoxOra";
            this.textBoxOra.Size = new System.Drawing.Size(180, 22);
            this.textBoxOra.TabIndex = 19;
            // 
            // buttonAddOrar
            // 
            this.buttonAddOrar.Location = new System.Drawing.Point(36, 240);
            this.buttonAddOrar.Name = "buttonAddOrar";
            this.buttonAddOrar.Size = new System.Drawing.Size(150, 40);
            this.buttonAddOrar.TabIndex = 21;
            this.buttonAddOrar.Text = "Adaugă Orar";
            this.buttonAddOrar.UseVisualStyleBackColor = true;
            // 
            // Dashboard
            // 
            this.BackColor = System.Drawing.Color.Brown;
            this.ClientSize = new System.Drawing.Size(1305, 665);
            this.Controls.Add(this.panelForm);
            this.Controls.Add(this.panelMain);
            this.Name = "Dashboard";
            this.Text = "Dashboard";
            this.Load += new System.EventHandler(this.Dashboard_Load);
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSpectacole)).EndInit();
            this.panelForm.ResumeLayout(false);
            this.panelForm.PerformLayout();
            this.spectacolPanel.ResumeLayout(false);
            this.spectacolPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDirectors)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.DataGridView dataGridViewSpectacole;
        private System.Windows.Forms.Button buttonSterge;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Button butonImport;
        private System.Windows.Forms.Button buttonAddSpectacol;
        private System.Windows.Forms.Panel panelForm;
        private System.Windows.Forms.Panel spectacolPanel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ComboBox comboBoxRegizor;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridView dataGridViewDirectors;
        private System.Windows.Forms.TextBox textBoxNume;
        private System.Windows.Forms.TextBox textBoxPrenume;
        private System.Windows.Forms.DateTimePicker dateTimePickerDataNasterii;
        private System.Windows.Forms.Button buttonAddDirector;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonAdaugareSpectacole;
        private System.Windows.Forms.Panel panelAdaugareSpectacole;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ButtonSaveEdit;
        private System.Windows.Forms.ComboBox comboBoxOras;
        private System.Windows.Forms.ComboBox comboBoxTeatru;
        private System.Windows.Forms.ComboBox comboBoxSala;
        private System.Windows.Forms.ComboBox comboBoxSpectacol;
        private System.Windows.Forms.DateTimePicker dateTimePickerData;
        private System.Windows.Forms.TextBox textBoxOra;
        private System.Windows.Forms.Button buttonAddOrar;
    }
}