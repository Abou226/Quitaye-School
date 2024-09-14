using Bunifu.Framework.UI;
using FontAwesome.Sharp;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System;

namespace Quitaye_School.User_Interface
{
    partial class AchatVente
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.Label lblfour;
        private System.Windows.Forms.Label lblclient;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.ComboBox cbxBonCommande;
        private System.Windows.Forms.TextBox txtNum;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label lblBC;
        private System.Windows.Forms.Label lblNum;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ComboBox cbxFournisseurs;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel panel3;
        private DataGridView dataGridView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox txtQuantité;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label14;
        public System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Panel panel4;
        public System.Windows.Forms.Panel panel5;
        public System.Windows.Forms.Panel panel6;
        public FontAwesome.Sharp.IconPictureBox btnFermer;
        private System.Windows.Forms.ComboBox cbxMesure;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private FontAwesome.Sharp.IconButton iconButton1;
        private FontAwesome.Sharp.IconButton btnFile;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        private FontAwesome.Sharp.IconButton btnImprimerTicket;
        private System.Windows.Forms.Label label19;
        private FontAwesome.Sharp.IconButton btnAddPayement;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtReference;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private System.Windows.Forms.ComboBox cbxClient;
        private FontAwesome.Sharp.IconButton btnAddClient;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox cbxFiliale;
        private System.Windows.Forms.Label lblFiliale;
        private FontAwesome.Sharp.IconButton btnAddFiliale;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel11;        
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.ComboBox cbxType;
        private System.Windows.Forms.TextBox txtStock;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AchatVente));
            this.label18 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblfour = new System.Windows.Forms.Label();
            this.lblclient = new System.Windows.Forms.Label();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.cbxBonCommande = new System.Windows.Forms.ComboBox();
            this.txtNum = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.cbxClient = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.cbxFournisseurs = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblBC = new System.Windows.Forms.Label();
            this.lblNum = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.cbxType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.checkCrédit = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel12 = new System.Windows.Forms.TableLayoutPanel();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel13 = new System.Windows.Forms.TableLayoutPanel();
            this.cbxPayement = new System.Windows.Forms.ComboBox();
            this.txtmontantpayer = new System.Windows.Forms.TextBox();
            this.txtNetTotal = new System.Windows.Forms.TextBox();
            this.txtRéduction = new System.Windows.Forms.TextBox();
            this.txtSousTotal = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.txtAretourner = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel20 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel21 = new System.Windows.Forms.TableLayoutPanel();
            this.cbxPendingList = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.cbxModePrix = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel14 = new System.Windows.Forms.TableLayoutPanel();
            this.label20 = new System.Windows.Forms.Label();
            this.cbxMesure = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel15 = new System.Windows.Forms.TableLayoutPanel();
            this.lblFiliale = new System.Windows.Forms.Label();
            this.cbxFiliale = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel16 = new System.Windows.Forms.TableLayoutPanel();
            this.txtNewCode = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.tableLayoutPanel17 = new System.Windows.Forms.TableLayoutPanel();
            this.cbxSearch = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tableLayoutPanel19 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel18 = new System.Windows.Forms.TableLayoutPanel();
            this.Date_Expiration = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxCode = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtNom = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label9 = new System.Windows.Forms.Label();
            this.txtQuantité = new System.Windows.Forms.TextBox();
            this.txtStock = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.txtMontant = new System.Windows.Forms.TextBox();
            this.txtPrixUnité = new System.Windows.Forms.TextBox();
            this.txtReference = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.label11 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.btnMinimize = new FontAwesome.Sharp.IconPictureBox();
            this.btnRestore = new FontAwesome.Sharp.IconPictureBox();
            this.btnClotureJournée = new FontAwesome.Sharp.IconButton();
            this.btnReprintTicket = new FontAwesome.Sharp.IconButton();
            this.btnFermer = new FontAwesome.Sharp.IconPictureBox();
            this.btnAddClient = new FontAwesome.Sharp.IconButton();
            this.iconButton1 = new FontAwesome.Sharp.IconButton();
            this.btnEnregistrer = new FontAwesome.Sharp.IconButton();
            this.iconButton3 = new FontAwesome.Sharp.IconButton();
            this.iconButton2 = new FontAwesome.Sharp.IconButton();
            this.iconButton5 = new FontAwesome.Sharp.IconButton();
            this.btnPending = new FontAwesome.Sharp.IconButton();
            this.iconPictureBox2 = new FontAwesome.Sharp.IconPictureBox();
            this.btnDeleteAll = new FontAwesome.Sharp.IconButton();
            this.btnReturn = new FontAwesome.Sharp.IconButton();
            this.btnAjouter = new FontAwesome.Sharp.IconButton();
            this.btnAddFiliale = new FontAwesome.Sharp.IconButton();
            this.btnSaveCode = new FontAwesome.Sharp.IconButton();
            this.iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            this.btnFile = new FontAwesome.Sharp.IconButton();
            this.btnAddPayement = new FontAwesome.Sharp.IconButton();
            this.btnImprimerTicket = new FontAwesome.Sharp.IconButton();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel7.SuspendLayout();
            this.tableLayoutPanel12.SuspendLayout();
            this.tableLayoutPanel13.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel20.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel21.SuspendLayout();
            this.panel8.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel14.SuspendLayout();
            this.tableLayoutPanel15.SuspendLayout();
            this.tableLayoutPanel16.SuspendLayout();
            this.tableLayoutPanel17.SuspendLayout();
            this.tableLayoutPanel19.SuspendLayout();
            this.tableLayoutPanel18.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRestore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFermer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label18
            // 
            this.label18.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.LightBlue;
            this.label18.Location = new System.Drawing.Point(445, 10);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(168, 32);
            this.label18.TabIndex = 61;
            this.label18.Text = "Achat && Vente";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.tableLayoutPanel4);
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.MaximumSize = new System.Drawing.Size(2119, 329);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(984, 66);
            this.panel1.TabIndex = 0;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.1446F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58.67074F));
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel8, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(-1, 0);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(981, 50);
            this.tableLayoutPanel4.TabIndex = 58;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel8.ColumnCount = 4;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.04762F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.61905F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.04762F));
            this.tableLayoutPanel8.Controls.Add(this.flowLayoutPanel4, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.flowLayoutPanel3, 3, 0);
            this.tableLayoutPanel8.Controls.Add(this.flowLayoutPanel1, 1, 0);
            this.tableLayoutPanel8.Controls.Add(this.flowLayoutPanel2, 2, 0);
            this.tableLayoutPanel8.Location = new System.Drawing.Point(298, 4);
            this.tableLayoutPanel8.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 1;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(679, 41);
            this.tableLayoutPanel8.TabIndex = 0;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel4.Controls.Add(this.lblfour);
            this.flowLayoutPanel4.Controls.Add(this.lblclient);
            this.flowLayoutPanel4.Location = new System.Drawing.Point(4, 4);
            this.flowLayoutPanel4.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(121, 25);
            this.flowLayoutPanel4.TabIndex = 58;
            // 
            // lblfour
            // 
            this.lblfour.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblfour.AutoSize = true;
            this.lblfour.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblfour.ForeColor = System.Drawing.Color.LightBlue;
            this.lblfour.Location = new System.Drawing.Point(4, 0);
            this.lblfour.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblfour.Name = "lblfour";
            this.lblfour.Size = new System.Drawing.Size(111, 23);
            this.lblfour.TabIndex = 46;
            this.lblfour.Text = "Fournisseur  :";
            // 
            // lblclient
            // 
            this.lblclient.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblclient.AutoSize = true;
            this.lblclient.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblclient.ForeColor = System.Drawing.Color.LightBlue;
            this.lblclient.Location = new System.Drawing.Point(4, 23);
            this.lblclient.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblclient.Name = "lblclient";
            this.lblclient.Size = new System.Drawing.Size(63, 23);
            this.lblclient.TabIndex = 46;
            this.lblclient.Text = "Client :";
            this.lblclient.Visible = false;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel3.Controls.Add(this.cbxBonCommande);
            this.flowLayoutPanel3.Controls.Add(this.txtNum);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(552, 4);
            this.flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(123, 33);
            this.flowLayoutPanel3.TabIndex = 58;
            // 
            // cbxBonCommande
            // 
            this.cbxBonCommande.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxBonCommande.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxBonCommande.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxBonCommande.FormattingEnabled = true;
            this.cbxBonCommande.Location = new System.Drawing.Point(4, 4);
            this.cbxBonCommande.Margin = new System.Windows.Forms.Padding(4);
            this.cbxBonCommande.Name = "cbxBonCommande";
            this.cbxBonCommande.Size = new System.Drawing.Size(209, 28);
            this.cbxBonCommande.TabIndex = 44;
            // 
            // txtNum
            // 
            this.txtNum.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.txtNum.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNum.Location = new System.Drawing.Point(4, 40);
            this.txtNum.Margin = new System.Windows.Forms.Padding(4);
            this.txtNum.Name = "txtNum";
            this.txtNum.Size = new System.Drawing.Size(232, 29);
            this.txtNum.TabIndex = 2;
            this.txtNum.Visible = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.tableLayoutPanel10);
            this.flowLayoutPanel1.Controls.Add(this.tableLayoutPanel2);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(133, 4);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(315, 33);
            this.flowLayoutPanel1.TabIndex = 59;
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel10.ColumnCount = 2;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel10.Controls.Add(this.btnAddClient, 1, 0);
            this.tableLayoutPanel10.Controls.Add(this.cbxClient, 0, 0);
            this.tableLayoutPanel10.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel10.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 1;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(311, 30);
            this.tableLayoutPanel10.TabIndex = 263;
            // 
            // cbxClient
            // 
            this.cbxClient.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxClient.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxClient.FormattingEnabled = true;
            this.cbxClient.Items.AddRange(new object[] {
            "Vente",
            "Achat"});
            this.cbxClient.Location = new System.Drawing.Point(4, 4);
            this.cbxClient.Margin = new System.Windows.Forms.Padding(4);
            this.cbxClient.Name = "cbxClient";
            this.cbxClient.Size = new System.Drawing.Size(235, 28);
            this.cbxClient.TabIndex = 266;
            this.cbxClient.Visible = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.Controls.Add(this.cbxFournisseurs, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.iconButton1, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 42);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(280, 30);
            this.tableLayoutPanel2.TabIndex = 263;
            // 
            // cbxFournisseurs
            // 
            this.cbxFournisseurs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxFournisseurs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxFournisseurs.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxFournisseurs.FormattingEnabled = true;
            this.cbxFournisseurs.Location = new System.Drawing.Point(4, 4);
            this.cbxFournisseurs.Margin = new System.Windows.Forms.Padding(4);
            this.cbxFournisseurs.Name = "cbxFournisseurs";
            this.cbxFournisseurs.Size = new System.Drawing.Size(232, 28);
            this.cbxFournisseurs.TabIndex = 44;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.flowLayoutPanel2.Controls.Add(this.lblBC);
            this.flowLayoutPanel2.Controls.Add(this.lblNum);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(456, 7);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(88, 26);
            this.flowLayoutPanel2.TabIndex = 58;
            // 
            // lblBC
            // 
            this.lblBC.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblBC.AutoSize = true;
            this.lblBC.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBC.ForeColor = System.Drawing.Color.LightBlue;
            this.lblBC.Location = new System.Drawing.Point(4, 0);
            this.lblBC.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBC.Name = "lblBC";
            this.lblBC.Size = new System.Drawing.Size(77, 69);
            this.lblBC.TabIndex = 46;
            this.lblBC.Text = "Bon de commande :";
            // 
            // lblNum
            // 
            this.lblNum.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblNum.AutoSize = true;
            this.lblNum.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNum.ForeColor = System.Drawing.Color.LightBlue;
            this.lblNum.Location = new System.Drawing.Point(4, 69);
            this.lblNum.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNum.Name = "lblNum";
            this.lblNum.Size = new System.Drawing.Size(78, 46);
            this.lblNum.TabIndex = 46;
            this.lblNum.Text = "Numéro :";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.cbxType, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(286, 41);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // cbxType
            // 
            this.cbxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxType.FormattingEnabled = true;
            this.cbxType.Items.AddRange(new object[] {
            "Vente",
            "Achat",
            "Expense Damaged"});
            this.cbxType.Location = new System.Drawing.Point(147, 4);
            this.cbxType.Margin = new System.Windows.Forms.Padding(4);
            this.cbxType.Name = "cbxType";
            this.cbxType.Size = new System.Drawing.Size(133, 28);
            this.cbxType.TabIndex = 43;
            this.cbxType.SelectedIndexChanged += new System.EventHandler(this.CbxType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.LightBlue;
            this.label1.Location = new System.Drawing.Point(64, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 23);
            this.label1.TabIndex = 44;
            this.label1.Text = "Type      ";
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel3, 0, 2);
            this.tableLayoutPanel6.Controls.Add(this.panel8, 0, 1);
            this.tableLayoutPanel6.Location = new System.Drawing.Point(15, 44);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 3;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 74F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 29F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 71F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(992, 713);
            this.tableLayoutPanel6.TabIndex = 62;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel3.Controls.Add(this.panel7, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(4, 263);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel3.MaximumSize = new System.Drawing.Size(2119, 646);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(984, 446);
            this.tableLayoutPanel3.TabIndex = 54;
            // 
            // panel7
            // 
            this.panel7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel7.Controls.Add(this.btnEnregistrer);
            this.panel7.Controls.Add(this.iconButton3);
            this.panel7.Controls.Add(this.iconButton2);
            this.panel7.Controls.Add(this.checkCrédit);
            this.panel7.Controls.Add(this.tableLayoutPanel12);
            this.panel7.Controls.Add(this.label30);
            this.panel7.Location = new System.Drawing.Point(593, 2);
            this.panel7.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(388, 442);
            this.panel7.TabIndex = 267;
            // 
            // checkCrédit
            // 
            this.checkCrédit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkCrédit.AutoSize = true;
            this.checkCrédit.BackColor = System.Drawing.Color.Transparent;
            this.checkCrédit.ForeColor = System.Drawing.Color.LightBlue;
            this.checkCrédit.Location = new System.Drawing.Point(299, 9);
            this.checkCrédit.Margin = new System.Windows.Forms.Padding(4);
            this.checkCrédit.Name = "checkCrédit";
            this.checkCrédit.Size = new System.Drawing.Size(76, 20);
            this.checkCrédit.TabIndex = 60;
            this.checkCrédit.Text = "A Crédit";
            this.checkCrédit.UseVisualStyleBackColor = false;
            // 
            // tableLayoutPanel12
            // 
            this.tableLayoutPanel12.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel12.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel12.ColumnCount = 2;
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62F));
            this.tableLayoutPanel12.Controls.Add(this.textBox2, 1, 5);
            this.tableLayoutPanel12.Controls.Add(this.tableLayoutPanel13, 1, 4);
            this.tableLayoutPanel12.Controls.Add(this.txtmontantpayer, 1, 3);
            this.tableLayoutPanel12.Controls.Add(this.txtNetTotal, 1, 2);
            this.tableLayoutPanel12.Controls.Add(this.txtRéduction, 1, 1);
            this.tableLayoutPanel12.Controls.Add(this.txtSousTotal, 1, 0);
            this.tableLayoutPanel12.Controls.Add(this.label23, 0, 0);
            this.tableLayoutPanel12.Controls.Add(this.label24, 0, 1);
            this.tableLayoutPanel12.Controls.Add(this.label25, 0, 2);
            this.tableLayoutPanel12.Controls.Add(this.label26, 0, 3);
            this.tableLayoutPanel12.Controls.Add(this.label27, 0, 6);
            this.tableLayoutPanel12.Controls.Add(this.txtAretourner, 1, 6);
            this.tableLayoutPanel12.Controls.Add(this.label28, 0, 4);
            this.tableLayoutPanel12.Controls.Add(this.label29, 0, 5);
            this.tableLayoutPanel12.Location = new System.Drawing.Point(11, 47);
            this.tableLayoutPanel12.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel12.Name = "tableLayoutPanel12";
            this.tableLayoutPanel12.RowCount = 7;
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.2849F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.2849F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.2849F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.2849F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.2849F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28775F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28775F));
            this.tableLayoutPanel12.Size = new System.Drawing.Size(366, 342);
            this.tableLayoutPanel12.TabIndex = 5;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBox2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(144, 244);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(217, 34);
            this.textBox2.TabIndex = 267;
            this.textBox2.TabStop = false;
            // 
            // tableLayoutPanel13
            // 
            this.tableLayoutPanel13.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tableLayoutPanel13.ColumnCount = 2;
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel13.Controls.Add(this.cbxPayement, 0, 0);
            this.tableLayoutPanel13.Controls.Add(this.iconButton5, 1, 0);
            this.tableLayoutPanel13.Location = new System.Drawing.Point(143, 196);
            this.tableLayoutPanel13.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel13.Name = "tableLayoutPanel13";
            this.tableLayoutPanel13.RowCount = 1;
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel13.Size = new System.Drawing.Size(219, 34);
            this.tableLayoutPanel13.TabIndex = 266;
            // 
            // cbxPayement
            // 
            this.cbxPayement.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxPayement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPayement.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxPayement.FormattingEnabled = true;
            this.cbxPayement.Location = new System.Drawing.Point(4, 4);
            this.cbxPayement.Margin = new System.Windows.Forms.Padding(4);
            this.cbxPayement.Name = "cbxPayement";
            this.cbxPayement.Size = new System.Drawing.Size(171, 28);
            this.cbxPayement.TabIndex = 5;
            this.cbxPayement.TabStop = false;
            // 
            // txtmontantpayer
            // 
            this.txtmontantpayer.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtmontantpayer.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtmontantpayer.Location = new System.Drawing.Point(144, 148);
            this.txtmontantpayer.Margin = new System.Windows.Forms.Padding(4);
            this.txtmontantpayer.MaxLength = 11;
            this.txtmontantpayer.Name = "txtmontantpayer";
            this.txtmontantpayer.Size = new System.Drawing.Size(217, 34);
            this.txtmontantpayer.TabIndex = 2;
            // 
            // txtNetTotal
            // 
            this.txtNetTotal.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtNetTotal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNetTotal.Location = new System.Drawing.Point(144, 100);
            this.txtNetTotal.Margin = new System.Windows.Forms.Padding(4);
            this.txtNetTotal.Name = "txtNetTotal";
            this.txtNetTotal.ReadOnly = true;
            this.txtNetTotal.Size = new System.Drawing.Size(217, 34);
            this.txtNetTotal.TabIndex = 56;
            this.txtNetTotal.TabStop = false;
            // 
            // txtRéduction
            // 
            this.txtRéduction.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtRéduction.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRéduction.Location = new System.Drawing.Point(144, 52);
            this.txtRéduction.Margin = new System.Windows.Forms.Padding(4);
            this.txtRéduction.Name = "txtRéduction";
            this.txtRéduction.Size = new System.Drawing.Size(217, 34);
            this.txtRéduction.TabIndex = 48;
            this.txtRéduction.TabStop = false;
            // 
            // txtSousTotal
            // 
            this.txtSousTotal.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtSousTotal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSousTotal.Location = new System.Drawing.Point(144, 4);
            this.txtSousTotal.Margin = new System.Windows.Forms.Padding(4);
            this.txtSousTotal.Name = "txtSousTotal";
            this.txtSousTotal.ReadOnly = true;
            this.txtSousTotal.Size = new System.Drawing.Size(217, 34);
            this.txtSousTotal.TabIndex = 57;
            this.txtSousTotal.TabStop = false;
            this.txtSousTotal.Text = "0";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.BackColor = System.Drawing.Color.Transparent;
            this.label23.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.Color.LightBlue;
            this.label23.Location = new System.Drawing.Point(4, 0);
            this.label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(87, 23);
            this.label23.TabIndex = 59;
            this.label23.Text = "Sous Total";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.BackColor = System.Drawing.Color.Transparent;
            this.label24.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.Color.LightBlue;
            this.label24.Location = new System.Drawing.Point(4, 48);
            this.label24.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(92, 23);
            this.label24.TabIndex = 60;
            this.label24.Text = "Réduction ";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.BackColor = System.Drawing.Color.Transparent;
            this.label25.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.ForeColor = System.Drawing.Color.LightBlue;
            this.label25.Location = new System.Drawing.Point(4, 96);
            this.label25.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(99, 23);
            this.label25.TabIndex = 65;
            this.label25.Text = "Net à payer";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.BackColor = System.Drawing.Color.Transparent;
            this.label26.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.Color.LightBlue;
            this.label26.Location = new System.Drawing.Point(4, 144);
            this.label26.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(122, 23);
            this.label26.TabIndex = 67;
            this.label26.Text = "Montant Payer";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.BackColor = System.Drawing.Color.Transparent;
            this.label27.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.Color.LightBlue;
            this.label27.Location = new System.Drawing.Point(4, 288);
            this.label27.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(95, 46);
            this.label27.TabIndex = 66;
            this.label27.Text = "Montant à retourner";
            // 
            // txtAretourner
            // 
            this.txtAretourner.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtAretourner.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAretourner.Location = new System.Drawing.Point(144, 292);
            this.txtAretourner.Margin = new System.Windows.Forms.Padding(4);
            this.txtAretourner.Name = "txtAretourner";
            this.txtAretourner.ReadOnly = true;
            this.txtAretourner.Size = new System.Drawing.Size(217, 34);
            this.txtAretourner.TabIndex = 55;
            this.txtAretourner.TabStop = false;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.BackColor = System.Drawing.Color.Transparent;
            this.label28.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.ForeColor = System.Drawing.Color.LightBlue;
            this.label28.Location = new System.Drawing.Point(4, 192);
            this.label28.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(85, 46);
            this.label28.TabIndex = 67;
            this.label28.Text = "Mode Payement";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.BackColor = System.Drawing.Color.Transparent;
            this.label29.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.ForeColor = System.Drawing.Color.LightBlue;
            this.label29.Location = new System.Drawing.Point(4, 240);
            this.label29.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(90, 46);
            this.label29.TabIndex = 67;
            this.label29.Text = "Reference Payement";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.BackColor = System.Drawing.Color.Transparent;
            this.label30.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.ForeColor = System.Drawing.Color.LightBlue;
            this.label30.Location = new System.Drawing.Point(5, 7);
            this.label30.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(208, 28);
            this.label30.TabIndex = 58;
            this.label30.Text = "Calcule des opérations";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.tableLayoutPanel20);
            this.panel3.Controls.Add(this.iconPictureBox2);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.btnDeleteAll);
            this.panel3.Controls.Add(this.txtSearch);
            this.panel3.Location = new System.Drawing.Point(4, 4);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(582, 438);
            this.panel3.TabIndex = 0;
            // 
            // tableLayoutPanel20
            // 
            this.tableLayoutPanel20.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel20.ColumnCount = 1;
            this.tableLayoutPanel20.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel20.Controls.Add(this.dataGridView1, 0, 0);
            this.tableLayoutPanel20.Controls.Add(this.tableLayoutPanel21, 0, 1);
            this.tableLayoutPanel20.Location = new System.Drawing.Point(3, 41);
            this.tableLayoutPanel20.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel20.Name = "tableLayoutPanel20";
            this.tableLayoutPanel20.RowCount = 2;
            this.tableLayoutPanel20.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel20.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel20.Size = new System.Drawing.Size(577, 394);
            this.tableLayoutPanel20.TabIndex = 268;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(4, 4);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(569, 340);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.TabStop = false;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // tableLayoutPanel21
            // 
            this.tableLayoutPanel21.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel21.ColumnCount = 3;
            this.tableLayoutPanel21.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.80048F));
            this.tableLayoutPanel21.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.83326F));
            this.tableLayoutPanel21.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.36626F));
            this.tableLayoutPanel21.Controls.Add(this.cbxPendingList, 2, 0);
            this.tableLayoutPanel21.Controls.Add(this.btnPending, 0, 0);
            this.tableLayoutPanel21.Controls.Add(this.label3, 1, 0);
            this.tableLayoutPanel21.Location = new System.Drawing.Point(3, 350);
            this.tableLayoutPanel21.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel21.Name = "tableLayoutPanel21";
            this.tableLayoutPanel21.RowCount = 1;
            this.tableLayoutPanel21.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel21.Size = new System.Drawing.Size(571, 42);
            this.tableLayoutPanel21.TabIndex = 1;
            // 
            // cbxPendingList
            // 
            this.cbxPendingList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxPendingList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPendingList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxPendingList.FormattingEnabled = true;
            this.cbxPendingList.Location = new System.Drawing.Point(332, 4);
            this.cbxPendingList.Margin = new System.Windows.Forms.Padding(4);
            this.cbxPendingList.Name = "cbxPendingList";
            this.cbxPendingList.Size = new System.Drawing.Size(235, 28);
            this.cbxPendingList.TabIndex = 5;
            this.cbxPendingList.TabStop = false;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.LightBlue;
            this.label3.Location = new System.Drawing.Point(208, 9);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 23);
            this.label3.TabIndex = 67;
            this.label3.Text = "Pending List";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.LightBlue;
            this.label2.Location = new System.Drawing.Point(4, 6);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 28);
            this.label2.TabIndex = 45;
            this.label2.Text = "Item List";
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(430, 2);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(139, 29);
            this.txtSearch.TabIndex = 0;
            // 
            // panel8
            // 
            this.panel8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel8.Controls.Add(this.label5);
            this.panel8.Controls.Add(this.cbxModePrix);
            this.panel8.Controls.Add(this.btnReturn);
            this.panel8.Controls.Add(this.btnAjouter);
            this.panel8.Controls.Add(this.tableLayoutPanel7);
            this.panel8.Controls.Add(this.label10);
            this.panel8.Controls.Add(this.tableLayoutPanel19);
            this.panel8.Location = new System.Drawing.Point(3, 76);
            this.panel8.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(986, 181);
            this.panel8.TabIndex = 55;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.LightBlue;
            this.label5.Location = new System.Drawing.Point(768, 46);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 23);
            this.label5.TabIndex = 266;
            this.label5.Text = "Mode Prix :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbxModePrix
            // 
            this.cbxModePrix.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxModePrix.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxModePrix.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxModePrix.FormattingEnabled = true;
            this.cbxModePrix.Items.AddRange(new object[] {
            "Detaillant",
            "Grossiste"});
            this.cbxModePrix.Location = new System.Drawing.Point(868, 44);
            this.cbxModePrix.Margin = new System.Windows.Forms.Padding(4);
            this.cbxModePrix.Name = "cbxModePrix";
            this.cbxModePrix.Size = new System.Drawing.Size(113, 28);
            this.cbxModePrix.TabIndex = 265;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel7.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel7.ColumnCount = 4;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel7.Controls.Add(this.tableLayoutPanel14, 3, 0);
            this.tableLayoutPanel7.Controls.Add(this.tableLayoutPanel15, 2, 0);
            this.tableLayoutPanel7.Controls.Add(this.tableLayoutPanel16, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.tableLayoutPanel17, 0, 0);
            this.tableLayoutPanel7.Location = new System.Drawing.Point(77, 4);
            this.tableLayoutPanel7.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(907, 33);
            this.tableLayoutPanel7.TabIndex = 264;
            // 
            // tableLayoutPanel14
            // 
            this.tableLayoutPanel14.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel14.ColumnCount = 2;
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel14.Controls.Add(this.label20, 0, 0);
            this.tableLayoutPanel14.Controls.Add(this.cbxMesure, 1, 0);
            this.tableLayoutPanel14.Location = new System.Drawing.Point(728, 2);
            this.tableLayoutPanel14.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel14.Name = "tableLayoutPanel14";
            this.tableLayoutPanel14.RowCount = 1;
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel14.Size = new System.Drawing.Size(176, 29);
            this.tableLayoutPanel14.TabIndex = 265;
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.LightBlue;
            this.label20.Location = new System.Drawing.Point(10, 0);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(66, 23);
            this.label20.TabIndex = 47;
            this.label20.Text = "Mesure";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbxMesure
            // 
            this.cbxMesure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxMesure.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMesure.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxMesure.FormattingEnabled = true;
            this.cbxMesure.Location = new System.Drawing.Point(84, 4);
            this.cbxMesure.Margin = new System.Windows.Forms.Padding(4);
            this.cbxMesure.Name = "cbxMesure";
            this.cbxMesure.Size = new System.Drawing.Size(88, 28);
            this.cbxMesure.TabIndex = 44;
            // 
            // tableLayoutPanel15
            // 
            this.tableLayoutPanel15.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel15.ColumnCount = 3;
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 69F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel15.Controls.Add(this.lblFiliale, 0, 0);
            this.tableLayoutPanel15.Controls.Add(this.cbxFiliale, 1, 0);
            this.tableLayoutPanel15.Controls.Add(this.btnAddFiliale, 2, 0);
            this.tableLayoutPanel15.Location = new System.Drawing.Point(547, 2);
            this.tableLayoutPanel15.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel15.Name = "tableLayoutPanel15";
            this.tableLayoutPanel15.RowCount = 1;
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel15.Size = new System.Drawing.Size(175, 29);
            this.tableLayoutPanel15.TabIndex = 266;
            // 
            // lblFiliale
            // 
            this.lblFiliale.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFiliale.AutoSize = true;
            this.lblFiliale.BackColor = System.Drawing.Color.Transparent;
            this.lblFiliale.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFiliale.ForeColor = System.Drawing.Color.LightBlue;
            this.lblFiliale.Location = new System.Drawing.Point(4, 0);
            this.lblFiliale.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFiliale.Name = "lblFiliale";
            this.lblFiliale.Size = new System.Drawing.Size(61, 23);
            this.lblFiliale.TabIndex = 47;
            this.lblFiliale.Text = "Filiale :";
            this.lblFiliale.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblFiliale.Visible = false;
            // 
            // cbxFiliale
            // 
            this.cbxFiliale.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxFiliale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxFiliale.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxFiliale.FormattingEnabled = true;
            this.cbxFiliale.Location = new System.Drawing.Point(75, 4);
            this.cbxFiliale.Margin = new System.Windows.Forms.Padding(4);
            this.cbxFiliale.Name = "cbxFiliale";
            this.cbxFiliale.Size = new System.Drawing.Size(67, 28);
            this.cbxFiliale.TabIndex = 0;
            this.cbxFiliale.Visible = false;
            // 
            // tableLayoutPanel16
            // 
            this.tableLayoutPanel16.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel16.ColumnCount = 3;
            this.tableLayoutPanel16.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel16.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel16.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel16.Controls.Add(this.btnSaveCode, 2, 0);
            this.tableLayoutPanel16.Controls.Add(this.txtNewCode, 1, 0);
            this.tableLayoutPanel16.Controls.Add(this.label17, 0, 0);
            this.tableLayoutPanel16.Location = new System.Drawing.Point(275, 2);
            this.tableLayoutPanel16.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel16.Name = "tableLayoutPanel16";
            this.tableLayoutPanel16.RowCount = 1;
            this.tableLayoutPanel16.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel16.Size = new System.Drawing.Size(266, 29);
            this.tableLayoutPanel16.TabIndex = 267;
            // 
            // txtNewCode
            // 
            this.txtNewCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNewCode.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNewCode.Location = new System.Drawing.Point(85, 4);
            this.txtNewCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtNewCode.Name = "txtNewCode";
            this.txtNewCode.Size = new System.Drawing.Size(137, 29);
            this.txtNewCode.TabIndex = 101;
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.LightBlue;
            this.label17.Location = new System.Drawing.Point(4, 0);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(72, 23);
            this.label17.TabIndex = 45;
            this.label17.Text = "Barcode";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel17
            // 
            this.tableLayoutPanel17.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel17.ColumnCount = 2;
            this.tableLayoutPanel17.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel17.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel17.Controls.Add(this.cbxSearch, 1, 0);
            this.tableLayoutPanel17.Controls.Add(this.iconPictureBox1, 0, 0);
            this.tableLayoutPanel17.Location = new System.Drawing.Point(3, 2);
            this.tableLayoutPanel17.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel17.Name = "tableLayoutPanel17";
            this.tableLayoutPanel17.RowCount = 1;
            this.tableLayoutPanel17.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel17.Size = new System.Drawing.Size(266, 29);
            this.tableLayoutPanel17.TabIndex = 268;
            // 
            // cbxSearch
            // 
            this.cbxSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxSearch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxSearch.FormattingEnabled = true;
            this.cbxSearch.Items.AddRange(new object[] {
            "Vente",
            "Achat"});
            this.cbxSearch.Location = new System.Drawing.Point(44, 4);
            this.cbxSearch.Margin = new System.Windows.Forms.Padding(4);
            this.cbxSearch.Name = "cbxSearch";
            this.cbxSearch.Size = new System.Drawing.Size(218, 28);
            this.cbxSearch.TabIndex = 43;
            this.cbxSearch.SelectedIndexChanged += new System.EventHandler(this.CbxType_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.LightBlue;
            this.label10.Location = new System.Drawing.Point(0, 4);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 28);
            this.label10.TabIndex = 46;
            this.label10.Text = "Détails";
            // 
            // tableLayoutPanel19
            // 
            this.tableLayoutPanel19.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel19.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel19.ColumnCount = 1;
            this.tableLayoutPanel19.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel19.Controls.Add(this.tableLayoutPanel18, 0, 0);
            this.tableLayoutPanel19.Controls.Add(this.tableLayoutPanel1, 0, 1);
            this.tableLayoutPanel19.Location = new System.Drawing.Point(1, 39);
            this.tableLayoutPanel19.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel19.Name = "tableLayoutPanel19";
            this.tableLayoutPanel19.RowCount = 2;
            this.tableLayoutPanel19.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel19.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel19.Size = new System.Drawing.Size(767, 143);
            this.tableLayoutPanel19.TabIndex = 264;
            // 
            // tableLayoutPanel18
            // 
            this.tableLayoutPanel18.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel18.ColumnCount = 3;
            this.tableLayoutPanel18.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel18.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel18.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel18.Controls.Add(this.Date_Expiration, 2, 1);
            this.tableLayoutPanel18.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel18.Controls.Add(this.cbxCode, 0, 1);
            this.tableLayoutPanel18.Controls.Add(this.label14, 1, 0);
            this.tableLayoutPanel18.Controls.Add(this.txtNom, 1, 1);
            this.tableLayoutPanel18.Controls.Add(this.label31, 2, 0);
            this.tableLayoutPanel18.Location = new System.Drawing.Point(1, 1);
            this.tableLayoutPanel18.Margin = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanel18.Name = "tableLayoutPanel18";
            this.tableLayoutPanel18.RowCount = 2;
            this.tableLayoutPanel18.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel18.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel18.Size = new System.Drawing.Size(765, 69);
            this.tableLayoutPanel18.TabIndex = 264;
            // 
            // Date_Expiration
            // 
            this.Date_Expiration.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Date_Expiration.CalendarMonthBackground = System.Drawing.Color.LightCoral;
            this.Date_Expiration.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Date_Expiration.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Date_Expiration.Location = new System.Drawing.Point(577, 31);
            this.Date_Expiration.Margin = new System.Windows.Forms.Padding(4);
            this.Date_Expiration.Name = "Date_Expiration";
            this.Date_Expiration.Size = new System.Drawing.Size(184, 29);
            this.Date_Expiration.TabIndex = 321;
            this.Date_Expiration.Visible = false;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.LightBlue;
            this.label4.Location = new System.Drawing.Point(27, 0);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 27);
            this.label4.TabIndex = 45;
            this.label4.Text = "Code Barre";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbxCode
            // 
            this.cbxCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxCode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxCode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxCode.FormattingEnabled = true;
            this.cbxCode.Items.AddRange(new object[] {
            "Vente",
            "Achat"});
            this.cbxCode.Location = new System.Drawing.Point(4, 31);
            this.cbxCode.Margin = new System.Windows.Forms.Padding(4);
            this.cbxCode.Name = "cbxCode";
            this.cbxCode.Size = new System.Drawing.Size(183, 28);
            this.cbxCode.TabIndex = 1;
            this.cbxCode.Visible = false;
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label14.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.LightBlue;
            this.label14.Location = new System.Drawing.Point(313, 0);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(137, 27);
            this.label14.TabIndex = 45;
            this.label14.Text = "Désignation";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtNom
            // 
            this.txtNom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNom.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNom.Location = new System.Drawing.Point(195, 31);
            this.txtNom.Margin = new System.Windows.Forms.Padding(4);
            this.txtNom.Name = "txtNom";
            this.txtNom.ReadOnly = true;
            this.txtNom.Size = new System.Drawing.Size(374, 29);
            this.txtNom.TabIndex = 0;
            this.txtNom.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCode_KeyPress);
            // 
            // label31
            // 
            this.label31.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label31.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.ForeColor = System.Drawing.Color.LightBlue;
            this.label31.Location = new System.Drawing.Point(600, 0);
            this.label31.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(137, 27);
            this.label31.TabIndex = 47;
            this.label31.Text = "Date Expiration";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label31.Visible = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.00062F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.00063F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.00063F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.99812F));
            this.tableLayoutPanel1.Controls.Add(this.label9, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtQuantité, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtStock, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label8, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label22, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtMontant, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtPrixUnité, 2, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 72);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(765, 70);
            this.tableLayoutPanel1.TabIndex = 48;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.LightBlue;
            this.label9.Location = new System.Drawing.Point(413, 14);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(129, 21);
            this.label9.TabIndex = 47;
            this.label9.Text = "Prix Unité";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtQuantité
            // 
            this.txtQuantité.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.txtQuantité.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuantité.Location = new System.Drawing.Point(208, 39);
            this.txtQuantité.Margin = new System.Windows.Forms.Padding(4);
            this.txtQuantité.MaxLength = 4;
            this.txtQuantité.Name = "txtQuantité";
            this.txtQuantité.Size = new System.Drawing.Size(156, 29);
            this.txtQuantité.TabIndex = 2;
            this.txtQuantité.Text = "1";
            // 
            // txtStock
            // 
            this.txtStock.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.txtStock.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStock.Location = new System.Drawing.Point(17, 39);
            this.txtStock.Margin = new System.Windows.Forms.Padding(4);
            this.txtStock.Name = "txtStock";
            this.txtStock.ReadOnly = true;
            this.txtStock.Size = new System.Drawing.Size(156, 29);
            this.txtStock.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.LightBlue;
            this.label7.Location = new System.Drawing.Point(17, 14);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(156, 21);
            this.label7.TabIndex = 47;
            this.label7.Text = "Stock disponible";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.LightBlue;
            this.label8.Location = new System.Drawing.Point(208, 14);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(156, 21);
            this.label8.TabIndex = 47;
            this.label8.Text = "Quantité";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label22
            // 
            this.label22.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label22.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.LightBlue;
            this.label22.Location = new System.Drawing.Point(604, 14);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(129, 21);
            this.label22.TabIndex = 47;
            this.label22.Text = "Montant";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtMontant
            // 
            this.txtMontant.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.txtMontant.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMontant.Location = new System.Drawing.Point(589, 39);
            this.txtMontant.Margin = new System.Windows.Forms.Padding(4);
            this.txtMontant.Name = "txtMontant";
            this.txtMontant.Size = new System.Drawing.Size(159, 29);
            this.txtMontant.TabIndex = 0;
            // 
            // txtPrixUnité
            // 
            this.txtPrixUnité.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.txtPrixUnité.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrixUnité.Location = new System.Drawing.Point(399, 39);
            this.txtPrixUnité.Margin = new System.Windows.Forms.Padding(4);
            this.txtPrixUnité.Name = "txtPrixUnité";
            this.txtPrixUnité.Size = new System.Drawing.Size(156, 29);
            this.txtPrixUnité.TabIndex = 0;
            // 
            // txtReference
            // 
            this.txtReference.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtReference.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReference.Location = new System.Drawing.Point(165, 198);
            this.txtReference.Name = "txtReference";
            this.txtReference.Size = new System.Drawing.Size(227, 34);
            this.txtReference.TabIndex = 6;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.LightBlue;
            this.label12.Location = new System.Drawing.Point(3, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(68, 17);
            this.label12.TabIndex = 59;
            this.label12.Text = "Sous Total";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.LightBlue;
            this.label13.Location = new System.Drawing.Point(3, 39);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(70, 17);
            this.label13.TabIndex = 60;
            this.label13.Text = "Réduction ";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.LightBlue;
            this.label15.Location = new System.Drawing.Point(3, 78);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(77, 17);
            this.label15.TabIndex = 65;
            this.label15.Text = "Net à payer";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.LightBlue;
            this.label16.Location = new System.Drawing.Point(3, 117);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(93, 17);
            this.label16.TabIndex = 67;
            this.label16.Text = "Montant Payer";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.LightBlue;
            this.label19.Location = new System.Drawing.Point(3, 156);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(103, 17);
            this.label19.TabIndex = 67;
            this.label19.Text = "Mode Payement";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.LightBlue;
            this.label21.Location = new System.Drawing.Point(3, 195);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(126, 17);
            this.label21.TabIndex = 67;
            this.label21.Text = "Reference Payement";
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.Size = new System.Drawing.Size(200, 100);
            this.tableLayoutPanel11.TabIndex = 0;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.LightBlue;
            this.label11.Location = new System.Drawing.Point(5, 5);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(166, 21);
            this.label11.TabIndex = 58;
            this.label11.Text = "Calcule des opérations";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1018, 2);
            this.panel2.TabIndex = 176;
            this.panel2.Visible = false;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(3, 761);
            this.panel4.TabIndex = 174;
            this.panel4.Visible = false;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel5.Location = new System.Drawing.Point(1021, 0);
            this.panel5.Margin = new System.Windows.Forms.Padding(4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(3, 761);
            this.panel5.TabIndex = 175;
            this.panel5.Visible = false;
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.Size = new System.Drawing.Size(200, 100);
            this.tableLayoutPanel9.TabIndex = 0;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(0, 761);
            this.panel6.Margin = new System.Windows.Forms.Padding(4);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1024, 2);
            this.panel6.TabIndex = 177;
            this.panel6.Visible = false;
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 15;
            this.bunifuElipse1.TargetControl = this.txtSearch;
            // 
            // bunifuDragControl1
            // 
            this.bunifuDragControl1.Fixed = true;
            this.bunifuDragControl1.Horizontal = true;
            this.bunifuDragControl1.TargetControl = this;
            this.bunifuDragControl1.Vertical = true;
            // 
            // btnMinimize
            // 
            this.btnMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.btnMinimize.ForeColor = System.Drawing.Color.LightBlue;
            this.btnMinimize.IconChar = FontAwesome.Sharp.IconChar.WindowMinimize;
            this.btnMinimize.IconColor = System.Drawing.Color.LightBlue;
            this.btnMinimize.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnMinimize.IconSize = 31;
            this.btnMinimize.Location = new System.Drawing.Point(893, 7);
            this.btnMinimize.Margin = new System.Windows.Forms.Padding(4);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(33, 31);
            this.btnMinimize.TabIndex = 265;
            this.btnMinimize.TabStop = false;
            this.toolTip1.SetToolTip(this.btnMinimize, "Reduire");
            this.btnMinimize.Visible = false;
            // 
            // btnRestore
            // 
            this.btnRestore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRestore.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.btnRestore.ForeColor = System.Drawing.Color.LightBlue;
            this.btnRestore.IconChar = FontAwesome.Sharp.IconChar.WindowRestore;
            this.btnRestore.IconColor = System.Drawing.Color.LightBlue;
            this.btnRestore.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnRestore.IconSize = 31;
            this.btnRestore.Location = new System.Drawing.Point(935, 7);
            this.btnRestore.Margin = new System.Windows.Forms.Padding(4);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(33, 31);
            this.btnRestore.TabIndex = 266;
            this.btnRestore.TabStop = false;
            this.toolTip1.SetToolTip(this.btnRestore, "Agrandir / Normal");
            this.btnRestore.Visible = false;
            // 
            // btnClotureJournée
            // 
            this.btnClotureJournée.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.btnClotureJournée.FlatAppearance.BorderSize = 0;
            this.btnClotureJournée.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClotureJournée.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClotureJournée.ForeColor = System.Drawing.Color.LightBlue;
            this.btnClotureJournée.IconChar = FontAwesome.Sharp.IconChar.Print;
            this.btnClotureJournée.IconColor = System.Drawing.Color.LightBlue;
            this.btnClotureJournée.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnClotureJournée.IconSize = 24;
            this.btnClotureJournée.Location = new System.Drawing.Point(19, 12);
            this.btnClotureJournée.Margin = new System.Windows.Forms.Padding(4);
            this.btnClotureJournée.Name = "btnClotureJournée";
            this.btnClotureJournée.Size = new System.Drawing.Size(201, 30);
            this.btnClotureJournée.TabIndex = 264;
            this.btnClotureJournée.Text = "Resumé Journée";
            this.btnClotureJournée.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClotureJournée.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClotureJournée.UseVisualStyleBackColor = false;
            // 
            // btnReprintTicket
            // 
            this.btnReprintTicket.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReprintTicket.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.btnReprintTicket.FlatAppearance.BorderSize = 0;
            this.btnReprintTicket.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReprintTicket.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReprintTicket.ForeColor = System.Drawing.Color.LightBlue;
            this.btnReprintTicket.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            this.btnReprintTicket.IconColor = System.Drawing.Color.LightBlue;
            this.btnReprintTicket.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnReprintTicket.IconSize = 32;
            this.btnReprintTicket.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReprintTicket.Location = new System.Drawing.Point(687, 6);
            this.btnReprintTicket.Margin = new System.Windows.Forms.Padding(0);
            this.btnReprintTicket.Name = "btnReprintTicket";
            this.btnReprintTicket.Size = new System.Drawing.Size(181, 30);
            this.btnReprintTicket.TabIndex = 6;
            this.btnReprintTicket.Text = "Reprint Ticket";
            this.btnReprintTicket.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReprintTicket.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnReprintTicket.UseVisualStyleBackColor = false;
            // 
            // btnFermer
            // 
            this.btnFermer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFermer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.btnFermer.ForeColor = System.Drawing.Color.LightBlue;
            this.btnFermer.IconChar = FontAwesome.Sharp.IconChar.RectangleXmark;
            this.btnFermer.IconColor = System.Drawing.Color.LightBlue;
            this.btnFermer.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnFermer.IconSize = 31;
            this.btnFermer.Location = new System.Drawing.Point(976, 6);
            this.btnFermer.Margin = new System.Windows.Forms.Padding(4);
            this.btnFermer.Name = "btnFermer";
            this.btnFermer.Size = new System.Drawing.Size(33, 31);
            this.btnFermer.TabIndex = 262;
            this.btnFermer.TabStop = false;
            this.btnFermer.Visible = false;
            // 
            // btnAddClient
            // 
            this.btnAddClient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddClient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.btnAddClient.FlatAppearance.BorderSize = 0;
            this.btnAddClient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddClient.ForeColor = System.Drawing.Color.LightBlue;
            this.btnAddClient.IconChar = FontAwesome.Sharp.IconChar.Plus;
            this.btnAddClient.IconColor = System.Drawing.Color.LightBlue;
            this.btnAddClient.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnAddClient.IconSize = 32;
            this.btnAddClient.Location = new System.Drawing.Point(275, 4);
            this.btnAddClient.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddClient.Name = "btnAddClient";
            this.btnAddClient.Size = new System.Drawing.Size(32, 22);
            this.btnAddClient.TabIndex = 49;
            this.btnAddClient.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddClient.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAddClient.UseVisualStyleBackColor = false;
            // 
            // iconButton1
            // 
            this.iconButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.iconButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.iconButton1.FlatAppearance.BorderSize = 0;
            this.iconButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButton1.ForeColor = System.Drawing.Color.LightBlue;
            this.iconButton1.IconChar = FontAwesome.Sharp.IconChar.Plus;
            this.iconButton1.IconColor = System.Drawing.Color.LightBlue;
            this.iconButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton1.IconSize = 32;
            this.iconButton1.Location = new System.Drawing.Point(244, 4);
            this.iconButton1.Margin = new System.Windows.Forms.Padding(4);
            this.iconButton1.Name = "iconButton1";
            this.iconButton1.Size = new System.Drawing.Size(32, 22);
            this.iconButton1.TabIndex = 49;
            this.iconButton1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton1.UseVisualStyleBackColor = false;
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
            this.btnEnregistrer.IconSize = 32;
            this.btnEnregistrer.Location = new System.Drawing.Point(215, 393);
            this.btnEnregistrer.Margin = new System.Windows.Forms.Padding(0);
            this.btnEnregistrer.Name = "btnEnregistrer";
            this.btnEnregistrer.Size = new System.Drawing.Size(165, 42);
            this.btnEnregistrer.TabIndex = 6;
            this.btnEnregistrer.Text = "Enregistrer";
            this.btnEnregistrer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEnregistrer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEnregistrer.UseVisualStyleBackColor = false;
            // 
            // iconButton3
            // 
            this.iconButton3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.iconButton3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.iconButton3.FlatAppearance.BorderSize = 0;
            this.iconButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButton3.ForeColor = System.Drawing.Color.LightBlue;
            this.iconButton3.IconChar = FontAwesome.Sharp.IconChar.Link;
            this.iconButton3.IconColor = System.Drawing.Color.LightBlue;
            this.iconButton3.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton3.IconSize = 24;
            this.iconButton3.Location = new System.Drawing.Point(153, 398);
            this.iconButton3.Margin = new System.Windows.Forms.Padding(4);
            this.iconButton3.Name = "iconButton3";
            this.iconButton3.Size = new System.Drawing.Size(36, 30);
            this.iconButton3.TabIndex = 49;
            this.iconButton3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.iconButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.iconButton3, "Cliquer pour selectionner une pièce jointe");
            this.iconButton3.UseVisualStyleBackColor = false;
            this.iconButton3.Visible = false;
            // 
            // iconButton2
            // 
            this.iconButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.iconButton2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.iconButton2.FlatAppearance.BorderSize = 0;
            this.iconButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButton2.ForeColor = System.Drawing.Color.LightBlue;
            this.iconButton2.IconChar = FontAwesome.Sharp.IconChar.Print;
            this.iconButton2.IconColor = System.Drawing.Color.LightBlue;
            this.iconButton2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton2.IconSize = 24;
            this.iconButton2.Location = new System.Drawing.Point(7, 398);
            this.iconButton2.Margin = new System.Windows.Forms.Padding(4);
            this.iconButton2.Name = "iconButton2";
            this.iconButton2.Size = new System.Drawing.Size(107, 30);
            this.iconButton2.TabIndex = 49;
            this.iconButton2.Text = "Ticket";
            this.iconButton2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.iconButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton2.UseVisualStyleBackColor = false;
            // 
            // iconButton5
            // 
            this.iconButton5.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.iconButton5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.iconButton5.FlatAppearance.BorderSize = 0;
            this.iconButton5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButton5.ForeColor = System.Drawing.Color.LightBlue;
            this.iconButton5.IconChar = FontAwesome.Sharp.IconChar.Plus;
            this.iconButton5.IconColor = System.Drawing.Color.LightBlue;
            this.iconButton5.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton5.IconSize = 24;
            this.iconButton5.Location = new System.Drawing.Point(183, 4);
            this.iconButton5.Margin = new System.Windows.Forms.Padding(4);
            this.iconButton5.Name = "iconButton5";
            this.iconButton5.Size = new System.Drawing.Size(32, 26);
            this.iconButton5.TabIndex = 49;
            this.iconButton5.TabStop = false;
            this.iconButton5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.iconButton5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton5.UseVisualStyleBackColor = false;
            // 
            // btnPending
            // 
            this.btnPending.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnPending.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.btnPending.FlatAppearance.BorderSize = 0;
            this.btnPending.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPending.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPending.ForeColor = System.Drawing.Color.LightBlue;
            this.btnPending.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            this.btnPending.IconColor = System.Drawing.Color.LightBlue;
            this.btnPending.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnPending.IconSize = 32;
            this.btnPending.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPending.Location = new System.Drawing.Point(0, 6);
            this.btnPending.Margin = new System.Windows.Forms.Padding(0);
            this.btnPending.Name = "btnPending";
            this.btnPending.Size = new System.Drawing.Size(184, 30);
            this.btnPending.TabIndex = 6;
            this.btnPending.Text = "Pending state";
            this.btnPending.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPending.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPending.UseVisualStyleBackColor = false;
            // 
            // iconPictureBox2
            // 
            this.iconPictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.iconPictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.iconPictureBox2.ForeColor = System.Drawing.Color.LightBlue;
            this.iconPictureBox2.IconChar = FontAwesome.Sharp.IconChar.Search;
            this.iconPictureBox2.IconColor = System.Drawing.Color.LightBlue;
            this.iconPictureBox2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconPictureBox2.IconSize = 31;
            this.iconPictureBox2.Location = new System.Drawing.Point(394, 2);
            this.iconPictureBox2.Margin = new System.Windows.Forms.Padding(4);
            this.iconPictureBox2.Name = "iconPictureBox2";
            this.iconPictureBox2.Size = new System.Drawing.Size(32, 31);
            this.iconPictureBox2.TabIndex = 267;
            this.iconPictureBox2.TabStop = false;
            // 
            // btnDeleteAll
            // 
            this.btnDeleteAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.btnDeleteAll.FlatAppearance.BorderSize = 0;
            this.btnDeleteAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteAll.ForeColor = System.Drawing.Color.LightBlue;
            this.btnDeleteAll.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            this.btnDeleteAll.IconColor = System.Drawing.Color.LightBlue;
            this.btnDeleteAll.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnDeleteAll.IconSize = 32;
            this.btnDeleteAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeleteAll.Location = new System.Drawing.Point(254, 6);
            this.btnDeleteAll.Margin = new System.Windows.Forms.Padding(0);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new System.Drawing.Size(143, 30);
            this.btnDeleteAll.TabIndex = 6;
            this.btnDeleteAll.Text = "Delete All";
            this.btnDeleteAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDeleteAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDeleteAll.UseVisualStyleBackColor = false;
            // 
            // btnReturn
            // 
            this.btnReturn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReturn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.btnReturn.FlatAppearance.BorderSize = 0;
            this.btnReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReturn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReturn.ForeColor = System.Drawing.Color.LightBlue;
            this.btnReturn.IconChar = FontAwesome.Sharp.IconChar.ArrowRotateForward;
            this.btnReturn.IconColor = System.Drawing.Color.LightBlue;
            this.btnReturn.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnReturn.IconSize = 32;
            this.btnReturn.Location = new System.Drawing.Point(786, 87);
            this.btnReturn.Margin = new System.Windows.Forms.Padding(4);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Padding = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.btnReturn.Size = new System.Drawing.Size(164, 39);
            this.btnReturn.TabIndex = 4;
            this.btnReturn.Text = "Retourner";
            this.btnReturn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReturn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnReturn.UseVisualStyleBackColor = false;
            // 
            // btnAjouter
            // 
            this.btnAjouter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAjouter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.btnAjouter.FlatAppearance.BorderSize = 0;
            this.btnAjouter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAjouter.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAjouter.ForeColor = System.Drawing.Color.LightBlue;
            this.btnAjouter.IconChar = FontAwesome.Sharp.IconChar.Plus;
            this.btnAjouter.IconColor = System.Drawing.Color.LightBlue;
            this.btnAjouter.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnAjouter.IconSize = 32;
            this.btnAjouter.Location = new System.Drawing.Point(786, 135);
            this.btnAjouter.Margin = new System.Windows.Forms.Padding(4);
            this.btnAjouter.Name = "btnAjouter";
            this.btnAjouter.Padding = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.btnAjouter.Size = new System.Drawing.Size(164, 42);
            this.btnAjouter.TabIndex = 4;
            this.btnAjouter.Text = "Ajouter";
            this.btnAjouter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAjouter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAjouter.UseVisualStyleBackColor = false;
            // 
            // btnAddFiliale
            // 
            this.btnAddFiliale.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddFiliale.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.btnAddFiliale.FlatAppearance.BorderSize = 0;
            this.btnAddFiliale.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddFiliale.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddFiliale.ForeColor = System.Drawing.Color.LightBlue;
            this.btnAddFiliale.IconChar = FontAwesome.Sharp.IconChar.Plus;
            this.btnAddFiliale.IconColor = System.Drawing.Color.LightBlue;
            this.btnAddFiliale.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnAddFiliale.IconSize = 32;
            this.btnAddFiliale.Location = new System.Drawing.Point(150, 4);
            this.btnAddFiliale.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddFiliale.Name = "btnAddFiliale";
            this.btnAddFiliale.Size = new System.Drawing.Size(21, 18);
            this.btnAddFiliale.TabIndex = 0;
            this.btnAddFiliale.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddFiliale.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAddFiliale.UseVisualStyleBackColor = false;
            this.btnAddFiliale.Visible = false;
            // 
            // btnSaveCode
            // 
            this.btnSaveCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.btnSaveCode.FlatAppearance.BorderSize = 0;
            this.btnSaveCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveCode.ForeColor = System.Drawing.Color.LightBlue;
            this.btnSaveCode.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            this.btnSaveCode.IconColor = System.Drawing.Color.LightBlue;
            this.btnSaveCode.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnSaveCode.IconSize = 32;
            this.btnSaveCode.Location = new System.Drawing.Point(230, 4);
            this.btnSaveCode.Margin = new System.Windows.Forms.Padding(4);
            this.btnSaveCode.Name = "btnSaveCode";
            this.btnSaveCode.Size = new System.Drawing.Size(32, 18);
            this.btnSaveCode.TabIndex = 102;
            this.btnSaveCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveCode.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSaveCode.UseVisualStyleBackColor = false;
            // 
            // iconPictureBox1
            // 
            this.iconPictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.iconPictureBox1.ForeColor = System.Drawing.Color.LightBlue;
            this.iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.Search;
            this.iconPictureBox1.IconColor = System.Drawing.Color.LightBlue;
            this.iconPictureBox1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconPictureBox1.IconSize = 18;
            this.iconPictureBox1.Location = new System.Drawing.Point(4, 4);
            this.iconPictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.iconPictureBox1.Name = "iconPictureBox1";
            this.iconPictureBox1.Size = new System.Drawing.Size(32, 18);
            this.iconPictureBox1.TabIndex = 266;
            this.iconPictureBox1.TabStop = false;
            // 
            // btnFile
            // 
            this.btnFile.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.btnFile.FlatAppearance.BorderSize = 0;
            this.btnFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFile.ForeColor = System.Drawing.Color.LightBlue;
            this.btnFile.IconChar = FontAwesome.Sharp.IconChar.Link;
            this.btnFile.IconColor = System.Drawing.Color.LightBlue;
            this.btnFile.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnFile.IconSize = 24;
            this.btnFile.Location = new System.Drawing.Point(111, 7);
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(27, 29);
            this.btnFile.TabIndex = 49;
            this.btnFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnFile, "Cliquer pour selectionner une pièce jointe");
            this.btnFile.UseVisualStyleBackColor = false;
            this.btnFile.Visible = false;
            // 
            // btnAddPayement
            // 
            this.btnAddPayement.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnAddPayement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.btnAddPayement.FlatAppearance.BorderSize = 0;
            this.btnAddPayement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddPayement.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddPayement.ForeColor = System.Drawing.Color.LightBlue;
            this.btnAddPayement.IconChar = FontAwesome.Sharp.IconChar.Plus;
            this.btnAddPayement.IconColor = System.Drawing.Color.LightBlue;
            this.btnAddPayement.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnAddPayement.IconSize = 24;
            this.btnAddPayement.Location = new System.Drawing.Point(201, 3);
            this.btnAddPayement.Name = "btnAddPayement";
            this.btnAddPayement.Size = new System.Drawing.Size(24, 22);
            this.btnAddPayement.TabIndex = 49;
            this.btnAddPayement.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddPayement.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAddPayement.UseVisualStyleBackColor = false;
            // 
            // btnImprimerTicket
            // 
            this.btnImprimerTicket.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnImprimerTicket.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.btnImprimerTicket.FlatAppearance.BorderSize = 0;
            this.btnImprimerTicket.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimerTicket.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimerTicket.ForeColor = System.Drawing.Color.LightBlue;
            this.btnImprimerTicket.IconChar = FontAwesome.Sharp.IconChar.Print;
            this.btnImprimerTicket.IconColor = System.Drawing.Color.LightBlue;
            this.btnImprimerTicket.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnImprimerTicket.IconSize = 24;
            this.btnImprimerTicket.Location = new System.Drawing.Point(11, 7);
            this.btnImprimerTicket.Name = "btnImprimerTicket";
            this.btnImprimerTicket.Size = new System.Drawing.Size(80, 29);
            this.btnImprimerTicket.TabIndex = 49;
            this.btnImprimerTicket.Text = "Ticket";
            this.btnImprimerTicket.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnImprimerTicket.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnImprimerTicket.UseVisualStyleBackColor = false;
            // 
            // AchatVente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.ClientSize = new System.Drawing.Size(1024, 763);
            this.Controls.Add(this.btnMinimize);
            this.Controls.Add(this.btnRestore);
            this.Controls.Add(this.btnClotureJournée);
            this.Controls.Add(this.btnReprintTicket);
            this.Controls.Add(this.btnFermer);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.tableLayoutPanel6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AchatVente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Achat & Vente";
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel10.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.tableLayoutPanel12.ResumeLayout(false);
            this.tableLayoutPanel12.PerformLayout();
            this.tableLayoutPanel13.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tableLayoutPanel20.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel21.ResumeLayout(false);
            this.tableLayoutPanel21.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel14.ResumeLayout(false);
            this.tableLayoutPanel14.PerformLayout();
            this.tableLayoutPanel15.ResumeLayout(false);
            this.tableLayoutPanel15.PerformLayout();
            this.tableLayoutPanel16.ResumeLayout(false);
            this.tableLayoutPanel16.PerformLayout();
            this.tableLayoutPanel17.ResumeLayout(false);
            this.tableLayoutPanel19.ResumeLayout(false);
            this.tableLayoutPanel18.ResumeLayout(false);
            this.tableLayoutPanel18.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRestore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFermer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private Label label22;
