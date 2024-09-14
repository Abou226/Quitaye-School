namespace Quitaye_School.User_Interface
{
    partial class List_Passage
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblFille = new System.Windows.Forms.Label();
            this.lblGarçon = new System.Windows.Forms.Label();
            this.lblEffectif = new System.Windows.Forms.Label();
            this.cbxClasse = new System.Windows.Forms.ComboBox();
            this.txtsearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.cbxAnnéeSuivante = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbxAnnéePrecedente = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.btnSelectionDoublons = new FontAwesome.Sharp.IconButton();
            this.btnToutSelectionner = new FontAwesome.Sharp.IconButton();
            this.btnToutAnnuler = new FontAwesome.Sharp.IconButton();
            this.iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            this.btnExcel = new FontAwesome.Sharp.IconButton();
            this.btnPDF = new FontAwesome.Sharp.IconButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1)).BeginInit();
            this.SuspendLayout();
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
            this.dataGridView1.Location = new System.Drawing.Point(16, 143);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.SlateBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Indigo;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1131, 492);
            this.dataGridView1.TabIndex = 246;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 9;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46.82045F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.89478F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.28477F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 132F));
            this.tableLayoutPanel1.Controls.Add(this.lblFille, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblGarçon, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblEffectif, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbxClasse, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.iconPictureBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtsearch, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnExcel, 8, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnPDF, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(16, 16);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 53F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1135, 53);
            this.tableLayoutPanel1.TabIndex = 245;
            // 
            // lblFille
            // 
            this.lblFille.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblFille.AutoSize = true;
            this.lblFille.BackColor = System.Drawing.Color.Transparent;
            this.lblFille.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFille.ForeColor = System.Drawing.Color.LightBlue;
            this.lblFille.Location = new System.Drawing.Point(811, 0);
            this.lblFille.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFille.Name = "lblFille";
            this.lblFille.Size = new System.Drawing.Size(80, 23);
            this.lblFille.TabIndex = 247;
            this.lblFille.Text = "Fille : 870";
            // 
            // lblGarçon
            // 
            this.lblGarçon.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblGarçon.AutoSize = true;
            this.lblGarçon.BackColor = System.Drawing.Color.Transparent;
            this.lblGarçon.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGarçon.ForeColor = System.Drawing.Color.LightBlue;
            this.lblGarçon.Location = new System.Drawing.Point(688, 0);
            this.lblGarçon.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGarçon.Name = "lblGarçon";
            this.lblGarçon.Size = new System.Drawing.Size(106, 23);
            this.lblGarçon.TabIndex = 247;
            this.lblGarçon.Text = "Garçon : 565";
            // 
            // lblEffectif
            // 
            this.lblEffectif.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblEffectif.AutoSize = true;
            this.lblEffectif.BackColor = System.Drawing.Color.Transparent;
            this.lblEffectif.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEffectif.ForeColor = System.Drawing.Color.LightBlue;
            this.lblEffectif.Location = new System.Drawing.Point(570, 0);
            this.lblEffectif.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEffectif.Name = "lblEffectif";
            this.lblEffectif.Size = new System.Drawing.Size(102, 23);
            this.lblEffectif.TabIndex = 247;
            this.lblEffectif.Text = "Effectif : 759";
            // 
            // cbxClasse
            // 
            this.cbxClasse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxClasse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxClasse.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxClasse.FormattingEnabled = true;
            this.cbxClasse.Items.AddRange(new object[] {
            "Espèce",
            "Chèque",
            "Virement",
            "Carte Bleu"});
            this.cbxClasse.Location = new System.Drawing.Point(412, 4);
            this.cbxClasse.Margin = new System.Windows.Forms.Padding(4);
            this.cbxClasse.Name = "cbxClasse";
            this.cbxClasse.Size = new System.Drawing.Size(145, 29);
            this.cbxClasse.TabIndex = 234;
            // 
            // txtsearch
            // 
            this.txtsearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtsearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsearch.Location = new System.Drawing.Point(44, 4);
            this.txtsearch.Margin = new System.Windows.Forms.Padding(4);
            this.txtsearch.Name = "txtsearch";
            this.txtsearch.Size = new System.Drawing.Size(236, 29);
            this.txtsearch.TabIndex = 105;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.LightBlue;
            this.label1.Location = new System.Drawing.Point(313, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 23);
            this.label1.TabIndex = 49;
            this.label1.Text = "Classe :";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 7;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.83212F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.23648F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.23648F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.23648F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.44255F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.44255F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.57334F));
            this.tableLayoutPanel2.Controls.Add(this.cbxAnnéeSuivante, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.label6, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbxAnnéePrecedente, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnSelectionDoublons, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnToutSelectionner, 6, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnToutAnnuler, 5, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(16, 78);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1135, 53);
            this.tableLayoutPanel2.TabIndex = 247;
            // 
            // cbxAnnéeSuivante
            // 
            this.cbxAnnéeSuivante.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxAnnéeSuivante.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxAnnéeSuivante.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxAnnéeSuivante.FormattingEnabled = true;
            this.cbxAnnéeSuivante.Items.AddRange(new object[] {
            "Espèce",
            "Chèque",
            "Virement",
            "Carte Bleu"});
            this.cbxAnnéeSuivante.Location = new System.Drawing.Point(460, 4);
            this.cbxAnnéeSuivante.Margin = new System.Windows.Forms.Padding(4);
            this.cbxAnnéeSuivante.Name = "cbxAnnéeSuivante";
            this.cbxAnnéeSuivante.Size = new System.Drawing.Size(142, 29);
            this.cbxAnnéeSuivante.TabIndex = 252;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.LightBlue;
            this.label6.Location = new System.Drawing.Point(312, 0);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(138, 23);
            this.label6.TabIndex = 251;
            this.label6.Text = "Année Suivante :";
            // 
            // cbxAnnéePrecedente
            // 
            this.cbxAnnéePrecedente.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxAnnéePrecedente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxAnnéePrecedente.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxAnnéePrecedente.FormattingEnabled = true;
            this.cbxAnnéePrecedente.Items.AddRange(new object[] {
            "Espèce",
            "Chèque",
            "Virement",
            "Carte Bleu"});
            this.cbxAnnéePrecedente.Location = new System.Drawing.Point(160, 4);
            this.cbxAnnéePrecedente.Margin = new System.Windows.Forms.Padding(4);
            this.cbxAnnéePrecedente.Name = "cbxAnnéePrecedente";
            this.cbxAnnéePrecedente.Size = new System.Drawing.Size(142, 29);
            this.cbxAnnéePrecedente.TabIndex = 250;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.LightBlue;
            this.label5.Location = new System.Drawing.Point(25, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 46);
            this.label5.TabIndex = 249;
            this.label5.Text = "Année Precedente :";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(51, 110);
            this.dataGridView2.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.Size = new System.Drawing.Size(80, 12);
            this.dataGridView2.TabIndex = 248;
            this.dataGridView2.Visible = false;
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 15;
            this.bunifuElipse1.TargetControl = this.txtsearch;
            // 
            // btnSelectionDoublons
            // 
            this.btnSelectionDoublons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectionDoublons.FlatAppearance.BorderSize = 0;
            this.btnSelectionDoublons.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectionDoublons.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectionDoublons.ForeColor = System.Drawing.Color.LightBlue;
            this.btnSelectionDoublons.IconChar = FontAwesome.Sharp.IconChar.Print;
            this.btnSelectionDoublons.IconColor = System.Drawing.Color.LightBlue;
            this.btnSelectionDoublons.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnSelectionDoublons.IconSize = 20;
            this.btnSelectionDoublons.Location = new System.Drawing.Point(610, 4);
            this.btnSelectionDoublons.Margin = new System.Windows.Forms.Padding(4);
            this.btnSelectionDoublons.Name = "btnSelectionDoublons";
            this.btnSelectionDoublons.Size = new System.Drawing.Size(167, 44);
            this.btnSelectionDoublons.TabIndex = 232;
            this.btnSelectionDoublons.Text = "Selection Doublons";
            this.btnSelectionDoublons.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSelectionDoublons.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSelectionDoublons.UseVisualStyleBackColor = true;
            // 
            // btnToutSelectionner
            // 
            this.btnToutSelectionner.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnToutSelectionner.FlatAppearance.BorderSize = 0;
            this.btnToutSelectionner.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToutSelectionner.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnToutSelectionner.ForeColor = System.Drawing.Color.LightBlue;
            this.btnToutSelectionner.IconChar = FontAwesome.Sharp.IconChar.Print;
            this.btnToutSelectionner.IconColor = System.Drawing.Color.LightBlue;
            this.btnToutSelectionner.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnToutSelectionner.IconSize = 20;
            this.btnToutSelectionner.Location = new System.Drawing.Point(960, 4);
            this.btnToutSelectionner.Margin = new System.Windows.Forms.Padding(4);
            this.btnToutSelectionner.Name = "btnToutSelectionner";
            this.btnToutSelectionner.Size = new System.Drawing.Size(171, 44);
            this.btnToutSelectionner.TabIndex = 232;
            this.btnToutSelectionner.Text = "Tout Selectionner";
            this.btnToutSelectionner.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnToutSelectionner.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnToutSelectionner.UseVisualStyleBackColor = true;
            // 
            // btnToutAnnuler
            // 
            this.btnToutAnnuler.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnToutAnnuler.FlatAppearance.BorderSize = 0;
            this.btnToutAnnuler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToutAnnuler.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnToutAnnuler.ForeColor = System.Drawing.Color.Magenta;
            this.btnToutAnnuler.IconChar = FontAwesome.Sharp.IconChar.Print;
            this.btnToutAnnuler.IconColor = System.Drawing.Color.Magenta;
            this.btnToutAnnuler.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnToutAnnuler.IconSize = 20;
            this.btnToutAnnuler.Location = new System.Drawing.Point(785, 4);
            this.btnToutAnnuler.Margin = new System.Windows.Forms.Padding(4);
            this.btnToutAnnuler.Name = "btnToutAnnuler";
            this.btnToutAnnuler.Size = new System.Drawing.Size(167, 44);
            this.btnToutAnnuler.TabIndex = 248;
            this.btnToutAnnuler.Text = "Tout Annuler";
            this.btnToutAnnuler.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnToutAnnuler.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnToutAnnuler.UseVisualStyleBackColor = true;
            // 
            // iconPictureBox1
            // 
            this.iconPictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.iconPictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.iconPictureBox1.ForeColor = System.Drawing.Color.LightBlue;
            this.iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.Search;
            this.iconPictureBox1.IconColor = System.Drawing.Color.LightBlue;
            this.iconPictureBox1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconPictureBox1.IconSize = 31;
            this.iconPictureBox1.Location = new System.Drawing.Point(4, 4);
            this.iconPictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.iconPictureBox1.Name = "iconPictureBox1";
            this.iconPictureBox1.Size = new System.Drawing.Size(32, 31);
            this.iconPictureBox1.TabIndex = 211;
            this.iconPictureBox1.TabStop = false;
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcel.FlatAppearance.BorderSize = 0;
            this.btnExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.ForeColor = System.Drawing.Color.LightBlue;
            this.btnExcel.IconChar = FontAwesome.Sharp.IconChar.Print;
            this.btnExcel.IconColor = System.Drawing.Color.LightBlue;
            this.btnExcel.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnExcel.IconSize = 20;
            this.btnExcel.Location = new System.Drawing.Point(1035, 4);
            this.btnExcel.Margin = new System.Windows.Forms.Padding(4);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(96, 44);
            this.btnExcel.TabIndex = 231;
            this.btnExcel.Text = "Excel";
            this.btnExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExcel.UseVisualStyleBackColor = true;
            // 
            // btnPDF
            // 
            this.btnPDF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPDF.FlatAppearance.BorderSize = 0;
            this.btnPDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPDF.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPDF.ForeColor = System.Drawing.Color.LightBlue;
            this.btnPDF.IconChar = FontAwesome.Sharp.IconChar.Print;
            this.btnPDF.IconColor = System.Drawing.Color.LightBlue;
            this.btnPDF.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnPDF.IconSize = 20;
            this.btnPDF.Location = new System.Drawing.Point(905, 4);
            this.btnPDF.Margin = new System.Windows.Forms.Padding(4);
            this.btnPDF.Name = "btnPDF";
            this.btnPDF.Size = new System.Drawing.Size(92, 44);
            this.btnPDF.TabIndex = 230;
            this.btnPDF.Text = "PDF";
            this.btnPDF.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPDF.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPDF.UseVisualStyleBackColor = true;
            // 
            // List_Passage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.ClientSize = new System.Drawing.Size(1167, 663);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.dataGridView2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "List_Passage";
            this.Text = "List_Passage";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblFille;
        private System.Windows.Forms.Label lblGarçon;
        private System.Windows.Forms.Label lblEffectif;
        private System.Windows.Forms.ComboBox cbxClasse;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        private System.Windows.Forms.TextBox txtsearch;
        public FontAwesome.Sharp.IconButton btnExcel;
        public FontAwesome.Sharp.IconButton btnPDF;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ComboBox cbxAnnéeSuivante;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbxAnnéePrecedente;
        private System.Windows.Forms.Label label5;
        public FontAwesome.Sharp.IconButton btnToutAnnuler;
        public FontAwesome.Sharp.IconButton btnToutSelectionner;
        private System.Windows.Forms.DataGridView dataGridView2;
        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        public FontAwesome.Sharp.IconButton btnSelectionDoublons;
    }
}