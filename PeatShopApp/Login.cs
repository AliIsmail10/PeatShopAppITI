using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.Logging;
using PeatShopApp.Model;

namespace PeatShopApp
{
    public partial class Login : Form
    {
        private readonly UniversalContext context = new UniversalContext();

        // Store the logged-in user's email (passed from the login form)
        private string loggedInUserEmail;
        private ToolTip productToolTip = new ToolTip(); // Add this at the class level
        public Login(string userEmail) // Constructor to accept the logged-in user's email
        {
            InitializeComponent();
            loggedInUserEmail = userEmail; // Set the logged-in user's email
            this.FormClosed += LoginForm_FormClosed;
            SetFormSizeAndPosition(this);

            flowLayoutPanelProducts.BackColor = ColorTranslator.FromHtml("#f2e8da");
            flowLayoutPanelProfile.BackColor = ColorTranslator.FromHtml("#f2e8da");

        }
        private async void Login_Load(object sender, EventArgs e)
        {
            HideAllContainers();
            ShowProducts();

            // Ensure the WebView is initialized
            await webViewLogin.EnsureCoreWebView2Async();

            // Retrieve the logged-in user's details
            var loggedInUser = context.Users
                .Where(u => u.Email == loggedInUserEmail) // Filter by the logged-in user's email
                .Select(u => new { u.Email, u.UserName }) // Select email and username
                .FirstOrDefault(); // Retrieve the first matching user (or null if not found)

            if (loggedInUser == null)
            {
                MessageBox.Show("User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Generate the HTML content using the homePage function
            string postRegistrationHtml = homePage(loggedInUser.UserName); // Pass the user's name

            // Load the HTML content into the WebView
            webViewLogin.CoreWebView2.NavigateToString(postRegistrationHtml);
        }
        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit(); // Close the entire application
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
            this.Hide(); // Hide the current form
            Form1 form1 = new Form1();
            form1.StartPosition = FormStartPosition.Manual; // Set StartPosition to Manual
            form1.Location = this.Location; // Match the location of the current form
            form1.Show(); // Show Form1
        }
        private void ShowProducts()
        {
            HideAllContainers();
            flowLayoutPanelProducts.Visible = true;
            flowLayoutPanelProducts.BringToFront(); // Bring to front
            LoadProducts();
        }

        private void LoadProducts()
        {
            flowLayoutPanelProducts.Controls.Clear();
            var products = context.Products.ToList();

            if (products.Count == 0)
            {
                MessageBox.Show("No products found in the database.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (var product in products)
            {
                var productCard = CreateCard(product.Name, product.Description, product.Id, "Product", product.ImagePath, product.Price, product.Stock);

                flowLayoutPanelProducts.Controls.Add(productCard);
            }
        }

        private Panel CreateCard(string title, string description, int id, string type, string imagePath, decimal price, int stock)
        {
            var card = new Panel
            {
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(10),
                Margin = new Padding(10),
                BackColor = Color.White,
                Size = new Size(200, 350) // Adjust size to fit the new layout
            };

            // Add the image
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

            // Add the title (product name)
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

            // Add the price
            var lblPrice = new Label
            {
                Text = $"Price: {price:C}", // Format as currency
                AutoSize = false, // Allow manual sizing
                Font = new Font("Arial", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter, // Center the text
                Location = new Point(10, 240), // Below the description
                Size = new Size(card.Width - 20, 20) // Stretch across the card
            };
            card.Controls.Add(lblPrice);

            // Add the stock
            var lblStock = new Label
            {
                Text = $"Stock: {stock}", // Display stock
                AutoSize = false, // Allow manual sizing
                Font = new Font("Arial", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter, // Center the text
                Location = new Point(10, 260), // Below the price
                Size = new Size(card.Width - 20, 20) // Stretch across the card
            };
            card.Controls.Add(lblStock);

            // Add a "Buy Now" button
            var btnBuyNow = new Button
            {
                Text = "Buy Now",
                Tag = new { Id = id, Type = type },
                Size = new Size(80, 30),
                ForeColor = Color.White,
                BackColor = Color.Red,
                Location = new Point((card.Width - 80) / 2, 290) // Center the button horizontally
            };
            btnBuyNow.Click += (s, ev) =>
            {
                var tag = (dynamic)btnBuyNow.Tag;
                if (tag.Type == "Product")
                {
                    MakeOrder(tag.Id, lblStock); // Pass the stock label to update it
                }
            };
            card.Controls.Add(btnBuyNow);

            // Add a tooltip for the card
            card.MouseHover += (s, ev) =>
            {
                string tooltipText = $"Name: {title}\nDescription: {description}\nPrice: {price:C}\nStock: {stock}";
                productToolTip.SetToolTip(card, tooltipText);
            };

            return card;
        }
        private void MakeOrder(int productId, Label stockLabel)
        {
            // Retrieve the logged-in user
            var loggedInUser = context.Users
                .Include(u => u.Customer) // Include the Customer navigation property
                .FirstOrDefault(u => u.Email == loggedInUserEmail);

            if (loggedInUser == null)
            {
                MessageBox.Show("User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Ensure the user has a corresponding Customer record
            if (loggedInUser.Customer == null)
            {
                MessageBox.Show("No customer record found for the logged-in user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Retrieve the selected product
            var product = context.Products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                MessageBox.Show("Product not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if the product is in stock
            if (product.Stock <= 0)
            {
                MessageBox.Show("This product is out of stock.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Create a new order
            var order = new order
            {
                OrderDate = DateTime.Now,
                TotalAmount = product.Price, // Assuming the user buys one unit
                CustomerId = loggedInUser.Customer.Id, // Use the CustomerId from the logged-in user
                OrderItems = new List<OrderItem>
                {
                    new OrderItem
                    {
                        ProductId = product.Id,
                        Quantity = 1, // Assuming the user buys one unit
                        Price = product.Price
                    }
                }
            };

            // Update the product stock
            product.Stock -= 1;

            try
            {
                // Save the order and update the product in the database
                context.Orders.Add(order);
                context.SaveChanges();

                // Update the stock label on the card
                stockLabel.Text = $"Price: {product.Price:C}, Stock: {product.Stock}";

                MessageBox.Show("Order placed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (DbUpdateException ex)
            {
                // Handle database update errors
                MessageBox.Show($"An error occurred while saving the order: {ex.InnerException?.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HideAllContainers()
        {
            flowLayoutPanelProducts.Visible = false;
        }
        private string homePage(string userName)
        {
            // Create HTML content for the logged-in user
            string home =
                            $@"
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
                        <p>Welcome back, {userName}!</p>
                        <p>Email: {loggedInUserEmail}</p>
                    </div>
                </body>
                </html>";

            return home;
        }        // Add the missing methods to avoid errors
        private void MyProduct(object sender, EventArgs e)
        {
            HideAllContainers();
            flowLayoutPanelProducts.Visible = true;
            flowLayoutPanelProducts.BringToFront(); // Bring to front
            getMyProduct();
        }

        private void getMyProduct()
        {
            flowLayoutPanelProducts.Controls.Clear();

            // Group OrderItems by ProductId and calculate the total quantity for each product
            var productQuantities = context.OrderItems
                .Include(oi => oi.Order) // Include the Order navigation property
                .Where(oi => oi.Order.Customer.User.Email == loggedInUserEmail) // Filter by the logged-in user's email
                .GroupBy(oi => oi.ProductId) // Group by ProductId
                .Select(g => new
                {
                    ProductId = g.Key,
                    TotalQuantity = g.Sum(oi => oi.Quantity) // Calculate the total quantity for each product
                })
                .ToList();

            if (productQuantities.Count == 0)
            {
                MessageBox.Show("No products found in the database.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Retrieve the product details for the grouped ProductIds
            var products = context.Products
                .Where(p => productQuantities.Select(pq => pq.ProductId).Contains(p.Id))
                .ToList();

            // Create a card for each product
            foreach (var product in products)
            {
                // Find the total quantity for the current product
                var productQuantity = productQuantities.FirstOrDefault(pq => pq.ProductId == product.Id);

                // Create a card for the product
                var productCard = CreateProductCard(product, productQuantity?.TotalQuantity ?? 0);
                flowLayoutPanelProducts.Controls.Add(productCard);
            }
        }

        private Panel CreateProductCard(Product product, int totalQuantity)
        {
            var card = new Panel
            {
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(10),
                Margin = new Padding(10),
                BackColor = Color.White,
                Size = new Size(250, 120) // Adjusted size to fit the content
            };

            // Create labels for product name, price, stock, and total quantity
            var lblTitle = new Label
            {
                Text = product.Name,
                AutoSize = true,
                Font = new Font("Arial", 12, FontStyle.Bold),
                Location = new Point(10, 10)
            };

            var lblDescription = new Label
            {
                Text = $"Price: {product.Price:C}",
                AutoSize = true,
                Font = new Font("Arial", 10),
                MaximumSize = new Size(230, 40),
                Location = new Point(10, 40)
            };

            var lblTotalQuantity = new Label
            {
                Text = $"Total Ordered: {totalQuantity}",
                AutoSize = true,
                Font = new Font("Arial", 10, FontStyle.Italic),
                ForeColor = Color.Green,
                Location = new Point(10, 70)
            };


            // Add controls to the card
            card.Controls.Add(lblTitle);
            card.Controls.Add(lblDescription);
            card.Controls.Add(lblTotalQuantity);

            return card;
        }
        private void MyCard(object sender, EventArgs e)
        {
            HideAllContainers();
            flowLayoutPanelProducts.Visible = true;
            flowLayoutPanelProducts.BringToFront();

            // Clear existing controls in the panel
            flowLayoutPanelProducts.Controls.Clear();

            // Calculate the total price of all orders for the logged-in user
            decimal totalPrice = context.Orders
                .Include(o => o.Customer) // Include Customer for filtering
                .Where(o => o.Customer.User.Email == loggedInUserEmail) // Filter by logged-in user
                .Sum(o => o.TotalAmount);

            // Create a big label to display the total price
            var lblTotalPrice = new Label
            {
                Text = $"Total Price of All Orders: {totalPrice:C}",
                AutoSize = true,
                Font = new Font("Arial", 18, FontStyle.Bold), // Big font size
                ForeColor = Color.DarkBlue, // Dark blue color
                TextAlign = ContentAlignment.MiddleCenter, // Center alignment
                Dock = DockStyle.Top, // Dock at the top
                Margin = new Padding(10) // Add margin for spacing
            };

            // Add the total price label to the flowLayoutPanelProducts
            flowLayoutPanelProducts.Controls.Add(lblTotalPrice);

            // Retrieve all orders for the logged-in user
            var orders = context.Orders
                .Include(o => o.OrderItems) // Include OrderItems
                .ThenInclude(oi => oi.Product) // Include Product for each OrderItem
                .Where(o => o.Customer.User.Email == loggedInUserEmail) // Filter by logged-in user
                .ToList();

            // Create a panel for each order
            foreach (var order in orders)
            {
                // Create a panel for the order
                var orderPanel = new Panel
                {
                    BorderStyle = BorderStyle.FixedSingle,
                    Padding = new Padding(10),
                    Margin = new Padding(10),
                    BackColor = Color.White,
                    Size = new Size(flowLayoutPanelProducts.Width - 30, 150) // Adjust width to fit container
                };

                // Add order date and total amount
                var lblOrderDate = new Label
                {
                    Text = $"Order Date: {order.OrderDate.ToString("yyyy-MM-dd HH:mm")}",
                    AutoSize = true,
                    Font = new Font("Arial", 12, FontStyle.Bold),
                    Location = new Point(10, 10)
                };

                var lblOrderTotal = new Label
                {
                    Text = $"Order Total: {order.TotalAmount:C}",
                    AutoSize = true,
                    Font = new Font("Arial", 12, FontStyle.Bold),
                    Location = new Point(10, 40)
                };

                // Add a list of products in the order
                var lblProducts = new Label
                {
                    Text = "Products:",
                    AutoSize = true,
                    Font = new Font("Arial", 10, FontStyle.Underline),
                    Location = new Point(10, 70)
                };

                var productList = new StringBuilder();
                foreach (var orderItem in order.OrderItems)
                {
                    productList.AppendLine($"{orderItem.Product.Name} - {orderItem.Quantity} x {orderItem.Price:C}");
                }

                var lblProductDetails = new Label
                {
                    Text = productList.ToString(),
                    AutoSize = true,
                    Font = new Font("Arial", 10),
                    Location = new Point(20, 90)
                };

                // Add a "Delete" button for the order
                var DeleteFromCard = new Button
                {
                    Text = "Delete",
                    Tag = order.Id, // Assign the specific order ID to the Tag property
                    Size = new Size(80, 30),
                    ForeColor = Color.Red,
                    Location = new Point(500, 60) // Center the button horizontally
                };
                DeleteFromCard.Click += (s, ev) =>
                {
                    var orderId = (int)DeleteFromCard.Tag; // Cast the Tag to int
                    RemoveProductFromCard(orderId); // Pass the order ID to the method
                };

                // Add controls to the order panel
                orderPanel.Controls.Add(lblOrderDate);
                orderPanel.Controls.Add(lblOrderTotal);
                orderPanel.Controls.Add(lblProducts);
                orderPanel.Controls.Add(lblProductDetails);
                orderPanel.Controls.Add(DeleteFromCard);

                // Add the order panel to the flowLayoutPanelProducts
                flowLayoutPanelProducts.Controls.Add(orderPanel);
            }
        }
        private void RemoveProductFromCard(int orderId)
        {
            var order = context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefault(o => o.Id == orderId);

            if (order != null && MessageBox.Show("Are you sure you want to delete this order?", "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // Increment the stock for each product in the order
                foreach (var orderItem in order.OrderItems)
                {
                    var product = context.Products.FirstOrDefault(p => p.Id == orderItem.ProductId);
                    if (product != null)
                    {
                        product.Stock += orderItem.Quantity; // Increment the stock
                    }
                }

                context.Orders.Remove(order);
                context.SaveChanges();
                MyCard(null, null); // Refresh the card view
            }
        }
        private void getAllProduct(object sender, EventArgs e)
        {
            HideAllContainers();
            flowLayoutPanelProducts.Visible = true;
            flowLayoutPanelProducts.BringToFront(); // Bring to front
            LoadProducts();
        }
        private void GetProfile(object sender, EventArgs e)
        {
            HideAllContainers();

            // Fetch the user profile for the logged-in user
            var getMyProfile = context.Users
                .Include(u => u.Customer) // Include the related Customer entity
                .FirstOrDefault(u => u.Email == loggedInUserEmail); // Filter by logged-in user's email

            // Ensure the profile exists
            if (getMyProfile == null)
            {
                MessageBox.Show("Profile not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Show the profile container
            flowLayoutPanelProfile.Visible = true;
            flowLayoutPanelProfile.BringToFront();

            // Clear existing controls in the panel
            flowLayoutPanelProfile.Controls.Clear();

            // Create and add a TextBox for the user's name
            var txtName = new TextBox
            {
                Text = getMyProfile.UserName, // Use UserName or Customer.Name depending on your model
                Font = new Font("Arial", 12),
                ForeColor = Color.Black,
                Margin = new Padding(10),
                Width = 200 // Set a fixed width for the TextBox
            };
            flowLayoutPanelProfile.Controls.Add(new Label { Text = "Name:", AutoSize = true, Font = new Font("Arial", 12, FontStyle.Bold), Margin = new Padding(10) });
            flowLayoutPanelProfile.Controls.Add(txtName);

            // Create and add a TextBox for the user's email
            var txtEmail = new TextBox
            {
                Text = getMyProfile.Email,
                Font = new Font("Arial", 12),
                ForeColor = Color.Black,
                Margin = new Padding(10),
                Width = 200
            };
            flowLayoutPanelProfile.Controls.Add(new Label { Text = "Email:", AutoSize = true, Font = new Font("Arial", 12, FontStyle.Bold), Margin = new Padding(10) });
            flowLayoutPanelProfile.Controls.Add(txtEmail);

            // Create and add a TextBox for the user's phone (if Customer exists)
            TextBox txtPhone = null;
            if (getMyProfile.Customer != null)
            {
                txtPhone = new TextBox
                {
                    Text = getMyProfile.Customer.Phone.ToString(),
                    Font = new Font("Arial", 12),
                    ForeColor = Color.Black,
                    Margin = new Padding(10),
                    Width = 200
                };
                flowLayoutPanelProfile.Controls.Add(new Label { Text = "Phone:", AutoSize = true, Font = new Font("Arial", 12, FontStyle.Bold), Margin = new Padding(10) });
                flowLayoutPanelProfile.Controls.Add(txtPhone);
            }

            // Create and add a "Save" button
            var btnSave = new Button
            {
                Text = "Save",
                ForeColor = Color.White,
                BackColor = Color.Green,
                Font = new Font("Arial", 12, FontStyle.Bold),
                Size = new Size(100, 40),
                Margin = new Padding(10)
            };
            btnSave.Click += async (s, ev) =>
            {
                // Save the updated profile data
                getMyProfile.UserName = txtName.Text;
                getMyProfile.Email = txtEmail.Text;

                if (getMyProfile.Customer != null && txtPhone != null)
                {
                    if (long.TryParse(txtPhone.Text, out long phoneNumber))
                    {
                        getMyProfile.Customer.Phone = phoneNumber; // Assign the parsed long value
                    }
                    else
                    {
                        MessageBox.Show("Invalid phone number. Please enter a valid number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Stop further execution if the phone number is invalid
                    }
                }

                try
                {
                    // Save changes to the database
                    context.SaveChanges();

                    // Update the logged-in user's email if it was changed
                    loggedInUserEmail = txtEmail.Text;

                    // Generate the updated HTML content
                    string updatedHtml = homePage(txtName.Text);

                    // Reload the WebView with the updated HTML content
                    await webViewLogin.EnsureCoreWebView2Async(); // Ensure the WebView is initialized
                    webViewLogin.CoreWebView2.NavigateToString(updatedHtml);

                    // Show success message
                    MessageBox.Show("Profile updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Optionally, you can refresh the profile data to reflect the changes
                    GetProfile(null, null); // Reload the profile data
                }
                catch (DbUpdateException ex)
                {
                    // Handle database update errors (e.g., duplicate email)
                    MessageBox.Show("An error occurred while saving the profile. Please check the email address and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    // Handle other exceptions
                    MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
            flowLayoutPanelProfile.Controls.Add(btnSave);
        }
        private void webViewLogin_Click(object sender, EventArgs e)
        {

        }
    }
}