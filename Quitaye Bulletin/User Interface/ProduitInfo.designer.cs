namespace Quitaye_School.User_Interface
{
    partial class ProduitInfo
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProduitInfo));
            lblStkmax = new System.Windows.Forms.Label();
            lblStkmin = new System.Windows.Forms.Label();
            lblMontant = new System.Windows.Forms.Label();
            lblPrix = new System.Windows.Forms.Label();
            lblRef = new System.Windows.Forms.Label();
            dataGridView1 = new System.Windows.Forms.DataGridView();
            bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(components);
            bunifuGradientPanel3 = new Bunifu.Framework.UI.BunifuGradientPanel();
            lblCatégorie = new System.Windows.Forms.Label();
            lblTitre = new System.Windows.Forms.Label();
            lblTaille = new System.Windows.Forms.Label();
            lblUsage = new System.Windows.Forms.Label();
            panel2 = new System.Windows.Forms.Panel();
            panel3 = new System.Windows.Forms.Panel();
            panel1 = new System.Windows.Forms.Panel();
            lblQuantité = new System.Windows.Forms.Label();
            btnImage = new FontAwesome.Sharp.IconPictureBox();
            ((System.ComponentModel.ISupportInitialize)(dataGridView1)).BeginInit();
            bunifuGradientPanel3.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(btnImage)).BeginInit();
            SuspendLayout();
            // 
            // lblStkmax
            // 
            lblStkmax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            lblStkmax.BackColor = System.Drawing.Color.Transparent;
            lblStkmax.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblStkmax.ForeColor = System.Drawing.Color.Aqua;
            lblStkmax.Location = new System.Drawing.Point(690, 163);
            lblStkmax.Name = "lblStkmax";
            lblStkmax.Size = new System.Drawing.Size(238, 29);
            lblStkmax.TabIndex = 133;
            lblStkmax.Text = "label1";
            // 
            // lblStkmin
            // 
            lblStkmin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            lblStkmin.BackColor = System.Drawing.Color.Transparent;
            lblStkmin.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblStkmin.ForeColor = System.Drawing.Color.Aqua;
            lblStkmin.Location = new System.Drawing.Point(688, 124);
            lblStkmin.Name = "lblStkmin";
            lblStkmin.Size = new System.Drawing.Size(238, 39);
            lblStkmin.TabIndex = 134;
            lblStkmin.Text = "label1";
            // 
            // lblMontant
            // 
            lblMontant.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            lblMontant.BackColor = System.Drawing.Color.Transparent;
            lblMontant.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblMontant.ForeColor = System.Drawing.Color.Aqua;
            lblMontant.Location = new System.Drawing.Point(689, 84);
            lblMontant.Name = "lblMontant";
            lblMontant.Size = new System.Drawing.Size(237, 40);
            lblMontant.TabIndex = 135;
            lblMontant.Text = "label1";
            // 
            // lblPrix
            // 
            lblPrix.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            lblPrix.BackColor = System.Drawing.Color.Transparent;
            lblPrix.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblPrix.ForeColor = System.Drawing.Color.Aqua;
            lblPrix.Location = new System.Drawing.Point(689, 45);
            lblPrix.Name = "lblPrix";
            lblPrix.Size = new System.Drawing.Size(237, 39);
            lblPrix.TabIndex = 136;
            lblPrix.Text = "label1";
            // 
            // lblRef
            // 
            lblRef.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            lblRef.BackColor = System.Drawing.Color.Transparent;
            lblRef.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblRef.ForeColor = System.Drawing.Color.Aqua;
            lblRef.Location = new System.Drawing.Point(560, 0);
            lblRef.Name = "lblRef";
            lblRef.Size = new System.Drawing.Size(119, 21);
            lblRef.TabIndex = 137;
            lblRef.Text = "label1";
            lblRef.Visible = false;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new System.Drawing.Point(297, 4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.Size = new System.Drawing.Size(240, 11);
            dataGridView1.TabIndex = 142;
            dataGridView1.Visible = false;
            // 
            // bunifuElipse1
            // 
            bunifuElipse1.ElipseRadius = 20;
            bunifuElipse1.TargetControl = bunifuGradientPanel3;
            // 
            // bunifuGradientPanel3
            // 
            bunifuGradientPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            bunifuGradientPanel3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bunifuGradientPanel3.BackgroundImage")));
            bunifuGradientPanel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            bunifuGradientPanel3.Controls.Add(lblCatégorie);
            bunifuGradientPanel3.Controls.Add(lblTitre);
            bunifuGradientPanel3.Controls.Add(lblTaille);
            bunifuGradientPanel3.Controls.Add(lblUsage);
            bunifuGradientPanel3.GradientBottomLeft = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            bunifuGradientPanel3.GradientBottomRight = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            bunifuGradientPanel3.GradientTopLeft = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            bunifuGradientPanel3.GradientTopRight = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(21)))), ((int)(((byte)(234)))));
            bunifuGradientPanel3.Location = new System.Drawing.Point(213, 21);
            bunifuGradientPanel3.Name = "bunifuGradientPanel3";
            bunifuGradientPanel3.Quality = 10;
            bunifuGradientPanel3.Size = new System.Drawing.Size(460, 169);
            bunifuGradientPanel3.TabIndex = 143;
            // 
            // lblCatégorie
            // 
            lblCatégorie.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            lblCatégorie.BackColor = System.Drawing.Color.Transparent;
            lblCatégorie.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblCatégorie.ForeColor = System.Drawing.Color.LightBlue;
            lblCatégorie.Location = new System.Drawing.Point(2, 63);
            lblCatégorie.Name = "lblCatégorie";
            lblCatégorie.Size = new System.Drawing.Size(458, 56);
            lblCatégorie.TabIndex = 24;
            lblCatégorie.Text = "label2";
            // 
            // lblTitre
            // 
            lblTitre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            lblTitre.BackColor = System.Drawing.Color.Transparent;
            lblTitre.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblTitre.ForeColor = System.Drawing.Color.Aqua;
            lblTitre.Location = new System.Drawing.Point(2, 0);
            lblTitre.Name = "lblTitre";
            lblTitre.Size = new System.Drawing.Size(458, 63);
            lblTitre.TabIndex = 23;
            lblTitre.Text = "label1";
            // 
            // lblTaille
            // 
            lblTaille.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            lblTaille.BackColor = System.Drawing.Color.Transparent;
            lblTaille.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblTaille.ForeColor = System.Drawing.Color.Aqua;
            lblTaille.Location = new System.Drawing.Point(292, 119);
            lblTaille.Name = "lblTaille";
            lblTaille.Size = new System.Drawing.Size(165, 43);
            lblTaille.TabIndex = 22;
            lblTaille.Text = "label1";
            // 
            // lblUsage
            // 
            lblUsage.BackColor = System.Drawing.Color.Transparent;
            lblUsage.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblUsage.ForeColor = System.Drawing.Color.DarkTurquoise;
            lblUsage.Location = new System.Drawing.Point(3, 119);
            lblUsage.Name = "lblUsage";
            lblUsage.Size = new System.Drawing.Size(242, 48);
            lblUsage.TabIndex = 22;
            lblUsage.Text = "label1";
            // 
            // panel2
            // 
            panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            panel2.BackColor = System.Drawing.Color.DarkBlue;
            panel2.Location = new System.Drawing.Point(0, 198);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(928, 3);
            panel2.TabIndex = 140;
            // 
            // panel3
            // 
            panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            panel3.BackColor = System.Drawing.Color.DarkGray;
            panel3.Location = new System.Drawing.Point(679, 0);
            panel3.Name = "panel3";
            panel3.Size = new System.Drawing.Size(3, 201);
            panel3.TabIndex = 141;
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.Color.DarkBlue;
            panel1.Controls.Add(btnImage);
            panel1.Location = new System.Drawing.Point(7, 7);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(200, 185);
            panel1.TabIndex = 139;
            // 
            // lblQuantité
            // 
            lblQuantité.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            lblQuantité.BackColor = System.Drawing.Color.Transparent;
            lblQuantité.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblQuantité.ForeColor = System.Drawing.Color.Aqua;
            lblQuantité.Location = new System.Drawing.Point(690, 8);
            lblQuantité.Name = "lblQuantité";
            lblQuantité.Size = new System.Drawing.Size(236, 37);
            lblQuantité.TabIndex = 138;
            lblQuantité.Text = "label1";
            // 
            // btnImage
            // 
            btnImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            btnImage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            btnImage.ForeColor = System.Drawing.Color.LightBlue;
            btnImage.IconChar = FontAwesome.Sharp.IconChar.User;
            btnImage.IconColor = System.Drawing.Color.LightBlue;
            btnImage.IconSize = 176;
            btnImage.Location = new System.Drawing.Point(3, 4);
            btnImage.Name = "btnImage";
            btnImage.Size = new System.Drawing.Size(195, 176);
            btnImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            btnImage.TabIndex = 323;
            btnImage.TabStop = false;
            // 
            // ProduitInfo
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            Controls.Add(lblStkmax);
            Controls.Add(lblStkmin);
            Controls.Add(lblMontant);
            Controls.Add(lblPrix);
            Controls.Add(lblRef);
            Controls.Add(dataGridView1);
            Controls.Add(bunifuGradientPanel3);
            Controls.Add(panel2);
            Controls.Add(panel3);
            Controls.Add(panel1);
            Controls.Add(lblQuantité);
            Name = "ProduitInfo";
            Size = new System.Drawing.Size(928, 201);
            ((System.ComponentModel.ISupportInitialize)(dataGridView1)).EndInit();
            bunifuGradientPanel3.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(btnImage)).EndInit();
            ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lblStkmax;
        public System.Windows.Forms.Label lblStkmin;
        public System.Windows.Forms.Label lblMontant;
        public System.Windows.Forms.Label lblPrix;
        private System.Windows.Forms.Label lblRef;
        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lblCatégorie;
        private System.Windows.Forms.Label lblTitre;
        public System.Windows.Forms.Label lblTaille;
        private System.Windows.Forms.Label lblUsage;
        private Bunifu.Framework.UI.BunifuGradientPanel bunifuGradientPanel3;
        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblQuantité;
        public FontAwesome.Sharp.IconPictureBox btnImage;
    }
}
