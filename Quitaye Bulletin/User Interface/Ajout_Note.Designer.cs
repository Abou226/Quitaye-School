namespace Quitaye_School.User_Interface
{
    partial class Ajout_Note
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblTitre = new System.Windows.Forms.Label();
            this.cbxExamen = new System.Windows.Forms.ComboBox();
            this.lblClasse = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCoeff = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.NaissanceDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNoteClass = new System.Windows.Forms.TextBox();
            this.bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.btnExamen = new FontAwesome.Sharp.IconButton();
            this.btnMatière = new FontAwesome.Sharp.IconButton();
            this.btnValider = new FontAwesome.Sharp.IconButton();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnFermer = new FontAwesome.Sharp.IconPictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNoteCompo = new System.Windows.Forms.TextBox();
            this.cbxMat = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFermer)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(3, 237);
            this.panel1.TabIndex = 193;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(945, 2);
            this.panel4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(3, 237);
            this.panel4.TabIndex = 194;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 239);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(948, 2);
            this.panel3.TabIndex = 196;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(948, 2);
            this.panel2.TabIndex = 195;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panel5.Location = new System.Drawing.Point(3, 52);
            this.panel5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(948, 4);
            this.panel5.TabIndex = 197;
            // 
            // lblTitre
            // 
            this.lblTitre.AutoSize = true;
            this.lblTitre.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitre.ForeColor = System.Drawing.Color.LightBlue;
            this.lblTitre.Location = new System.Drawing.Point(59, 17);
            this.lblTitre.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitre.Name = "lblTitre";
            this.lblTitre.Size = new System.Drawing.Size(56, 28);
            this.lblTitre.TabIndex = 224;
            this.lblTitre.Text = "Note";
            // 
            // cbxExamen
            // 
            this.cbxExamen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxExamen.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxExamen.FormattingEnabled = true;
            this.cbxExamen.Location = new System.Drawing.Point(112, 79);
            this.cbxExamen.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbxExamen.Name = "cbxExamen";
            this.cbxExamen.Size = new System.Drawing.Size(167, 29);
            this.cbxExamen.TabIndex = 227;
            this.cbxExamen.SelectedIndexChanged += new System.EventHandler(this.cbxExamen_SelectedIndexChanged);
            // 
            // lblClasse
            // 
            this.lblClasse.AutoSize = true;
            this.lblClasse.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClasse.ForeColor = System.Drawing.Color.LightBlue;
            this.lblClasse.Location = new System.Drawing.Point(23, 84);
            this.lblClasse.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblClasse.Name = "lblClasse";
            this.lblClasse.Size = new System.Drawing.Size(79, 23);
            this.lblClasse.TabIndex = 226;
            this.lblClasse.Text = "Examen :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.LightBlue;
            this.label1.Location = new System.Drawing.Point(341, 84);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 23);
            this.label1.TabIndex = 226;
            this.label1.Text = "Matière :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.LightBlue;
            this.label2.Location = new System.Drawing.Point(689, 80);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 226;
            this.label2.Text = "Coefficient :";
            // 
            // txtCoeff
            // 
            this.txtCoeff.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCoeff.Location = new System.Drawing.Point(799, 80);
            this.txtCoeff.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCoeff.Name = "txtCoeff";
            this.txtCoeff.ReadOnly = true;
            this.txtCoeff.Size = new System.Drawing.Size(132, 29);
            this.txtCoeff.TabIndex = 228;
            this.txtCoeff.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNote_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.LightBlue;
            this.label3.Location = new System.Drawing.Point(23, 165);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 23);
            this.label3.TabIndex = 226;
            this.label3.Text = "Date :";
            // 
            // NaissanceDate
            // 
            this.NaissanceDate.CalendarMonthBackground = System.Drawing.Color.LightCoral;
            this.NaissanceDate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NaissanceDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.NaissanceDate.Location = new System.Drawing.Point(87, 161);
            this.NaissanceDate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.NaissanceDate.Name = "NaissanceDate";
            this.NaissanceDate.Size = new System.Drawing.Size(203, 29);
            this.NaissanceDate.TabIndex = 229;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.LightBlue;
            this.label4.Location = new System.Drawing.Point(297, 167);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(139, 23);
            this.label4.TabIndex = 226;
            this.label4.Text = "Note Classe /20 :";
            this.label4.Visible = false;
            // 
            // txtNoteClass
            // 
            this.txtNoteClass.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNoteClass.Location = new System.Drawing.Point(448, 165);
            this.txtNoteClass.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtNoteClass.MaxLength = 2;
            this.txtNoteClass.Name = "txtNoteClass";
            this.txtNoteClass.Size = new System.Drawing.Size(87, 29);
            this.txtNoteClass.TabIndex = 228;
            this.txtNoteClass.Visible = false;
            this.txtNoteClass.TextChanged += new System.EventHandler(this.txtNote_TextChanged);
            this.txtNoteClass.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNote_KeyPress);
            // 
            // bunifuDragControl1
            // 
            this.bunifuDragControl1.Fixed = true;
            this.bunifuDragControl1.Horizontal = true;
            this.bunifuDragControl1.TargetControl = this;
            this.bunifuDragControl1.Vertical = true;
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
            this.btnExamen.Location = new System.Drawing.Point(283, 79);
            this.btnExamen.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExamen.Name = "btnExamen";
            this.btnExamen.Size = new System.Drawing.Size(33, 31);
            this.btnExamen.TabIndex = 231;
            this.btnExamen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExamen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExamen.UseVisualStyleBackColor = true;
            this.btnExamen.Click += new System.EventHandler(this.btnExamen_Click);
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
            this.btnMatière.Location = new System.Drawing.Point(599, 79);
            this.btnMatière.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnMatière.Name = "btnMatière";
            this.btnMatière.Size = new System.Drawing.Size(33, 31);
            this.btnMatière.TabIndex = 231;
            this.btnMatière.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMatière.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnMatière.UseVisualStyleBackColor = true;
            this.btnMatière.Click += new System.EventHandler(this.btnMatière_Click);
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
            this.btnValider.Location = new System.Drawing.Point(799, 160);
            this.btnValider.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnValider.Name = "btnValider";
            this.btnValider.Size = new System.Drawing.Size(140, 44);
            this.btnValider.TabIndex = 230;
            this.btnValider.Text = "Valider";
            this.btnValider.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnValider.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnValider.UseVisualStyleBackColor = true;
            this.btnValider.Click += new System.EventHandler(this.btnValider_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Quitaye_School.Properties.Resources.Logopit_1584731094407;
            this.pictureBox2.Location = new System.Drawing.Point(11, 9);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(40, 37);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 225;
            this.pictureBox2.TabStop = false;
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
            this.btnFermer.Location = new System.Drawing.Point(901, 12);
            this.btnFermer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnFermer.Name = "btnFermer";
            this.btnFermer.Size = new System.Drawing.Size(33, 31);
            this.btnFermer.TabIndex = 205;
            this.btnFermer.TabStop = false;
            this.btnFermer.Click += new System.EventHandler(this.btnFermer_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.LightBlue;
            this.label5.Location = new System.Drawing.Point(543, 169);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(148, 23);
            this.label5.TabIndex = 226;
            this.label5.Text = "Note Compo /40 :";
            // 
            // txtNoteCompo
            // 
            this.txtNoteCompo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNoteCompo.Location = new System.Drawing.Point(703, 164);
            this.txtNoteCompo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtNoteCompo.MaxLength = 2;
            this.txtNoteCompo.Name = "txtNoteCompo";
            this.txtNoteCompo.Size = new System.Drawing.Size(87, 29);
            this.txtNoteCompo.TabIndex = 228;
            this.txtNoteCompo.TextChanged += new System.EventHandler(this.txtNoteCompo_TextChanged);
            this.txtNoteCompo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNote_KeyPress);
            // 
            // cbxMat
            // 
            this.cbxMat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMat.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxMat.FormattingEnabled = true;
            this.cbxMat.Location = new System.Drawing.Point(423, 79);
            this.cbxMat.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbxMat.Name = "cbxMat";
            this.cbxMat.Size = new System.Drawing.Size(167, 29);
            this.cbxMat.TabIndex = 227;
            this.cbxMat.SelectedIndexChanged += new System.EventHandler(this.cbxMat_SelectedIndexChanged);
            // 
            // Ajout_Note
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.ClientSize = new System.Drawing.Size(948, 241);
            this.Controls.Add(this.btnExamen);
            this.Controls.Add(this.btnMatière);
            this.Controls.Add(this.btnValider);
            this.Controls.Add(this.NaissanceDate);
            this.Controls.Add(this.txtNoteCompo);
            this.Controls.Add(this.txtNoteClass);
            this.Controls.Add(this.txtCoeff);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxMat);
            this.Controls.Add(this.cbxExamen);
            this.Controls.Add(this.lblClasse);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.lblTitre);
            this.Controls.Add(this.btnFermer);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Ajout_Note";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ajout_Note";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFermer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel5;
        private FontAwesome.Sharp.IconPictureBox btnFermer;
        private System.Windows.Forms.PictureBox pictureBox2;
        public System.Windows.Forms.Label lblTitre;
        private System.Windows.Forms.Label lblClasse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.DateTimePicker NaissanceDate;
        public FontAwesome.Sharp.IconButton btnValider;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
        private FontAwesome.Sharp.IconButton btnExamen;
        private FontAwesome.Sharp.IconButton btnMatière;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txtNoteClass;
        public System.Windows.Forms.TextBox txtNoteCompo;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.ComboBox cbxExamen;
        public System.Windows.Forms.TextBox txtCoeff;
        public System.Windows.Forms.ComboBox cbxMat;
    }
}