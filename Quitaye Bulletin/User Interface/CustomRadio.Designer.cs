namespace Quitaye_School.User_Interface
{
    partial class CustomRadio
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
            this.lblMontant = new System.Windows.Forms.Label();
            this.radioButton = new System.Windows.Forms.RadioButton();
            this.lblRadio = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblMontant
            // 
            this.lblMontant.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMontant.AutoSize = true;
            this.lblMontant.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMontant.ForeColor = System.Drawing.Color.LightBlue;
            this.lblMontant.Location = new System.Drawing.Point(219, 9);
            this.lblMontant.Name = "lblMontant";
            this.lblMontant.Size = new System.Drawing.Size(43, 17);
            this.lblMontant.TabIndex = 3;
            this.lblMontant.Text = "label1";
            this.lblMontant.Click += new System.EventHandler(this.CustomRadio_Click);
            // 
            // radioButton
            // 
            this.radioButton.AutoSize = true;
            this.radioButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton.ForeColor = System.Drawing.Color.LightBlue;
            this.radioButton.Location = new System.Drawing.Point(20, 10);
            this.radioButton.Name = "radioButton";
            this.radioButton.Size = new System.Drawing.Size(14, 13);
            this.radioButton.TabIndex = 2;
            this.radioButton.TabStop = true;
            this.radioButton.UseVisualStyleBackColor = true;
            this.radioButton.Click += new System.EventHandler(this.CustomRadio_Click);
            // 
            // lblRadio
            // 
            this.lblRadio.AutoSize = true;
            this.lblRadio.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRadio.ForeColor = System.Drawing.Color.LightBlue;
            this.lblRadio.Location = new System.Drawing.Point(40, 8);
            this.lblRadio.Name = "lblRadio";
            this.lblRadio.Size = new System.Drawing.Size(83, 17);
            this.lblRadio.TabIndex = 4;
            this.lblRadio.Text = "Radio Button";
            this.lblRadio.Click += new System.EventHandler(this.CustomRadio_Click);
            // 
            // CustomRadio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.Controls.Add(this.lblMontant);
            this.Controls.Add(this.radioButton);
            this.Controls.Add(this.lblRadio);
            this.Name = "CustomRadio";
            this.Size = new System.Drawing.Size(283, 35);
            this.Click += new System.EventHandler(this.CustomRadio_Click);
            this.MouseEnter += new System.EventHandler(this.PictureBox1_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.PictureBox1_MouseLeave);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lblMontant;
        public System.Windows.Forms.RadioButton radioButton;
        public System.Windows.Forms.Label lblRadio;
    }
}
