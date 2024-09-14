namespace Quitaye_School.User_Interface
{
    partial class LogIn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogIn));
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.Header = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnFermer = new FontAwesome.Sharp.IconPictureBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.bunifuElipse2 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.dataGridView6 = new System.Windows.Forms.DataGridView();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.bunifuElipse3 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.lblAvertiss = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.bunifuDragControl2 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.btnConnecter = new FontAwesome.Sharp.IconButton();
            this.btnInscrire = new FontAwesome.Sharp.IconButton();
            this.Header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFermer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView6)).BeginInit();
            this.SuspendLayout();
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 20;
            this.bunifuElipse1.TargetControl = this;
            // 
            // Header
            // 
            this.Header.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(6)))), ((int)(((byte)(74)))));
            this.Header.Controls.Add(this.label1);
            this.Header.Controls.Add(this.pictureBox2);
            this.Header.Controls.Add(this.btnFermer);
            this.Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.Header.Location = new System.Drawing.Point(0, 0);
            this.Header.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(639, 75);
            this.Header.TabIndex = 95;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.LightBlue;
            this.label1.Location = new System.Drawing.Point(248, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 37);
            this.label1.TabIndex = 33;
            this.label1.Text = "Utilisateur";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Quitaye_School.Properties.Resources.Logopit_1584731094407;
            this.pictureBox2.Location = new System.Drawing.Point(16, 7);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(67, 62);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 32;
            this.pictureBox2.TabStop = false;
            // 
            // btnFermer
            // 
            this.btnFermer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFermer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(6)))), ((int)(((byte)(74)))));
            this.btnFermer.ForeColor = System.Drawing.Color.LightBlue;
            this.btnFermer.IconChar = FontAwesome.Sharp.IconChar.RectangleXmark;
            this.btnFermer.IconColor = System.Drawing.Color.LightBlue;
            this.btnFermer.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnFermer.IconSize = 31;
            this.btnFermer.Location = new System.Drawing.Point(589, 14);
            this.btnFermer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnFermer.Name = "btnFermer";
            this.btnFermer.Size = new System.Drawing.Size(33, 31);
            this.btnFermer.TabIndex = 31;
            this.btnFermer.TabStop = false;
            this.toolTip1.SetToolTip(this.btnFermer, "Cliquez pour fermer");
            this.btnFermer.Click += new System.EventHandler(this.btnFermer_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(98)))), ((int)(((byte)(23)))));
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(332, 143);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(175, 20);
            this.dataGridView1.TabIndex = 93;
            this.dataGridView1.Visible = false;
            // 
            // bunifuElipse2
            // 
            this.bunifuElipse2.ElipseRadius = 20;
            this.bunifuElipse2.TargetControl = this;
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(243, 182);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '•';
            this.txtPassword.Size = new System.Drawing.Size(263, 29);
            this.txtPassword.TabIndex = 92;
            this.txtPassword.TextChanged += new System.EventHandler(this.txtUser_TextChanged);
            this.txtPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPassword_KeyPress);
            // 
            // txtUser
            // 
            this.txtUser.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUser.Location = new System.Drawing.Point(243, 132);
            this.txtUser.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(263, 29);
            this.txtUser.TabIndex = 91;
            this.txtUser.TextChanged += new System.EventHandler(this.txtUser_TextChanged);
            this.txtUser.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPassword_KeyPress);
            // 
            // dataGridView6
            // 
            this.dataGridView6.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(98)))), ((int)(((byte)(23)))));
            this.dataGridView6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView6.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView6.Location = new System.Drawing.Point(327, 140);
            this.dataGridView6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView6.Name = "dataGridView6";
            this.dataGridView6.RowHeadersWidth = 51;
            this.dataGridView6.Size = new System.Drawing.Size(77, 12);
            this.dataGridView6.TabIndex = 94;
            // 
            // bunifuElipse3
            // 
            this.bunifuElipse3.ElipseRadius = 20;
            this.bunifuElipse3.TargetControl = this;
            // 
            // bunifuDragControl1
            // 
            this.bunifuDragControl1.Fixed = true;
            this.bunifuDragControl1.Horizontal = true;
            this.bunifuDragControl1.TargetControl = this.Header;
            this.bunifuDragControl1.Vertical = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.LightBlue;
            this.label4.Location = new System.Drawing.Point(81, 134);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(142, 23);
            this.label4.TabIndex = 96;
            this.label4.Text = "Nom d\'utilisateur";
            // 
            // lblAvertiss
            // 
            this.lblAvertiss.AutoSize = true;
            this.lblAvertiss.BackColor = System.Drawing.Color.Transparent;
            this.lblAvertiss.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvertiss.ForeColor = System.Drawing.Color.Gold;
            this.lblAvertiss.Location = new System.Drawing.Point(239, 228);
            this.lblAvertiss.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAvertiss.Name = "lblAvertiss";
            this.lblAvertiss.Size = new System.Drawing.Size(296, 20);
            this.lblAvertiss.TabIndex = 98;
            this.lblAvertiss.Text = "Mot de passe ou nom d\'utilisateur incorrect";
            this.lblAvertiss.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.LightBlue;
            this.label5.Location = new System.Drawing.Point(81, 191);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 23);
            this.label5.TabIndex = 97;
            this.label5.Text = "Mot de passe";
            // 
            // bunifuDragControl2
            // 
            this.bunifuDragControl2.Fixed = true;
            this.bunifuDragControl2.Horizontal = true;
            this.bunifuDragControl2.TargetControl = this.Header;
            this.bunifuDragControl2.Vertical = true;
            // 
            // btnConnecter
            // 
            this.btnConnecter.FlatAppearance.BorderSize = 0;
            this.btnConnecter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnecter.ForeColor = System.Drawing.Color.LightBlue;
            this.btnConnecter.IconChar = FontAwesome.Sharp.IconChar.AssistiveListeningSystems;
            this.btnConnecter.IconColor = System.Drawing.Color.LightBlue;
            this.btnConnecter.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnConnecter.IconSize = 32;
            this.btnConnecter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConnecter.Location = new System.Drawing.Point(252, 286);
            this.btnConnecter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnConnecter.Name = "btnConnecter";
            this.btnConnecter.Padding = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.btnConnecter.Size = new System.Drawing.Size(161, 55);
            this.btnConnecter.TabIndex = 99;
            this.btnConnecter.Text = "Connecter";
            this.btnConnecter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnConnecter.UseVisualStyleBackColor = true;
            this.btnConnecter.Click += new System.EventHandler(this.btnConnecter_Click);
            // 
            // btnInscrire
            // 
            this.btnInscrire.FlatAppearance.BorderSize = 0;
            this.btnInscrire.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInscrire.ForeColor = System.Drawing.Color.LightBlue;
            this.btnInscrire.IconChar = FontAwesome.Sharp.IconChar.UserPlus;
            this.btnInscrire.IconColor = System.Drawing.Color.LightBlue;
            this.btnInscrire.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnInscrire.IconSize = 32;
            this.btnInscrire.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInscrire.Location = new System.Drawing.Point(461, 286);
            this.btnInscrire.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnInscrire.Name = "btnInscrire";
            this.btnInscrire.Padding = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.btnInscrire.Size = new System.Drawing.Size(161, 55);
            this.btnInscrire.TabIndex = 99;
            this.btnInscrire.Text = "Inscrire";
            this.btnInscrire.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnInscrire.UseVisualStyleBackColor = true;
            this.btnInscrire.Visible = false;
            this.btnInscrire.Click += new System.EventHandler(this.btnInscrire_Click);
            // 
            // LogIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.ClientSize = new System.Drawing.Size(639, 356);
            this.Controls.Add(this.btnInscrire);
            this.Controls.Add(this.btnConnecter);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblAvertiss);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Header);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.dataGridView6);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "LogIn";
            this.Opacity = 0.9D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LogIn";
            this.Header.ResumeLayout(false);
            this.Header.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFermer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView6)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private System.Windows.Forms.Panel Header;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.DataGridView dataGridView6;
        private Bunifu.Framework.UI.BunifuElipse bunifuElipse2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Bunifu.Framework.UI.BunifuElipse bunifuElipse3;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
        private FontAwesome.Sharp.IconPictureBox btnFermer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblAvertiss;
        private System.Windows.Forms.Label label5;
        private FontAwesome.Sharp.IconButton btnConnecter;
        private System.Windows.Forms.ToolTip toolTip1;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private FontAwesome.Sharp.IconButton btnInscrire;
        private System.Windows.Forms.Label label1;
    }
}