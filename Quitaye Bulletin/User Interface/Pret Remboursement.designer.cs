
namespace Quitaye_School.User_Interface
{
    partial class Pret_Remboursement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Pret_Remboursement));
            dataGridView1 = new System.Windows.Forms.DataGridView();
            btnNouvelleOpération = new FontAwesome.Sharp.IconButton();
            tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            txtsearch = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            btnPdf = new FontAwesome.Sharp.IconButton();
            btnExcel = new FontAwesome.Sharp.IconButton();
            EndDate = new System.Windows.Forms.DateTimePicker();
            label3 = new System.Windows.Forms.Label();
            startDate = new System.Windows.Forms.DateTimePicker();
            label2 = new System.Windows.Forms.Label();
            cbxType = new System.Windows.Forms.ComboBox();
            label4 = new System.Windows.Forms.Label();
            cbxMode = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(dataGridView1)).BeginInit();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(iconPictureBox1)).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(21)))), ((int)(((byte)(234)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Indigo;
            dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new System.Drawing.Point(12, 138);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.SlateBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Indigo;
            dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new System.Drawing.Size(1076, 470);
            dataGridView1.TabIndex = 190;
            // 
            // btnNouvelleOpération
            // 
            btnNouvelleOpération.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            btnNouvelleOpération.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            btnNouvelleOpération.FlatAppearance.BorderSize = 0;
            btnNouvelleOpération.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnNouvelleOpération.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            btnNouvelleOpération.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            btnNouvelleOpération.ForeColor = System.Drawing.Color.LightBlue;
            btnNouvelleOpération.IconChar = FontAwesome.Sharp.IconChar.Plus;
            btnNouvelleOpération.IconColor = System.Drawing.Color.LightBlue;
            btnNouvelleOpération.IconSize = 25;
            btnNouvelleOpération.Location = new System.Drawing.Point(23, 40);
            btnNouvelleOpération.Name = "btnNouvelleOpération";
            btnNouvelleOpération.Rotation = 0D;
            btnNouvelleOpération.Size = new System.Drawing.Size(170, 34);
            btnNouvelleOpération.TabIndex = 6;
            btnNouvelleOpération.Text = "Nouvelle Opération";
            btnNouvelleOpération.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnNouvelleOpération.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnNouvelleOpération.UseVisualStyleBackColor = false;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            tableLayoutPanel2.ColumnCount = 12;
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.99493F));
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.960094F));
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.1083F));
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.68283F));
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.64303F));
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.769199F));
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.769199F));
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.77153F));
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.768099F));
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.766397F));
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.766397F));
            tableLayoutPanel2.Controls.Add(iconPictureBox1, 0, 0);
            tableLayoutPanel2.Controls.Add(txtsearch, 1, 0);
            tableLayoutPanel2.Controls.Add(label1, 2, 0);
            tableLayoutPanel2.Controls.Add(btnPdf, 11, 0);
            tableLayoutPanel2.Controls.Add(btnExcel, 10, 0);
            tableLayoutPanel2.Controls.Add(EndDate, 9, 0);
            tableLayoutPanel2.Controls.Add(label3, 8, 0);
            tableLayoutPanel2.Controls.Add(startDate, 7, 0);
            tableLayoutPanel2.Controls.Add(label2, 6, 0);
            tableLayoutPanel2.Controls.Add(cbxType, 5, 0);
            tableLayoutPanel2.Controls.Add(label4, 4, 0);
            tableLayoutPanel2.Controls.Add(cbxMode, 3, 0);
            tableLayoutPanel2.Location = new System.Drawing.Point(12, 89);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new System.Drawing.Size(1076, 40);
            tableLayoutPanel2.TabIndex = 191;
            // 
            // iconPictureBox1
            // 
            iconPictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            iconPictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            iconPictureBox1.ForeColor = System.Drawing.Color.LightBlue;
            iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.Search;
            iconPictureBox1.IconColor = System.Drawing.Color.LightBlue;
            iconPictureBox1.IconSize = 25;
            iconPictureBox1.Location = new System.Drawing.Point(12, 3);
            iconPictureBox1.Name = "iconPictureBox1";
            iconPictureBox1.Size = new System.Drawing.Size(25, 25);
            iconPictureBox1.TabIndex = 263;
            iconPictureBox1.TabStop = false;
            // 
            // txtsearch
            // 
            txtsearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            txtsearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            txtsearch.Location = new System.Drawing.Point(43, 3);
            txtsearch.Name = "txtsearch";
            txtsearch.Size = new System.Drawing.Size(118, 25);
            txtsearch.TabIndex = 105;
            // 
            // label1
            // 
            label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            label1.AutoSize = true;
            label1.BackColor = System.Drawing.Color.Transparent;
            label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label1.ForeColor = System.Drawing.Color.LightBlue;
            label1.Location = new System.Drawing.Point(169, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(50, 17);
            label1.TabIndex = 49;
            label1.Text = "Mode :";
            // 
            // btnPdf
            // 
            btnPdf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            btnPdf.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            btnPdf.FlatAppearance.BorderSize = 0;
            btnPdf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnPdf.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            btnPdf.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            btnPdf.ForeColor = System.Drawing.Color.LightBlue;
            btnPdf.IconChar = FontAwesome.Sharp.IconChar.Print;
            btnPdf.IconColor = System.Drawing.Color.LightBlue;
            btnPdf.IconSize = 32;
            btnPdf.Location = new System.Drawing.Point(995, 3);
            btnPdf.Name = "btnPdf";
            btnPdf.Rotation = 0D;
            btnPdf.Size = new System.Drawing.Size(78, 34);
            btnPdf.TabIndex = 167;
            btnPdf.Text = "PDF";
            btnPdf.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnPdf.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnPdf.UseVisualStyleBackColor = false;
            // 
            // btnExcel
            // 
            btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            btnExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            btnExcel.FlatAppearance.BorderSize = 0;
            btnExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnExcel.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            btnExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            btnExcel.ForeColor = System.Drawing.Color.LightBlue;
            btnExcel.IconChar = FontAwesome.Sharp.IconChar.Print;
            btnExcel.IconColor = System.Drawing.Color.LightBlue;
            btnExcel.IconSize = 32;
            btnExcel.Location = new System.Drawing.Point(892, 3);
            btnExcel.Name = "btnExcel";
            btnExcel.Rotation = 0D;
            btnExcel.Size = new System.Drawing.Size(84, 34);
            btnExcel.TabIndex = 167;
            btnExcel.Text = "Excel";
            btnExcel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnExcel.UseVisualStyleBackColor = false;
            // 
            // EndDate
            // 
            EndDate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            EndDate.CalendarMonthBackground = System.Drawing.Color.LightCoral;
            EndDate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            EndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            EndDate.Location = new System.Drawing.Point(802, 3);
            EndDate.Name = "EndDate";
            EndDate.Size = new System.Drawing.Size(84, 25);
            EndDate.TabIndex = 108;
            // 
            // label3
            // 
            label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            label3.AutoSize = true;
            label3.BackColor = System.Drawing.Color.Transparent;
            label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label3.ForeColor = System.Drawing.Color.LightBlue;
            label3.Location = new System.Drawing.Point(738, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(31, 17);
            label3.TabIndex = 163;
            label3.Text = "Fin :";
            // 
            // startDate
            // 
            startDate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            startDate.CalendarMonthBackground = System.Drawing.Color.LightCoral;
            startDate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            startDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            startDate.Location = new System.Drawing.Point(622, 3);
            startDate.Name = "startDate";
            startDate.Size = new System.Drawing.Size(84, 25);
            startDate.TabIndex = 108;
            // 
            // label2
            // 
            label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            label2.AutoSize = true;
            label2.BackColor = System.Drawing.Color.Transparent;
            label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label2.ForeColor = System.Drawing.Color.LightBlue;
            label2.Location = new System.Drawing.Point(549, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(50, 17);
            label2.TabIndex = 163;
            label2.Text = "Début :";
            // 
            // cbxType
            // 
            cbxType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            cbxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cbxType.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            cbxType.FormattingEnabled = true;
            cbxType.Location = new System.Drawing.Point(422, 3);
            cbxType.Name = "cbxType";
            cbxType.Size = new System.Drawing.Size(104, 25);
            cbxType.TabIndex = 107;
            // 
            // label4
            // 
            label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            label4.AutoSize = true;
            label4.BackColor = System.Drawing.Color.Transparent;
            label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label4.ForeColor = System.Drawing.Color.LightBlue;
            label4.Location = new System.Drawing.Point(363, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(42, 17);
            label4.TabIndex = 49;
            label4.Text = "Type :";
            // 
            // cbxMode
            // 
            cbxMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            cbxMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cbxMode.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            cbxMode.FormattingEnabled = true;
            cbxMode.Location = new System.Drawing.Point(228, 3);
            cbxMode.Name = "cbxMode";
            cbxMode.Size = new System.Drawing.Size(119, 25);
            cbxMode.TabIndex = 107;
            // 
            // Pret_Remboursement
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            ClientSize = new System.Drawing.Size(1100, 620);
            Controls.Add(tableLayoutPanel2);
            Controls.Add(dataGridView1);
            Controls.Add(btnNouvelleOpération);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Icon = ((System.Drawing.Icon)(resources.GetObject("$Icon")));
            Name = "Pret_Remboursement";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Pret_Remboursement";
            ((System.ComponentModel.ISupportInitialize)(dataGridView1)).EndInit();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(iconPictureBox1)).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private FontAwesome.Sharp.IconButton btnNouvelleOpération;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        private System.Windows.Forms.TextBox txtsearch;
        private System.Windows.Forms.Label label1;
        private FontAwesome.Sharp.IconButton btnPdf;
        private FontAwesome.Sharp.IconButton btnExcel;
        private System.Windows.Forms.DateTimePicker EndDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker startDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbxMode;
    }
}