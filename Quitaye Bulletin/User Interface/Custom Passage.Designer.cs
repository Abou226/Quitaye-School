namespace Quitaye_School.User_Interface
{
    partial class Custom_Passage
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblNom = new System.Windows.Forms.Label();
            this.lblMaticule = new System.Windows.Forms.Label();
            this.lblClasse = new System.Windows.Forms.Label();
            this.check = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblNom
            // 
            this.lblNom.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblNom.AutoSize = true;
            this.lblNom.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNom.ForeColor = System.Drawing.Color.LightBlue;
            this.lblNom.Location = new System.Drawing.Point(207, 8);
            this.lblNom.Name = "lblNom";
            this.lblNom.Size = new System.Drawing.Size(43, 17);
            this.lblNom.TabIndex = 14;
            this.lblNom.Text = "label1";
            // 
            // lblMaticule
            // 
            this.lblMaticule.AutoSize = true;
            this.lblMaticule.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaticule.ForeColor = System.Drawing.Color.LightBlue;
            this.lblMaticule.Location = new System.Drawing.Point(10, 8);
            this.lblMaticule.Name = "lblMaticule";
            this.lblMaticule.Size = new System.Drawing.Size(83, 17);
            this.lblMaticule.TabIndex = 15;
            this.lblMaticule.Text = "Radio Button";
            // 
            // lblClasse
            // 
            this.lblClasse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblClasse.AutoSize = true;
            this.lblClasse.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClasse.ForeColor = System.Drawing.Color.LightBlue;
            this.lblClasse.Location = new System.Drawing.Point(431, 8);
            this.lblClasse.Name = "lblClasse";
            this.lblClasse.Size = new System.Drawing.Size(52, 17);
            this.lblClasse.TabIndex = 13;
            this.lblClasse.Text = "Classe :";
            // 
            // check
            // 
            this.check.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.check.AutoSize = true;
            this.check.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check.ForeColor = System.Drawing.Color.LightBlue;
            this.check.Location = new System.Drawing.Point(604, 7);
            this.check.Name = "check";
            this.check.Size = new System.Drawing.Size(78, 21);
            this.check.TabIndex = 16;
            this.check.Text = "Admis(e)";
            this.check.UseVisualStyleBackColor = true;
            // 
            // Custom_Passage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.Controls.Add(this.check);
            this.Controls.Add(this.lblClasse);
            this.Controls.Add(this.lblNom);
            this.Controls.Add(this.lblMaticule);
            this.Name = "Custom_Passage";
            this.Size = new System.Drawing.Size(704, 35);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Label lblNom;
        public System.Windows.Forms.Label lblMaticule;
        public System.Windows.Forms.Label lblClasse;
        private System.Windows.Forms.CheckBox check;
    }
}
