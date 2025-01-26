namespace PeatShopApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        private PictureBox pictureBoxShowHidePassword;
        private PictureBox pictureBoxShowHideLoginPassword;

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            usernameBox = new TextBox();
            PhoneBox = new TextBox();
            PasswordBox = new TextBox();
            EmailBox = new TextBox();
            Register = new Button();
            label1 = new Label();
            Login = new LinkLabel();
            webViewRegister = new Microsoft.Web.WebView2.WinForms.WebView2();
            webViewlogin = new Microsoft.Web.WebView2.WinForms.WebView2();
            EmailLoginBox = new TextBox();
            PassLogin = new TextBox();
            LoginB = new Button();
            RegisterLable = new LinkLabel();
            webView = new Microsoft.Web.WebView2.WinForms.WebView2();
            panel1 = new Panel();
            label2 = new Label();
            pictureBoxShowHidePassword = new PictureBox();
            pictureBoxShowHideLoginPassword = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)webViewRegister).BeginInit();
            ((System.ComponentModel.ISupportInitialize)webViewlogin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)webView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxShowHidePassword).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxShowHideLoginPassword).BeginInit();
            SuspendLayout();
            // 
            // usernameBox
            // 
            usernameBox.Location = new Point(547, 116);
            usernameBox.Multiline = true;
            usernameBox.Name = "usernameBox";
            usernameBox.PlaceholderText = "Enter Name";
            usernameBox.Size = new Size(243, 31);
            usernameBox.TabIndex = 2;
            // 
            // PhoneBox
            // 
            PhoneBox.Location = new Point(547, 238);
            PhoneBox.Multiline = true;
            PhoneBox.Name = "PhoneBox";
            PhoneBox.PlaceholderText = "Phone num";
            PhoneBox.Size = new Size(243, 32);
            PhoneBox.TabIndex = 5;
            // 
            // PasswordBox
            // 
            PasswordBox.Location = new Point(547, 299);
            PasswordBox.Multiline = true;
            PasswordBox.Name = "PasswordBox";
            PasswordBox.PasswordChar = '*';
            PasswordBox.PlaceholderText = "Password";
            PasswordBox.Size = new Size(243, 32);
            PasswordBox.TabIndex = 6;
            // 
            // EmailBox
            // 
            EmailBox.Location = new Point(547, 181);
            EmailBox.Multiline = true;
            EmailBox.Name = "EmailBox";
            EmailBox.PlaceholderText = "Enter Email";
            EmailBox.Size = new Size(243, 31);
            EmailBox.TabIndex = 7;
            // 
            // Register
            // 
            Register.BackColor = Color.CornflowerBlue;
            Register.ForeColor = SystemColors.ButtonFace;
            Register.Location = new Point(516, 371);
            Register.Name = "Register";
            Register.Size = new Size(122, 35);
            Register.TabIndex = 8;
            Register.Text = "Register";
            Register.UseVisualStyleBackColor = false;
            Register.Click += Register_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(654, 381);
            label1.Name = "label1";
            label1.Size = new Size(99, 15);
            label1.TabIndex = 9;
            label1.Text = "not have account";
            // 
            // Login
            // 
            Login.AutoSize = true;
            Login.Location = new Point(790, 381);
            Login.Name = "Login";
            Login.Size = new Size(37, 15);
            Login.TabIndex = 10;
            Login.TabStop = true;
            Login.Text = "Login";
            Login.LinkClicked += Login_LinkClicked;
            // 
            // webViewRegister
            // 
            webViewRegister.AllowExternalDrop = true;
            webViewRegister.CreationProperties = null;
            webViewRegister.DefaultBackgroundColor = Color.White;
            webViewRegister.Location = new Point(476, 27);
            webViewRegister.Name = "webViewRegister";
            webViewRegister.Size = new Size(432, 423);
            webViewRegister.TabIndex = 11;
            webViewRegister.ZoomFactor = 1D;
            // 
            // webViewlogin
            // 
            webViewlogin.AllowExternalDrop = true;
            webViewlogin.CreationProperties = null;
            webViewlogin.DefaultBackgroundColor = Color.White;
            webViewlogin.Location = new Point(492, 84);
            webViewlogin.Name = "webViewlogin";
            webViewlogin.Size = new Size(399, 209);
            webViewlogin.TabIndex = 12;
            webViewlogin.ZoomFactor = 1D;
            // 
            // EmailLoginBox
            // 
            EmailLoginBox.Location = new Point(547, 181);
            EmailLoginBox.Multiline = true;
            EmailLoginBox.Name = "EmailLoginBox";
            EmailLoginBox.PlaceholderText = "Enter Email";
            EmailLoginBox.Size = new Size(243, 31);
            EmailLoginBox.TabIndex = 13;
            // 
            // PassLogin
            // 
            PassLogin.Location = new Point(547, 238);
            PassLogin.Multiline = true;
            PassLogin.Name = "PassLogin";
            PassLogin.PasswordChar = '*';
            PassLogin.PlaceholderText = "Password";
            PassLogin.Size = new Size(243, 32);
            PassLogin.TabIndex = 14;
            // 
            // LoginB
            // 
            LoginB.BackColor = Color.CornflowerBlue;
            LoginB.ForeColor = SystemColors.ButtonHighlight;
            LoginB.Location = new Point(547, 321);
            LoginB.Name = "LoginB";
            LoginB.Size = new Size(112, 40);
            LoginB.TabIndex = 20;
            LoginB.Text = "Login";
            LoginB.UseVisualStyleBackColor = false;
            LoginB.Click += LoginToHome;
            // 
            // RegisterLable
            // 
            RegisterLable.AutoSize = true;
            RegisterLable.Location = new Point(758, 334);
            RegisterLable.Name = "RegisterLable";
            RegisterLable.Size = new Size(32, 15);
            RegisterLable.TabIndex = 16;
            RegisterLable.TabStop = true;
            RegisterLable.Text = "Back";
            RegisterLable.Click += RegisterLable_LinkClicked;
            // 
            // webView
            // 
            webView.AccessibleName = "webView";
            webView.AllowExternalDrop = true;
            webView.CreationProperties = null;
            webView.DefaultBackgroundColor = Color.White;
            webView.Location = new Point(469, 0);
            webView.Name = "webView";
            webView.Size = new Size(449, 416);
            webView.TabIndex = 17;
            webView.ZoomFactor = 1D;
            // 
            // panel1
            // 
            panel1.BackgroundImage = (Image)resources.GetObject("panel1.BackgroundImage");
            panel1.BackgroundImageLayout = ImageLayout.Stretch;
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(470, 450);
            panel1.TabIndex = 18;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.RoyalBlue;
            label2.Location = new Point(637, 132);
            label2.Name = "label2";
            label2.Size = new Size(37, 15);
            label2.TabIndex = 25;
            label2.Text = "Login";
            // 
            // pictureBoxShowHidePassword
            // 
            pictureBoxShowHidePassword.Cursor = Cursors.Hand;
            pictureBoxShowHidePassword.Location = new Point(0, 0);
            pictureBoxShowHidePassword.Name = "pictureBoxShowHidePassword";
            pictureBoxShowHidePassword.Size = new Size(109, 24);
            pictureBoxShowHidePassword.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxShowHidePassword.TabIndex = 0;
            pictureBoxShowHidePassword.TabStop = false;
            //pictureBoxShowHidePassword.Click += pictureBoxShowHidePassword_Click;
            // 
            // pictureBoxShowHideLoginPassword
            // 
            pictureBoxShowHideLoginPassword.Cursor = Cursors.Hand;
            pictureBoxShowHideLoginPassword.Location = new Point(0, 0);
            pictureBoxShowHideLoginPassword.Name = "pictureBoxShowHideLoginPassword";
            pictureBoxShowHideLoginPassword.Size = new Size(24, 24);
            pictureBoxShowHideLoginPassword.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxShowHideLoginPassword.TabIndex = 1;
            pictureBoxShowHideLoginPassword.TabStop = false;
            pictureBoxShowHideLoginPassword.Click += pictureBoxShowHideLoginPassword_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(920, 450);
            Controls.Add(Login);
            Controls.Add(label1);
            Controls.Add(Register);
            Controls.Add(PasswordBox);
            Controls.Add(EmailLoginBox);
            Controls.Add(usernameBox);
            Controls.Add(PhoneBox);
            Controls.Add(label2);
            Controls.Add(panel1);
            Controls.Add(EmailBox);
            Controls.Add(PassLogin);
            Controls.Add(webViewRegister);
            Controls.Add(webView);
            Controls.Add(webViewlogin);
            Controls.Add(LoginB);
            Controls.Add(RegisterLable);
            Controls.Add(pictureBoxShowHidePassword);
            Controls.Add(pictureBoxShowHideLoginPassword);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)webViewRegister).EndInit();
            ((System.ComponentModel.ISupportInitialize)webViewlogin).EndInit();
            ((System.ComponentModel.ISupportInitialize)webView).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxShowHidePassword).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxShowHideLoginPassword).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion
        private TextBox usernameBox;
        private TextBox PhoneBox;
        private TextBox PasswordBox;
        private TextBox EmailBox;
        private Button Register;
        private Label label1;
        private LinkLabel Login;
        private Microsoft.Web.WebView2.WinForms.WebView2 webViewRegister;
        private Microsoft.Web.WebView2.WinForms.WebView2 webViewlogin;
        private TextBox EmailLoginBox;
        private TextBox PassLogin;
        private Button LoginB;
        private LinkLabel RegisterLable;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView;
        private Panel panel1;
        private Label label2;
    }
}
