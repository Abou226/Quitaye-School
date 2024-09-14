namespace Quitaye_School.User_Interface
{
    partial class Dépenses_Recette
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
            this.lblRef = new System.Windows.Forms.Label();
            this.Date_Operation = new System.Windows.Forms.DateTimePicker();
            this.txtFacture = new System.Windows.Forms.TextBox();
            this.txtCommentaire = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbxCompteTier = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblMontantCrédit = new System.Windows.Forms.Label();
            this.lblMontantDébit = new System.Windows.Forms.Label();
            this.lblcomptegénéral = new System.Windows.Forms.Label();
            this.btnAjouterCatégorie = new FontAwesome.Sharp.IconButton();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtDébit = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.btnEnregistrer = new FontAwesome.Sharp.IconButton();
            this.txtCrédit = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbxJournal = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cbxCompte = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnAjouter = new FontAwesome.Sharp.IconButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblRef
            // 
            this.lblRef.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRef.AutoSize = true;
            this.lblRef.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRef.ForeColor = System.Drawing.Color.Aqua;
            this.lblRef.Location = new System.Drawing.Point(474, 0);
            this.lblRef.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRef.Name = "lblRef";
            this.lblRef.Size = new System.Drawing.Size(139, 23);
            this.lblRef.TabIndex = 100;
            this.lblRef.Text = "Référence Pièce :";
            this.lblRef.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Date_Operation
            // 
            this.Date_Operation.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Date_Operation.CalendarMonthBackground = System.Drawing.Color.LightCoral;
            this.Date_Operation.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Date_Operation.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Date_Operation.Location = new System.Drawing.Point(4, 46);
            this.Date_Operation.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Date_Operation.Name = "Date_Operation";
            this.Date_Operation.Size = new System.Drawing.Size(135, 29);
            this.Date_Operation.TabIndex = 0;
            // 
            // txtFacture
            // 
            this.txtFacture.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtFacture.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFacture.Location = new System.Drawing.Point(147, 46);
            this.txtFacture.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtFacture.Name = "txtFacture";
            this.txtFacture.Size = new System.Drawing.Size(135, 29);
            this.txtFacture.TabIndex = 1;
            // 
            // txtCommentaire
            // 
            this.txtCommentaire.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtCommentaire.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCommentaire.Location = new System.Drawing.Point(576, 46);
            this.txtCommentaire.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCommentaire.Name = "txtCommentaire";
            this.txtCommentaire.Size = new System.Drawing.Size(135, 29);
            this.txtCommentaire.TabIndex = 4;
            this.txtCommentaire.TextChanged += new System.EventHandler(this.txtCommentaire_TextChanged);
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.LightBlue;
            this.label8.Location = new System.Drawing.Point(5, 0);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(133, 42);
            this.label8.TabIndex = 100;
            this.label8.Text = "Date Opération :";
            // 
            // cbxCompteTier
            // 
            this.cbxCompteTier.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxCompteTier.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxCompteTier.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxCompteTier.FormattingEnabled = true;
            this.cbxCompteTier.Location = new System.Drawing.Point(433, 46);
            this.cbxCompteTier.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbxCompteTier.Name = "cbxCompteTier";
            this.cbxCompteTier.Size = new System.Drawing.Size(135, 29);
            this.cbxCompteTier.TabIndex = 3;
            this.cbxCompteTier.SelectedIndexChanged += new System.EventHandler(this.cbxCompteTier_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.LightBlue;
            this.label9.Location = new System.Drawing.Point(165, 0);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(98, 23);
            this.label9.TabIndex = 100;
            this.label9.Text = "N° Facture :";
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.LightBlue;
            this.label10.Location = new System.Drawing.Point(290, 0);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(135, 42);
            this.label10.TabIndex = 100;
            this.label10.Text = "Compte Général :";
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.LightBlue;
            this.label11.Location = new System.Drawing.Point(444, 0);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(113, 23);
            this.label11.TabIndex = 100;
            this.label11.Text = "Compte Tier :";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.lblRef);
            this.flowLayoutPanel1.Controls.Add(this.lblMontantCrédit);
            this.flowLayoutPanel1.Controls.Add(this.lblMontantDébit);
            this.flowLayoutPanel1.Controls.Add(this.lblcomptegénéral);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(533, 11);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(617, 37);
            this.flowLayoutPanel1.TabIndex = 192;
            // 
            // lblMontantCrédit
            // 
            this.lblMontantCrédit.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblMontantCrédit.AutoSize = true;
            this.lblMontantCrédit.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMontantCrédit.ForeColor = System.Drawing.Color.Aqua;
            this.lblMontantCrédit.Location = new System.Drawing.Point(401, 0);
            this.lblMontantCrédit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMontantCrédit.Name = "lblMontantCrédit";
            this.lblMontantCrédit.Size = new System.Drawing.Size(65, 23);
            this.lblMontantCrédit.TabIndex = 100;
            this.lblMontantCrédit.Text = "Crédit :";
            // 
            // lblMontantDébit
            // 
            this.lblMontantDébit.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblMontantDébit.AutoSize = true;
            this.lblMontantDébit.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMontantDébit.ForeColor = System.Drawing.Color.Aqua;
            this.lblMontantDébit.Location = new System.Drawing.Point(333, 0);
            this.lblMontantDébit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMontantDébit.Name = "lblMontantDébit";
            this.lblMontantDébit.Size = new System.Drawing.Size(60, 23);
            this.lblMontantDébit.TabIndex = 100;
            this.lblMontantDébit.Text = "Débit :";
            // 
            // lblcomptegénéral
            // 
            this.lblcomptegénéral.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblcomptegénéral.AutoSize = true;
            this.lblcomptegénéral.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcomptegénéral.ForeColor = System.Drawing.Color.Aqua;
            this.lblcomptegénéral.Location = new System.Drawing.Point(181, 0);
            this.lblcomptegénéral.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblcomptegénéral.Name = "lblcomptegénéral";
            this.lblcomptegénéral.Size = new System.Drawing.Size(144, 23);
            this.lblcomptegénéral.TabIndex = 100;
            this.lblcomptegénéral.Text = "Compte Général :";
            this.lblcomptegénéral.Click += new System.EventHandler(this.lblcomptegénéral_Click);
            // 
            // btnAjouterCatégorie
            // 
            this.btnAjouterCatégorie.FlatAppearance.BorderSize = 0;
            this.btnAjouterCatégorie.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAjouterCatégorie.ForeColor = System.Drawing.Color.LightBlue;
            this.btnAjouterCatégorie.IconChar = FontAwesome.Sharp.IconChar.Plus;
            this.btnAjouterCatégorie.IconColor = System.Drawing.Color.LightBlue;
            this.btnAjouterCatégorie.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnAjouterCatégorie.IconSize = 20;
            this.btnAjouterCatégorie.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAjouterCatégorie.Location = new System.Drawing.Point(279, 14);
            this.btnAjouterCatégorie.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAjouterCatégorie.Name = "btnAjouterCatégorie";
            this.btnAjouterCatégorie.Size = new System.Drawing.Size(247, 34);
            this.btnAjouterCatégorie.TabIndex = 186;
            this.btnAjouterCatégorie.Text = "Ajouter un Compte Comptable";
            this.btnAjouterCatégorie.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAjouterCatégorie.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAjouterCatégorie.UseVisualStyleBackColor = true;
            this.btnAjouterCatégorie.Click += new System.EventHandler(this.btnAjouterCatégorie_Click);
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.LightBlue;
            this.label12.Location = new System.Drawing.Point(588, 0);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(110, 23);
            this.label12.TabIndex = 100;
            this.label12.Text = "Description  :";
            // 
            // label13
            // 
            this.label13.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.LightBlue;
            this.label13.Location = new System.Drawing.Point(756, 0);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(60, 23);
            this.label13.TabIndex = 100;
            this.label13.Text = "Débit :";
            // 
            // txtDébit
            // 
            this.txtDébit.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtDébit.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDébit.Location = new System.Drawing.Point(719, 46);
            this.txtDébit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDébit.Name = "txtDébit";
            this.txtDébit.Size = new System.Drawing.Size(135, 29);
            this.txtDébit.TabIndex = 5;
            this.txtDébit.TextChanged += new System.EventHandler(this.txtDébit_TextChanged);
            this.txtDébit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDébit_KeyPress);
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.LightBlue;
            this.label14.Location = new System.Drawing.Point(898, 0);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 23);
            this.label14.TabIndex = 100;
            this.label14.Text = "Crédit :";
            // 
            // btnEnregistrer
            // 
            this.btnEnregistrer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEnregistrer.FlatAppearance.BorderSize = 0;
            this.btnEnregistrer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnregistrer.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnregistrer.ForeColor = System.Drawing.Color.LightBlue;
            this.btnEnregistrer.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            this.btnEnregistrer.IconColor = System.Drawing.Color.LightBlue;
            this.btnEnregistrer.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnEnregistrer.IconSize = 32;
            this.btnEnregistrer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEnregistrer.Location = new System.Drawing.Point(981, 612);
            this.btnEnregistrer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnEnregistrer.Name = "btnEnregistrer";
            this.btnEnregistrer.Padding = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.btnEnregistrer.Size = new System.Drawing.Size(176, 42);
            this.btnEnregistrer.TabIndex = 189;
            this.btnEnregistrer.Text = "Enregistrer";
            this.btnEnregistrer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEnregistrer.UseVisualStyleBackColor = true;
            this.btnEnregistrer.Click += new System.EventHandler(this.btnEnregistrer_Click);
            // 
            // txtCrédit
            // 
            this.txtCrédit.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtCrédit.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCrédit.Location = new System.Drawing.Point(862, 46);
            this.txtCrédit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCrédit.Name = "txtCrédit";
            this.txtCrédit.Size = new System.Drawing.Size(138, 29);
            this.txtCrédit.TabIndex = 6;
            this.txtCrédit.TextChanged += new System.EventHandler(this.txtCrédit_TextChanged);
            this.txtCrédit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDébit_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.LightBlue;
            this.label7.Location = new System.Drawing.Point(21, 20);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(119, 23);
            this.label7.TabIndex = 188;
            this.label7.Text = "Code Journal :";
            // 
            // cbxJournal
            // 
            this.cbxJournal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxJournal.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxJournal.FormattingEnabled = true;
            this.cbxJournal.Location = new System.Drawing.Point(152, 16);
            this.cbxJournal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbxJournal.Name = "cbxJournal";
            this.cbxJournal.Size = new System.Drawing.Size(101, 29);
            this.cbxJournal.TabIndex = 185;
            this.cbxJournal.SelectedIndexChanged += new System.EventHandler(this.cbxJournal_SelectedIndexChanged);
            // 
            // cbxCompte
            // 
            this.cbxCompte.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxCompte.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxCompte.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxCompte.FormattingEnabled = true;
            this.cbxCompte.Location = new System.Drawing.Point(290, 46);
            this.cbxCompte.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbxCompte.Name = "cbxCompte";
            this.cbxCompte.Size = new System.Drawing.Size(135, 29);
            this.cbxCompte.TabIndex = 2;
            this.cbxCompte.SelectedIndexChanged += new System.EventHandler(this.txtCompte_TextChanged);
            this.cbxCompte.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCompte_KeyPress);
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
            this.dataGridView1.Location = new System.Drawing.Point(11, 150);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.SlateBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Indigo;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1141, 454);
            this.dataGridView1.TabIndex = 191;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // btnAjouter
            // 
            this.btnAjouter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAjouter.FlatAppearance.BorderSize = 0;
            this.btnAjouter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAjouter.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAjouter.ForeColor = System.Drawing.Color.LightBlue;
            this.btnAjouter.IconChar = FontAwesome.Sharp.IconChar.Plus;
            this.btnAjouter.IconColor = System.Drawing.Color.LightBlue;
            this.btnAjouter.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnAjouter.IconSize = 20;
            this.btnAjouter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAjouter.Location = new System.Drawing.Point(1023, 79);
            this.btnAjouter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAjouter.Name = "btnAjouter";
            this.btnAjouter.Padding = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.btnAjouter.Size = new System.Drawing.Size(135, 42);
            this.btnAjouter.TabIndex = 187;
            this.btnAjouter.Text = "Ajouter";
            this.btnAjouter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAjouter.UseVisualStyleBackColor = true;
            this.btnAjouter.Click += new System.EventHandler(this.btnAjouter_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.49994F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.49994F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.49994F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.49994F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.49994F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.49994F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.49994F));
            this.tableLayoutPanel1.Controls.Add(this.Date_Operation, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtFacture, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtCommentaire, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbxCompteTier, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label9, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label10, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label11, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label12, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.label13, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtDébit, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.label14, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtCrédit, 6, 1);
            this.tableLayoutPanel1.Controls.Add(this.cbxCompte, 2, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(11, 58);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1004, 85);
            this.tableLayoutPanel1.TabIndex = 190;
            // 
            // Dépenses_Recette
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.ClientSize = new System.Drawing.Size(1167, 663);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.btnAjouterCatégorie);
            this.Controls.Add(this.btnEnregistrer);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbxJournal);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnAjouter);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Dépenses_Recette";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dépenses_Recette";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblRef;
        private System.Windows.Forms.DateTimePicker Date_Operation;
        private System.Windows.Forms.TextBox txtFacture;
        private System.Windows.Forms.TextBox txtCommentaire;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbxCompteTier;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label lblMontantCrédit;
        private System.Windows.Forms.Label lblMontantDébit;
        private System.Windows.Forms.Label lblcomptegénéral;
        private FontAwesome.Sharp.IconButton btnAjouterCatégorie;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtDébit;
        private System.Windows.Forms.Label label14;
        private FontAwesome.Sharp.IconButton btnEnregistrer;
        private System.Windows.Forms.TextBox txtCrédit;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbxJournal;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox cbxCompte;
        private System.Windows.Forms.DataGridView dataGridView1;
        private FontAwesome.Sharp.IconButton btnAjouter;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}