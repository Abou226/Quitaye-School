namespace Quitaye_School.User_Interface
{
    partial class RavitaillementStock
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtQuantité = new System.Windows.Forms.TextBox();
            this.txtProduit = new System.Windows.Forms.TextBox();
            this.cbxMesure = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.cbxCode = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.Date_Expiration = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.cbxDetails = new System.Windows.Forms.ComboBox();
            this.txtNewCode = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAjouter = new FontAwesome.Sharp.IconButton();
            this.btnSaveCode = new FontAwesome.Sharp.IconButton();
            this.btnEnregistrer = new FontAwesome.Sharp.IconButton();
            this.btnFermer = new FontAwesome.Sharp.IconPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnFermer)).BeginInit();
            this.SuspendLayout();
            // 
            // txtQuantité
            // 
            this.txtQuantité.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuantité.Location = new System.Drawing.Point(609, 3);
            this.txtQuantité.Name = "txtQuantité";
            this.txtQuantité.Size = new System.Drawing.Size(36, 25);
            this.txtQuantité.TabIndex = 304;
            // 
            // txtProduit
            // 
            this.txtProduit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProduit.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProduit.Location = new System.Drawing.Point(244, 3);
            this.txtProduit.Name = "txtProduit";
            this.txtProduit.ReadOnly = true;
            this.txtProduit.Size = new System.Drawing.Size(145, 25);
            this.txtProduit.TabIndex = 316;
            // 
            // cbxMesure
            // 
            this.cbxMesure.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMesure.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxMesure.FormattingEnabled = true;
            this.cbxMesure.Location = new System.Drawing.Point(456, 3);
            this.cbxMesure.Name = "cbxMesure";
            this.cbxMesure.Size = new System.Drawing.Size(74, 24);
            this.cbxMesure.TabIndex = 303;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.LightBlue;
            this.label20.Location = new System.Drawing.Point(395, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(52, 17);
            this.label20.TabIndex = 319;
            this.label20.Text = "Mesure";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbxCode
            // 
            this.cbxCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxCode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxCode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxCode.FormattingEnabled = true;
            this.cbxCode.Items.AddRange(new object[] {
            "Vente",
            "Achat"});
            this.cbxCode.Location = new System.Drawing.Point(68, 3);
            this.cbxCode.Name = "cbxCode";
            this.cbxCode.Size = new System.Drawing.Size(113, 24);
            this.cbxCode.TabIndex = 302;
            this.cbxCode.Visible = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(21)))), ((int)(((byte)(234)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Indigo;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 103);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.SlateBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Indigo;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(894, 304);
            this.dataGridView1.TabIndex = 318;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.LightBlue;
            this.label2.Location = new System.Drawing.Point(187, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 17);
            this.label2.TabIndex = 314;
            this.label2.Text = "Produt";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.LightBlue;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 311;
            this.label1.Text = "Barcode";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(2, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(914, 2);
            this.panel2.TabIndex = 308;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(2, 448);
            this.panel4.TabIndex = 306;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel5.Location = new System.Drawing.Point(916, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(2, 448);
            this.panel5.TabIndex = 307;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(0, 448);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(918, 2);
            this.panel6.TabIndex = 309;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.LightBlue;
            this.label3.Location = new System.Drawing.Point(542, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 17);
            this.label3.TabIndex = 315;
            this.label3.Text = "Quantité";
            // 
            // bunifuDragControl1
            // 
            this.bunifuDragControl1.Fixed = true;
            this.bunifuDragControl1.Horizontal = true;
            this.bunifuDragControl1.TargetControl = this;
            this.bunifuDragControl1.Vertical = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.LightBlue;
            this.label4.Location = new System.Drawing.Point(654, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 34);
            this.label4.TabIndex = 315;
            this.label4.Text = "Expiration :";
            // 
            // Date_Expiration
            // 
            this.Date_Expiration.CalendarMonthBackground = System.Drawing.Color.LightCoral;
            this.Date_Expiration.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Date_Expiration.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Date_Expiration.Location = new System.Drawing.Point(732, 3);
            this.Date_Expiration.Name = "Date_Expiration";
            this.Date_Expiration.Size = new System.Drawing.Size(72, 25);
            this.Date_Expiration.TabIndex = 320;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.LightBlue;
            this.label5.Location = new System.Drawing.Point(12, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 17);
            this.label5.TabIndex = 311;
            this.label5.Text = "Details Produit :";
            // 
            // cbxDetails
            // 
            this.cbxDetails.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxDetails.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxDetails.FormattingEnabled = true;
            this.cbxDetails.Location = new System.Drawing.Point(118, 16);
            this.cbxDetails.Name = "cbxDetails";
            this.cbxDetails.Size = new System.Drawing.Size(244, 24);
            this.cbxDetails.TabIndex = 302;
            this.cbxDetails.Visible = false;
            // 
            // txtNewCode
            // 
            this.txtNewCode.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNewCode.Location = new System.Drawing.Point(443, 14);
            this.txtNewCode.Name = "txtNewCode";
            this.txtNewCode.Size = new System.Drawing.Size(184, 25);
            this.txtNewCode.TabIndex = 322;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.LightBlue;
            this.label17.Location = new System.Drawing.Point(372, 17);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(75, 17);
            this.label17.TabIndex = 321;
            this.label17.Text = "Code_Barre";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 11;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.111353F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.67909F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.081019F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.70763F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.547289F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.64906F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.349483F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.601772F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.636651F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.636651F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 97F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbxCode, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtProduit, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.Date_Expiration, 9, 0);
            this.tableLayoutPanel1.Controls.Add(this.label20, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtQuantité, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnAjouter, 10, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbxMesure, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 8, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(8, 45);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(908, 38);
            this.tableLayoutPanel1.TabIndex = 324;
            // 
            // btnAjouter
            // 
            this.btnAjouter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.btnAjouter.FlatAppearance.BorderSize = 0;
            this.btnAjouter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAjouter.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAjouter.ForeColor = System.Drawing.Color.LightBlue;
            this.btnAjouter.IconChar = FontAwesome.Sharp.IconChar.Add;
            this.btnAjouter.IconColor = System.Drawing.Color.LightBlue;
            this.btnAjouter.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnAjouter.IconSize = 26;
            this.btnAjouter.Location = new System.Drawing.Point(810, 3);
            this.btnAjouter.Name = "btnAjouter";
            this.btnAjouter.Size = new System.Drawing.Size(78, 29);
            this.btnAjouter.TabIndex = 305;
            this.btnAjouter.Text = "Ajouter";
            this.btnAjouter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAjouter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAjouter.UseVisualStyleBackColor = false;
            // 
            // btnSaveCode
            // 
            this.btnSaveCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.btnSaveCode.FlatAppearance.BorderSize = 0;
            this.btnSaveCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveCode.ForeColor = System.Drawing.Color.LightBlue;
            this.btnSaveCode.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            this.btnSaveCode.IconColor = System.Drawing.Color.LightBlue;
            this.btnSaveCode.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnSaveCode.IconSize = 32;
            this.btnSaveCode.Location = new System.Drawing.Point(633, 17);
            this.btnSaveCode.Name = "btnSaveCode";
            this.btnSaveCode.Size = new System.Drawing.Size(24, 18);
            this.btnSaveCode.TabIndex = 323;
            this.btnSaveCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveCode.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSaveCode.UseVisualStyleBackColor = false;
            // 
            // btnEnregistrer
            // 
            this.btnEnregistrer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEnregistrer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.btnEnregistrer.FlatAppearance.BorderSize = 0;
            this.btnEnregistrer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnregistrer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnregistrer.ForeColor = System.Drawing.Color.LightBlue;
            this.btnEnregistrer.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            this.btnEnregistrer.IconColor = System.Drawing.Color.LightBlue;
            this.btnEnregistrer.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnEnregistrer.IconSize = 26;
            this.btnEnregistrer.Location = new System.Drawing.Point(774, 413);
            this.btnEnregistrer.Name = "btnEnregistrer";
            this.btnEnregistrer.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnEnregistrer.Size = new System.Drawing.Size(137, 29);
            this.btnEnregistrer.TabIndex = 317;
            this.btnEnregistrer.Text = "Enregistrer";
            this.btnEnregistrer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEnregistrer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEnregistrer.UseVisualStyleBackColor = false;
            // 
            // btnFermer
            // 
            this.btnFermer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFermer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.btnFermer.ForeColor = System.Drawing.Color.LightBlue;
            this.btnFermer.IconChar = FontAwesome.Sharp.IconChar.TimesRectangle;
            this.btnFermer.IconColor = System.Drawing.Color.LightBlue;
            this.btnFermer.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnFermer.IconSize = 25;
            this.btnFermer.Location = new System.Drawing.Point(885, 8);
            this.btnFermer.Name = "btnFermer";
            this.btnFermer.Size = new System.Drawing.Size(25, 25);
            this.btnFermer.TabIndex = 310;
            this.btnFermer.TabStop = false;
            // 
            // RavitaillementStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.ClientSize = new System.Drawing.Size(918, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.btnSaveCode);
            this.Controls.Add(this.txtNewCode);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.cbxDetails);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnEnregistrer);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnFermer);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RavitaillementStock";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RavitaillementStock";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnFermer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtQuantité;
        private System.Windows.Forms.TextBox txtProduit;
        private System.Windows.Forms.ComboBox cbxMesure;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox cbxCode;
        private System.Windows.Forms.DataGridView dataGridView1;
        private FontAwesome.Sharp.IconButton btnEnregistrer;
        private FontAwesome.Sharp.IconButton btnAjouter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public FontAwesome.Sharp.IconPictureBox btnFermer;
        public System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Panel panel4;
        public System.Windows.Forms.Panel panel5;
        public System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label3;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker Date_Expiration;
        private System.Windows.Forms.ComboBox cbxDetails;
        private System.Windows.Forms.Label label5;
        private FontAwesome.Sharp.IconButton btnSaveCode;
        private System.Windows.Forms.TextBox txtNewCode;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}