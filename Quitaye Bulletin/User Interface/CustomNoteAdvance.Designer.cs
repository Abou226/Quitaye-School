namespace Quitaye_School.User_Interface
{
    partial class CustomNoteAdvance
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
            this.txtClasse = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblNom = new System.Windows.Forms.Label();
            this.lblMaticule = new System.Windows.Forms.Label();
            this.txtCompo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtClasse
            // 
            this.txtClasse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtClasse.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClasse.ForeColor = System.Drawing.Color.Silver;
            this.txtClasse.Location = new System.Drawing.Point(491, 5);
            this.txtClasse.Name = "txtClasse";
            this.txtClasse.Size = new System.Drawing.Size(100, 25);
            this.txtClasse.TabIndex = 1;
            this.txtClasse.Text = "Classe/20";
            this.txtClasse.Click += new System.EventHandler(this.txtClasse_Click);
            this.txtClasse.TextChanged += new System.EventHandler(this.txtClasse_TextChanged);
            this.txtClasse.MouseEnter += new System.EventHandler(this.PictureBox1_MouseEnter);
            this.txtClasse.MouseLeave += new System.EventHandler(this.PictureBox1_MouseLeave);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.LightBlue;
            this.label1.Location = new System.Drawing.Point(416, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "Note :";
            this.label1.MouseEnter += new System.EventHandler(this.PictureBox1_MouseEnter);
            this.label1.MouseLeave += new System.EventHandler(this.PictureBox1_MouseLeave);
            // 
            // lblNom
            // 
            this.lblNom.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblNom.AutoSize = true;
            this.lblNom.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNom.ForeColor = System.Drawing.Color.LightBlue;
            this.lblNom.Location = new System.Drawing.Point(213, 9);
            this.lblNom.Name = "lblNom";
            this.lblNom.Size = new System.Drawing.Size(43, 17);
            this.lblNom.TabIndex = 9;
            this.lblNom.Text = "label1";
            this.lblNom.MouseEnter += new System.EventHandler(this.PictureBox1_MouseEnter);
            this.lblNom.MouseLeave += new System.EventHandler(this.PictureBox1_MouseLeave);
            // 
            // lblMaticule
            // 
            this.lblMaticule.AutoSize = true;
            this.lblMaticule.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaticule.ForeColor = System.Drawing.Color.LightBlue;
            this.lblMaticule.Location = new System.Drawing.Point(7, 8);
            this.lblMaticule.Name = "lblMaticule";
            this.lblMaticule.Size = new System.Drawing.Size(83, 17);
            this.lblMaticule.TabIndex = 10;
            this.lblMaticule.Text = "Radio Button";
            this.lblMaticule.MouseEnter += new System.EventHandler(this.PictureBox1_MouseEnter);
            this.lblMaticule.MouseLeave += new System.EventHandler(this.PictureBox1_MouseLeave);
            // 
            // txtCompo
            // 
            this.txtCompo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCompo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCompo.ForeColor = System.Drawing.Color.Silver;
            this.txtCompo.Location = new System.Drawing.Point(611, 5);
            this.txtCompo.Name = "txtCompo";
            this.txtCompo.Size = new System.Drawing.Size(100, 25);
            this.txtCompo.TabIndex = 2;
            this.txtCompo.Text = "Compo/40";
            this.txtCompo.Click += new System.EventHandler(this.txtCompo_Click);
            this.txtCompo.TextChanged += new System.EventHandler(this.txtCompo_TextChanged);
            this.txtCompo.MouseEnter += new System.EventHandler(this.PictureBox1_MouseEnter);
            this.txtCompo.MouseLeave += new System.EventHandler(this.PictureBox1_MouseLeave);
            // 
            // CustomNoteAdvance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.Controls.Add(this.txtCompo);
            this.Controls.Add(this.txtClasse);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblNom);
            this.Controls.Add(this.lblMaticule);
            this.Name = "CustomNoteAdvance";
            this.Size = new System.Drawing.Size(724, 35);
            this.MouseEnter += new System.EventHandler(this.PictureBox1_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.PictureBox1_MouseLeave);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox txtClasse;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label lblNom;
        public System.Windows.Forms.Label lblMaticule;
        public System.Windows.Forms.TextBox txtCompo;
    }
}
