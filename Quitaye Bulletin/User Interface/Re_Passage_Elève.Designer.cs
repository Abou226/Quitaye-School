namespace Quitaye_School.User_Interface
{
    partial class Re_Passage_Elève
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
            this.cbxAnnéePrecedent = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAdmission = new FontAwesome.Sharp.IconButton();
            this.btnSelectTout = new FontAwesome.Sharp.IconButton();
            this.cbxClasse = new System.Windows.Forms.ComboBox();
            this.btnClasse = new FontAwesome.Sharp.IconButton();
            this.lblFille = new System.Windows.Forms.Label();
            this.lblGarçon = new System.Windows.Forms.Label();
            this.lblEffectif = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            this.txtsearch = new System.Windows.Forms.TextBox();
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.cbxClassePassage = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
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
            this.dataGridView1.Location = new System.Drawing.Point(19, 97);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.SlateBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Indigo;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1127, 506);
            this.dataGridView1.TabIndex = 264;
            // 
            // cbxAnnéePrecedent
            // 
            this.cbxAnnéePrecedent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxAnnéePrecedent.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxAnnéePrecedent.FormattingEnabled = true;
            this.cbxAnnéePrecedent.Location = new System.Drawing.Point(192, 47);
            this.cbxAnnéePrecedent.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbxAnnéePrecedent.Name = "cbxAnnéePrecedent";
            this.cbxAnnéePrecedent.Size = new System.Drawing.Size(181, 29);
            this.cbxAnnéePrecedent.TabIndex = 263;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.LightBlue;
            this.label3.Location = new System.Drawing.Point(25, 50);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(159, 23);
            this.label3.TabIndex = 262;
            this.label3.Text = "Année Precédente :";
            // 
            // btnAdmission
            // 
            this.btnAdmission.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdmission.FlatAppearance.BorderSize = 0;
            this.btnAdmission.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdmission.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdmission.ForeColor = System.Drawing.Color.LightBlue;
            this.btnAdmission.IconChar = FontAwesome.Sharp.IconChar.GraduationCap;
            this.btnAdmission.IconColor = System.Drawing.Color.LightBlue;
            this.btnAdmission.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnAdmission.IconSize = 32;
            this.btnAdmission.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdmission.Location = new System.Drawing.Point(1001, 609);
            this.btnAdmission.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAdmission.Name = "btnAdmission";
            this.btnAdmission.Size = new System.Drawing.Size(148, 42);
            this.btnAdmission.TabIndex = 261;
            this.btnAdmission.Text = "Admission";
            this.btnAdmission.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdmission.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAdmission.UseVisualStyleBackColor = true;
            // 
            // btnSelectTout
            // 
            this.btnSelectTout.FlatAppearance.BorderSize = 0;
            this.btnSelectTout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectTout.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectTout.ForeColor = System.Drawing.Color.LightBlue;
            this.btnSelectTout.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.btnSelectTout.IconColor = System.Drawing.Color.LightBlue;
            this.btnSelectTout.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnSelectTout.IconSize = 32;
            this.btnSelectTout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSelectTout.Location = new System.Drawing.Point(728, 43);
            this.btnSelectTout.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSelectTout.Name = "btnSelectTout";
            this.btnSelectTout.Size = new System.Drawing.Size(245, 42);
            this.btnSelectTout.TabIndex = 259;
            this.btnSelectTout.Text = "Tout Selectionner";
            this.btnSelectTout.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSelectTout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSelectTout.UseVisualStyleBackColor = true;
            // 
            // cbxClasse
            // 
            this.cbxClasse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxClasse.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxClasse.FormattingEnabled = true;
            this.cbxClasse.Location = new System.Drawing.Point(472, 49);
            this.cbxClasse.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbxClasse.Name = "cbxClasse";
            this.cbxClasse.Size = new System.Drawing.Size(179, 29);
            this.cbxClasse.TabIndex = 258;
            // 
            // btnClasse
            // 
            this.btnClasse.FlatAppearance.BorderSize = 0;
            this.btnClasse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClasse.ForeColor = System.Drawing.Color.LightBlue;
            this.btnClasse.IconChar = FontAwesome.Sharp.IconChar.Plus;
            this.btnClasse.IconColor = System.Drawing.Color.LightBlue;
            this.btnClasse.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnClasse.IconSize = 20;
            this.btnClasse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClasse.Location = new System.Drawing.Point(672, 50);
            this.btnClasse.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClasse.Name = "btnClasse";
            this.btnClasse.Size = new System.Drawing.Size(33, 31);
            this.btnClasse.TabIndex = 253;
            this.btnClasse.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClasse.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClasse.UseVisualStyleBackColor = true;
            // 
            // lblFille
            // 
            this.lblFille.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblFille.AutoSize = true;
            this.lblFille.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFille.ForeColor = System.Drawing.Color.LightBlue;
            this.lblFille.Location = new System.Drawing.Point(1177, 52);
            this.lblFille.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFille.Name = "lblFille";
            this.lblFille.Size = new System.Drawing.Size(74, 28);
            this.lblFille.TabIndex = 254;
            this.lblFille.Text = "Classe :";
            // 
            // lblGarçon
            // 
            this.lblGarçon.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblGarçon.AutoSize = true;
            this.lblGarçon.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGarçon.ForeColor = System.Drawing.Color.LightBlue;
            this.lblGarçon.Location = new System.Drawing.Point(1031, 55);
            this.lblGarçon.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGarçon.Name = "lblGarçon";
            this.lblGarçon.Size = new System.Drawing.Size(74, 28);
            this.lblGarçon.TabIndex = 255;
            this.lblGarçon.Text = "Classe :";
            // 
            // lblEffectif
            // 
            this.lblEffectif.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblEffectif.AutoSize = true;
            this.lblEffectif.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEffectif.ForeColor = System.Drawing.Color.LightBlue;
            this.lblEffectif.Location = new System.Drawing.Point(886, 55);
            this.lblEffectif.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEffectif.Name = "lblEffectif";
            this.lblEffectif.Size = new System.Drawing.Size(74, 28);
            this.lblEffectif.TabIndex = 256;
            this.lblEffectif.Text = "Classe :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.LightBlue;
            this.label1.Location = new System.Drawing.Point(391, 54);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 23);
            this.label1.TabIndex = 257;
            this.label1.Text = "Classe :";
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
            this.iconPictureBox1.Location = new System.Drawing.Point(741, 10);
            this.iconPictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.iconPictureBox1.Name = "iconPictureBox1";
            this.iconPictureBox1.Size = new System.Drawing.Size(32, 31);
            this.iconPictureBox1.TabIndex = 266;
            this.iconPictureBox1.TabStop = false;
            // 
            // txtsearch
            // 
            this.txtsearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtsearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsearch.Location = new System.Drawing.Point(781, 10);
            this.txtsearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtsearch.Name = "txtsearch";
            this.txtsearch.Size = new System.Drawing.Size(325, 29);
            this.txtsearch.TabIndex = 265;
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 15;
            this.bunifuElipse1.TargetControl = this.txtsearch;
            // 
            // cbxClassePassage
            // 
            this.cbxClassePassage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxClassePassage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxClassePassage.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxClassePassage.FormattingEnabled = true;
            this.cbxClassePassage.Location = new System.Drawing.Point(781, 618);
            this.cbxClassePassage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbxClassePassage.Name = "cbxClassePassage";
            this.cbxClassePassage.Size = new System.Drawing.Size(179, 29);
            this.cbxClassePassage.TabIndex = 268;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.LightBlue;
            this.label2.Location = new System.Drawing.Point(609, 619);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 23);
            this.label2.TabIndex = 267;
            this.label2.Text = "Classe de Passage :";
            // 
            // Re_Passage_Elève
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.ClientSize = new System.Drawing.Size(1167, 663);
            this.Controls.Add(this.cbxClassePassage);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.iconPictureBox1);
            this.Controls.Add(this.txtsearch);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cbxAnnéePrecedent);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnAdmission);
            this.Controls.Add(this.btnSelectTout);
            this.Controls.Add(this.cbxClasse);
            this.Controls.Add(this.btnClasse);
            this.Controls.Add(this.lblFille);
            this.Controls.Add(this.lblGarçon);
            this.Controls.Add(this.lblEffectif);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Re_Passage_Elève";
            this.Text = "Ajustement Passage";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox cbxAnnéePrecedent;
        private System.Windows.Forms.Label label3;
        public FontAwesome.Sharp.IconButton btnAdmission;
        public FontAwesome.Sharp.IconButton btnSelectTout;
        private System.Windows.Forms.ComboBox cbxClasse;
        private FontAwesome.Sharp.IconButton btnClasse;
        private System.Windows.Forms.Label lblFille;
        private System.Windows.Forms.Label lblGarçon;
        private System.Windows.Forms.Label lblEffectif;
        private System.Windows.Forms.Label label1;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        private System.Windows.Forms.TextBox txtsearch;
        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private System.Windows.Forms.ComboBox cbxClassePassage;
        private System.Windows.Forms.Label label2;
    }
}