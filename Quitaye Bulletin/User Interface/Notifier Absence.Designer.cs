namespace Quitaye_School.User_Interface
{
    partial class Notifier_Absence
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Notifier_Absence));
            this.DateOp = new System.Windows.Forms.DateTimePicker();
            this.btnFile = new FontAwesome.Sharp.IconButton();
            this.btnValider = new FontAwesome.Sharp.IconButton();
            this.txtCommentaire = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnFermer = new FontAwesome.Sharp.IconPictureBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblTitre = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.btnFermer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // DateOp
            // 
            this.DateOp.CalendarMonthBackground = System.Drawing.Color.LightCoral;
            this.DateOp.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateOp.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DateOp.Location = new System.Drawing.Point(140, 102);
            this.DateOp.Name = "DateOp";
            this.DateOp.Size = new System.Drawing.Size(204, 25);
            this.DateOp.TabIndex = 218;
            // 
            // btnFile
            // 
            this.btnFile.FlatAppearance.BorderSize = 0;
            this.btnFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFile.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFile.ForeColor = System.Drawing.Color.LightBlue;
            this.btnFile.IconChar = FontAwesome.Sharp.IconChar.Link;
            this.btnFile.IconColor = System.Drawing.Color.LightBlue;
            this.btnFile.IconSize = 20;
            this.btnFile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFile.Location = new System.Drawing.Point(349, 61);
            this.btnFile.Name = "btnFile";
            this.btnFile.Rotation = 0D;
            this.btnFile.Size = new System.Drawing.Size(30, 28);
            this.btnFile.TabIndex = 217;
            this.btnFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFile.UseVisualStyleBackColor = true;
            this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // btnValider
            // 
            this.btnValider.FlatAppearance.BorderSize = 0;
            this.btnValider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnValider.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnValider.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnValider.ForeColor = System.Drawing.Color.LightBlue;
            this.btnValider.IconChar = FontAwesome.Sharp.IconChar.Save;
            this.btnValider.IconColor = System.Drawing.Color.LightBlue;
            this.btnValider.IconSize = 32;
            this.btnValider.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnValider.Location = new System.Drawing.Point(255, 135);
            this.btnValider.Name = "btnValider";
            this.btnValider.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnValider.Rotation = 0D;
            this.btnValider.Size = new System.Drawing.Size(101, 45);
            this.btnValider.TabIndex = 219;
            this.btnValider.Text = "Valider";
            this.btnValider.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnValider.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnValider.UseVisualStyleBackColor = true;
            this.btnValider.Click += new System.EventHandler(this.btnValider_Click);
            // 
            // txtCommentaire
            // 
            this.txtCommentaire.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCommentaire.Location = new System.Drawing.Point(140, 62);
            this.txtCommentaire.Name = "txtCommentaire";
            this.txtCommentaire.Size = new System.Drawing.Size(204, 25);
            this.txtCommentaire.TabIndex = 216;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.LightBlue;
            this.label3.Location = new System.Drawing.Point(7, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 21);
            this.label3.TabIndex = 220;
            this.label3.Text = "Commentaire :";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.LightBlue;
            this.label2.Location = new System.Drawing.Point(7, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 21);
            this.label2.TabIndex = 221;
            this.label2.Text = "Date :*";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(2, 188);
            this.panel1.TabIndex = 222;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(394, 2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(2, 188);
            this.panel4.TabIndex = 223;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 190);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(396, 2);
            this.panel3.TabIndex = 225;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(396, 2);
            this.panel2.TabIndex = 224;
            // 
            // btnFermer
            // 
            this.btnFermer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFermer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.btnFermer.ForeColor = System.Drawing.Color.LightBlue;
            this.btnFermer.IconChar = FontAwesome.Sharp.IconChar.WindowClose;
            this.btnFermer.IconColor = System.Drawing.Color.LightBlue;
            this.btnFermer.IconSize = 25;
            this.btnFermer.Location = new System.Drawing.Point(363, 8);
            this.btnFermer.Name = "btnFermer";
            this.btnFermer.Size = new System.Drawing.Size(25, 25);
            this.btnFermer.TabIndex = 226;
            this.btnFermer.TabStop = false;
            this.btnFermer.Click += new System.EventHandler(this.btnFermer_Click);
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panel5.Location = new System.Drawing.Point(3, 39);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(400, 3);
            this.panel5.TabIndex = 227;
            // 
            // lblTitre
            // 
            this.lblTitre.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTitre.AutoSize = true;
            this.lblTitre.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitre.ForeColor = System.Drawing.Color.LightBlue;
            this.lblTitre.Location = new System.Drawing.Point(136, 10);
            this.lblTitre.Name = "lblTitre";
            this.lblTitre.Size = new System.Drawing.Size(125, 21);
            this.lblTitre.TabIndex = 229;
            this.lblTitre.Text = "Notifier Absence";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Quitaye_School.Properties.Resources.Logopit_1584731094407;
            this.pictureBox2.Location = new System.Drawing.Point(8, 6);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(30, 30);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 230;
            this.pictureBox2.TabStop = false;
            // 
            // Notifier_Absence
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.ClientSize = new System.Drawing.Size(396, 192);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.lblTitre);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.btnFermer);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.DateOp);
            this.Controls.Add(this.btnFile);
            this.Controls.Add(this.btnValider);
            this.Controls.Add(this.txtCommentaire);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Notifier_Absence";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Notifier Absence";
            ((System.ComponentModel.ISupportInitialize)(this.btnFermer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DateTimePicker DateOp;
        public FontAwesome.Sharp.IconButton btnFile;
        public FontAwesome.Sharp.IconButton btnValider;
        private System.Windows.Forms.TextBox txtCommentaire;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private FontAwesome.Sharp.IconPictureBox btnFermer;
        private System.Windows.Forms.Panel panel5;
        public System.Windows.Forms.Label lblTitre;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}