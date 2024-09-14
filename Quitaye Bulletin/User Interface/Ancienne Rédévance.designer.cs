
namespace Quitaye_School.User_Interface
{
    partial class Ancienne_Rédévance
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
            panel2 = new System.Windows.Forms.Panel();
            panel4 = new System.Windows.Forms.Panel();
            panel3 = new System.Windows.Forms.Panel();
            panel1 = new System.Windows.Forms.Panel();
            btnFermer = new FontAwesome.Sharp.IconPictureBox();
            bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(components);
            label1 = new System.Windows.Forms.Label();
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            rvous = new System.Windows.Forms.RadioButton();
            rfournisseur = new System.Windows.Forms.RadioButton();
            label2 = new System.Windows.Forms.Label();
            txtmontant = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            btnEnregistrer = new FontAwesome.Sharp.IconButton();
            ((System.ComponentModel.ISupportInitialize)(btnFermer)).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.BackColor = System.Drawing.Color.DarkTurquoise;
            panel2.Dock = System.Windows.Forms.DockStyle.Top;
            panel2.Location = new System.Drawing.Point(2, 0);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(347, 2);
            panel2.TabIndex = 287;
            // 
            // panel4
            // 
            panel4.BackColor = System.Drawing.Color.DarkTurquoise;
            panel4.Dock = System.Windows.Forms.DockStyle.Right;
            panel4.Location = new System.Drawing.Point(349, 0);
            panel4.Name = "panel4";
            panel4.Size = new System.Drawing.Size(2, 235);
            panel4.TabIndex = 286;
            // 
            // panel3
            // 
            panel3.BackColor = System.Drawing.Color.DarkTurquoise;
            panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            panel3.Location = new System.Drawing.Point(2, 235);
            panel3.Name = "panel3";
            panel3.Size = new System.Drawing.Size(349, 2);
            panel3.TabIndex = 288;
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.Color.DarkTurquoise;
            panel1.Dock = System.Windows.Forms.DockStyle.Left;
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(2, 237);
            panel1.TabIndex = 285;
            // 
            // btnFermer
            // 
            btnFermer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            btnFermer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            btnFermer.ForeColor = System.Drawing.Color.LightBlue;
            btnFermer.IconChar = FontAwesome.Sharp.IconChar.WindowClose;
            btnFermer.IconColor = System.Drawing.Color.LightBlue;
            btnFermer.IconSize = 25;
            btnFermer.Location = new System.Drawing.Point(318, 8);
            btnFermer.Name = "btnFermer";
            btnFermer.Size = new System.Drawing.Size(25, 25);
            btnFermer.TabIndex = 289;
            btnFermer.TabStop = false;
            // 
            // bunifuDragControl1
            // 
            bunifuDragControl1.Fixed = true;
            bunifuDragControl1.Horizontal = true;
            bunifuDragControl1.TargetControl = this;
            bunifuDragControl1.Vertical = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = System.Drawing.Color.Transparent;
            label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label1.ForeColor = System.Drawing.Color.LightBlue;
            label1.Location = new System.Drawing.Point(9, 71);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(70, 17);
            label1.TabIndex = 290;
            label1.Text = "Rédeveur :";
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(rvous);
            flowLayoutPanel1.Controls.Add(rfournisseur);
            flowLayoutPanel1.Location = new System.Drawing.Point(84, 68);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new System.Drawing.Size(259, 27);
            flowLayoutPanel1.TabIndex = 322;
            // 
            // rvous
            // 
            rvous.AutoSize = true;
            rvous.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            rvous.ForeColor = System.Drawing.Color.LightBlue;
            rvous.Location = new System.Drawing.Point(20, 3);
            rvous.Margin = new System.Windows.Forms.Padding(20, 3, 3, 3);
            rvous.Name = "rvous";
            rvous.Size = new System.Drawing.Size(54, 21);
            rvous.TabIndex = 299;
            rvous.TabStop = true;
            rvous.Text = "Vous";
            rvous.UseVisualStyleBackColor = true;
            // 
            // rfournisseur
            // 
            rfournisseur.AutoSize = true;
            rfournisseur.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            rfournisseur.ForeColor = System.Drawing.Color.LightBlue;
            rfournisseur.Location = new System.Drawing.Point(97, 3);
            rfournisseur.Margin = new System.Windows.Forms.Padding(20, 3, 3, 3);
            rfournisseur.Name = "rfournisseur";
            rfournisseur.Size = new System.Drawing.Size(66, 21);
            rfournisseur.TabIndex = 299;
            rfournisseur.TabStop = true;
            rfournisseur.Text = "Moyen";
            rfournisseur.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = System.Drawing.Color.Transparent;
            label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label2.ForeColor = System.Drawing.Color.LightBlue;
            label2.Location = new System.Drawing.Point(7, 125);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(64, 17);
            label2.TabIndex = 290;
            label2.Text = "Montant :";
            // 
            // txtmontant
            // 
            txtmontant.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            txtmontant.Location = new System.Drawing.Point(84, 119);
            txtmontant.Name = "txtmontant";
            txtmontant.Size = new System.Drawing.Size(121, 29);
            txtmontant.TabIndex = 323;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = System.Drawing.Color.Transparent;
            label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label3.ForeColor = System.Drawing.Color.LightBlue;
            label3.Location = new System.Drawing.Point(140, 10);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(71, 17);
            label3.TabIndex = 324;
            label3.Text = "Rédevance";
            // 
            // btnEnregistrer
            // 
            btnEnregistrer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            btnEnregistrer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            btnEnregistrer.FlatAppearance.BorderSize = 0;
            btnEnregistrer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnEnregistrer.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            btnEnregistrer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            btnEnregistrer.ForeColor = System.Drawing.Color.LightBlue;
            btnEnregistrer.IconChar = FontAwesome.Sharp.IconChar.Save;
            btnEnregistrer.IconColor = System.Drawing.Color.LightBlue;
            btnEnregistrer.IconSize = 26;
            btnEnregistrer.Location = new System.Drawing.Point(223, 191);
            btnEnregistrer.Name = "btnEnregistrer";
            btnEnregistrer.Rotation = 0D;
            btnEnregistrer.Size = new System.Drawing.Size(116, 34);
            btnEnregistrer.TabIndex = 325;
            btnEnregistrer.Text = "Enregistrer";
            btnEnregistrer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnEnregistrer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnEnregistrer.UseVisualStyleBackColor = false;
            // 
            // Ancienne_Rédévance
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            ClientSize = new System.Drawing.Size(351, 237);
            Controls.Add(btnEnregistrer);
            Controls.Add(label3);
            Controls.Add(txtmontant);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnFermer);
            Controls.Add(panel2);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "Ancienne_Rédévance";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Ancienne Rédévance";
            ((System.ComponentModel.ISupportInitialize)(btnFermer)).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        private FontAwesome.Sharp.IconPictureBox btnFermer;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.RadioButton rvous;
        private System.Windows.Forms.RadioButton rfournisseur;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtmontant;
        private FontAwesome.Sharp.IconButton btnEnregistrer;
    }
}