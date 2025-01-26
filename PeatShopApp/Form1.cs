using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using PeatShopApp.Model;

namespace PeatShopApp
{
    public partial class Form1 : Form
    {
        private readonly UniversalContext context = new UniversalContext();
        private AdminFrom adminForm; // Preload the AdminFrom form
        private Login loginForm; // Preload the Login form

        // Separate PictureBox for each password field
        private PictureBox pictureBoxShowHidePasswordRegister; // For registration form
        private PictureBox pictureBoxShowHidePasswordLogin;   // For login form

        public Form1()
        {
            InitializeComponent();
            CreateAdminUser();
            this.FormClosed += Form1_FormClosed;
            webView.Dock = DockStyle.Fill;

            // Preload forms for fast navigation
            adminForm = new AdminFrom("Admin", 1); // Replace with actual role and user ID
            loginForm = new Login(""); // Replace with actual email

            // Initialize PictureBox for registration form
            pictureBoxShowHidePasswordRegister = new PictureBox
            {
                Size = new Size(24, 24), // Adjust size as needed
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = Image.FromFile("Images/eye-slash.png"), // Set to "eye-slash" initially
                Visible = false, // Initially hidden
                Cursor = Cursors.Hand // Change cursor to hand when hovering
            };
            pictureBoxShowHidePasswordRegister.Click += PictureBoxShowHidePasswordRegister_Click;

            // Initialize PictureBox for login form
            pictureBoxShowHidePasswordLogin = new PictureBox
            {
                Size = new Size(24, 24), // Adjust size as needed
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = Image.FromFile("Images/eye-slash.png"), // Set to "eye-slash" initially
                Visible = false, // Initially hidden
                Cursor = Cursors.Hand // Change cursor to hand when hovering
            };
            pictureBoxShowHidePasswordLogin.Click += PictureBoxShowHidePasswordLogin_Click;

            // Add PictureBox controls to the form
            this.Controls.Add(pictureBoxShowHidePasswordRegister);
            this.Controls.Add(pictureBoxShowHidePasswordLogin);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit(); // Close the entire application
        }

        private async void DesignLoadApp()
        {
            await webView.EnsureCoreWebView2Async();

            // Ensure correct path to the image
            string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "login.jpg");

            // Output the path for debugging
            Debug.WriteLine(imagePath);

