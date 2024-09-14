
namespace Quitaye_School.User_Interface
{
    partial class Comparaison_Mesure
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
            lblMesure = new System.Windows.Forms.Label();
            txtsearch = new System.Windows.Forms.TextBox();
            SuspendLayout();
            // 
            // lblMesure
            // 
            lblMesure.AutoSize = true;
            lblMesure.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblMesure.ForeColor = System.Drawing.Color.LightBlue;
            lblMesure.Location = new System.Drawing.Point(3, 8);
            lblMesure.Name = "lblMesure";
            lblMesure.Size = new System.Drawing.Size(57, 17);
            lblMesure.TabIndex = 109;
            lblMesure.Text = "Produit :";
            // 
            // txtsearch
            // 
            txtsearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            txtsearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            txtsearch.Location = new System.Drawing.Point(114, 3);
            txtsearch.Name = "txtsearch";
            txtsearch.Size = new System.Drawing.Size(87, 25);
            txtsearch.TabIndex = 108;
            // 
            // Comparaison_Mesure
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            Controls.Add(lblMesure);
            Controls.Add(txtsearch);
            Name = "Comparaison_Mesure";
            Size = new System.Drawing.Size(205, 30);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMesure;
        private System.Windows.Forms.TextBox txtsearch;
    }
}
