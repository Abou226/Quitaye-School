namespace Quitaye_School.User_Interface
{
    partial class FeedBack
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FeedBack));
            this.label7 = new System.Windows.Forms.Label();
            this.txtcommentaire = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.btnEnvoyer = new FontAwesome.Sharp.IconButton();
            this.btnFermer = new FontAwesome.Sharp.IconPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.btnFermer)).BeginInit();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.LightBlue;
            this.label7.Location = new System.Drawing.Point(17, 44);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(123, 23);
            this.label7.TabIndex = 379;
            this.label7.Text = "Commentaire :";
            // 
            // txtcommentaire
            // 
            this.txtcommentaire.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcommentaire.Location = new System.Drawing.Point(20, 96);
            this.txtcommentaire.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtcommentaire.Multiline = true;
            this.txtcommentaire.Name = "txtcommentaire";
            this.txtcommentaire.Size = new System.Drawing.Size(1163, 355);
            this.txtcommentaire.TabIndex = 378;
            this.txtcommentaire.Text = resources.GetString("txtcommentaire.Text");
            this.txtcommentaire.Click += new System.EventHandler(this.txtcommentaire_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(3, 513);
            this.panel1.TabIndex = 373;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(1097, 2);
            this.panel4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(3, 513);
            this.panel4.TabIndex = 374;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 515);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1100, 2);
            this.panel3.TabIndex = 376;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1100, 2);
            this.panel2.TabIndex = 375;
            // 
            // bunifuDragControl1
            // 
            this.bunifuDragControl1.Fixed = true;
            this.bunifuDragControl1.Horizontal = true;
            this.bunifuDragControl1.TargetControl = this;
            this.bunifuDragControl1.Vertical = true;
            // 
            // btnEnvoyer
            // 
            this.btnEnvoyer.FlatAppearance.BorderSize = 0;
            this.btnEnvoyer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnvoyer.ForeColor = System.Drawing.Color.LightBlue;
            this.btnEnvoyer.IconChar = FontAwesome.Sharp.IconChar.PaperPlane;
            this.btnEnvoyer.IconColor = System.Drawing.Color.LightBlue;
            this.btnEnvoyer.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnEnvoyer.IconSize = 32;
            this.btnEnvoyer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEnvoyer.Location = new System.Drawing.Point(1029, 459);
            this.btnEnvoyer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnEnvoyer.Name = "btnEnvoyer";
            this.btnEnvoyer.Padding = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.btnEnvoyer.Size = new System.Drawing.Size(155, 43);
            this.btnEnvoyer.TabIndex = 380;
            this.btnEnvoyer.Text = "Envoyer";
            this.btnEnvoyer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEnvoyer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEnvoyer.UseVisualStyleBackColor = true;
            this.btnEnvoyer.Click += new System.EventHandler(this.btnEnvoyer_Click);
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
            this.btnFermer.Location = new System.Drawing.Point(1051, 11);
            this.btnFermer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnFermer.Name = "btnFermer";
            this.btnFermer.Size = new System.Drawing.Size(33, 31);
            this.btnFermer.TabIndex = 377;
            this.btnFermer.TabStop = false;
            this.btnFermer.Click += new System.EventHandler(this.btnFermer_Click);
            // 
            // FeedBack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.ClientSize = new System.Drawing.Size(1100, 517);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtcommentaire);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnEnvoyer);
            this.Controls.Add(this.btnFermer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FeedBack";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FeedBack";
            ((System.ComponentModel.ISupportInitialize)(this.btnFermer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtcommentaire;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
        private FontAwesome.Sharp.IconButton btnEnvoyer;
        private FontAwesome.Sharp.IconPictureBox btnFermer;
    }
}