            // Create a local HTML file with the image included
            string htmlFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "login.html");
            string htmlContent = $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <style>
                        body {{ font-family: Arial, sans-serif; background-color: #fcf3e5; }}
                        h1 {{ color: #333; margin: 50px; }}
                    </style>
                </head>
                <body>
                    <h1>Pet Shop</h1>
                    <div class='login'>
                    </div>
                </body>
                </html>";

            File.WriteAllText(htmlFilePath, htmlContent);

            // Load the local HTML file into the WebView
            webView.CoreWebView2.Navigate(htmlFilePath);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DesignLoadApp();
            HiddenLoginFrom();
            SetFormSizeAndPosition(this);

            // Set initial icon for the password visibility toggle
            pictureBoxShowHidePasswordRegister.Image = Image.FromFile("Images/eye-slash.png"); // Set to "eye-slash" initially
            pictureBoxShowHidePasswordLogin.Image = Image.FromFile("Images/eye-slash.png"); // Set to "eye-slash" initially

            // Hide PictureBox controls initially
            pictureBoxShowHidePasswordRegister.Visible = false;
            pictureBoxShowHidePasswordLogin.Visible = false;

            // Set the PictureBox location next to the active password field
            if (PasswordBox.Visible)
            {
                pictureBoxShowHidePasswordRegister.Location = new Point(PasswordBox.Location.X + PasswordBox.Width + 5, PasswordBox.Location.Y);
                pictureBoxShowHidePasswordRegister.Visible = true; // Ensure the PictureBox is visible
                pictureBoxShowHidePasswordRegister.BringToFront(); // Bring the PictureBox to the front
            }
            else if (PassLogin.Visible)
            {
                pictureBoxShowHidePasswordLogin.Location = new Point(PassLogin.Location.X + PassLogin.Width + 5, PassLogin.Location.Y);
                pictureBoxShowHidePasswordLogin.Visible = true; // Ensure the PictureBox is visible
                pictureBoxShowHidePasswordLogin.BringToFront(); // Bring the PictureBox to the front
            }
        }

        private void SetFormSizeAndPosition(Form form)
        {
            // Set the size of the form
            form.Size = new Size(1000, 600); // Adjust the size as needed

            // Center the form on the screen
            form.StartPosition = FormStartPosition.CenterScreen;
        }

        private async void Register_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(usernameBox.Text) || string.IsNullOrWhiteSpace(PhoneBox.Text) || string.IsNullOrWhiteSpace(EmailBox.Text) || string.IsNullOrWhiteSpace(PasswordBox.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!new EmailAddressAttribute().IsValid(EmailBox.Text))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!long.TryParse(PhoneBox.Text, out long phone))
            {
                MessageBox.Show("Please enter a valid phone number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (context.Users.Any(u => u.Email == EmailBox.Text))
            {
                MessageBox.Show("This email is already registered.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(PasswordBox.Text);
            try
            {
                var customer = new Customer
                {
                    Name = usernameBox.Text,
                    Email = EmailBox.Text,
                    Phone = phone,
                    Password = hashedPassword,
                };
                var user = new User
                {
                    UserName = usernameBox.Text,
                    Email = EmailBox.Text,
                    Password = hashedPassword,
                    Role = "Customer",
                    Customer = customer
                };

                await context.Customers.AddAsync(customer);
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

                // Show a welcome message
                string postRegistrationHtml = homePage(usernameBox.Text);
                webView.CoreWebView2.NavigateToString(postRegistrationHtml);
                Hidden();

                // Redirect based on user role
                if (user.Role == "Admin")
                {
                    adminForm = new AdminFrom(user.Role, user.Id); // Reinitialize if needed
                    adminForm.StartPosition = FormStartPosition.Manual;
                    adminForm.Location = this.Location;
                    adminForm.Show();
                    this.Hide();
                }
                else
                {
                    loginForm = new Login(user.Email); // Reinitialize if needed
                    loginForm.StartPosition = FormStartPosition.Manual;
                    loginForm.Location = this.Location;
                    loginForm.Show();
                    this.Hide();
                }

                MessageBox.Show("Registration successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show("An error occurred while saving the data. Please try again.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug.WriteLine(ex.Message);
            }
        }

        private string homePage(string username)
        {
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
                <p>Thank you, {username}!</p>
                <p>You have been successfully registered.</p>
            </div>
        </body>
        </html>";

            return home;
        }

        private void Hidden()
        {
            webViewRegister.Visible = false;
            usernameBox.Visible = false;
            PhoneBox.Visible = false;
            EmailBox.Visible = false;
            PasswordBox.Visible = false;
            Register.Visible = false;
            Login.Visible = false;
            label1.Visible = false;

            // Hide the PictureBox for registration form
            pictureBoxShowHidePasswordRegister.Visible = false;
        }

        private void HiddenLoginFrom()
        {
            webViewlogin.Visible = false;
            EmailLoginBox.Visible = false;
            PassLogin.Visible = false;
            LoginB.Visible = false;
            RegisterLable.Visible = false;

            // Hide the PictureBox for login form
            pictureBoxShowHidePasswordLogin.Visible = false;
        }

        private void Show()
        {
            webViewRegister.Visible = true;
            usernameBox.Visible = true;
            PhoneBox.Visible = true;
            EmailBox.Visible = true;
            PasswordBox.Visible = true;
            Register.Visible = true;
            Login.Visible = true;
            label1.Visible = true;
            HiddenLoginFrom();

            // Position the PictureBox next to PasswordBox
            pictureBoxShowHidePasswordRegister.Location = new Point(PasswordBox.Location.X + PasswordBox.Width + 5, PasswordBox.Location.Y);
            pictureBoxShowHidePasswordRegister.Visible = true; // Ensure the PictureBox is visible
            pictureBoxShowHidePasswordRegister.BringToFront(); // Bring the PictureBox to the front

            // Hide the login PictureBox
            pictureBoxShowHidePasswordLogin.Visible = false;
        }

        private void Login_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowLoginFrom();
            Hidden();
            LoginB.Visible = true;
            LoginB.BringToFront();
            RegisterLable.Visible = true;
            RegisterLable.BringToFront();
        }

        private void LoginToHome(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EmailLoginBox.Text) || string.IsNullOrWhiteSpace(PassLogin.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var userLogin = context.Users.FirstOrDefault(u => u.Email == EmailLoginBox.Text);

            if (userLogin != null && BCrypt.Net.BCrypt.Verify(PassLogin.Text, userLogin.Password))
            {
                this.Hide(); // Hide Form1

                if (userLogin.Role == "Admin")
                {
                    adminForm = new AdminFrom(userLogin.Role, userLogin.Id); // Reinitialize if needed
                    adminForm.StartPosition = FormStartPosition.Manual;
                    adminForm.Location = this.Location;
                    adminForm.Show();
                }
                else
                {
                    loginForm = new Login(userLogin.Email); // Reinitialize if needed
                    loginForm.StartPosition = FormStartPosition.Manual;
                    loginForm.Location = this.Location;
                    loginForm.Show();
                }
            }
            else
            {
                MessageBox.Show("Invalid email or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowLoginFrom()
        {
            webViewlogin.Visible = true;
            EmailLoginBox.Visible = true;
            PassLogin.Visible = true;
            LoginB.Visible = true;
            RegisterLable.Visible = true;

            // Position the PictureBox next to PassLogin
            pictureBoxShowHidePasswordLogin.Location = new Point(PassLogin.Location.X + PassLogin.Width + 5, PassLogin.Location.Y);
            pictureBoxShowHidePasswordLogin.Visible = true; // Ensure the PictureBox is visible
            pictureBoxShowHidePasswordLogin.BringToFront(); // Bring the PictureBox to the front

            // Hide the registration PictureBox
            pictureBoxShowHidePasswordRegister.Visible = false;
        }

        private void HomeForUser_Click(object sender, EventArgs e)
        {
            // Assuming you have the logged-in user's email stored in a variable
            string loggedInUserEmail = EmailLoginBox.Text; // Replace with the actual logged-in user's email

            // Pass the logged-in user's email to the Login form
            loginForm = new Login(loggedInUserEmail); // Reinitialize if needed
            loginForm.StartPosition = FormStartPosition.Manual;
            loginForm.Location = this.Location;
            loginForm.Show();
            this.Hide();
        }

        private async void CreateAdminUser()
        {
            string adminEmail = "a@a.com";
            string adminPassword = "1234";

            if (!context.Users.Any(u => u.Email == adminEmail))
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(adminPassword);
                var adminUser = new User
                {
                    UserName = "Admin",
                    Email = adminEmail,
                    Password = hashedPassword,
                    Role = "Admin"
                };

                await context.Users.AddAsync(adminUser);
                await context.SaveChangesAsync();
            }
        }

        private void RegisterLable_LinkClicked(object sender, EventArgs e)
        {
            Show();
        }

        private void PictureBoxShowHidePasswordRegister_Click(object sender, EventArgs e)
        {
            // Toggle visibility for PasswordBox
            PasswordBox.UseSystemPasswordChar = !PasswordBox.UseSystemPasswordChar;

            // Change the icon based on the visibility state
            pictureBoxShowHidePasswordRegister.Image = PasswordBox.UseSystemPasswordChar
                ? Image.FromFile("Images/eye-slash.png") // Replace with the path to your "eye-slash" icon
                : Image.FromFile("Images/eye.png"); // Replace with the path to your "eye" icon
        }

        private void PictureBoxShowHidePasswordLogin_Click(object sender, EventArgs e)
        {
            // Toggle visibility for PassLogin
            PassLogin.UseSystemPasswordChar = !PassLogin.UseSystemPasswordChar;

            // Change the icon based on the visibility state
            pictureBoxShowHidePasswordLogin.Image = PassLogin.UseSystemPasswordChar
                ? Image.FromFile("Images/eye-slash.png") // Replace with the path to your "eye-slash" icon
                : Image.FromFile("Images/eye.png"); // Replace with the path to your "eye" icon
        }

        private void pictureBoxShowHidePassword_Click(object sender, EventArgs e)
        {
            // Toggle visibility for PasswordBox
            PasswordBox.UseSystemPasswordChar = !PasswordBox.UseSystemPasswordChar;

            // Change the icon based on the visibility state
            pictureBoxShowHidePassword.Image = PasswordBox.UseSystemPasswordChar
                ? Image.FromFile("Images/eye-slash.png") // Replace with the path to your "eye-slash" icon
                : Image.FromFile("Images/eye.png"); // Replace with the path to your "eye" icon
        }

        private void pictureBoxShowHideLoginPassword_Click(object sender, EventArgs e)
        {
            // Toggle visibility for PassLogin
            PassLogin.UseSystemPasswordChar = !PassLogin.UseSystemPasswordChar;

            // Change the icon based on the visibility state
            pictureBoxShowHideLoginPassword.Image = PassLogin.UseSystemPasswordChar
                ? Image.FromFile("Images/eye-slash.png") // Replace with the path to your "eye-slash" icon
                : Image.FromFile("Images/eye.png"); // Replace with the path to your "eye" icon
        }
    }
}