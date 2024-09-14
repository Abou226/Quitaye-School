namespace Quitaye_School.User_Interface
{
    partial class Transfère_Eleve
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Transfère_Eleve));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnFile = new FontAwesome.Sharp.IconButton();
            this.btnValider = new FontAwesome.Sharp.IconButton();
            this.btnFermer = new FontAwesome.Sharp.IconPictureBox();
            this.DateOp = new System.Windows.Forms.DateTimePicker();
            this.txtCommentaire = new System.Windows.Forms.TextBox();
            this.txtMontant = new System.Windows.Forms.TextBox();
            this.lblScolarité = new System.Windows.Forms.Label();
            this.lblReste = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblTitre = new System.Windows.Forms.Label();
            this.bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.bunifuDragControl2 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.btnFermer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
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
            this.btnFile.Location = new System.Drawing.Point(481, 192);
            this.btnFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(40, 34);
            this.btnFile.TabIndex = 226;
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
            this.btnValider.Location = new System.Drawing.Point(356, 281);
            this.btnValider.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnValider.Name = "btnValider";
            this.btnValider.Padding = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.btnValider.Size = new System.Drawing.Size(160, 52);
            this.btnValider.TabIndex = 228;
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
            this.btnFermer.Location = new System.Drawing.Point(491, 10);
            this.btnFermer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnFermer.Name = "btnFermer";
            this.btnFermer.Size = new System.Drawing.Size(33, 31);
            this.btnFermer.TabIndex = 235;
            this.btnFermer.TabStop = false;
            this.toolTip1.SetToolTip(this.btnFermer, "Fermer");
            this.btnFermer.Click += new System.EventHandler(this.btnFermer_Click);
            // 
            // DateOp
            // 
            this.DateOp.CalendarMonthBackground = System.Drawing.Color.LightCoral;
            this.DateOp.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateOp.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DateOp.Location = new System.Drawing.Point(203, 242);
            this.DateOp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DateOp.Name = "DateOp";
            this.DateOp.Size = new System.Drawing.Size(271, 29);
            this.DateOp.TabIndex = 227;
            // 
            // txtCommentaire
            // 
            this.txtCommentaire.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCommentaire.Location = new System.Drawing.Point(203, 192);
            this.txtCommentaire.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCommentaire.Name = "txtCommentaire";
            this.txtCommentaire.Size = new System.Drawing.Size(271, 29);
            this.txtCommentaire.TabIndex = 225;
            // 
            // txtMontant
            // 
            this.txtMontant.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMontant.Location = new System.Drawing.Point(203, 138);
            this.txtMontant.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtMontant.Name = "txtMontant";
            this.txtMontant.Size = new System.Drawing.Size(271, 29);
            this.txtMontant.TabIndex = 224;
            this.txtMontant.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMontant_KeyPress);
            // 
            // lblScolarité
            // 
            this.lblScolarité.AutoSize = true;
            this.lblScolarité.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScolarité.ForeColor = System.Drawing.Color.LightBlue;
            this.lblScolarité.Location = new System.Drawing.Point(135, 54);
            this.lblScolarité.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblScolarité.Name = "lblScolarité";
            this.lblScolarité.Size = new System.Drawing.Size(56, 28);
            this.lblScolarité.TabIndex = 236;
            this.lblScolarité.Text = "Nom";
            this.lblScolarité.Visible = false;
            // 
            // lblReste
            // 
            this.lblReste.AutoSize = true;
            this.lblReste.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReste.ForeColor = System.Drawing.Color.LightBlue;
            this.lblReste.Location = new System.Drawing.Point(135, 102);
            this.lblReste.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReste.Name = "lblReste";
            this.lblReste.Size = new System.Drawing.Size(56, 28);
            this.lblReste.TabIndex = 237;
            this.lblReste.Text = "Nom";
            this.lblReste.Visible = false;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.LightBlue;
            this.label3.Location = new System.Drawing.Point(45, 193);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(139, 28);
            this.label3.TabIndex = 240;
            this.label3.Text = "Commentaire :";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.LightBlue;
            this.label2.Location = new System.Drawing.Point(45, 247);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 28);
            this.label2.TabIndex = 238;
            this.label2.Text = "Date :*";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.LightBlue;
            this.label1.Location = new System.Drawing.Point(45, 138);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 28);
            this.label1.TabIndex = 239;
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
            this.panel5.Size = new System.Drawing.Size(531, 4);
            this.panel5.TabIndex = 233;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(3, 338);
            this.panel1.TabIndex = 229;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(528, 2);
            this.panel4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(3, 338);
            this.panel4.TabIndex = 230;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 340);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(531, 2);
            this.panel3.TabIndex = 232;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(531, 2);
            this.panel2.TabIndex = 231;
            // 
            // lblTitre
            // 
            this.lblTitre.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTitre.AutoSize = true;
            this.lblTitre.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitre.ForeColor = System.Drawing.Color.LightBlue;
            this.lblTitre.Location = new System.Drawing.Point(63, 11);
            this.lblTitre.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitre.Name = "lblTitre";
            this.lblTitre.Size = new System.Drawing.Size(139, 28);
            this.lblTitre.TabIndex = 242;
            this.lblTitre.Text = "Transfère Elève";
            // 
            // bunifuDragControl1
            // 
            this.bunifuDragControl1.Fixed = true;
            this.bunifuDragControl1.Horizontal = true;
            this.bunifuDragControl1.TargetControl = this;
            this.bunifuDragControl1.Vertical = true;
            // 
            // bunifuDragControl2
            // 
            this.bunifuDragControl2.Fixed = true;
            this.bunifuDragControl2.Horizontal = true;
            this.bunifuDragControl2.TargetControl = this.lblTitre;
            this.bunifuDragControl2.Vertical = true;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Quitaye_School.Properties.Resources.Logopit_1584731094407;
            this.pictureBox2.Location = new System.Drawing.Point(15, 6);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(40, 37);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 241;
            this.pictureBox2.TabStop = false;
            // 
            // Transfère_Eleve
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.ClientSize = new System.Drawing.Size(531, 342);
            this.Controls.Add(this.lblTitre);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.DateOp);
            this.Controls.Add(this.btnFile);
            this.Controls.Add(this.btnValider);
            this.Controls.Add(this.txtCommentaire);
            this.Controls.Add(this.txtMontant);
            this.Controls.Add(this.lblScolarité);
            this.Controls.Add(this.lblReste);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnFermer);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Transfère_Eleve";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transfère Eleve";
            ((System.ComponentModel.ISupportInitialize)(this.btnFermer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        public FontAwesome.Sharp.IconButton btnFile;
        public FontAwesome.Sharp.IconButton btnValider;
        private FontAwesome.Sharp.IconPictureBox btnFermer;
        private System.Windows.Forms.PictureBox pictureBox2;
        public System.Windows.Forms.DateTimePicker DateOp;
        private System.Windows.Forms.TextBox txtCommentaire;
        private System.Windows.Forms.TextBox txtMontant;
        public System.Windows.Forms.Label lblScolarité;
        public System.Windows.Forms.Label lblReste;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Label lblTitre;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl2;
    }
}