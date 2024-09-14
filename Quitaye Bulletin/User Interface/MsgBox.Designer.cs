namespace Quitaye_School.User_Interface
{
    partial class MsgBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MsgBox));
            this.bunifuSeparator1 = new Bunifu.Framework.UI.BunifuSeparator();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTitre = new System.Windows.Forms.Label();
            this.Header = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnFermer = new FontAwesome.Sharp.IconPictureBox();
            this.bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.lblMessage = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.btnIcon = new FontAwesome.Sharp.IconPictureBox();
            this.btnOk = new FontAwesome.Sharp.IconButton();
            this.btnNon = new FontAwesome.Sharp.IconButton();
            this.btnOui = new FontAwesome.Sharp.IconButton();
            this.panel4.SuspendLayout();
            this.Header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFermer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // bunifuSeparator1
            // 
            this.bunifuSeparator1.BackColor = System.Drawing.Color.Transparent;
            this.bunifuSeparator1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(216)))), ((int)(((byte)(230)))));
            this.bunifuSeparator1.LineThickness = 3;
            this.bunifuSeparator1.Location = new System.Drawing.Point(4, 43);
            this.bunifuSeparator1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.bunifuSeparator1.Name = "bunifuSeparator1";
            this.bunifuSeparator1.Size = new System.Drawing.Size(604, 12);
            this.bunifuSeparator1.TabIndex = 129;
            this.bunifuSeparator1.Transparency = 255;
            this.bunifuSeparator1.Vertical = false;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(610, 2);
            this.panel4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(3, 274);
            this.panel4.TabIndex = 125;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(3, 273);
            this.panel5.TabIndex = 115;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(3, 276);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(610, 2);
            this.panel3.TabIndex = 127;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(3, 276);
            this.panel1.TabIndex = 124;
            // 
            // lblTitre
            // 
            this.lblTitre.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitre.AutoSize = true;
            this.lblTitre.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitre.ForeColor = System.Drawing.Color.LightBlue;
            this.lblTitre.Location = new System.Drawing.Point(73, 11);
            this.lblTitre.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitre.Name = "lblTitre";
            this.lblTitre.Size = new System.Drawing.Size(61, 28);
            this.lblTitre.TabIndex = 45;
            this.lblTitre.Text = "Titre  ";
            // 
            // Header
            // 
            this.Header.Controls.Add(this.pictureBox2);
            this.Header.Controls.Add(this.btnFermer);
            this.Header.Controls.Add(this.lblTitre);
            this.Header.Location = new System.Drawing.Point(0, 4);
            this.Header.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(613, 42);
            this.Header.TabIndex = 128;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Quitaye_School.Properties.Resources.Logopit_1584731094407;
            this.pictureBox2.Location = new System.Drawing.Point(16, 5);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(40, 37);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 220;
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
            this.btnFermer.Location = new System.Drawing.Point(569, 7);
            this.btnFermer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnFermer.Name = "btnFermer";
            this.btnFermer.Size = new System.Drawing.Size(33, 31);
            this.btnFermer.TabIndex = 204;
            this.btnFermer.TabStop = false;
            this.btnFermer.Click += new System.EventHandler(this.btnFermer_Click);
            // 
            // bunifuDragControl1
            // 
            this.bunifuDragControl1.Fixed = true;
            this.bunifuDragControl1.Horizontal = true;
            this.bunifuDragControl1.TargetControl = this.Header;
            this.bunifuDragControl1.Vertical = true;
            // 
            // lblMessage
            // 
            this.lblMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMessage.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.LightBlue;
            this.lblMessage.Location = new System.Drawing.Point(124, 59);
            this.lblMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(473, 156);
            this.lblMessage.TabIndex = 122;
            this.lblMessage.Text = "Titre  ";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(613, 2);
            this.panel2.TabIndex = 126;
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 0;
            this.bunifuElipse1.TargetControl = this;
            // 
            // btnIcon
            // 
            this.btnIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.btnIcon.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnIcon.IconChar = FontAwesome.Sharp.IconChar.User;
            this.btnIcon.IconColor = System.Drawing.Color.Gainsboro;
            this.btnIcon.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnIcon.IconSize = 62;
            this.btnIcon.Location = new System.Drawing.Point(49, 123);
            this.btnIcon.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnIcon.Name = "btnIcon";
            this.btnIcon.Size = new System.Drawing.Size(67, 62);
            this.btnIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnIcon.TabIndex = 196;
            this.btnIcon.TabStop = false;
            // 
            // btnOk
            // 
            this.btnOk.FlatAppearance.BorderSize = 0;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnOk.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnOk.IconColor = System.Drawing.Color.Gainsboro;
            this.btnOk.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnOk.IconSize = 5;
            this.btnOk.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOk.Location = new System.Drawing.Point(272, 219);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Padding = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.btnOk.Size = new System.Drawing.Size(71, 43);
            this.btnOk.TabIndex = 121;
            this.btnOk.Text = "OK";
            this.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // btnNon
            // 
            this.btnNon.FlatAppearance.BorderSize = 0;
            this.btnNon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNon.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNon.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnNon.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnNon.IconColor = System.Drawing.Color.Gainsboro;
            this.btnNon.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnNon.IconSize = 5;
            this.btnNon.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNon.Location = new System.Drawing.Point(485, 219);
            this.btnNon.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnNon.Name = "btnNon";
            this.btnNon.Padding = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.btnNon.Size = new System.Drawing.Size(83, 43);
            this.btnNon.TabIndex = 119;
            this.btnNon.Text = "Non";
            this.btnNon.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnNon.UseVisualStyleBackColor = true;
            this.btnNon.Click += new System.EventHandler(this.BtnNon_Click);
            // 
            // btnOui
            // 
            this.btnOui.FlatAppearance.BorderSize = 0;
            this.btnOui.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOui.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOui.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnOui.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnOui.IconColor = System.Drawing.Color.Gainsboro;
            this.btnOui.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnOui.IconSize = 5;
            this.btnOui.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOui.Location = new System.Drawing.Point(407, 219);
            this.btnOui.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOui.Name = "btnOui";
            this.btnOui.Padding = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.btnOui.Size = new System.Drawing.Size(71, 43);
            this.btnOui.TabIndex = 120;
            this.btnOui.Text = "Oui";
            this.btnOui.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnOui.UseVisualStyleBackColor = true;
            this.btnOui.Click += new System.EventHandler(this.BtnOui_Click);
            // 
            // MsgBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.ClientSize = new System.Drawing.Size(613, 278);
            this.Controls.Add(this.btnIcon);
            this.Controls.Add(this.bunifuSeparator1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Header);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnNon);
            this.Controls.Add(this.btnOui);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MsgBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MsgBox";
            this.panel4.ResumeLayout(false);
            this.Header.ResumeLayout(false);
            this.Header.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFermer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Bunifu.Framework.UI.BunifuSeparator bunifuSeparator1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTitre;
        private System.Windows.Forms.Panel Header;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
        private FontAwesome.Sharp.IconButton btnOk;
        private FontAwesome.Sharp.IconButton btnNon;
        private FontAwesome.Sharp.IconButton btnOui;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Panel panel2;
        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private FontAwesome.Sharp.IconPictureBox btnFermer;
        public FontAwesome.Sharp.IconPictureBox btnIcon;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}