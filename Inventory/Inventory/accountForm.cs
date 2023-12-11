using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory
{
    public partial class accountForm : Form
    {
        connection connection = new connection();
        string query;
        Home home = new Home();
        method method = new method();
        public accountForm()
        {
            InitializeComponent();
            disableNewAccount();
            reload();
        }

        private void btn_saveuser_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connection.conn());
                conn.Open();

                // Check for duplicate username
                string checkDuplicateQuery = "SELECT COUNT(*) FROM [User] WHERE username = @user;";
                using (SqlCommand checkDuplicateCommand = new SqlCommand(checkDuplicateQuery, conn))
                {
                    checkDuplicateCommand.Parameters.AddWithValue("@user", username.Text);

                    int duplicateCount = (int)checkDuplicateCommand.ExecuteScalar();

                    if (duplicateCount > 0)
                    {
                        MessageBox.Show("Username already exists. Please choose a different username.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Exit the method without executing the query
                    }
                }

                // If no duplicate, proceed with the insert
                string insertQuery = "INSERT INTO [User] (name, username, password, [type]) VALUES (@name, @user, @password, @type);";
                using (SqlCommand com = new SqlCommand(insertQuery, conn))
                {
                    com.Parameters.AddWithValue("@name", name.Text);
                    com.Parameters.AddWithValue("@user", username.Text);
                    com.Parameters.AddWithValue("@password", method.HashPassword(password.Text));
                    com.Parameters.AddWithValue("@type", type.Text);

                    // Use ExecuteNonQuery for INSERT, UPDATE, DELETE, etc.
                    int rowsAffected = com.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Data Saved!");
                        reload();
                        disableNewAccount();
                    }
                    else
                    {
                        MessageBox.Show("Data not saved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btn_New_Click(object sender, EventArgs e)
        {
            enableNewAccount();
        }

        public void disableNewAccount()
        {
            name.Enabled = false;
            username.Enabled = false;
            password.Enabled = false;
            type.Enabled = false;
        }
        public void enableNewAccount()
        {
            name.Enabled = true;
            username.Enabled = true;
            password.Enabled = true;
            type.Enabled = true;
        }

        private void accountForm_Load(object sender, EventArgs e)
        {

        }

        public void reload()
        {

            SqlConnection conn = new SqlConnection(connection.conn());
            conn.Open();
            string query = "SELECT name,username,password FROM [User]";
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);

                    // Assuming dataGridView1 is the name of your DataGridView control
                    listUser.DataSource = dataTable;
                    conn.Close();
                }
            }
        }

    }
}
