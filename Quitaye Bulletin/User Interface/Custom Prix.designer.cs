
namespace Quitaye_School.User_Interface
{
    partial class Custom_Prix
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
            label5 = new System.Windows.Forms.Label();
            txtPrix = new System.Windows.Forms.TextBox();
            SuspendLayout();
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = System.Drawing.Color.Transparent;
            label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label5.ForeColor = System.Drawing.Color.LightBlue;
            label5.Location = new System.Drawing.Point(3, 8);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(70, 17);
            label5.TabIndex = 281;
            label5.Text = "Prix Unité :";
            // 
            // txtPrix
            // 
            txtPrix.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            txtPrix.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            txtPrix.Location = new System.Drawing.Point(126, 3);
            txtPrix.Name = "txtPrix";
            txtPrix.Size = new System.Drawing.Size(203, 25);
            txtPrix.TabIndex = 280;
            txtPrix.Text = "0";
            // 
            // Custom_Prix
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            Controls.Add(label5);
            Controls.Add(txtPrix);
            Name = "Custom_Prix";
            Size = new System.Drawing.Size(332, 30);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox txtPrix;
    }
}
