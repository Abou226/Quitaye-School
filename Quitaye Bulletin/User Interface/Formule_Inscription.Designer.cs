namespace Quitaye_School.User_Interface
{
    partial class Formule_Inscription
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
            this.bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.checkFacture = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.panelFacture = new System.Windows.Forms.FlowLayoutPanel();
            this.ch_Gratuit = new System.Windows.Forms.CheckBox();
            this.txtMontant = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFormule = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblTitre = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnFermer = new FontAwesome.Sharp.IconPictureBox();
            this.btnAjouterFormule = new FontAwesome.Sharp.IconButton();
            this.panelFacture.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFermer)).BeginInit();
            this.SuspendLayout();
            // 
            // bunifuDragControl1
            // 
            this.bunifuDragControl1.Fixed = true;
            this.bunifuDragControl1.Horizontal = true;
            this.bunifuDragControl1.TargetControl = this;
            this.bunifuDragControl1.Vertical = true;
            // 
            // checkFacture
            // 
            this.checkFacture.AutoSize = true;
            this.checkFacture.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkFacture.ForeColor = System.Drawing.Color.LightBlue;
            this.checkFacture.Location = new System.Drawing.Point(50, 138);
            this.checkFacture.Name = "checkFacture";
            this.checkFacture.Size = new System.Drawing.Size(143, 21);
            this.checkFacture.TabIndex = 197;
            this.checkFacture.Text = "Lier à une catégorie";
            this.checkFacture.UseVisualStyleBackColor = true;
            this.checkFacture.CheckedChanged += new System.EventHandler(this.checkFacture_CheckedChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(3, 3);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(264, 25);
            this.comboBox1.TabIndex = 184;
            // 
            // panelFacture
            // 
            this.panelFacture.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelFacture.Controls.Add(this.comboBox1);
            this.panelFacture.Location = new System.Drawing.Point(45, 199);
            this.panelFacture.MaximumSize = new System.Drawing.Size(277, 36);
            this.panelFacture.MinimumSize = new System.Drawing.Size(277, 10);
            this.panelFacture.Name = "panelFacture";
            this.panelFacture.Size = new System.Drawing.Size(277, 36);
            this.panelFacture.TabIndex = 198;
            this.panelFacture.Visible = false;
            // 
            // ch_Gratuit
            // 
            this.ch_Gratuit.AutoSize = true;
            this.ch_Gratuit.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ch_Gratuit.ForeColor = System.Drawing.Color.LightBlue;
            this.ch_Gratuit.Location = new System.Drawing.Point(348, 107);
            this.ch_Gratuit.Name = "ch_Gratuit";
            this.ch_Gratuit.Size = new System.Drawing.Size(66, 21);
            this.ch_Gratuit.TabIndex = 193;
            this.ch_Gratuit.Text = "Gratuit";
            this.ch_Gratuit.UseVisualStyleBackColor = true;
            this.ch_Gratuit.CheckStateChanged += new System.EventHandler(this.ch_Gratuit_CheckStateChanged);
            // 
            // txtMontant
            // 
            this.txtMontant.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMontant.Location = new System.Drawing.Point(120, 105);
            this.txtMontant.Name = "txtMontant";
            this.txtMontant.Size = new System.Drawing.Size(207, 25);
            this.txtMontant.TabIndex = 190;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.LightBlue;
            this.label2.Location = new System.Drawing.Point(47, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 17);
            this.label2.TabIndex = 191;
            this.label2.Text = "Montant :";
            // 
            // txtFormule
            // 
            this.txtFormule.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFormule.Location = new System.Drawing.Point(120, 67);
            this.txtFormule.Name = "txtFormule";
            this.txtFormule.Size = new System.Drawing.Size(207, 25);
            this.txtFormule.TabIndex = 189;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.LightBlue;
            this.label1.Location = new System.Drawing.Point(47, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 17);
            this.label1.TabIndex = 192;
            this.label1.Text = "Formule :";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(2, 244);
            this.panel1.TabIndex = 184;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(493, 2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(2, 244);
            this.panel4.TabIndex = 185;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 246);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(495, 2);
            this.panel3.TabIndex = 187;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(495, 2);
            this.panel2.TabIndex = 186;
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panel5.Location = new System.Drawing.Point(0, 38);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(495, 3);
            this.panel5.TabIndex = 188;
            // 
            // lblTitre
            // 
            this.lblTitre.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTitre.AutoSize = true;
            this.lblTitre.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitre.ForeColor = System.Drawing.Color.LightBlue;
            this.lblTitre.Location = new System.Drawing.Point(175, 10);
            this.lblTitre.Name = "lblTitre";
            this.lblTitre.Size = new System.Drawing.Size(145, 21);
            this.lblTitre.TabIndex = 199;
            this.lblTitre.Text = "Formule Inscription";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Quitaye_School.Properties.Resources.Logopit_1584731094407;
            this.pictureBox2.Location = new System.Drawing.Point(12, 6);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(30, 30);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 220;
            this.pictureBox2.TabStop = false;
            // 
            // btnFermer
            // 
            this.btnFermer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFermer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.btnFermer.ForeColor = System.Drawing.Color.LightBlue;
            this.btnFermer.IconChar = FontAwesome.Sharp.IconChar.WindowClose;
            this.btnFermer.IconColor = System.Drawing.Color.LightBlue;
            this.btnFermer.IconSize = 25;
            this.btnFermer.Location = new System.Drawing.Point(461, 6);
            this.btnFermer.Name = "btnFermer";
            this.btnFermer.Size = new System.Drawing.Size(25, 25);
            this.btnFermer.TabIndex = 195;
            this.btnFermer.TabStop = false;
            this.btnFermer.Click += new System.EventHandler(this.btnFermer_Click);
            // 
            // btnAjouterFormule
            // 
            this.btnAjouterFormule.FlatAppearance.BorderSize = 0;
            this.btnAjouterFormule.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAjouterFormule.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnAjouterFormule.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAjouterFormule.ForeColor = System.Drawing.Color.LightBlue;
            this.btnAjouterFormule.IconChar = FontAwesome.Sharp.IconChar.Plus;
            this.btnAjouterFormule.IconColor = System.Drawing.Color.LightBlue;
            this.btnAjouterFormule.IconSize = 32;
            this.btnAjouterFormule.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAjouterFormule.Location = new System.Drawing.Point(328, 160);
            this.btnAjouterFormule.Name = "btnAjouterFormule";
            this.btnAjouterFormule.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnAjouterFormule.Rotation = 0D;
            this.btnAjouterFormule.Size = new System.Drawing.Size(161, 39);
            this.btnAjouterFormule.TabIndex = 194;
            this.btnAjouterFormule.Text = "Ajouter la formule";
            this.btnAjouterFormule.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAjouterFormule.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAjouterFormule.UseVisualStyleBackColor = true;
            this.btnAjouterFormule.Click += new System.EventHandler(this.btnAjouterFormule_Click);
            // 
            // Formule_Inscription
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.ClientSize = new System.Drawing.Size(495, 248);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.lblTitre);
            this.Controls.Add(this.checkFacture);
            this.Controls.Add(this.panelFacture);
            this.Controls.Add(this.btnFermer);
            this.Controls.Add(this.btnAjouterFormule);
            this.Controls.Add(this.ch_Gratuit);
            this.Controls.Add(this.txtMontant);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtFormule);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Formule_Inscription";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Formule_Inscription";
            this.panelFacture.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFermer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
        private System.Windows.Forms.CheckBox checkFacture;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.FlowLayoutPanel panelFacture;
        private FontAwesome.Sharp.IconPictureBox btnFermer;
        public FontAwesome.Sharp.IconButton btnAjouterFormule;
        private System.Windows.Forms.CheckBox ch_Gratuit;
        public System.Windows.Forms.TextBox txtMontant;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtFormule;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel5;
        public System.Windows.Forms.Label lblTitre;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}