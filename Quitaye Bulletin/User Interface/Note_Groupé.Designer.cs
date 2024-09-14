namespace Quitaye_School.User_Interface
{
    partial class Note_Groupé
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
            this.txtCoeff = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxMatière = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxExamen = new System.Windows.Forms.ComboBox();
            this.lblClasse = new System.Windows.Forms.Label();
            this.NaissanceDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnValider = new FontAwesome.Sharp.IconButton();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnMatière = new FontAwesome.Sharp.IconButton();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnExamen = new FontAwesome.Sharp.IconButton();
            this.label4 = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAjoutClasse = new FontAwesome.Sharp.IconButton();
            this.cbxClasse = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnEnregistrer = new FontAwesome.Sharp.IconButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCoeff
            // 
            this.txtCoeff.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCoeff.Location = new System.Drawing.Point(768, 4);
            this.txtCoeff.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCoeff.Name = "txtCoeff";
            this.txtCoeff.ReadOnly = true;
            this.txtCoeff.Size = new System.Drawing.Size(51, 29);
            this.txtCoeff.TabIndex = 237;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.LightBlue;
            this.label2.Location = new System.Drawing.Point(670, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 46);
            this.label2.TabIndex = 232;
            this.label2.Text = "Coefficient :";
            // 
            // cbxMatière
            // 
            this.cbxMatière.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMatière.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxMatière.FormattingEnabled = true;
            this.cbxMatière.Location = new System.Drawing.Point(4, 4);
            this.cbxMatière.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbxMatière.Name = "cbxMatière";
            this.cbxMatière.Size = new System.Drawing.Size(94, 29);
            this.cbxMatière.TabIndex = 235;
            this.cbxMatière.SelectedIndexChanged += new System.EventHandler(this.cbxMatière_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.LightBlue;
            this.label1.Location = new System.Drawing.Point(446, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 46);
            this.label1.TabIndex = 233;
            this.label1.Text = "Matière :";
            // 
            // cbxExamen
            // 
            this.cbxExamen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxExamen.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxExamen.FormattingEnabled = true;
            this.cbxExamen.Location = new System.Drawing.Point(4, 4);
            this.cbxExamen.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbxExamen.Name = "cbxExamen";
            this.cbxExamen.Size = new System.Drawing.Size(92, 29);
            this.cbxExamen.TabIndex = 236;
            this.cbxExamen.SelectedIndexChanged += new System.EventHandler(this.cbxExamen_SelectedIndexChanged);
            // 
            // lblClasse
            // 
            this.lblClasse.AutoSize = true;
            this.lblClasse.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClasse.ForeColor = System.Drawing.Color.LightBlue;
            this.lblClasse.Location = new System.Drawing.Point(224, 0);
            this.lblClasse.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblClasse.Name = "lblClasse";
            this.lblClasse.Size = new System.Drawing.Size(60, 46);
            this.lblClasse.TabIndex = 234;
            this.lblClasse.Text = "Examen :";
            // 
            // NaissanceDate
            // 
            this.NaissanceDate.CalendarMonthBackground = System.Drawing.Color.LightCoral;
            this.NaissanceDate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NaissanceDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.NaissanceDate.Location = new System.Drawing.Point(888, 4);
            this.NaissanceDate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.NaissanceDate.Name = "NaissanceDate";
            this.NaissanceDate.Size = new System.Drawing.Size(105, 29);
            this.NaissanceDate.TabIndex = 241;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.LightBlue;
            this.label3.Location = new System.Drawing.Point(827, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 46);
            this.label3.TabIndex = 240;
            this.label3.Text = "Date :";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 11;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.23471F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.83506F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.42366F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.83506F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.457116F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.03133F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.499971F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.132653F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.311229F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.8566F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.38262F));
            this.tableLayoutPanel1.Controls.Add(this.btnValider, 10, 0);
            this.tableLayoutPanel1.Controls.Add(this.NaissanceDate, 9, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 8, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtCoeff, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblClasse, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 34);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1155, 49);
            this.tableLayoutPanel1.TabIndex = 242;
            // 
            // btnValider
            // 
            this.btnValider.FlatAppearance.BorderSize = 0;
            this.btnValider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnValider.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnValider.ForeColor = System.Drawing.Color.LightBlue;
            this.btnValider.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.btnValider.IconColor = System.Drawing.Color.LightBlue;
            this.btnValider.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnValider.IconSize = 32;
            this.btnValider.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnValider.Location = new System.Drawing.Point(1001, 4);
            this.btnValider.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnValider.Name = "btnValider";
            this.btnValider.Size = new System.Drawing.Size(150, 41);
            this.btnValider.TabIndex = 243;
            this.btnValider.Text = "Afficher Elèves";
            this.btnValider.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnValider.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnValider.UseVisualStyleBackColor = true;
            this.btnValider.Click += new System.EventHandler(this.btnValider_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel3.Controls.Add(this.cbxMatière, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnMatière, 1, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(520, 4);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(142, 39);
            this.tableLayoutPanel3.TabIndex = 243;
            // 
            // btnMatière
            // 
            this.btnMatière.FlatAppearance.BorderSize = 0;
            this.btnMatière.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMatière.ForeColor = System.Drawing.Color.LightBlue;
            this.btnMatière.IconChar = FontAwesome.Sharp.IconChar.Plus;
            this.btnMatière.IconColor = System.Drawing.Color.LightBlue;
            this.btnMatière.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnMatière.IconSize = 20;
            this.btnMatière.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMatière.Location = new System.Drawing.Point(106, 4);
            this.btnMatière.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnMatière.Name = "btnMatière";
            this.btnMatière.Size = new System.Drawing.Size(32, 31);
            this.btnMatière.TabIndex = 239;
            this.btnMatière.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMatière.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnMatière.UseVisualStyleBackColor = true;
            this.btnMatière.Click += new System.EventHandler(this.btnMatière_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.Controls.Add(this.btnExamen, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbxExamen, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(298, 4);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(140, 41);
            this.tableLayoutPanel2.TabIndex = 243;
            // 
            // btnExamen
            // 
            this.btnExamen.FlatAppearance.BorderSize = 0;
            this.btnExamen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExamen.ForeColor = System.Drawing.Color.LightBlue;
            this.btnExamen.IconChar = FontAwesome.Sharp.IconChar.Plus;
            this.btnExamen.IconColor = System.Drawing.Color.LightBlue;
            this.btnExamen.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnExamen.IconSize = 20;
            this.btnExamen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExamen.Location = new System.Drawing.Point(104, 4);
            this.btnExamen.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExamen.Name = "btnExamen";
            this.btnExamen.Size = new System.Drawing.Size(32, 31);
            this.btnExamen.TabIndex = 238;
            this.btnExamen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExamen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExamen.UseVisualStyleBackColor = true;
            this.btnExamen.Click += new System.EventHandler(this.btnExamen_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.LightBlue;
            this.label4.Location = new System.Drawing.Point(4, 0);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 46);
            this.label4.TabIndex = 234;
            this.label4.Text = "Classe :";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel4.Controls.Add(this.btnAjoutClasse, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.cbxClasse, 0, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(76, 4);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(140, 41);
            this.tableLayoutPanel4.TabIndex = 243;
            // 
            // btnAjoutClasse
            // 
            this.btnAjoutClasse.FlatAppearance.BorderSize = 0;
            this.btnAjoutClasse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAjoutClasse.ForeColor = System.Drawing.Color.LightBlue;
            this.btnAjoutClasse.IconChar = FontAwesome.Sharp.IconChar.Plus;
            this.btnAjoutClasse.IconColor = System.Drawing.Color.LightBlue;
            this.btnAjoutClasse.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnAjoutClasse.IconSize = 20;
            this.btnAjoutClasse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAjoutClasse.Location = new System.Drawing.Point(104, 4);
            this.btnAjoutClasse.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAjoutClasse.Name = "btnAjoutClasse";
            this.btnAjoutClasse.Size = new System.Drawing.Size(32, 31);
            this.btnAjoutClasse.TabIndex = 238;
            this.btnAjoutClasse.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAjoutClasse.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAjoutClasse.UseVisualStyleBackColor = true;
            this.btnAjoutClasse.Click += new System.EventHandler(this.btnAjoutClasse_Click);
            // 
            // cbxClasse
            // 
            this.cbxClasse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxClasse.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxClasse.FormattingEnabled = true;
            this.cbxClasse.Location = new System.Drawing.Point(4, 4);
            this.cbxClasse.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbxClasse.Name = "cbxClasse";
            this.cbxClasse.Size = new System.Drawing.Size(92, 29);
            this.cbxClasse.TabIndex = 236;
            this.cbxClasse.SelectedIndexChanged += new System.EventHandler(this.cbxClasse_SelectedIndexChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(16, 91);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1135, 514);
            this.flowLayoutPanel1.TabIndex = 243;
            this.flowLayoutPanel1.SizeChanged += new System.EventHandler(this.flowLayoutPanel1_SizeChanged);
            this.flowLayoutPanel1.Resize += new System.EventHandler(this.flowLayoutPanel1_Resize);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(268, 11);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(13, 16);
            this.dataGridView1.TabIndex = 244;
            this.dataGridView1.Visible = false;
            // 
            // btnEnregistrer
            // 
            this.btnEnregistrer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEnregistrer.FlatAppearance.BorderSize = 0;
            this.btnEnregistrer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnregistrer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnregistrer.ForeColor = System.Drawing.Color.LightBlue;
            this.btnEnregistrer.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            this.btnEnregistrer.IconColor = System.Drawing.Color.LightBlue;
            this.btnEnregistrer.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnEnregistrer.IconSize = 32;
            this.btnEnregistrer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEnregistrer.Location = new System.Drawing.Point(961, 613);
            this.btnEnregistrer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnEnregistrer.Name = "btnEnregistrer";
            this.btnEnregistrer.Size = new System.Drawing.Size(189, 42);
            this.btnEnregistrer.TabIndex = 245;
            this.btnEnregistrer.Text = "Enregistrer";
            this.btnEnregistrer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEnregistrer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEnregistrer.UseVisualStyleBackColor = true;
            this.btnEnregistrer.Click += new System.EventHandler(this.btnEnregistrer_Click);
            // 
            // Note_Groupé
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.ClientSize = new System.Drawing.Size(1167, 663);
            this.Controls.Add(this.btnEnregistrer);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Note_Groupé";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Note Groupé";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FontAwesome.Sharp.IconButton btnExamen;
        private FontAwesome.Sharp.IconButton btnMatière;
        private System.Windows.Forms.TextBox txtCoeff;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxMatière;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxExamen;
        private System.Windows.Forms.Label lblClasse;
        public System.Windows.Forms.DateTimePicker NaissanceDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        public FontAwesome.Sharp.IconButton btnValider;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private FontAwesome.Sharp.IconButton btnAjoutClasse;
        private System.Windows.Forms.ComboBox cbxClasse;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        public FontAwesome.Sharp.IconButton btnEnregistrer;
    }
}