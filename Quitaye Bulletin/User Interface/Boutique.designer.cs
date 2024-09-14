namespace Quitaye_School.User_Interface
{
    partial class Boutique
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnPdf = new FontAwesome.Sharp.IconButton();
            this.btnExcel = new FontAwesome.Sharp.IconButton();
            this.cbxTaille = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxCatégorie = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxMarque = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnInitialiserStock = new FontAwesome.Sharp.IconButton();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.lblMontant = new System.Windows.Forms.Label();
            this.iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            this.btnEttiquetteProduit = new FontAwesome.Sharp.IconButton();
            this.btnImporterExcel = new FontAwesome.Sharp.IconButton();
            this.btnNouvelleInventaire = new FontAwesome.Sharp.IconButton();
            this.btnProduitSimple = new FontAwesome.Sharp.IconButton();
            this.btnAjouter = new FontAwesome.Sharp.IconButton();
            this.btnNonPrice = new FontAwesome.Sharp.IconButton();
            this.btnCheckPrice = new FontAwesome.Sharp.IconButton();
            this.btnNext = new FontAwesome.Sharp.IconButton();
            this.btnPreview = new FontAwesome.Sharp.IconButton();
            this.lblCurrentPage = new System.Windows.Forms.Label();
            this.pagePanel = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1)).BeginInit();
            this.pagePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.DarkOrchid;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Indigo;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(16, 149);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.SlateBlue;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Indigo;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1135, 561);
            this.dataGridView1.TabIndex = 162;
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DataGridView1_CellFormatting);
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(887, 9);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(262, 29);
            this.txtSearch.TabIndex = 158;
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 15;
            this.bunifuElipse1.TargetControl = this.txtSearch;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 9;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.81815F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.102351F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.81019F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.102606F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.80719F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.102606F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.80719F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.22686F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.22286F));
            this.tableLayoutPanel2.Controls.Add(this.btnPdf, 8, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnExcel, 7, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbxTaille, 6, 0);
            this.tableLayoutPanel2.Controls.Add(this.label3, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbxCatégorie, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbxMarque, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnInitialiserStock, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(16, 95);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1135, 49);
            this.tableLayoutPanel2.TabIndex = 263;
            // 
            // btnPdf
            // 
            this.btnPdf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPdf.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.btnPdf.FlatAppearance.BorderSize = 0;
            this.btnPdf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPdf.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPdf.ForeColor = System.Drawing.Color.LightBlue;
            this.btnPdf.IconChar = FontAwesome.Sharp.IconChar.Print;
            this.btnPdf.IconColor = System.Drawing.Color.LightBlue;
            this.btnPdf.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnPdf.IconSize = 32;
            this.btnPdf.Location = new System.Drawing.Point(1019, 4);
            this.btnPdf.Margin = new System.Windows.Forms.Padding(4);
            this.btnPdf.Name = "btnPdf";
            this.btnPdf.Padding = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.btnPdf.Size = new System.Drawing.Size(112, 41);
            this.btnPdf.TabIndex = 167;
            this.btnPdf.Text = "PDF";
            this.btnPdf.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPdf.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPdf.UseVisualStyleBackColor = false;
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.btnExcel.FlatAppearance.BorderSize = 0;
            this.btnExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.ForeColor = System.Drawing.Color.LightBlue;
            this.btnExcel.IconChar = FontAwesome.Sharp.IconChar.Print;
            this.btnExcel.IconColor = System.Drawing.Color.LightBlue;
            this.btnExcel.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnExcel.IconSize = 32;
            this.btnExcel.Location = new System.Drawing.Point(903, 4);
            this.btnExcel.Margin = new System.Windows.Forms.Padding(4);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Padding = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.btnExcel.Size = new System.Drawing.Size(108, 41);
            this.btnExcel.TabIndex = 167;
            this.btnExcel.Text = "Excel";
            this.btnExcel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExcel.UseVisualStyleBackColor = false;
            // 
            // cbxTaille
            // 
            this.cbxTaille.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxTaille.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTaille.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxTaille.FormattingEnabled = true;
            this.cbxTaille.Location = new System.Drawing.Point(769, 4);
            this.cbxTaille.Margin = new System.Windows.Forms.Padding(4);
            this.cbxTaille.Name = "cbxTaille";
            this.cbxTaille.Size = new System.Drawing.Size(126, 29);
            this.cbxTaille.TabIndex = 107;
            this.cbxTaille.Visible = false;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.LightBlue;
            this.label3.Location = new System.Drawing.Point(691, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 23);
            this.label3.TabIndex = 163;
            this.label3.Text = "Taille :";
            this.label3.Visible = false;
            // 
            // cbxCatégorie
            // 
            this.cbxCatégorie.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxCatégorie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCatégorie.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxCatégorie.FormattingEnabled = true;
            this.cbxCatégorie.Location = new System.Drawing.Point(544, 4);
            this.cbxCatégorie.Margin = new System.Windows.Forms.Padding(4);
            this.cbxCatégorie.Name = "cbxCatégorie";
            this.cbxCatégorie.Size = new System.Drawing.Size(126, 29);
            this.cbxCatégorie.TabIndex = 107;
            this.cbxCatégorie.Visible = false;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.LightBlue;
            this.label2.Location = new System.Drawing.Point(457, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 46);
            this.label2.TabIndex = 163;
            this.label2.Text = "Catégorie :";
            this.label2.Visible = false;
            // 
            // cbxMarque
            // 
            this.cbxMarque.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxMarque.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMarque.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxMarque.FormattingEnabled = true;
            this.cbxMarque.Location = new System.Drawing.Point(319, 4);
            this.cbxMarque.Margin = new System.Windows.Forms.Padding(4);
            this.cbxMarque.Name = "cbxMarque";
            this.cbxMarque.Size = new System.Drawing.Size(126, 29);
            this.cbxMarque.TabIndex = 107;
            this.cbxMarque.Visible = false;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.LightBlue;
            this.label1.Location = new System.Drawing.Point(230, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 23);
            this.label1.TabIndex = 49;
            this.label1.Text = "Marque :";
            this.label1.Visible = false;
            // 
            // btnInitialiserStock
            // 
            this.btnInitialiserStock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.btnInitialiserStock.FlatAppearance.BorderSize = 0;
            this.btnInitialiserStock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInitialiserStock.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInitialiserStock.ForeColor = System.Drawing.Color.LightBlue;
            this.btnInitialiserStock.IconChar = FontAwesome.Sharp.IconChar.List;
            this.btnInitialiserStock.IconColor = System.Drawing.Color.LightBlue;
            this.btnInitialiserStock.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnInitialiserStock.IconSize = 26;
            this.btnInitialiserStock.Location = new System.Drawing.Point(4, 4);
            this.btnInitialiserStock.Margin = new System.Windows.Forms.Padding(4);
            this.btnInitialiserStock.Name = "btnInitialiserStock";
            this.btnInitialiserStock.Size = new System.Drawing.Size(209, 41);
            this.btnInitialiserStock.TabIndex = 163;
            this.btnInitialiserStock.Text = "Reinitialiser Stock";
            this.btnInitialiserStock.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInitialiserStock.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnInitialiserStock.UseVisualStyleBackColor = false;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(37, 5);
            this.dataGridView2.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.Size = new System.Drawing.Size(55, 18);
            this.dataGridView2.TabIndex = 265;
            this.dataGridView2.Visible = false;
            // 
            // lblMontant
            // 
            this.lblMontant.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMontant.BackColor = System.Drawing.Color.Transparent;
            this.lblMontant.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMontant.ForeColor = System.Drawing.Color.Cyan;
            this.lblMontant.Location = new System.Drawing.Point(691, 52);
            this.lblMontant.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMontant.Name = "lblMontant";
            this.lblMontant.Size = new System.Drawing.Size(472, 21);
            this.lblMontant.TabIndex = 49;
            this.lblMontant.Text = "Marque :";
            this.lblMontant.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // iconPictureBox1
            // 
            this.iconPictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.iconPictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.iconPictureBox1.ForeColor = System.Drawing.Color.LightBlue;
            this.iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlass;
            this.iconPictureBox1.IconColor = System.Drawing.Color.LightBlue;
            this.iconPictureBox1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconPictureBox1.IconSize = 31;
            this.iconPictureBox1.Location = new System.Drawing.Point(846, 9);
            this.iconPictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.iconPictureBox1.Name = "iconPictureBox1";
            this.iconPictureBox1.Size = new System.Drawing.Size(33, 31);
            this.iconPictureBox1.TabIndex = 262;
            this.iconPictureBox1.TabStop = false;
            // 
            // btnEttiquetteProduit
            // 
            this.btnEttiquetteProduit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.btnEttiquetteProduit.FlatAppearance.BorderSize = 0;
            this.btnEttiquetteProduit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEttiquetteProduit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEttiquetteProduit.ForeColor = System.Drawing.Color.LightBlue;
            this.btnEttiquetteProduit.IconChar = FontAwesome.Sharp.IconChar.Ticket;
            this.btnEttiquetteProduit.IconColor = System.Drawing.Color.LightBlue;
            this.btnEttiquetteProduit.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnEttiquetteProduit.IconSize = 26;
            this.btnEttiquetteProduit.Location = new System.Drawing.Point(631, 1);
            this.btnEttiquetteProduit.Margin = new System.Windows.Forms.Padding(4);
            this.btnEttiquetteProduit.Name = "btnEttiquetteProduit";
            this.btnEttiquetteProduit.Size = new System.Drawing.Size(203, 42);
            this.btnEttiquetteProduit.TabIndex = 163;
            this.btnEttiquetteProduit.Text = "Ettiquette Produit";
            this.btnEttiquetteProduit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEttiquetteProduit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEttiquetteProduit.UseVisualStyleBackColor = false;
            // 
            // btnImporterExcel
            // 
            this.btnImporterExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.btnImporterExcel.FlatAppearance.BorderSize = 0;
            this.btnImporterExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImporterExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImporterExcel.ForeColor = System.Drawing.Color.LightBlue;
            this.btnImporterExcel.IconChar = FontAwesome.Sharp.IconChar.FileExcel;
            this.btnImporterExcel.IconColor = System.Drawing.Color.LightBlue;
            this.btnImporterExcel.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnImporterExcel.IconSize = 26;
            this.btnImporterExcel.Location = new System.Drawing.Point(440, 6);
            this.btnImporterExcel.Margin = new System.Windows.Forms.Padding(4);
            this.btnImporterExcel.Name = "btnImporterExcel";
            this.btnImporterExcel.Size = new System.Drawing.Size(183, 38);
            this.btnImporterExcel.TabIndex = 163;
            this.btnImporterExcel.Text = "Importer Excel";
            this.btnImporterExcel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImporterExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnImporterExcel.UseVisualStyleBackColor = false;
            // 
            // btnNouvelleInventaire
            // 
            this.btnNouvelleInventaire.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.btnNouvelleInventaire.FlatAppearance.BorderSize = 0;
            this.btnNouvelleInventaire.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNouvelleInventaire.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNouvelleInventaire.ForeColor = System.Drawing.Color.LightBlue;
            this.btnNouvelleInventaire.IconChar = FontAwesome.Sharp.IconChar.List;
            this.btnNouvelleInventaire.IconColor = System.Drawing.Color.LightBlue;
            this.btnNouvelleInventaire.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnNouvelleInventaire.IconSize = 26;
            this.btnNouvelleInventaire.Location = new System.Drawing.Point(223, 5);
            this.btnNouvelleInventaire.Margin = new System.Windows.Forms.Padding(4);
            this.btnNouvelleInventaire.Name = "btnNouvelleInventaire";
            this.btnNouvelleInventaire.Size = new System.Drawing.Size(217, 39);
            this.btnNouvelleInventaire.TabIndex = 163;
            this.btnNouvelleInventaire.Text = "Nouvelle Inventaire";
            this.btnNouvelleInventaire.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNouvelleInventaire.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNouvelleInventaire.UseVisualStyleBackColor = false;
            // 
            // btnProduitSimple
            // 
            this.btnProduitSimple.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.btnProduitSimple.FlatAppearance.BorderSize = 0;
            this.btnProduitSimple.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProduitSimple.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProduitSimple.ForeColor = System.Drawing.Color.LightBlue;
            this.btnProduitSimple.IconChar = FontAwesome.Sharp.IconChar.Add;
            this.btnProduitSimple.IconColor = System.Drawing.Color.LightBlue;
            this.btnProduitSimple.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnProduitSimple.IconSize = 26;
            this.btnProduitSimple.Location = new System.Drawing.Point(16, 52);
            this.btnProduitSimple.Margin = new System.Windows.Forms.Padding(4);
            this.btnProduitSimple.Name = "btnProduitSimple";
            this.btnProduitSimple.Size = new System.Drawing.Size(291, 39);
            this.btnProduitSimple.TabIndex = 163;
            this.btnProduitSimple.Text = "Nouveau Produit Simplifié";
            this.btnProduitSimple.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProduitSimple.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnProduitSimple.UseVisualStyleBackColor = false;
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
            this.btnAjouter.Location = new System.Drawing.Point(16, 5);
            this.btnAjouter.Margin = new System.Windows.Forms.Padding(4);
            this.btnAjouter.Name = "btnAjouter";
            this.btnAjouter.Size = new System.Drawing.Size(199, 39);
            this.btnAjouter.TabIndex = 163;
            this.btnAjouter.Text = "Nouveau Produit";
            this.btnAjouter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAjouter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAjouter.UseVisualStyleBackColor = false;
            // 
            // btnNonPrice
            // 
            this.btnNonPrice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.btnNonPrice.FlatAppearance.BorderSize = 0;
            this.btnNonPrice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNonPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNonPrice.ForeColor = System.Drawing.Color.LightBlue;
            this.btnNonPrice.IconChar = FontAwesome.Sharp.IconChar.Add;
            this.btnNonPrice.IconColor = System.Drawing.Color.LightBlue;
            this.btnNonPrice.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnNonPrice.IconSize = 26;
            this.btnNonPrice.Location = new System.Drawing.Point(315, 48);
            this.btnNonPrice.Margin = new System.Windows.Forms.Padding(4);
            this.btnNonPrice.Name = "btnNonPrice";
            this.btnNonPrice.Size = new System.Drawing.Size(139, 39);
            this.btnNonPrice.TabIndex = 163;
            this.btnNonPrice.Text = "All Price";
            this.btnNonPrice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNonPrice.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNonPrice.UseVisualStyleBackColor = false;
            // 
            // btnCheckPrice
            // 
            this.btnCheckPrice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.btnCheckPrice.FlatAppearance.BorderSize = 0;
            this.btnCheckPrice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheckPrice.ForeColor = System.Drawing.Color.LightBlue;
            this.btnCheckPrice.IconChar = FontAwesome.Sharp.IconChar.Add;
            this.btnCheckPrice.IconColor = System.Drawing.Color.LightBlue;
            this.btnCheckPrice.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnCheckPrice.IconSize = 26;
            this.btnCheckPrice.Location = new System.Drawing.Point(442, 52);
            this.btnCheckPrice.Margin = new System.Windows.Forms.Padding(4);
            this.btnCheckPrice.Name = "btnCheckPrice";
            this.btnCheckPrice.Size = new System.Drawing.Size(206, 35);
            this.btnCheckPrice.TabIndex = 163;
            this.btnCheckPrice.Text = "Purchase = Sale Price";
            this.btnCheckPrice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCheckPrice.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCheckPrice.UseVisualStyleBackColor = false;
            // 
            // btnNext
            // 
            this.btnNext.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.btnNext.FlatAppearance.BorderSize = 0;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.ForeColor = System.Drawing.Color.LightBlue;
            this.btnNext.IconChar = FontAwesome.Sharp.IconChar.ArrowRight;
            this.btnNext.IconColor = System.Drawing.Color.LightBlue;
            this.btnNext.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnNext.IconSize = 32;
            this.btnNext.Location = new System.Drawing.Point(308, 4);
            this.btnNext.Margin = new System.Windows.Forms.Padding(4);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(45, 38);
            this.btnNext.TabIndex = 170;
            this.btnNext.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNext.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNext.UseVisualStyleBackColor = false;
            // 
            // btnPreview
            // 
            this.btnPreview.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.btnPreview.FlatAppearance.BorderSize = 0;
            this.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPreview.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPreview.ForeColor = System.Drawing.Color.LightBlue;
            this.btnPreview.IconChar = FontAwesome.Sharp.IconChar.ArrowLeft;
            this.btnPreview.IconColor = System.Drawing.Color.LightBlue;
            this.btnPreview.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnPreview.IconSize = 32;
            this.btnPreview.Location = new System.Drawing.Point(4, 4);
            this.btnPreview.Margin = new System.Windows.Forms.Padding(4);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(45, 38);
            this.btnPreview.TabIndex = 169;
            this.btnPreview.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPreview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPreview.UseVisualStyleBackColor = false;
            // 
            // lblCurrentPage
            // 
            this.lblCurrentPage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblCurrentPage.AutoSize = true;
            this.lblCurrentPage.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrentPage.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentPage.ForeColor = System.Drawing.Color.LightBlue;
            this.lblCurrentPage.Location = new System.Drawing.Point(178, 11);
            this.lblCurrentPage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCurrentPage.Name = "lblCurrentPage";
            this.lblCurrentPage.Size = new System.Drawing.Size(0, 23);
            this.lblCurrentPage.TabIndex = 172;
            // 
            // pagePanel
            // 
            this.pagePanel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pagePanel.ColumnCount = 3;
            this.pagePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 53F));
            this.pagePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pagePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 53F));
            this.pagePanel.Controls.Add(this.lblCurrentPage, 1, 0);
            this.pagePanel.Controls.Add(this.btnPreview, 0, 0);
            this.pagePanel.Controls.Add(this.btnNext, 2, 0);
            this.pagePanel.Location = new System.Drawing.Point(405, 714);
            this.pagePanel.Margin = new System.Windows.Forms.Padding(4);
            this.pagePanel.Name = "pagePanel";
            this.pagePanel.RowCount = 1;
            this.pagePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pagePanel.Size = new System.Drawing.Size(357, 46);
            this.pagePanel.TabIndex = 264;
            // 
            // Boutique
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.ClientSize = new System.Drawing.Size(1167, 763);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.pagePanel);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.iconPictureBox1);
            this.Controls.Add(this.lblMontant);
            this.Controls.Add(this.btnEttiquetteProduit);
            this.Controls.Add(this.btnImporterExcel);
            this.Controls.Add(this.btnNouvelleInventaire);
            this.Controls.Add(this.btnCheckPrice);
            this.Controls.Add(this.btnNonPrice);
            this.Controls.Add(this.btnProduitSimple);
            this.Controls.Add(this.btnAjouter);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtSearch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Boutique";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inventaire Stock";
            this.Load += new System.EventHandler(this.Boutique_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1)).EndInit();
            this.pagePanel.ResumeLayout(false);
            this.pagePanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtSearch;
        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private FontAwesome.Sharp.IconButton btnAjouter;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private FontAwesome.Sharp.IconButton btnPdf;
        private FontAwesome.Sharp.IconButton btnExcel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxMarque;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxCatégorie;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbxTaille;
        private FontAwesome.Sharp.IconButton btnNouvelleInventaire;
        private FontAwesome.Sharp.IconButton btnInitialiserStock;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label lblMontant;
        private FontAwesome.Sharp.IconButton btnImporterExcel;
        private FontAwesome.Sharp.IconButton btnEttiquetteProduit;
        private FontAwesome.Sharp.IconButton btnProduitSimple;
        private FontAwesome.Sharp.IconButton btnNonPrice;
        private FontAwesome.Sharp.IconButton btnCheckPrice;
        private FontAwesome.Sharp.IconButton btnNext;
        private FontAwesome.Sharp.IconButton btnPreview;
        private System.Windows.Forms.Label lblCurrentPage;
        private System.Windows.Forms.TableLayoutPanel pagePanel;
    }
}