namespace Quitaye_School.User_Interface
{
    partial class PopVente
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopVente));
            panel4 = new System.Windows.Forms.Panel();
            panel3 = new System.Windows.Forms.Panel();
            panel2 = new System.Windows.Forms.Panel();
            panel1 = new System.Windows.Forms.Panel();
            bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(components);
            bunifuDragControl2 = new Bunifu.Framework.UI.BunifuDragControl(components);
            lblEtat = new System.Windows.Forms.Label();
            btnValidé = new FontAwesome.Sharp.IconButton();
            btnFermer = new FontAwesome.Sharp.IconPictureBox();
            venteInfo1 = new Quitaye_School.User_Interface.VenteInfo();
            ((System.ComponentModel.ISupportInitialize)(btnFermer)).BeginInit();
            SuspendLayout();
            // 
            // panel4
            // 
            panel4.BackColor = System.Drawing.Color.DarkTurquoise;
            panel4.Dock = System.Windows.Forms.DockStyle.Right;
            panel4.Location = new System.Drawing.Point(951, 2);
            panel4.Name = "panel4";
            panel4.Size = new System.Drawing.Size(2, 261);
            panel4.TabIndex = 233;
            panel4.Visible = false;
            // 
            // panel3
            // 
            panel3.BackColor = System.Drawing.Color.DarkTurquoise;
            panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            panel3.Location = new System.Drawing.Point(2, 263);
            panel3.Name = "panel3";
            panel3.Size = new System.Drawing.Size(951, 2);
            panel3.TabIndex = 235;
            panel3.Visible = false;
            // 
            // panel2
            // 
            panel2.BackColor = System.Drawing.Color.DarkTurquoise;
            panel2.Dock = System.Windows.Forms.DockStyle.Top;
            panel2.Location = new System.Drawing.Point(2, 0);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(951, 2);
            panel2.TabIndex = 234;
            panel2.Visible = false;
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.Color.DarkTurquoise;
            panel1.Dock = System.Windows.Forms.DockStyle.Left;
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(2, 265);
            panel1.TabIndex = 232;
            panel1.Visible = false;
            // 
            // bunifuDragControl1
            // 
            bunifuDragControl1.Fixed = true;
            bunifuDragControl1.Horizontal = true;
            bunifuDragControl1.TargetControl = null;
            bunifuDragControl1.Vertical = true;
            // 
            // bunifuDragControl2
            // 
            bunifuDragControl2.Fixed = true;
            bunifuDragControl2.Horizontal = true;
            bunifuDragControl2.TargetControl = this;
            bunifuDragControl2.Vertical = true;
            // 
            // lblEtat
            // 
            lblEtat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            lblEtat.AutoSize = true;
            lblEtat.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblEtat.ForeColor = System.Drawing.Color.LightBlue;
            lblEtat.Location = new System.Drawing.Point(780, 12);
            lblEtat.Name = "lblEtat";
            lblEtat.Size = new System.Drawing.Size(37, 17);
            lblEtat.TabIndex = 239;
            lblEtat.Text = "Etat :";
            // 
            // btnValidé
            // 
            btnValidé.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            btnValidé.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            btnValidé.FlatAppearance.BorderSize = 0;
            btnValidé.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnValidé.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            btnValidé.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            btnValidé.ForeColor = System.Drawing.Color.LightBlue;
            btnValidé.IconChar = FontAwesome.Sharp.IconChar.Check;
            btnValidé.IconColor = System.Drawing.Color.LightBlue;
            btnValidé.IconSize = 32;
            btnValidé.Location = new System.Drawing.Point(12, 12);
            btnValidé.Name = "btnValidé";
            btnValidé.Rotation = 0D;
            btnValidé.Size = new System.Drawing.Size(158, 33);
            btnValidé.TabIndex = 238;
            btnValidé.Text = "Validé Vente";
            btnValidé.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnValidé.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnValidé.UseVisualStyleBackColor = false;
            btnValidé.Visible = false;
            btnValidé.Click += btnValidé_Click;
            // 
            // btnFermer
            // 
            btnFermer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            btnFermer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            btnFermer.ForeColor = System.Drawing.Color.LightBlue;
            btnFermer.IconChar = FontAwesome.Sharp.IconChar.WindowClose;
            btnFermer.IconColor = System.Drawing.Color.LightBlue;
            btnFermer.IconSize = 25;
            btnFermer.Location = new System.Drawing.Point(919, 9);
            btnFermer.Name = "btnFermer";
            btnFermer.Size = new System.Drawing.Size(25, 25);
            btnFermer.TabIndex = 231;
            btnFermer.TabStop = false;
            btnFermer.Click += btnFermer_Click_1;
            // 
            // venteInfo1
            // 
            venteInfo1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            venteInfo1.Catégorie = null;
            venteInfo1.Client = null;
            venteInfo1.Code_Barre = null;
            venteInfo1.Contact = null;
            venteInfo1.Icon = null;
            venteInfo1.Location = new System.Drawing.Point(12, 55);
            venteInfo1.Montant = new decimal(new int[] {
            0,
            0,
            0,
            0});
            venteInfo1.Name = "venteInfo1";
            venteInfo1.Prix_Unité = new decimal(new int[] {
            0,
            0,
            0,
            0});
            venteInfo1.Quantité = new decimal(new int[] {
            0,
            0,
            0,
            0});
            venteInfo1.Ref = new decimal(new int[] {
            0,
            0,
            0,
            0});
            venteInfo1.Size = new System.Drawing.Size(928, 201);
            venteInfo1.TabIndex = 237;
            venteInfo1.Taille = null;
            venteInfo1.Titre = null;
            venteInfo1.Usage = null;
            // 
            // PopVente
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            ClientSize = new System.Drawing.Size(953, 265);
            Controls.Add(lblEtat);
            Controls.Add(panel4);
            Controls.Add(btnValidé);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(btnFermer);
            Controls.Add(venteInfo1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Icon = ((System.Drawing.Icon)(resources.GetObject("$Icon")));
            Name = "PopVente";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "PopVente";
            ((System.ComponentModel.ISupportInitialize)(btnFermer)).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Panel panel4;
        public System.Windows.Forms.Panel panel3;
        public System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Panel panel1;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl2;
        private FontAwesome.Sharp.IconPictureBox btnFermer;
        public VenteInfo venteInfo1;
        public System.Windows.Forms.Label lblEtat;
        public FontAwesome.Sharp.IconButton btnValidé;
    }
}