namespace Quitaye_School.User_Interface
{
    partial class Registre_Bulletin
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.cbxClasse = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClasse = new FontAwesome.Sharp.IconButton();
            this.cbxExamen = new System.Windows.Forms.ComboBox();
            this.lblClasse = new System.Windows.Forms.Label();
            this.btnExamen = new FontAwesome.Sharp.IconButton();
            this.btnAfficher = new FontAwesome.Sharp.IconButton();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.btnExcel = new FontAwesome.Sharp.IconButton();
            this.btnPDF = new FontAwesome.Sharp.IconButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(16, 76);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1135, 572);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // cbxClasse
            // 
            this.cbxClasse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxClasse.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxClasse.FormattingEnabled = true;
            this.cbxClasse.Location = new System.Drawing.Point(97, 25);
            this.cbxClasse.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbxClasse.Name = "cbxClasse";
            this.cbxClasse.Size = new System.Drawing.Size(207, 29);
            this.cbxClasse.TabIndex = 238;
            this.cbxClasse.SelectedIndexChanged += new System.EventHandler(this.cbxClasse_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.LightBlue;
            this.label1.Location = new System.Drawing.Point(16, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 23);
            this.label1.TabIndex = 237;
            this.label1.Text = "Classe :";
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
            this.btnClasse.Location = new System.Drawing.Point(313, 25);
            this.btnClasse.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClasse.Name = "btnClasse";
            this.btnClasse.Size = new System.Drawing.Size(33, 31);
            this.btnClasse.TabIndex = 239;
            this.btnClasse.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClasse.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClasse.UseVisualStyleBackColor = true;
            // 
            // cbxExamen
            // 
            this.cbxExamen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxExamen.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxExamen.FormattingEnabled = true;
            this.cbxExamen.Location = new System.Drawing.Point(479, 23);
            this.cbxExamen.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbxExamen.Name = "cbxExamen";
            this.cbxExamen.Size = new System.Drawing.Size(167, 29);
            this.cbxExamen.TabIndex = 246;
            // 
            // lblClasse
            // 
            this.lblClasse.AutoSize = true;
            this.lblClasse.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClasse.ForeColor = System.Drawing.Color.LightBlue;
            this.lblClasse.Location = new System.Drawing.Point(391, 28);
            this.lblClasse.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblClasse.Name = "lblClasse";
            this.lblClasse.Size = new System.Drawing.Size(79, 23);
            this.lblClasse.TabIndex = 245;
            this.lblClasse.Text = "Examen :";
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
            this.btnExamen.Location = new System.Drawing.Point(655, 25);
            this.btnExamen.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExamen.Name = "btnExamen";
            this.btnExamen.Size = new System.Drawing.Size(33, 31);
            this.btnExamen.TabIndex = 247;
            this.btnExamen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExamen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExamen.UseVisualStyleBackColor = true;
            // 
            // btnAfficher
            // 
            this.btnAfficher.FlatAppearance.BorderSize = 0;
            this.btnAfficher.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAfficher.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAfficher.ForeColor = System.Drawing.Color.LightBlue;
            this.btnAfficher.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.btnAfficher.IconColor = System.Drawing.Color.LightBlue;
            this.btnAfficher.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnAfficher.IconSize = 32;
            this.btnAfficher.Location = new System.Drawing.Point(971, 20);
            this.btnAfficher.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAfficher.Name = "btnAfficher";
            this.btnAfficher.Size = new System.Drawing.Size(239, 39);
            this.btnAfficher.TabIndex = 248;
            this.btnAfficher.Text = "Générer les Bulletins";
            this.btnAfficher.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAfficher.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAfficher.UseVisualStyleBackColor = true;
            this.btnAfficher.Click += new System.EventHandler(this.btnAfficher_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(1124, 4);
            this.dataGridView2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.Size = new System.Drawing.Size(57, 12);
            this.dataGridView2.TabIndex = 249;
            this.dataGridView2.Visible = false;
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.AllowUserToDeleteRows = false;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(1211, 15);
            this.dataGridView3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.ReadOnly = true;
            this.dataGridView3.RowHeadersWidth = 51;
            this.dataGridView3.Size = new System.Drawing.Size(57, 12);
            this.dataGridView3.TabIndex = 249;
            this.dataGridView3.Visible = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(1297, 23);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(57, 12);
            this.dataGridView1.TabIndex = 249;
            this.dataGridView1.Visible = false;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton2.ForeColor = System.Drawing.Color.LightBlue;
            this.radioButton2.Location = new System.Drawing.Point(823, 26);
            this.radioButton2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(137, 27);
            this.radioButton2.TabIndex = 252;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Examen Blanc";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton1.ForeColor = System.Drawing.Color.LightBlue;
            this.radioButton1.Location = new System.Drawing.Point(708, 26);
            this.radioButton1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(88, 27);
            this.radioButton1.TabIndex = 253;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Normal";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcel.FlatAppearance.BorderSize = 0;
            this.btnExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.ForeColor = System.Drawing.Color.LightBlue;
            this.btnExcel.IconChar = FontAwesome.Sharp.IconChar.Print;
            this.btnExcel.IconColor = System.Drawing.Color.LightBlue;
            this.btnExcel.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnExcel.IconSize = 20;
            this.btnExcel.Location = new System.Drawing.Point(1059, 17);
            this.btnExcel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(100, 44);
            this.btnExcel.TabIndex = 255;
            this.btnExcel.Text = "Excel";
            this.btnExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExcel.UseVisualStyleBackColor = true;
            // 
            // btnPDF
            // 
            this.btnPDF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPDF.FlatAppearance.BorderSize = 0;
            this.btnPDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPDF.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPDF.ForeColor = System.Drawing.Color.LightBlue;
            this.btnPDF.IconChar = FontAwesome.Sharp.IconChar.Print;
            this.btnPDF.IconColor = System.Drawing.Color.LightBlue;
            this.btnPDF.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnPDF.IconSize = 20;
            this.btnPDF.Location = new System.Drawing.Point(955, 20);
            this.btnPDF.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPDF.Name = "btnPDF";
            this.btnPDF.Size = new System.Drawing.Size(96, 44);
            this.btnPDF.TabIndex = 254;
            this.btnPDF.Text = "PDF";
            this.btnPDF.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPDF.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPDF.UseVisualStyleBackColor = true;
            this.btnPDF.Click += new System.EventHandler(this.btnPDF_Click);
            // 
            // Registre_Bulletin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.ClientSize = new System.Drawing.Size(1167, 663);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.btnPDF);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.dataGridView3);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.btnAfficher);
            this.Controls.Add(this.btnExamen);
            this.Controls.Add(this.cbxExamen);
            this.Controls.Add(this.lblClasse);
            this.Controls.Add(this.btnClasse);
            this.Controls.Add(this.cbxClasse);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Registre_Bulletin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registre_Bulletin";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ComboBox cbxClasse;
        private System.Windows.Forms.Label label1;
        private FontAwesome.Sharp.IconButton btnClasse;
        private System.Windows.Forms.ComboBox cbxExamen;
        private System.Windows.Forms.Label lblClasse;
        private FontAwesome.Sharp.IconButton btnExamen;
        public FontAwesome.Sharp.IconButton btnAfficher;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        public FontAwesome.Sharp.IconButton btnExcel;
        public FontAwesome.Sharp.IconButton btnPDF;
    }
}