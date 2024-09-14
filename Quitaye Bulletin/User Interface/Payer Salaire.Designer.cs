﻿namespace Quitaye_School.User_Interface
{
    partial class Payer_Salaire
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
            this.CheckNegatif = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnFile = new FontAwesome.Sharp.IconButton();
            this.btnValider = new FontAwesome.Sharp.IconButton();
            this.btnFermer = new FontAwesome.Sharp.IconPictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.DateOp = new System.Windows.Forms.DateTimePicker();
            this.txtCommentaire = new System.Windows.Forms.TextBox();
            this.txtMontant = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblTitre = new System.Windows.Forms.Label();
            this.lblMontant = new System.Windows.Forms.Label();
            this.rCarteBleu = new System.Windows.Forms.RadioButton();
            this.rVirement = new System.Windows.Forms.RadioButton();
            this.rChèque = new System.Windows.Forms.RadioButton();
            this.rEspèce = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.btnFermer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // CheckNegatif
            // 
            this.CheckNegatif.AutoSize = true;
            this.CheckNegatif.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckNegatif.ForeColor = System.Drawing.Color.LightBlue;
            this.CheckNegatif.Location = new System.Drawing.Point(389, 101);
            this.CheckNegatif.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CheckNegatif.Name = "CheckNegatif";
            this.CheckNegatif.Size = new System.Drawing.Size(97, 27);
            this.CheckNegatif.TabIndex = 243;
            this.CheckNegatif.Text = "Négatif :";
            this.CheckNegatif.UseVisualStyleBackColor = true;
            this.CheckNegatif.Visible = false;
            // 
            // btnFile
            // 
            this.btnFile.FlatAppearance.BorderSize = 0;
            this.btnFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFile.ForeColor = System.Drawing.Color.LightBlue;
            this.btnFile.IconChar = FontAwesome.Sharp.IconChar.Link;
            this.btnFile.IconColor = System.Drawing.Color.LightBlue;
            this.btnFile.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnFile.IconSize = 20;
            this.btnFile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFile.Location = new System.Drawing.Point(446, 232);
            this.btnFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(40, 34);
            this.btnFile.TabIndex = 227;
            this.btnFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnFile, "Cliquez pour selectionner un fichier");
            this.btnFile.UseVisualStyleBackColor = true;
            this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // btnValider
            // 
            this.btnValider.FlatAppearance.BorderSize = 0;
            this.btnValider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnValider.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnValider.ForeColor = System.Drawing.Color.LightBlue;
            this.btnValider.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            this.btnValider.IconColor = System.Drawing.Color.LightBlue;
            this.btnValider.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnValider.IconSize = 32;
            this.btnValider.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnValider.Location = new System.Drawing.Point(306, 320);
            this.btnValider.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnValider.Name = "btnValider";
            this.btnValider.Padding = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.btnValider.Size = new System.Drawing.Size(160, 52);
            this.btnValider.TabIndex = 229;
            this.btnValider.Text = "Valider";
            this.btnValider.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnValider.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnValider, "Cliquez pour valider le payement");
            this.btnValider.UseVisualStyleBackColor = true;
            this.btnValider.Click += new System.EventHandler(this.btnValider_Click);
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
            this.btnFermer.Location = new System.Drawing.Point(463, 10);
            this.btnFermer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnFermer.Name = "btnFermer";
            this.btnFermer.Size = new System.Drawing.Size(33, 31);
            this.btnFermer.TabIndex = 236;
            this.btnFermer.TabStop = false;
            this.toolTip1.SetToolTip(this.btnFermer, "Cliquez pour fermer");
            this.btnFermer.Click += new System.EventHandler(this.btnFermer_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Quitaye_School.Properties.Resources.Logopit_1584731094407;
            this.pictureBox2.Location = new System.Drawing.Point(15, 6);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(40, 37);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 242;
            this.pictureBox2.TabStop = false;
            // 
            // bunifuDragControl1
            // 
            this.bunifuDragControl1.Fixed = true;
            this.bunifuDragControl1.Horizontal = true;
            this.bunifuDragControl1.TargetControl = this;
            this.bunifuDragControl1.Vertical = true;
            // 
            // DateOp
            // 
            this.DateOp.CalendarMonthBackground = System.Drawing.Color.LightCoral;
            this.DateOp.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateOp.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DateOp.Location = new System.Drawing.Point(167, 283);
            this.DateOp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DateOp.Name = "DateOp";
            this.DateOp.Size = new System.Drawing.Size(271, 29);
            this.DateOp.TabIndex = 228;
            // 
            // txtCommentaire
            // 
            this.txtCommentaire.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCommentaire.Location = new System.Drawing.Point(167, 234);
            this.txtCommentaire.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCommentaire.Name = "txtCommentaire";
            this.txtCommentaire.Size = new System.Drawing.Size(271, 29);
            this.txtCommentaire.TabIndex = 226;
            // 
            // txtMontant
            // 
            this.txtMontant.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMontant.Location = new System.Drawing.Point(167, 178);
            this.txtMontant.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtMontant.Name = "txtMontant";
            this.txtMontant.Size = new System.Drawing.Size(271, 29);
            this.txtMontant.TabIndex = 225;
            this.txtMontant.TextChanged += new System.EventHandler(this.txtMontant_TextChanged);
            this.txtMontant.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMontant_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.LightBlue;
            this.label3.Location = new System.Drawing.Point(12, 237);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(139, 28);
            this.label3.TabIndex = 240;
            this.label3.Text = "Commentaire :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.LightBlue;
            this.label2.Location = new System.Drawing.Point(24, 289);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 28);
            this.label2.TabIndex = 239;
            this.label2.Text = "Date :*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.LightBlue;
            this.label1.Location = new System.Drawing.Point(12, 179);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 28);
            this.label1.TabIndex = 241;
            this.label1.Text = "Montant :*";
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panel5.Location = new System.Drawing.Point(0, 47);
            this.panel5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(503, 4);
            this.panel5.TabIndex = 234;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(3, 382);
            this.panel1.TabIndex = 230;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(500, 2);
            this.panel4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(3, 382);
            this.panel4.TabIndex = 231;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 384);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(503, 2);
            this.panel3.TabIndex = 233;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(503, 2);
            this.panel2.TabIndex = 232;
            // 
            // lblTitre
            // 
            this.lblTitre.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTitre.AutoSize = true;
            this.lblTitre.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitre.ForeColor = System.Drawing.Color.LightBlue;
            this.lblTitre.Location = new System.Drawing.Point(165, 12);
            this.lblTitre.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitre.Name = "lblTitre";
            this.lblTitre.Size = new System.Drawing.Size(160, 28);
            this.lblTitre.TabIndex = 244;
            this.lblTitre.Text = "Payement Salaire";
            // 
            // lblMontant
            // 
            this.lblMontant.AutoSize = true;
            this.lblMontant.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMontant.ForeColor = System.Drawing.Color.LightBlue;
            this.lblMontant.Location = new System.Drawing.Point(110, 63);
            this.lblMontant.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMontant.Name = "lblMontant";
            this.lblMontant.Size = new System.Drawing.Size(152, 28);
            this.lblMontant.TabIndex = 241;
            this.lblMontant.Text = "Montant Payée :";
            // 
            // rCarteBleu
            // 
            this.rCarteBleu.AutoSize = true;
            this.rCarteBleu.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rCarteBleu.ForeColor = System.Drawing.Color.LightBlue;
            this.rCarteBleu.Location = new System.Drawing.Point(386, 136);
            this.rCarteBleu.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rCarteBleu.Name = "rCarteBleu";
            this.rCarteBleu.Size = new System.Drawing.Size(110, 27);
            this.rCarteBleu.TabIndex = 245;
            this.rCarteBleu.TabStop = true;
            this.rCarteBleu.Text = "Carte Bleu";
            this.rCarteBleu.UseVisualStyleBackColor = true;
            // 
            // rVirement
            // 
            this.rVirement.AutoSize = true;
            this.rVirement.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rVirement.ForeColor = System.Drawing.Color.LightBlue;
            this.rVirement.Location = new System.Drawing.Point(277, 136);
            this.rVirement.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rVirement.Name = "rVirement";
            this.rVirement.Size = new System.Drawing.Size(101, 27);
            this.rVirement.TabIndex = 246;
            this.rVirement.TabStop = true;
            this.rVirement.Text = "Virement";
            this.rVirement.UseVisualStyleBackColor = true;
            // 
            // rChèque
            // 
            this.rChèque.AutoSize = true;
            this.rChèque.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rChèque.ForeColor = System.Drawing.Color.LightBlue;
            this.rChèque.Location = new System.Drawing.Point(175, 135);
            this.rChèque.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rChèque.Name = "rChèque";
            this.rChèque.Size = new System.Drawing.Size(90, 27);
            this.rChèque.TabIndex = 247;
            this.rChèque.TabStop = true;
            this.rChèque.Text = "Chèque";
            this.rChèque.UseVisualStyleBackColor = true;
            // 
            // rEspèce
            // 
            this.rEspèce.AutoSize = true;
            this.rEspèce.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rEspèce.ForeColor = System.Drawing.Color.LightBlue;
            this.rEspèce.Location = new System.Drawing.Point(85, 136);
            this.rEspèce.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rEspèce.Name = "rEspèce";
            this.rEspèce.Size = new System.Drawing.Size(83, 27);
            this.rEspèce.TabIndex = 248;
            this.rEspèce.TabStop = true;
            this.rEspèce.Text = "Espèce";
            this.rEspèce.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.LightBlue;
            this.label4.Location = new System.Drawing.Point(12, 135);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 28);
            this.label4.TabIndex = 241;
            this.label4.Text = "Type :*";
            // 
            // Payer_Salaire
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.ClientSize = new System.Drawing.Size(503, 386);
            this.Controls.Add(this.rCarteBleu);
            this.Controls.Add(this.rVirement);
            this.Controls.Add(this.rChèque);
            this.Controls.Add(this.rEspèce);
            this.Controls.Add(this.lblTitre);
            this.Controls.Add(this.CheckNegatif);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.DateOp);
            this.Controls.Add(this.btnFile);
            this.Controls.Add(this.btnValider);
            this.Controls.Add(this.txtCommentaire);
            this.Controls.Add(this.txtMontant);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblMontant);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnFermer);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Payer_Salaire";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Payer_Salaire";
            ((System.ComponentModel.ISupportInitialize)(this.btnFermer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox CheckNegatif;
        private System.Windows.Forms.ToolTip toolTip1;
        public FontAwesome.Sharp.IconButton btnFile;
        public FontAwesome.Sharp.IconButton btnValider;
        private FontAwesome.Sharp.IconPictureBox btnFermer;
        private System.Windows.Forms.PictureBox pictureBox2;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
        public System.Windows.Forms.DateTimePicker DateOp;
        private System.Windows.Forms.TextBox txtCommentaire;
        private System.Windows.Forms.TextBox txtMontant;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Label lblTitre;
        public System.Windows.Forms.Label lblMontant;
        private System.Windows.Forms.RadioButton rCarteBleu;
        private System.Windows.Forms.RadioButton rVirement;
        private System.Windows.Forms.RadioButton rChèque;
        private System.Windows.Forms.RadioButton rEspèce;
        public System.Windows.Forms.Label label4;
    }
}