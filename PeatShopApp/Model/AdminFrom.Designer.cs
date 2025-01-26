namespace PeatShopApp.Model
{
    partial class AdminFrom
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminFrom));
            Logout = new Button();
            button3 = new Button();
            button2 = new Button();
            button5 = new Button();
            groupBoxAddProduct = new GroupBox();
            btnBrowseImage = new Button();
            StockBox = new TextBox();
            PriceBox = new TextBox();
            Description = new TextBox();
            Productname = new TextBox();
            button6 = new Button();
            pictureBoxProductImage = new PictureBox();
            flowLayoutPanelProducts = new FlowLayoutPanel();
            flowLayoutPanelUsers = new FlowLayoutPanel();
            webViewAdmin = new Microsoft.Web.WebView2.WinForms.WebView2();
            btnGenerateReport = new Button();
            flowLayoutPanelReport = new FlowLayoutPanel();
            plotView = new OxyPlot.WindowsForms.PlotView();
            dateTimePickerEnd = new DateTimePicker();
            dateTimePickerStrat = new DateTimePicker();
            CreateReport = new Button();
            panel1 = new Panel();
            groupBoxAddProduct.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxProductImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)webViewAdmin).BeginInit();
            flowLayoutPanelReport.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // Logout
            // 
            Logout.BackgroundImage = (Image)resources.GetObject("Logout.BackgroundImage");
            Logout.BackgroundImageLayout = ImageLayout.Zoom;
            Logout.Location = new Point(71, 493);
            Logout.Name = "Logout";
            Logout.Size = new Size(79, 74);
            Logout.TabIndex = 5;
            Logout.UseVisualStyleBackColor = true;
            Logout.Click += Logout_Click;
            // 
            // button3
            // 
            button3.BackgroundImage = (Image)resources.GetObject("button3.BackgroundImage");
            button3.BackgroundImageLayout = ImageLayout.Stretch;
            button3.Location = new Point(55, 23);
            button3.Name = "button3";
            button3.Size = new Size(112, 115);
            button3.TabIndex = 8;
            button3.UseVisualStyleBackColor = true;
            button3.Click += ShowProducts;
            // 
            // button2
            // 
            button2.BackgroundImage = (Image)resources.GetObject("button2.BackgroundImage");
            button2.BackgroundImageLayout = ImageLayout.Zoom;
            button2.Location = new Point(55, 256);
            button2.Name = "button2";
            button2.Size = new Size(118, 107);
            button2.TabIndex = 10;
            button2.UseVisualStyleBackColor = true;
            button2.Click += AddProduct;
            // 
            // button5
            // 
            button5.BackgroundImage = (Image)resources.GetObject("button5.BackgroundImage");
            button5.BackgroundImageLayout = ImageLayout.Stretch;
            button5.Location = new Point(55, 144);
            button5.Name = "button5";
            button5.Size = new Size(118, 99);
            button5.TabIndex = 12;
            button5.UseVisualStyleBackColor = true;
            button5.Click += ShowUsers;
            // 
            // groupBoxAddProduct
            // 
            groupBoxAddProduct.AccessibleName = "groupBoxAddProduct";
            groupBoxAddProduct.Controls.Add(btnBrowseImage);
            groupBoxAddProduct.Controls.Add(StockBox);
            groupBoxAddProduct.Controls.Add(PriceBox);
            groupBoxAddProduct.Controls.Add(Description);
            groupBoxAddProduct.Controls.Add(Productname);
            groupBoxAddProduct.Controls.Add(button6);
            groupBoxAddProduct.Controls.Add(pictureBoxProductImage);
            groupBoxAddProduct.Location = new Point(237, 129);
            groupBoxAddProduct.Name = "groupBoxAddProduct";
            groupBoxAddProduct.Size = new Size(606, 322);
            groupBoxAddProduct.TabIndex = 19;
            groupBoxAddProduct.TabStop = false;
            groupBoxAddProduct.Enter += groupBoxAddProduct_Enter;
            // 
            // btnBrowseImage
            // 
            btnBrowseImage.Location = new Point(383, 293);
            btnBrowseImage.Name = "btnBrowseImage";
            btnBrowseImage.Size = new Size(160, 29);
            btnBrowseImage.TabIndex = 25;
            btnBrowseImage.Text = "Choose Image";
            btnBrowseImage.UseVisualStyleBackColor = true;
            btnBrowseImage.Click += btnBrowseImage_Click;
            // 
            // StockBox
            // 
            StockBox.Location = new Point(58, 228);
            StockBox.Name = "StockBox";
            StockBox.Size = new Size(238, 23);
            StockBox.TabIndex = 24;
            // 
            // PriceBox
            // 
            PriceBox.Location = new Point(58, 178);
            PriceBox.Name = "PriceBox";
            PriceBox.Size = new Size(238, 23);
            PriceBox.TabIndex = 23;
            // 
            // Description
            // 
            Description.Location = new Point(58, 127);
            Description.Name = "Description";
            Description.Size = new Size(238, 23);
            Description.TabIndex = 22;
            // 
            // Productname
            // 
            Productname.Location = new Point(58, 79);
            Productname.Name = "Productname";
            Productname.Size = new Size(238, 23);
            Productname.TabIndex = 21;
            // 
            // button6
            // 
            button6.Location = new Point(91, 293);
            button6.Name = "button6";
            button6.Size = new Size(160, 29);
            button6.TabIndex = 20;
            button6.Text = "Add ";
            button6.UseVisualStyleBackColor = true;
            button6.Click += AddPro;
            // 
            // pictureBoxProductImage
            // 
            pictureBoxProductImage.Location = new Point(336, 73);
            pictureBoxProductImage.Name = "pictureBoxProductImage";
            pictureBoxProductImage.Size = new Size(217, 178);
            pictureBoxProductImage.TabIndex = 26;
            pictureBoxProductImage.TabStop = false;
            // 
            // flowLayoutPanelProducts
            // 
            flowLayoutPanelProducts.Location = new Point(222, 109);
            flowLayoutPanelProducts.Name = "flowLayoutPanelProducts";
            flowLayoutPanelProducts.Size = new Size(650, 437);
            flowLayoutPanelProducts.TabIndex = 0;
            // 
            // flowLayoutPanelUsers
            // 
            flowLayoutPanelUsers.Location = new Point(237, 129);
            flowLayoutPanelUsers.Name = "flowLayoutPanelUsers";
            flowLayoutPanelUsers.Size = new Size(623, 417);
            flowLayoutPanelUsers.TabIndex = 21;
            // 
            // webViewAdmin
            // 
            webViewAdmin.AllowExternalDrop = true;
            webViewAdmin.CreationProperties = null;
            webViewAdmin.DefaultBackgroundColor = Color.White;
            webViewAdmin.Dock = DockStyle.Fill;
            webViewAdmin.Location = new Point(0, 0);
            webViewAdmin.Name = "webViewAdmin";
            webViewAdmin.Size = new Size(872, 570);
            webViewAdmin.TabIndex = 22;
            webViewAdmin.ZoomFactor = 1D;
            // 
            // btnGenerateReport
            // 
            btnGenerateReport.BackgroundImage = (Image)resources.GetObject("btnGenerateReport.BackgroundImage");
            btnGenerateReport.BackgroundImageLayout = ImageLayout.Zoom;
            btnGenerateReport.Location = new Point(55, 380);
            btnGenerateReport.Name = "btnGenerateReport";
            btnGenerateReport.Size = new Size(118, 98);
            btnGenerateReport.TabIndex = 23;
            btnGenerateReport.UseVisualStyleBackColor = true;
            btnGenerateReport.Click += GenerateReport_Click;
            // 
            // flowLayoutPanelReport
            // 
            flowLayoutPanelReport.AutoScroll = true;
            flowLayoutPanelReport.Controls.Add(plotView);
            flowLayoutPanelReport.Controls.Add(dateTimePickerEnd);
            flowLayoutPanelReport.Controls.Add(dateTimePickerStrat);
            flowLayoutPanelReport.Controls.Add(CreateReport);
            flowLayoutPanelReport.Location = new Point(222, 129);
            flowLayoutPanelReport.Name = "flowLayoutPanelReport";
            flowLayoutPanelReport.Size = new Size(638, 417);
            flowLayoutPanelReport.TabIndex = 1;
            // 
            // plotView
            // 
            plotView.Location = new Point(3, 3);
            plotView.Name = "plotView";
            plotView.PanCursor = Cursors.Hand;
            plotView.Size = new Size(603, 346);
            plotView.TabIndex = 0;
            plotView.Text = "plotView";
            plotView.ZoomHorizontalCursor = Cursors.SizeWE;
            plotView.ZoomRectangleCursor = Cursors.SizeNWSE;
            plotView.ZoomVerticalCursor = Cursors.SizeNS;
            // 
            // dateTimePickerEnd
            // 
            dateTimePickerEnd.Location = new Point(3, 355);
            dateTimePickerEnd.Name = "dateTimePickerEnd";
            dateTimePickerEnd.Size = new Size(200, 23);
            dateTimePickerEnd.TabIndex = 1;
            // 
            // dateTimePickerStrat
            // 
            dateTimePickerStrat.Location = new Point(209, 355);
            dateTimePickerStrat.Name = "dateTimePickerStrat";
            dateTimePickerStrat.Size = new Size(200, 23);
            dateTimePickerStrat.TabIndex = 0;
            // 
            // CreateReport
            // 
            CreateReport.Location = new Point(415, 355);
            CreateReport.Name = "CreateReport";
            CreateReport.Size = new Size(160, 23);
            CreateReport.TabIndex = 24;
            CreateReport.Text = "Reporting";
            CreateReport.UseVisualStyleBackColor = true;
            CreateReport.Click += CreateRep;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(255, 247, 237);
            panel1.Controls.Add(btnGenerateReport);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(Logout);
            panel1.Controls.Add(button5);
            panel1.Controls.Add(button3);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(224, 570);
            panel1.TabIndex = 24;
            // 
            // AdminFrom
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(872, 570);
            Controls.Add(panel1);
            Controls.Add(flowLayoutPanelProducts);
            Controls.Add(flowLayoutPanelReport);
            Controls.Add(groupBoxAddProduct);
            Controls.Add(flowLayoutPanelUsers);
            Controls.Add(webViewAdmin);
            Name = "AdminFrom";
            Text = "AdminFrom";
            Load += AdminFrom_Load;
            groupBoxAddProduct.ResumeLayout(false);
            groupBoxAddProduct.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxProductImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)webViewAdmin).EndInit();
            flowLayoutPanelReport.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button Logout;
        private Button button3;
        private Button button2;
        private Button button5;
        private DataGridView dataGridViewAllEmployee;
        private GroupBox groupBoxAddProduct;
        private Button button6;
        private TextBox Productname;
        private TextBox StockBox;
        private TextBox PriceBox;
        private TextBox Description;
        private FlowLayoutPanel flowLayoutPanelUsers;
        private FlowLayoutPanel flowLayoutPanelProducts;
        private Label label1;
        private Microsoft.Web.WebView2.WinForms.WebView2 webViewAdmin;
        private Button btnBrowseImage;
        private PictureBox pictureBoxProductImage;
        private Button btnGenerateReport;
        private FlowLayoutPanel flowLayoutPanelReport;
        private DateTimePicker dateTimePickerStrat;
        private DateTimePicker dateTimePickerEnd;
        private OxyPlot.WindowsForms.PlotView plotView;
        private Button CreateReport;
        private Panel panel1;
    }
}