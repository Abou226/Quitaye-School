namespace Quitaye_School.User_Interface
{
    partial class Passage_Elève
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cbxClasse = new System.Windows.Forms.ComboBox();
            this.lblFille = new System.Windows.Forms.Label();
            this.lblGarçon = new System.Windows.Forms.Label();
            this.lblEffectif = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSelectTout = new FontAwesome.Sharp.IconButton();
            this.btnClasse = new FontAwesome.Sharp.IconButton();
            this.btnAdmission = new FontAwesome.Sharp.IconButton();
            this.cbxAnnéePrecedent = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnRedoublant = new FontAwesome.Sharp.IconButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // cbxClasse
            // 
            this.cbxClasse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxClasse.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxClasse.FormattingEnabled = true;
            this.cbxClasse.Location = new System.Drawing.Point(473, 21);
            this.cbxClasse.Margin = new System.Windows.Forms.Padding(4);
            this.cbxClasse.Name = "cbxClasse";
            this.cbxClasse.Size = new System.Drawing.Size(179, 29);
            this.cbxClasse.TabIndex = 23;
            this.cbxClasse.SelectedIndexChanged += new System.EventHandler(this.cbxClasse_SelectedIndexChanged);
            // 
            // lblFille
            // 
            this.lblFille.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblFille.AutoSize = true;
            this.lblFille.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFille.ForeColor = System.Drawing.Color.LightBlue;
            this.lblFille.Location = new System.Drawing.Point(1178, 23);
            this.lblFille.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFille.Name = "lblFille";
            this.lblFille.Size = new System.Drawing.Size(74, 28);
            this.lblFille.TabIndex = 19;
            this.lblFille.Text = "Classe :";
            // 
            // lblGarçon
            // 
            this.lblGarçon.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblGarçon.AutoSize = true;
            this.lblGarçon.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGarçon.ForeColor = System.Drawing.Color.LightBlue;
            this.lblGarçon.Location = new System.Drawing.Point(1033, 27);
            this.lblGarçon.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGarçon.Name = "lblGarçon";
            this.lblGarçon.Size = new System.Drawing.Size(74, 28);
            this.lblGarçon.TabIndex = 20;
            this.lblGarçon.Text = "Classe :";
            // 
            // lblEffectif
            // 
            this.lblEffectif.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblEffectif.AutoSize = true;
            this.lblEffectif.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEffectif.ForeColor = System.Drawing.Color.LightBlue;
            this.lblEffectif.Location = new System.Drawing.Point(887, 27);
            this.lblEffectif.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEffectif.Name = "lblEffectif";
            this.lblEffectif.Size = new System.Drawing.Size(74, 28);
            this.lblEffectif.TabIndex = 21;
            this.lblEffectif.Text = "Classe :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.LightBlue;
            this.label1.Location = new System.Drawing.Point(392, 26);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 23);
            this.label1.TabIndex = 22;
            this.label1.Text = "Classe :";
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
            this.btnSelectTout.Location = new System.Drawing.Point(729, 15);
            this.btnSelectTout.Margin = new System.Windows.Forms.Padding(4);
            this.btnSelectTout.Name = "btnSelectTout";
            this.btnSelectTout.Size = new System.Drawing.Size(245, 42);
            this.btnSelectTout.TabIndex = 244;
            this.btnSelectTout.Text = "Tout Selectionner";
            this.btnSelectTout.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSelectTout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSelectTout.UseVisualStyleBackColor = true;
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
            this.btnClasse.Location = new System.Drawing.Point(673, 22);
            this.btnClasse.Margin = new System.Windows.Forms.Padding(4);
            this.btnClasse.Name = "btnClasse";
            this.btnClasse.Size = new System.Drawing.Size(33, 31);
            this.btnClasse.TabIndex = 18;
            this.btnClasse.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClasse.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClasse.UseVisualStyleBackColor = true;
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
            this.btnAdmission.Location = new System.Drawing.Point(1003, 613);
            this.btnAdmission.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdmission.Name = "btnAdmission";
            this.btnAdmission.Size = new System.Drawing.Size(148, 42);
            this.btnAdmission.TabIndex = 249;
            this.btnAdmission.Text = "Admission";
            this.btnAdmission.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdmission.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAdmission.UseVisualStyleBackColor = true;
            this.btnAdmission.Click += new System.EventHandler(this.btnAdmission_Click);
            // 
            // cbxAnnéePrecedent
            // 
            this.cbxAnnéePrecedent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxAnnéePrecedent.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxAnnéePrecedent.FormattingEnabled = true;
            this.cbxAnnéePrecedent.Location = new System.Drawing.Point(193, 18);
            this.cbxAnnéePrecedent.Margin = new System.Windows.Forms.Padding(4);
            this.cbxAnnéePrecedent.Name = "cbxAnnéePrecedent";
            this.cbxAnnéePrecedent.Size = new System.Drawing.Size(181, 29);
            this.cbxAnnéePrecedent.TabIndex = 251;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.LightBlue;
            this.label3.Location = new System.Drawing.Point(27, 22);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(159, 23);
            this.label3.TabIndex = 250;
            this.label3.Text = "Année Precédente :";
            // 
            // btnRedoublant
            // 
            this.btnRedoublant.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRedoublant.FlatAppearance.BorderSize = 0;
            this.btnRedoublant.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRedoublant.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRedoublant.ForeColor = System.Drawing.Color.Fuchsia;
            this.btnRedoublant.IconChar = FontAwesome.Sharp.IconChar.GraduationCap;
            this.btnRedoublant.IconColor = System.Drawing.Color.Fuchsia;
            this.btnRedoublant.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnRedoublant.IconSize = 32;
            this.btnRedoublant.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRedoublant.Location = new System.Drawing.Point(783, 614);
            this.btnRedoublant.Margin = new System.Windows.Forms.Padding(4);
            this.btnRedoublant.Name = "btnRedoublant";
            this.btnRedoublant.Size = new System.Drawing.Size(181, 42);
            this.btnRedoublant.TabIndex = 249;
            this.btnRedoublant.Text = "Redoublement";
            this.btnRedoublant.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRedoublant.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRedoublant.UseVisualStyleBackColor = true;
            this.btnRedoublant.Click += new System.EventHandler(this.btnAdmission_Click);
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
            this.dataGridView1.Location = new System.Drawing.Point(20, 64);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.SlateBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Indigo;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1127, 542);
            this.dataGridView1.TabIndex = 252;
            // 
            // Passage_Elève
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.ClientSize = new System.Drawing.Size(1167, 663);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cbxAnnéePrecedent);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnRedoublant);
            this.Controls.Add(this.btnAdmission);
            this.Controls.Add(this.btnSelectTout);
            this.Controls.Add(this.cbxClasse);
            this.Controls.Add(this.btnClasse);
            this.Controls.Add(this.lblFille);
            this.Controls.Add(this.lblGarçon);
            this.Controls.Add(this.lblEffectif);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Passage_Elève";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Passage Classe Superieur";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxClasse;
        private FontAwesome.Sharp.IconButton btnClasse;
        private System.Windows.Forms.Label lblFille;
        private System.Windows.Forms.Label lblGarçon;
        private System.Windows.Forms.Label lblEffectif;
        private System.Windows.Forms.Label label1;
        public FontAwesome.Sharp.IconButton btnSelectTout;
        public FontAwesome.Sharp.IconButton btnAdmission;
        private System.Windows.Forms.ComboBox cbxAnnéePrecedent;
        private System.Windows.Forms.Label label3;
        public FontAwesome.Sharp.IconButton btnRedoublant;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}