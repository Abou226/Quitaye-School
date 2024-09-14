namespace Quitaye_School.User_Interface
{
    partial class List_Enseignant
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
            this.cbxClasse = new System.Windows.Forms.ComboBox();
            this.lblFille = new System.Windows.Forms.Label();
            this.lblGarçon = new System.Windows.Forms.Label();
            this.lblEffectif = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.btnPDF = new FontAwesome.Sharp.IconButton();
            this.btnExcel = new FontAwesome.Sharp.IconButton();
            this.btnClasse = new FontAwesome.Sharp.IconButton();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
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
            this.dataGridView1.Location = new System.Drawing.Point(24, 135);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.SlateBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Indigo;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1127, 504);
            this.dataGridView1.TabIndex = 237;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // cbxClasse
            // 
            this.cbxClasse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxClasse.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxClasse.FormattingEnabled = true;
            this.cbxClasse.Location = new System.Drawing.Point(105, 23);
            this.cbxClasse.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbxClasse.Name = "cbxClasse";
            this.cbxClasse.Size = new System.Drawing.Size(207, 29);
            this.cbxClasse.TabIndex = 236;
            this.cbxClasse.SelectedIndexChanged += new System.EventHandler(this.cbxClasse_SelectedIndexChanged);
            // 
            // lblFille
            // 
            this.lblFille.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblFille.AutoSize = true;
            this.lblFille.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFille.ForeColor = System.Drawing.Color.LightBlue;
            this.lblFille.Location = new System.Drawing.Point(1089, 30);
            this.lblFille.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFille.Name = "lblFille";
            this.lblFille.Size = new System.Drawing.Size(74, 28);
            this.lblFille.TabIndex = 232;
            this.lblFille.Text = "Classe :";
            // 
            // lblGarçon
            // 
            this.lblGarçon.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblGarçon.AutoSize = true;
            this.lblGarçon.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGarçon.ForeColor = System.Drawing.Color.LightBlue;
            this.lblGarçon.Location = new System.Drawing.Point(705, 30);
            this.lblGarçon.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGarçon.Name = "lblGarçon";
            this.lblGarçon.Size = new System.Drawing.Size(74, 28);
            this.lblGarçon.TabIndex = 233;
            this.lblGarçon.Text = "Classe :";
            // 
            // lblEffectif
            // 
            this.lblEffectif.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblEffectif.AutoSize = true;
            this.lblEffectif.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEffectif.ForeColor = System.Drawing.Color.LightBlue;
            this.lblEffectif.Location = new System.Drawing.Point(333, 30);
            this.lblEffectif.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEffectif.Name = "lblEffectif";
            this.lblEffectif.Size = new System.Drawing.Size(74, 28);
            this.lblEffectif.TabIndex = 234;
            this.lblEffectif.Text = "Classe :";
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
            this.label1.TabIndex = 235;
            this.label1.Text = "Classe :";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(259, 63);
            this.dataGridView2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.Size = new System.Drawing.Size(13, 12);
            this.dataGridView2.TabIndex = 240;
            this.dataGridView2.Visible = false;
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
            this.btnPDF.Location = new System.Drawing.Point(877, 80);
            this.btnPDF.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPDF.Name = "btnPDF";
            this.btnPDF.Size = new System.Drawing.Size(108, 44);
            this.btnPDF.TabIndex = 238;
            this.btnPDF.Text = "PDF";
            this.btnPDF.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPDF.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPDF.UseVisualStyleBackColor = true;
            this.btnPDF.Click += new System.EventHandler(this.btnPDF_Click);
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
            this.btnExcel.Location = new System.Drawing.Point(1021, 79);
            this.btnExcel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(129, 44);
            this.btnExcel.TabIndex = 239;
            this.btnExcel.Text = "Excel";
            this.btnExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
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
            this.btnClasse.Location = new System.Drawing.Point(321, 22);
            this.btnClasse.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClasse.Name = "btnClasse";
            this.btnClasse.Size = new System.Drawing.Size(33, 31);
            this.btnClasse.TabIndex = 231;
            this.btnClasse.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClasse.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClasse.UseVisualStyleBackColor = true;
            this.btnClasse.Click += new System.EventHandler(this.btnClasse_Click);
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.AllowUserToDeleteRows = false;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(316, 63);
            this.dataGridView3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.ReadOnly = true;
            this.dataGridView3.RowHeadersWidth = 51;
            this.dataGridView3.Size = new System.Drawing.Size(13, 12);
            this.dataGridView3.TabIndex = 240;
            this.dataGridView3.Visible = false;
            // 
            // List_Enseignant
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.ClientSize = new System.Drawing.Size(1167, 663);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cbxClasse);
            this.Controls.Add(this.lblFille);
            this.Controls.Add(this.lblGarçon);
            this.Controls.Add(this.lblEffectif);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView3);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.btnPDF);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.btnClasse);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "List_Enseignant";
            this.Text = "List_Enseignant";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox cbxClasse;
        private System.Windows.Forms.Label lblFille;
        private System.Windows.Forms.Label lblGarçon;
        private System.Windows.Forms.Label lblEffectif;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridView dataGridView2;
        public FontAwesome.Sharp.IconButton btnPDF;
        public FontAwesome.Sharp.IconButton btnExcel;
        private FontAwesome.Sharp.IconButton btnClasse;
        private System.Windows.Forms.DataGridView dataGridView3;
    }
}