#pragma warning disable CS0169 // Le champ 'AchatVente.textBox1' n'est jamais utilisé
        private TextBox textBox1;
#pragma warning restore CS0169 // Le champ 'AchatVente.textBox1' n'est jamais utilisé
        private CheckBox checkCrédit;
        private IconButton iconButton2;
        private IconButton iconButton3;
        private IconButton btnEnregistrer;
        private TableLayoutPanel tableLayoutPanel12;
        private TextBox textBox2;
        private TableLayoutPanel tableLayoutPanel13;
        private ComboBox cbxPayement;
        private IconButton iconButton5;
        private TextBox txtmontantpayer;
        private TextBox txtNetTotal;
        private TextBox txtRéduction;
        private TextBox txtSousTotal;
        private Label label23;
        private Label label24;
        private Label label25;
        private Label label26;
        private Label label27;
        private TextBox txtAretourner;
        private Label label28;
        private Label label29;
        private Label label30;
        private Label label31;
        private DateTimePicker Date_Expiration;
        private IconButton btnSaveCode;
        private TextBox txtNewCode;
        private Label label17;
        private TextBox txtSearch;
        private BunifuElipse bunifuElipse1;
        private IconPictureBox iconPictureBox2;
        private TextBox txtMontant;
        private TableLayoutPanel tableLayoutPanel7;
        private TableLayoutPanel tableLayoutPanel14;
        private TableLayoutPanel tableLayoutPanel15;
        private TableLayoutPanel tableLayoutPanel16;
        private TableLayoutPanel tableLayoutPanel17;
        private ComboBox cbxSearch;
        private TableLayoutPanel tableLayoutPanel19;
        private TableLayoutPanel tableLayoutPanel18;
        private TextBox txtPrixUnité;
        private IconButton btnClotureJournée;
        private ComboBox cbxCode;
        private BunifuDragControl bunifuDragControl1;
        private IconPictureBox btnMinimize;
        private IconPictureBox btnRestore;
        private TableLayoutPanel tableLayoutPanel20;
        private TableLayoutPanel tableLayoutPanel21;
        private ComboBox cbxPendingList;
        private IconButton btnPending;
        private Label label3;
        private IconButton btnReprintTicket;
        private IconButton btnDeleteAll;
        private TextBox txtNom;
        private Panel panel7;
        private Panel panel8;
        private IconButton btnReturn;
        private IconButton btnAjouter;
        private Label label5;
        private ComboBox cbxModePrix;
    }
}