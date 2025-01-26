using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using OxyPlot;

namespace PeatShopApp.Model
{
    public partial class AdminFrom : Form
    {
        private readonly UniversalContext _context = new UniversalContext();
        private readonly string _currentUserRole; // Store the current user's role
        private readonly int _currentUserId; // Store the current user's ID
        private int _currentProductId = -1; // Track the product being edited
        public AdminFrom(string userRole, int currentUserId)
        {
            InitializeComponent();
            _currentUserRole = userRole; // Set the current user's role
            _currentUserId = currentUserId; // Set the current user's ID
            ConfigureFlowLayoutPanels(); // Configure FlowLayoutPanels for grid layout
            this.FormClosed += AdminFrom_FormClosed;
            SetFormSizeAndPosition(this);

            // Fill the text boxes with the product's data
            Productname.PlaceholderText = "Enter name Product";
            Description.PlaceholderText = "Enter Description";
            PriceBox.PlaceholderText = "Enter Price";
            StockBox.PlaceholderText = "Enter number of Stock";

            btnBrowseImage.ForeColor = Color.White;
            btnBrowseImage.BackColor = Color.Green;
            CreateReport.ForeColor = Color.White;
            CreateReport.BackColor = Color.Green;
            button6.BackColor = Color.Blue;
            button6.ForeColor = Color.White;
        }

        private void AdminFrom_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit(); // Close the entire application
        }

        private void ConfigureFlowLayoutPanels()
        {
            // Set up FlowLayoutPanel for Users
            flowLayoutPanelUsers.WrapContents = true;
            flowLayoutPanelUsers.AutoScroll = true;
            flowLayoutPanelUsers.FlowDirection = FlowDirection.LeftToRight;

            // Set up FlowLayoutPanel for Products
            flowLayoutPanelProducts.WrapContents = true;
            flowLayoutPanelProducts.AutoScroll = true;
            flowLayoutPanelProducts.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanelProducts.BackColor = ColorTranslator.FromHtml("#f2e8da");
            flowLayoutPanelUsers.BackColor = ColorTranslator.FromHtml("#f2e8da");
            flowLayoutPanelReport.BackColor = ColorTranslator.FromHtml("#f2e8da");
            groupBoxAddProduct.BackColor = ColorTranslator.FromHtml("#f2e8da");

        }

        private async void AdminFrom_Load(object sender, EventArgs e)
        {
            HideAllContainers();
            ShowProducts(); // Show all products when the form loads

            // Load the home page HTML
            string homeHtml = GenerateHomePageHtml("Admin"); // Replace with the actual username
            await webViewAdmin.EnsureCoreWebView2Async();
            webViewAdmin.CoreWebView2.NavigateToString(homeHtml);
            SetFormSizeAndPosition(this);
        }

        private void SetFormSizeAndPosition(Form form)
        {
            // Set the size of the form
            form.Size = new Size(1000, 600); // Adjust the size as needed

            // Center the form on the screen
            form.StartPosition = FormStartPosition.CenterScreen;
        }

        private void Logout_Click(object sender, EventArgs e)
        {
            this.Hide(); // Hide AdminFrom
            Form1 form1 = new Form1();
            form1.StartPosition = FormStartPosition.Manual; // Set StartPosition to Manual
            form1.Location = this.Location; // Match the location of AdminFrom
            form1.Show(); // Show Form1
        }

        private void ShowProducts(object sender, EventArgs e)
        {
            HideAllContainers();
            ShowProducts();
        }

        private void ShowUsers(object sender, EventArgs e)
        {
            HideAllContainers();
            ShowUsers();
        }

        private void LoadUsers()
        {
            flowLayoutPanelUsers.Controls.Clear();
            var users = _context.Users.ToList();

            foreach (var user in users)
            {
                var userCard = CreateCard(user.UserName, user.Email, user.Id, "User", null);
                flowLayoutPanelUsers.Controls.Add(userCard);
            }
        }

