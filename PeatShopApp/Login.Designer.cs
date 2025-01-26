namespace PeatShopApp
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            Logout = new Button();
            button1 = new Button();
            All_Product = new Button();
            webViewLogin = new Microsoft.Web.WebView2.WinForms.WebView2();
            flowLayoutPanelProducts = new FlowLayoutPanel();
            flowLayoutPanelProfile = new FlowLayoutPanel();
            flowLayoutPanelMyProduct = new FlowLayoutPanel();
            Card = new Button();
            errorProvider1 = new ErrorProvider(components);
            button2 = new Button();
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)webViewLogin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // Logout
            // 
            Logout.BackgroundImage = (Image)resources.GetObject("Logout.BackgroundImage");
            Logout.BackgroundImageLayout = ImageLayout.Zoom;
            Logout.Location = new Point(47, 475);
            Logout.Name = "Logout";
            Logout.Size = new Size(111, 81);
            Logout.TabIndex = 1;
            Logout.UseVisualStyleBackColor = true;
            Logout.Click += Logout_Click;
            // 
            // button1
            // 
            button1.BackgroundImage = (Image)resources.GetObject("button1.BackgroundImage");
            button1.BackgroundImageLayout = ImageLayout.Stretch;
            button1.Location = new Point(47, 247);
            button1.Name = "button1";
            button1.Size = new Size(111, 83);
            button1.TabIndex = 2;
            button1.UseVisualStyleBackColor = true;
            button1.Click += MyProduct;
            // 
            // All_Product
            // 
            All_Product.BackgroundImage = (Image)resources.GetObject("All_Product.BackgroundImage");
            All_Product.BackgroundImageLayout = ImageLayout.Stretch;
            All_Product.Location = new Point(47, 39);
            All_Product.Name = "All_Product";
            All_Product.Size = new Size(111, 74);
            All_Product.TabIndex = 4;
            All_Product.UseVisualStyleBackColor = true;
            All_Product.Click += getAllProduct;
            // 
            // webViewLogin
            // 
            webViewLogin.AllowExternalDrop = true;
            webViewLogin.CreationProperties = null;
            webViewLogin.DefaultBackgroundColor = Color.White;
            webViewLogin.Dock = DockStyle.Fill;
            webViewLogin.Location = new Point(0, 0);
            webViewLogin.Name = "webViewLogin";
            webViewLogin.Size = new Size(941, 568);
            webViewLogin.TabIndex = 6;
            webViewLogin.ZoomFactor = 1D;
            // 
            // flowLayoutPanelProducts
            // 
            flowLayoutPanelProducts.AutoScroll = true;
            flowLayoutPanelProducts.Location = new Point(250, 175);
            flowLayoutPanelProducts.Name = "flowLayoutPanelProducts";
            flowLayoutPanelProducts.Size = new Size(679, 355);
            flowLayoutPanelProducts.TabIndex = 23;
            // 
            // flowLayoutPanelProfile
            // 
            flowLayoutPanelProfile.AutoScroll = true;
            flowLayoutPanelProfile.Location = new Point(250, 175);
            flowLayoutPanelProfile.Name = "flowLayoutPanelProfile";
            flowLayoutPanelProfile.Size = new Size(679, 355);
            flowLayoutPanelProfile.TabIndex = 24;
            // 
            // flowLayoutPanelMyProduct
            // 
            flowLayoutPanelMyProduct.AutoScroll = true;
            flowLayoutPanelMyProduct.Location = new Point(250, 175);
            flowLayoutPanelMyProduct.Name = "flowLayoutPanelMyProduct";
            flowLayoutPanelMyProduct.Size = new Size(679, 355);
            flowLayoutPanelMyProduct.TabIndex = 25;
            // 
            // Card
            // 
            Card.BackgroundImage = (Image)resources.GetObject("Card.BackgroundImage");
            Card.BackgroundImageLayout = ImageLayout.Stretch;
            Card.Location = new Point(47, 366);
            Card.Name = "Card";
            Card.Size = new Size(111, 91);
            Card.TabIndex = 3;
            Card.Text = "Card";
            Card.UseVisualStyleBackColor = true;
            Card.Click += MyCard;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // button2
            // 
            button2.BackgroundImage = (Image)resources.GetObject("button2.BackgroundImage");
            button2.BackgroundImageLayout = ImageLayout.Stretch;
            button2.Location = new Point(47, 137);
            button2.Name = "button2";
            button2.Size = new Size(111, 83);
            button2.TabIndex = 26;
            button2.UseVisualStyleBackColor = true;
            button2.Click += GetProfile;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(255, 247, 237);
            panel1.Controls.Add(All_Product);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(Card);
            panel1.Controls.Add(Logout);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(244, 570);
            panel1.TabIndex = 27;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(941, 568);
            Controls.Add(panel1);
            Controls.Add(flowLayoutPanelProfile);
            Controls.Add(flowLayoutPanelProducts);
            Controls.Add(flowLayoutPanelMyProduct);
            Controls.Add(webViewLogin);
            Name = "Login";
            Text = "Login";
            Load += Login_Load;
            ((System.ComponentModel.ISupportInitialize)webViewLogin).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Button Logout;
        private Button button1;
        private Button All_Product;
        private Microsoft.Web.WebView2.WinForms.WebView2 webViewLogin;
        private FlowLayoutPanel flowLayoutPanelProducts;
        private FlowLayoutPanel flowLayoutPanelMyProduct;
        private Button Card;
        private ErrorProvider errorProvider1;
        private Button button2;
  
        private FlowLayoutPanel flowLayoutPanelProfile;
        private Panel panel1;
    }
}