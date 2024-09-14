
namespace Quitaye_School.User_Interface
{
    partial class Nouveau_Virement
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Nouveau_Virement));
            panel2 = new System.Windows.Forms.Panel();
            panel1 = new System.Windows.Forms.Panel();
            panel4 = new System.Windows.Forms.Panel();
            panel3 = new System.Windows.Forms.Panel();
            btnFermer = new FontAwesome.Sharp.IconPictureBox();
            label8 = new System.Windows.Forms.Label();
            cbxSource = new System.Windows.Forms.ComboBox();
            label5 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            cbxDestination = new System.Windows.Forms.ComboBox();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            DateOpération = new System.Windows.Forms.DateTimePicker();
            txtmontant = new System.Windows.Forms.TextBox();
            btnFile = new FontAwesome.Sharp.IconButton();
            toolTip1 = new System.Windows.Forms.ToolTip(components);
            btnNouvelleOpération = new FontAwesome.Sharp.IconButton();
            btnAddSource = new FontAwesome.Sharp.IconButton();
            btnAddDestination = new FontAwesome.Sharp.IconButton();
            bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(components);
            ((System.ComponentModel.ISupportInitialize)(btnFermer)).BeginInit();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.BackColor = System.Drawing.Color.DarkTurquoise;
            panel2.Dock = System.Windows.Forms.DockStyle.Top;
            panel2.Location = new System.Drawing.Point(2, 0);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(326, 2);
            panel2.TabIndex = 184;
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.Color.DarkTurquoise;
            panel1.Dock = System.Windows.Forms.DockStyle.Left;
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(2, 281);
            panel1.TabIndex = 182;
            // 
            // panel4
            // 
            panel4.BackColor = System.Drawing.Color.DarkTurquoise;
            panel4.Dock = System.Windows.Forms.DockStyle.Right;
            panel4.Location = new System.Drawing.Point(328, 0);
            panel4.Name = "panel4";
            panel4.Size = new System.Drawing.Size(2, 281);
            panel4.TabIndex = 183;
            // 
            // panel3
            // 
            panel3.BackColor = System.Drawing.Color.DarkTurquoise;
            panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            panel3.Location = new System.Drawing.Point(0, 281);
            panel3.Name = "panel3";
            panel3.Size = new System.Drawing.Size(330, 2);
            panel3.TabIndex = 185;
            // 
            // btnFermer
            // 
            btnFermer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            btnFermer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            btnFermer.ForeColor = System.Drawing.Color.LightBlue;
            btnFermer.IconChar = FontAwesome.Sharp.IconChar.WindowClose;
            btnFermer.IconColor = System.Drawing.Color.LightBlue;
            btnFermer.IconSize = 25;
            btnFermer.Location = new System.Drawing.Point(293, 3);
            btnFermer.Name = "btnFermer";
            btnFermer.Size = new System.Drawing.Size(25, 25);
            btnFermer.TabIndex = 264;
            btnFermer.TabStop = false;
            // 
            // label8
            // 
            label8.Anchor = System.Windows.Forms.AnchorStyles.Top;
            label8.AutoSize = true;
            label8.BackColor = System.Drawing.Color.Transparent;
            label8.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label8.ForeColor = System.Drawing.Color.Cyan;
            label8.Location = new System.Drawing.Point(68, 12);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(194, 21);
            label8.TabIndex = 274;
            label8.Text = "Nouveau Virement Interne";
            // 
            // cbxSource
            // 
            cbxSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cbxSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            cbxSource.FormattingEnabled = true;
            cbxSource.Items.AddRange(new object[] {
            "Prêt",
            "Remboursement"});
            cbxSource.Location = new System.Drawing.Point(114, 64);
            cbxSource.Name = "cbxSource";
            cbxSource.Size = new System.Drawing.Size(175, 24);
            cbxSource.TabIndex = 275;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = System.Drawing.Color.Transparent;
            label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label5.ForeColor = System.Drawing.Color.LightBlue;
            label5.Location = new System.Drawing.Point(28, 66);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(55, 17);
            label5.TabIndex = 276;
            label5.Text = "Source :";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = System.Drawing.Color.Transparent;
            label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label1.ForeColor = System.Drawing.Color.LightBlue;
            label1.Location = new System.Drawing.Point(28, 103);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(80, 17);
            label1.TabIndex = 276;
            label1.Text = "Destination :";
            // 
            // cbxDestination
            // 
            cbxDestination.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cbxDestination.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            cbxDestination.FormattingEnabled = true;
            cbxDestination.Items.AddRange(new object[] {
            "Prêt",
            "Remboursement"});
            cbxDestination.Location = new System.Drawing.Point(114, 100);
            cbxDestination.Name = "cbxDestination";
            cbxDestination.Size = new System.Drawing.Size(175, 24);
            cbxDestination.TabIndex = 275;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = System.Drawing.Color.Transparent;
            label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label2.ForeColor = System.Drawing.Color.LightBlue;
            label2.Location = new System.Drawing.Point(28, 140);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(64, 17);
            label2.TabIndex = 276;
            label2.Text = "Montant :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = System.Drawing.Color.Transparent;
            label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label3.ForeColor = System.Drawing.Color.LightBlue;
            label3.Location = new System.Drawing.Point(28, 177);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(42, 17);
            label3.TabIndex = 276;
            label3.Text = "Date :";
            // 
            // DateOpération
            // 
            DateOpération.CalendarMonthBackground = System.Drawing.Color.LightCoral;
            DateOpération.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            DateOpération.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            DateOpération.Location = new System.Drawing.Point(114, 177);
            DateOpération.Name = "DateOpération";
            DateOpération.Size = new System.Drawing.Size(175, 25);
            DateOpération.TabIndex = 277;
            // 
            // txtmontant
            // 
            txtmontant.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            txtmontant.Location = new System.Drawing.Point(114, 136);
            txtmontant.Name = "txtmontant";
            txtmontant.Size = new System.Drawing.Size(175, 29);
            txtmontant.TabIndex = 278;
            // 
            // btnFile
            // 
            btnFile.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            btnFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            btnFile.FlatAppearance.BorderSize = 0;
            btnFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnFile.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            btnFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            btnFile.ForeColor = System.Drawing.Color.LightBlue;
            btnFile.IconChar = FontAwesome.Sharp.IconChar.Link;
            btnFile.IconColor = System.Drawing.Color.LightBlue;
            btnFile.IconSize = 24;
            btnFile.Location = new System.Drawing.Point(291, 140);
            btnFile.Name = "btnFile";
            btnFile.Rotation = 0D;
            btnFile.Size = new System.Drawing.Size(27, 24);
            btnFile.TabIndex = 279;
            btnFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            btnFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            toolTip1.SetToolTip(btnFile, "Cliquer pour selectionner une pièce jointe");
            btnFile.UseVisualStyleBackColor = false;
            btnFile.Visible = false;
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
            btnNouvelleOpération.IconChar = FontAwesome.Sharp.IconChar.Save;
            btnNouvelleOpération.IconColor = System.Drawing.Color.LightBlue;
            btnNouvelleOpération.IconSize = 25;
            btnNouvelleOpération.Location = new System.Drawing.Point(198, 225);
            btnNouvelleOpération.Name = "btnNouvelleOpération";
            btnNouvelleOpération.Rotation = 0D;
            btnNouvelleOpération.Size = new System.Drawing.Size(91, 34);
            btnNouvelleOpération.TabIndex = 280;
            btnNouvelleOpération.Text = "Valider";
            btnNouvelleOpération.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnNouvelleOpération.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnNouvelleOpération.UseVisualStyleBackColor = false;
            // 
            // btnAddSource
            // 
            btnAddSource.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            btnAddSource.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            btnAddSource.FlatAppearance.BorderSize = 0;
            btnAddSource.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnAddSource.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            btnAddSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            btnAddSource.ForeColor = System.Drawing.Color.LightBlue;
            btnAddSource.IconChar = FontAwesome.Sharp.IconChar.Plus;
            btnAddSource.IconColor = System.Drawing.Color.LightBlue;
            btnAddSource.IconSize = 24;
            btnAddSource.Location = new System.Drawing.Point(291, 65);
            btnAddSource.Name = "btnAddSource";
            btnAddSource.Rotation = 0D;
            btnAddSource.Size = new System.Drawing.Size(24, 22);
            btnAddSource.TabIndex = 281;
            btnAddSource.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            btnAddSource.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnAddSource.UseVisualStyleBackColor = false;
            // 
            // btnAddDestination
            // 
            btnAddDestination.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            btnAddDestination.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            btnAddDestination.FlatAppearance.BorderSize = 0;
            btnAddDestination.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnAddDestination.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            btnAddDestination.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            btnAddDestination.ForeColor = System.Drawing.Color.LightBlue;
            btnAddDestination.IconChar = FontAwesome.Sharp.IconChar.Plus;
            btnAddDestination.IconColor = System.Drawing.Color.LightBlue;
            btnAddDestination.IconSize = 24;
            btnAddDestination.Location = new System.Drawing.Point(291, 103);
            btnAddDestination.Name = "btnAddDestination";
            btnAddDestination.Rotation = 0D;
            btnAddDestination.Size = new System.Drawing.Size(24, 22);
            btnAddDestination.TabIndex = 281;
            btnAddDestination.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            btnAddDestination.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnAddDestination.UseVisualStyleBackColor = false;
            // 
            // bunifuDragControl1
            // 
            bunifuDragControl1.Fixed = true;
            bunifuDragControl1.Horizontal = true;
            bunifuDragControl1.TargetControl = this;
            bunifuDragControl1.Vertical = true;
            // 
            // Nouveau_Virement
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            ClientSize = new System.Drawing.Size(330, 283);
            Controls.Add(btnAddDestination);
            Controls.Add(btnAddSource);
            Controls.Add(btnNouvelleOpération);
            Controls.Add(btnFile);
            Controls.Add(txtmontant);
            Controls.Add(DateOpération);
            Controls.Add(cbxDestination);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(cbxSource);
            Controls.Add(label5);
            Controls.Add(label8);
            Controls.Add(btnFermer);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(panel4);
            Controls.Add(panel3);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Icon = ((System.Drawing.Icon)(resources.GetObject("$Icon")));
            Name = "Nouveau_Virement";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Nouveau Virement";
            ((System.ComponentModel.ISupportInitialize)(btnFermer)).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private FontAwesome.Sharp.IconPictureBox btnFermer;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbxSource;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxDestination;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker DateOpération;
        private System.Windows.Forms.TextBox txtmontant;
        private FontAwesome.Sharp.IconButton btnFile;
        private System.Windows.Forms.ToolTip toolTip1;
        private FontAwesome.Sharp.IconButton btnNouvelleOpération;
        private FontAwesome.Sharp.IconButton btnAddSource;
        private FontAwesome.Sharp.IconButton btnAddDestination;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
    }
}