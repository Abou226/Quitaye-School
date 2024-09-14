namespace Quitaye_School.User_Interface
{
    partial class Inscription_Entreprise
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Inscription_Entreprise));
            this.lblAvertiss = new System.Windows.Forms.Label();
            this.txtemailConnection = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.panelConnection = new System.Windows.Forms.Panel();
            this.iconButton1 = new FontAwesome.Sharp.IconButton();
            this.iconButton2 = new FontAwesome.Sharp.IconButton();
            this.txtpasswordconnection = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.panelRegister = new System.Windows.Forms.Panel();
            this.btnRegister = new FontAwesome.Sharp.IconButton();
            this.btnConnecter = new FontAwesome.Sharp.IconButton();
            this.txtNomEntreprise = new System.Windows.Forms.TextBox();
            this.txtEmailEntre = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.Header = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnFermer = new FontAwesome.Sharp.IconPictureBox();
            this.bunifuDragControl2 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.panelConnection.SuspendLayout();
            this.panelRegister.SuspendLayout();
            this.Header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFermer)).BeginInit();
            this.SuspendLayout();
            // 
            // lblAvertiss
            // 
            this.lblAvertiss.AutoSize = true;
            this.lblAvertiss.BackColor = System.Drawing.Color.Transparent;
            this.lblAvertiss.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvertiss.ForeColor = System.Drawing.Color.Gold;
            this.lblAvertiss.Location = new System.Drawing.Point(149, 98);
            this.lblAvertiss.Name = "lblAvertiss";
            this.lblAvertiss.Size = new System.Drawing.Size(77, 15);
            this.lblAvertiss.TabIndex = 258;
            this.lblAvertiss.Text = "Mot de passe";
            this.lblAvertiss.Visible = false;
            // 
            // txtemailConnection
            // 
            this.txtemailConnection.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtemailConnection.Location = new System.Drawing.Point(152, 23);
            this.txtemailConnection.Name = "txtemailConnection";
            this.txtemailConnection.Size = new System.Drawing.Size(224, 25);
            this.txtemailConnection.TabIndex = 246;
            this.txtemailConnection.TextChanged += new System.EventHandler(this.txtemailConnection_TextChanged);
            this.txtemailConnection.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtpasswordconnection_KeyPress);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.LightBlue;
            this.label15.Location = new System.Drawing.Point(5, 31);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(51, 17);
            this.label15.TabIndex = 240;
            this.label15.Text = "Email :*";
            // 
            // panelConnection
            // 
            this.panelConnection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelConnection.Controls.Add(this.lblAvertiss);
            this.panelConnection.Controls.Add(this.iconButton1);
            this.panelConnection.Controls.Add(this.iconButton2);
            this.panelConnection.Controls.Add(this.txtemailConnection);
            this.panelConnection.Controls.Add(this.txtpasswordconnection);
            this.panelConnection.Controls.Add(this.label14);
            this.panelConnection.Controls.Add(this.label15);
            this.panelConnection.Location = new System.Drawing.Point(26, 84);
            this.panelConnection.Name = "panelConnection";
            this.panelConnection.Size = new System.Drawing.Size(376, 177);
            this.panelConnection.TabIndex = 283;
            this.panelConnection.Visible = false;
            // 
            // iconButton1
            // 
            this.iconButton1.FlatAppearance.BorderSize = 0;
            this.iconButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton1.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.iconButton1.ForeColor = System.Drawing.Color.LightBlue;
            this.iconButton1.IconChar = FontAwesome.Sharp.IconChar.Registered;
            this.iconButton1.IconColor = System.Drawing.Color.LightBlue;
            this.iconButton1.IconSize = 32;
            this.iconButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton1.Location = new System.Drawing.Point(121, 130);
            this.iconButton1.Name = "iconButton1";
            this.iconButton1.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.iconButton1.Rotation = 0D;
            this.iconButton1.Size = new System.Drawing.Size(121, 37);
            this.iconButton1.TabIndex = 257;
            this.iconButton1.Text = "Inscription";
            this.iconButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton1.UseVisualStyleBackColor = true;
            this.iconButton1.Click += new System.EventHandler(this.iconButton1_Click);
            // 
            // iconButton2
            // 
            this.iconButton2.FlatAppearance.BorderSize = 0;
            this.iconButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton2.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.iconButton2.ForeColor = System.Drawing.Color.LightBlue;
            this.iconButton2.IconChar = FontAwesome.Sharp.IconChar.AssistiveListeningSystems;
            this.iconButton2.IconColor = System.Drawing.Color.LightBlue;
            this.iconButton2.IconSize = 32;
            this.iconButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton2.Location = new System.Drawing.Point(258, 134);
            this.iconButton2.Name = "iconButton2";
            this.iconButton2.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.iconButton2.Rotation = 0D;
            this.iconButton2.Size = new System.Drawing.Size(121, 37);
            this.iconButton2.TabIndex = 257;
            this.iconButton2.Text = "Connection";
            this.iconButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton2.UseVisualStyleBackColor = true;
            this.iconButton2.Click += new System.EventHandler(this.iconButton2_Click);
            // 
            // txtpasswordconnection
            // 
            this.txtpasswordconnection.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtpasswordconnection.Location = new System.Drawing.Point(152, 58);
            this.txtpasswordconnection.Name = "txtpasswordconnection";
            this.txtpasswordconnection.PasswordChar = '•';
            this.txtpasswordconnection.Size = new System.Drawing.Size(224, 25);
            this.txtpasswordconnection.TabIndex = 248;
            this.txtpasswordconnection.TextChanged += new System.EventHandler(this.txtemailConnection_TextChanged);
            this.txtpasswordconnection.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtpasswordconnection_KeyPress);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.LightBlue;
            this.label14.Location = new System.Drawing.Point(5, 66);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(101, 17);
            this.label14.TabIndex = 240;
            this.label14.Text = "Mot de passe :*";
            // 
            // panelRegister
            // 
            this.panelRegister.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelRegister.Controls.Add(this.btnRegister);
            this.panelRegister.Controls.Add(this.btnConnecter);
            this.panelRegister.Controls.Add(this.txtNomEntreprise);
            this.panelRegister.Controls.Add(this.txtEmailEntre);
            this.panelRegister.Controls.Add(this.txtPassword);
            this.panelRegister.Controls.Add(this.label5);
            this.panelRegister.Controls.Add(this.label3);
            this.panelRegister.Controls.Add(this.label4);
            this.panelRegister.Location = new System.Drawing.Point(25, 82);
            this.panelRegister.Name = "panelRegister";
            this.panelRegister.Size = new System.Drawing.Size(377, 182);
            this.panelRegister.TabIndex = 282;
            // 
            // btnRegister
            // 
            this.btnRegister.FlatAppearance.BorderSize = 0;
            this.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegister.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnRegister.ForeColor = System.Drawing.Color.LightBlue;
            this.btnRegister.IconChar = FontAwesome.Sharp.IconChar.Registered;
            this.btnRegister.IconColor = System.Drawing.Color.LightBlue;
            this.btnRegister.IconSize = 32;
            this.btnRegister.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRegister.Location = new System.Drawing.Point(253, 141);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnRegister.Rotation = 0D;
            this.btnRegister.Size = new System.Drawing.Size(121, 37);
            this.btnRegister.TabIndex = 257;
            this.btnRegister.Text = "Inscription";
            this.btnRegister.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // btnConnecter
            // 
            this.btnConnecter.FlatAppearance.BorderSize = 0;
            this.btnConnecter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnecter.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnConnecter.ForeColor = System.Drawing.Color.LightBlue;
            this.btnConnecter.IconChar = FontAwesome.Sharp.IconChar.AssistiveListeningSystems;
            this.btnConnecter.IconColor = System.Drawing.Color.LightBlue;
            this.btnConnecter.IconSize = 32;
            this.btnConnecter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConnecter.Location = new System.Drawing.Point(62, 140);
            this.btnConnecter.Name = "btnConnecter";
            this.btnConnecter.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnConnecter.Rotation = 0D;
            this.btnConnecter.Size = new System.Drawing.Size(185, 37);
            this.btnConnecter.TabIndex = 257;
            this.btnConnecter.Text = "Déjà Inscrit (Connection)";
            this.btnConnecter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnConnecter.UseVisualStyleBackColor = true;
            this.btnConnecter.Click += new System.EventHandler(this.btnConnecter_Click);
            // 
            // txtNomEntreprise
            // 
            this.txtNomEntreprise.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNomEntreprise.Location = new System.Drawing.Point(150, 13);
            this.txtNomEntreprise.Name = "txtNomEntreprise";
            this.txtNomEntreprise.Size = new System.Drawing.Size(224, 25);
            this.txtNomEntreprise.TabIndex = 1;
            this.txtNomEntreprise.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNomEntreprise_KeyPress);
            // 
            // txtEmailEntre
            // 
            this.txtEmailEntre.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmailEntre.Location = new System.Drawing.Point(150, 44);
            this.txtEmailEntre.Name = "txtEmailEntre";
            this.txtEmailEntre.Size = new System.Drawing.Size(224, 25);
            this.txtEmailEntre.TabIndex = 2;
            this.txtEmailEntre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNomEntreprise_KeyPress);
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(150, 79);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '•';
            this.txtPassword.Size = new System.Drawing.Size(224, 25);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNomEntreprise_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.LightBlue;
            this.label5.Location = new System.Drawing.Point(3, 87);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 17);
            this.label5.TabIndex = 240;
            this.label5.Text = "Mot de passe :*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.LightBlue;
            this.label3.Location = new System.Drawing.Point(3, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 17);
            this.label3.TabIndex = 240;
            this.label3.Text = "Email :*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.LightBlue;
            this.label4.Location = new System.Drawing.Point(3, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 17);
            this.label4.TabIndex = 234;
            this.label4.Text = "Nom entreprise :*";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(436, 2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(2, 279);
            this.panel4.TabIndex = 278;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(2, 281);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(436, 2);
            this.panel3.TabIndex = 280;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(2, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(436, 2);
            this.panel2.TabIndex = 279;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(2, 283);
            this.panel1.TabIndex = 277;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Cyan;
            this.label2.Location = new System.Drawing.Point(121, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(200, 13);
            this.label2.TabIndex = 281;
            this.label2.Text = "Les éléments avec (*) sont obligatoire.";
            // 
            // bunifuDragControl1
            // 
            this.bunifuDragControl1.Fixed = true;
            this.bunifuDragControl1.Horizontal = true;
            this.bunifuDragControl1.TargetControl = this;
            this.bunifuDragControl1.Vertical = true;
            // 
            // Header
            // 
            this.Header.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(6)))), ((int)(((byte)(74)))));
            this.Header.Controls.Add(this.label1);
            this.Header.Controls.Add(this.pictureBox2);
            this.Header.Controls.Add(this.btnFermer);
            this.Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.Header.Location = new System.Drawing.Point(2, 2);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(434, 61);
            this.Header.TabIndex = 285;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.LightBlue;
            this.label1.Location = new System.Drawing.Point(144, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 30);
            this.label1.TabIndex = 241;
            this.label1.Text = "Etablissement ";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Quitaye_School.Properties.Resources.Logopit_1584731094407;
            this.pictureBox2.Location = new System.Drawing.Point(13, 6);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(50, 50);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 32;
            this.pictureBox2.TabStop = false;
            // 
            // btnFermer
            // 
            this.btnFermer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFermer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(6)))), ((int)(((byte)(74)))));
            this.btnFermer.ForeColor = System.Drawing.Color.LightBlue;
            this.btnFermer.IconChar = FontAwesome.Sharp.IconChar.WindowClose;
            this.btnFermer.IconColor = System.Drawing.Color.LightBlue;
            this.btnFermer.IconSize = 25;
            this.btnFermer.Location = new System.Drawing.Point(398, 11);
            this.btnFermer.Name = "btnFermer";
            this.btnFermer.Size = new System.Drawing.Size(25, 25);
            this.btnFermer.TabIndex = 31;
            this.btnFermer.TabStop = false;
            this.btnFermer.Click += new System.EventHandler(this.btnFermer_Click);
            // 
            // bunifuDragControl2
            // 
            this.bunifuDragControl2.Fixed = true;
            this.bunifuDragControl2.Horizontal = true;
            this.bunifuDragControl2.TargetControl = this.Header;
            this.bunifuDragControl2.Vertical = true;
            // 
            // Inscription_Entreprise
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(10)))), ((int)(((byte)(97)))));
            this.ClientSize = new System.Drawing.Size(438, 283);
            this.Controls.Add(this.Header);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panelConnection);
            this.Controls.Add(this.panelRegister);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Inscription_Entreprise";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inscription_Entreprise";
            this.panelConnection.ResumeLayout(false);
            this.panelConnection.PerformLayout();
            this.panelRegister.ResumeLayout(false);
            this.panelRegister.PerformLayout();
            this.Header.ResumeLayout(false);
            this.Header.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFermer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAvertiss;
        private FontAwesome.Sharp.IconButton iconButton1;
        private FontAwesome.Sharp.IconButton iconButton2;
        private System.Windows.Forms.TextBox txtemailConnection;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Panel panelConnection;
        private System.Windows.Forms.TextBox txtpasswordconnection;
        private System.Windows.Forms.Label label14;
        private FontAwesome.Sharp.IconButton btnRegister;
        private FontAwesome.Sharp.IconButton btnConnecter;
        private System.Windows.Forms.Panel panelRegister;
        private System.Windows.Forms.TextBox txtNomEntreprise;
        private System.Windows.Forms.TextBox txtEmailEntre;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.Panel panel4;
        public System.Windows.Forms.Panel panel3;
        public System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
        private System.Windows.Forms.Panel Header;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private FontAwesome.Sharp.IconPictureBox btnFermer;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl2;
    }
}