        private void LoadProducts()
        {
            try
            {
                Console.WriteLine("Loading products...");
                flowLayoutPanelProducts.Controls.Clear();
                var products = _context.Products.ToList();
                Console.WriteLine($"Found {products.Count} products.");

                if (products.Count == 0)
                {
                    MessageBox.Show("No products found in the database.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (var product in products)
                {
                    Console.WriteLine($"Product: {product.Name}, ImagePath: {product.ImagePath}");
                    var productCard = CreateCard(product.Name, $"Price: {product.Price:C}, Stock: {product.Stock}", product.Id, "Product", product.ImagePath);
                    flowLayoutPanelProducts.Controls.Add(productCard);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading products: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Panel CreateCard(string title, string description, int id, string type, string imagePath)
        {
            var card = new Panel
            {
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(10),
                Margin = new Padding(18),
                BackColor = Color.White,
                Size = new Size(250, 300) // Adjust size to fit the new layout
            };

            // Add the image
            if (type == "User")
            {
                // Use a default image for users if no image is provided
                var defaultImagePath = "Images/user.jpg"; // Relative path to the default image
                var imageToUse = !string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath) ? imagePath : defaultImagePath;

                if (System.IO.File.Exists(imageToUse))
                {
                    var pictureBox = new PictureBox
                    {
                        Size = new Size(150, 150), // Adjust image size
                        Location = new Point((card.Width - 150) / 2, 10), // Center the image horizontally
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Image = Image.FromFile(imageToUse) // Load the image from the file path
                    };
                    card.Controls.Add(pictureBox);
                }
            }
            else if (type == "Product")
            {
                // For products, use the provided image path
                if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
                {
                    var pictureBox = new PictureBox
                    {
                        Size = new Size(150, 150), // Adjust image size
                        Location = new Point((card.Width - 150) / 2, 10), // Center the image horizontally
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Image = Image.FromFile(imagePath) // Load the image from the file path
                    };
                    card.Controls.Add(pictureBox);
                }
            }

            // Add the title
            var lblTitle = new Label
            {
                Text = title,
                AutoSize = false, // Allow manual sizing
                Font = new Font("Arial", 12, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter, // Center the text
                Location = new Point(10, 170), // Below the image
                Size = new Size(card.Width - 20, 30) // Stretch across the card
            };
            card.Controls.Add(lblTitle);

            // Add the description
            var lblDescription = new Label
            {
                Text = description,
                AutoSize = false, // Allow manual sizing
                Font = new Font("Arial", 10),
                TextAlign = ContentAlignment.MiddleCenter, // Center the text
                Location = new Point(10, 200), // Below the title
                Size = new Size(card.Width - 20, 40) // Stretch across the card
            };
            card.Controls.Add(lblDescription);

            // Add Edit and Delete buttons for Admins
            if (_currentUserRole == "Admin")
            {
                // Only add the Edit button for Product cards
                if (type == "Product")
                {
                    var btnEdit = new Button
                    {
                        Text = "Edit",
                        Tag = new { Id = id, Type = type },
                        Size = new Size(80, 30),
                        ForeColor = Color.White,
                        BackColor = Color.Green,
                        Location = new Point((card.Width - 170) / 2, 250) // Center the buttons horizontally
                    };
                    btnEdit.Click += (s, ev) =>
                    {
                        var tag = (dynamic)btnEdit.Tag;
                        if (tag.Type == "Product")
                            EditProduct(tag.Id);
                    };
                    card.Controls.Add(btnEdit);
                }

                // Add the Delete button for both User and Product cards
                var btnDelete = new Button
                {
                    Text = "Delete",
                    Tag = new { Id = id, Type = type },
                    Size = new Size(80, 30),
                    BackColor = Color.Red,
                    ForeColor = Color.White,
                    Location = new Point((card.Width - 170) / 2 + 90, 250) // Place next to the Edit button
                };
                btnDelete.Click += (s, ev) =>
                {
                    var tag = (dynamic)btnDelete.Tag;
                    if (tag.Type == "User")
                        DeleteUser(tag.Id);
                    else if (tag.Type == "Product")
                        DeleteProduct(tag.Id);
                };
                card.Controls.Add(btnDelete);
            }

            return card;
        }
        private void EditUser(int userId)
        {
            if (_currentUserRole != "Admin")
            {
                MessageBox.Show("You do not have permission to edit users.", "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Prevent admin from editing their own account
            if (userId == _currentUserId)
            {
                MessageBox.Show("You cannot edit your own account.", "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                MessageBox.Show($"Edit user: {user.UserName}");
                // Implement user editing logic here
            }
        }

        private void DeleteUser(int userId)
        {
            if (_currentUserRole != "Admin")
            {
                MessageBox.Show("You do not have permission to delete users.", "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Prevent admin from deleting their own account
            if (userId == _currentUserId)
            {
                MessageBox.Show("You cannot delete your own account.", "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null && MessageBox.Show("Are you sure you want to delete this user?", "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
                LoadUsers();
            }
        }

        private void EditProduct(int productId)
        {
            if (_currentUserRole != "Admin")
            {
                MessageBox.Show("You do not have permission to edit products.", "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var product = _context.Products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                MessageBox.Show("Product not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Populate fields in AddProduct form for editing
            HideAllContainers();
            groupBoxAddProduct.Visible = true;
            groupBoxAddProduct.BringToFront();

            // Fill the text boxes with the product's data
            Productname.Text = product.Name;
            Description.Text = product.Description;
            PriceBox.Text = product.Price.ToString();
            StockBox.Text = product.Stock.ToString();

            // Load the image into the PictureBox
            if (!string.IsNullOrEmpty(product.ImagePath) && System.IO.File.Exists(product.ImagePath))
            {
                pictureBoxProductImage.Image = Image.FromFile(product.ImagePath);
                pictureBoxProductImage.SizeMode = PictureBoxSizeMode.Zoom; // Ensure the image fits within the PictureBox
            }
            else
            {
                pictureBoxProductImage.Image = null; // Clear the image if no valid path is found
            }

            pictureBoxProductImage.Tag = product.ImagePath; // Save the image path

            // Track the product being edited
            _currentProductId = product.Id;

            // Change the button text to "Update"
            button6.Text = "Update";
            button6.BackColor = Color.Green;
            button6.ForeColor = Color.White;
            // Unsubscribe previous event handlers to avoid multiple subscriptions

            button6.Click -= AddPro; // Unsubscribe AddPro
            button6.Click -= UpdateProduct; // Unsubscribe UpdateProduct (if already subscribed)
            button6.Click += UpdateProduct;
        }

        private void UpdateProduct(object sender, EventArgs e)
        {
            if (_currentProductId == -1)
            {
                MessageBox.Show("No product selected for update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(Productname.Text) ||
                string.IsNullOrWhiteSpace(Description.Text) ||
                string.IsNullOrWhiteSpace(PriceBox.Text) ||
                string.IsNullOrWhiteSpace(StockBox.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(PriceBox.Text, out decimal price) || !int.TryParse(StockBox.Text, out int stock))
            {
                MessageBox.Show("Please enter valid price and stock values.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var product = _context.Products.FirstOrDefault(p => p.Id == _currentProductId);
            if (product == null)
            {
                MessageBox.Show("Product not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Ensure the PictureBox displays the image in a consistent size
            if (pictureBoxProductImage.Image != null)
            {
                // Set the PictureBox size to 200x200 (or your desired size)
                product.ImagePath = pictureBoxProductImage.Tag?.ToString(); // Update the image path
                pictureBoxProductImage.Size = new Size(200, 200);
                pictureBoxProductImage.SizeMode = PictureBoxSizeMode.Zoom; // Zoom to fit
            }

            // Update product details
            product.Name = Productname.Text;
            product.Description = Description.Text;
            product.Price = price;
            product.Stock = stock;

            try
            {
                _context.SaveChanges();
                MessageBox.Show("Product updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadProducts();
                ClearTextBoxes(groupBoxAddProduct);
                _currentProductId = -1; // Reset the product ID
                button6.Text = "Add"; // Reset the button text
                button6.BackColor = Color.Blue;
                button6.ForeColor = Color.White;
                pictureBoxProductImage.Image = null; // Clear the image preview
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.InnerException?.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void DeleteProduct(int productId)
        {
            if (_currentUserRole != "Admin")
            {
                MessageBox.Show("You do not have permission to delete products.", "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var product = _context.Products.FirstOrDefault(p => p.Id == productId);
            if (product != null && MessageBox.Show("Are you sure you want to delete this product?", "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
                LoadProducts();
            }
        }

        private void HideAllContainers()
        {
            flowLayoutPanelUsers.Visible = false;
            flowLayoutPanelProducts.Visible = false;
            groupBoxAddProduct.Visible = false;
            flowLayoutPanelReport.Visible = false;
        }

        private void ShowUsers()
        {
            HideAllContainers();
            flowLayoutPanelUsers.Visible = true;
            flowLayoutPanelUsers.BringToFront(); // Bring to front
            LoadUsers();
        }

        private void ShowProducts()
        {
            HideAllContainers();
            flowLayoutPanelProducts.Visible = true;
            flowLayoutPanelProducts.BringToFront(); // Bring to front
            LoadProducts();
        }

        private void AddProduct(object sender, EventArgs e)
        {
            HideAllContainers();
            groupBoxAddProduct.Visible = true;
            groupBoxAddProduct.BringToFront(); // Bring to front
            ClearTextBoxes(groupBoxAddProduct);
            button6.Text = "Add"; // Reset the button text
            _currentProductId = -1; // Reset the product ID
                                    // Unsubscribe any existing handlers to avoid multiple subscriptions
            button6.Click -= UpdateProduct;
            button6.Click -= AddPro;

            // Subscribe to the AddPro method
            button6.Click += AddPro;
        }

        private void AddPro(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Productname.Text) || string.IsNullOrWhiteSpace(Description.Text) || string.IsNullOrWhiteSpace(PriceBox.Text) || string.IsNullOrWhiteSpace(StockBox.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(PriceBox.Text, out decimal price) || !int.TryParse(StockBox.Text, out int stock))
            {
                MessageBox.Show("Please enter valid price and stock values.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var product = new Product
                {
                    Name = Productname.Text,
                    Description = Description.Text,
                    Price = price,
                    Stock = stock,
                    ImagePath = pictureBoxProductImage.Image != null ? pictureBoxProductImage.Tag?.ToString() : null // Save the image path
                };

                _context.Products.Add(product);
                _context.SaveChanges();
                MessageBox.Show("Product added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadProducts();
                ClearTextBoxes(groupBoxAddProduct);
                pictureBoxProductImage.Image = null; // Clear the image preview
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.InnerException?.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearTextBoxes(Control container)
        {
            foreach (Control control in container.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.Text = string.Empty;
                }
            }
            if (pictureBoxProductImage != null)
            {
                pictureBoxProductImage.Image = null; // Use null instead of an empty string
            }
        }

        private string GenerateHomePageHtml(string username)
        {
            return $@"
        <!DOCTYPE html>
        <html>
        <head>
            <style>
                body {{ font-family: Arial, sans-serif; background-color: #fcf3e5; }}
                h1 {{ color: #333; text-align: center; }}
                .message {{ text-align: center; margin-top: 20px; }}
            </style>
        </head>
        <body>
            <h1>Welcome to Pet Shop!</h1>
            <div class='message'>
                <p>Welcome back, Admin {username}!</p>
            </div>
        </body>
        </html>";
        }

        private void btnBrowseImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Display the selected image in the PictureBox
                    pictureBoxProductImage.SizeMode = PictureBoxSizeMode.Zoom; // Ensure zoom mode
                    pictureBoxProductImage.Image = Image.FromFile(openFileDialog.FileName);
                    pictureBoxProductImage.Tag = openFileDialog.FileName; // Save the file path
                }
            }
        }

        private void GenerateReport_Click(object sender, EventArgs e)
        {
            flowLayoutPanelReport.Visible = true;
            HideAllContainers();

            flowLayoutPanelReport.Visible = true;
            flowLayoutPanelReport.BringToFront();


        }
        private void CreateRep(object sender, EventArgs e)
        {
            // Get the selected date range
            DateTime startDate = dateTimePickerStrat.Value;
            DateTime endDate = dateTimePickerEnd.Value;

            // Fetch data from the database
            var reportData = _context.Orders
                .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
                .GroupBy(o => o.OrderDate.Date) // Group by date
                .Select(g => new
                {
                    Date = g.Key,
                    TotalSales = g.Sum(o => o.TotalAmount)
                })
                .OrderBy(r => r.Date)
                .ToList();

            // Create a plot model
            var plotModel = new PlotModel { Title = "Sales Report" };

            // Create a line series
            var lineSeries = new LineSeries
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = 4,
                MarkerStroke = OxyColors.White
            };

            // Add data points to the series
            foreach (var data in reportData)
            {
                lineSeries.Points.Add(new OxyPlot.DataPoint(
                    DateTimeAxis.ToDouble(data.Date), // Convert DateTime to double
                    Convert.ToDouble(data.TotalSales) // Convert decimal to double
                ));
            }

            // Add the series to the plot model
            plotModel.Series.Add(lineSeries);

            // Add axes
            plotModel.Axes.Add(new DateTimeAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Date",
                StringFormat = "yyyy-MM-dd"
            });

            plotModel.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Total Sales"
            });

            // Assign the plot model to the PlotView
            plotView.Model = plotModel;

            // Clear existing labels in flowLayoutPanelReport (except PlotView)
            foreach (Control control in flowLayoutPanelReport.Controls)
            {
                if (control is Label)
                {
                    flowLayoutPanelReport.Controls.Remove(control);
                }
            }

            // Add a label to display the total sales
            var lblTotalSales = new Label
            {
                Text = $"Total Sales from {startDate.ToShortDateString()} to {endDate.ToShortDateString()}: {reportData.Sum(r => r.TotalSales):C}",
                AutoSize = true,
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.DarkBlue
            };
            flowLayoutPanelReport.Controls.Add(lblTotalSales);

            // Add a label for each day's sales
            foreach (var data in reportData)
            {
                var lblDailySales = new Label
                {
                    Text = $"{data.Date.ToShortDateString()}: {data.TotalSales:C}",
                    AutoSize = true,
                    Font = new Font("Arial", 10),
                    Margin = new Padding(5)
                };
                flowLayoutPanelReport.Controls.Add(lblDailySales);
            }

            // Show the flowLayoutPanelReport
            flowLayoutPanelReport.Visible = true;
        }

        private void groupBoxAddProduct_Enter(object sender, EventArgs e)
        {

        }
    }
}