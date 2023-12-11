using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Security.Cryptography;

namespace Inventory
{
    public partial class loginForm : Form
    {
        
        
        private Home form1;
        connection connection = new connection();
        method method = new method();
        public loginForm(Home form1)
        {
            InitializeComponent();
            this.form1 = form1;

        }

        private void logIn_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connection.conn());
            try
            {
                conn.Open();
                string query = "SELECT * FROM [User] WHERE username = @username AND password = @password";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    
                    cmd.Parameters.AddWithValue("@username", username.Text);
                    cmd.Parameters.AddWithValue("@password", method.HashPassword(password.Text));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string userType = reader["type"].ToString();

                            if (form1 != null)
                            {
                                form1.DisableLogin();

                                if (userType == "admin")
                                {
                                    form1.EnableAllButtons();
                                }
                                else if (userType == "staff")
                                {
                                    form1.EnableStaffButtons();
                                }

                                this.Hide();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Incorrect Username or Password. Please try again.");
                            username.Text = string.Empty;
                            password.Text = string.Empty;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        

        private void clear_Click(object sender, EventArgs e)
        {
            method.clearText(username);
            method.clearText(password);
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}