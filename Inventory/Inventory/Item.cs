using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Inventory
{
    public partial class Item : Form
    {
        connection connection = new connection();
        private string unit;
        public Item()
        {
            InitializeComponent();
            reload();
            Disable();
            buttonSave.Enabled = false;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void btnsave_Click(object sender, EventArgs e)
        {
           
        }

        private void comboUnit_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void reload()
        {
            SqlConnection conn = new SqlConnection(connection.conn());
            string query = "SELECT * FROM [Item];";
            conn.Open();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);

                    // Assuming dataGridView1 is the name of your DataGridView control
                    datagridItemList.DataSource = dataTable;
                    conn.Close();
                }
            }
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connection.conn());
                conn.Open();

                // Check if the provided SupplierId exists in the Supplier table
                int supplierIdToCheck = int.Parse(textBox1.Text);
                string checkSupplierQuery = "SELECT COUNT(*) FROM [Supplier] WHERE supplierId = @SupplierID;";
                using (SqlCommand checkSupplierCommand = new SqlCommand(checkSupplierQuery, conn))
                {
                    checkSupplierCommand.Parameters.AddWithValue("@SupplierID", supplierIdToCheck);

                    int supplierCount = (int)checkSupplierCommand.ExecuteScalar();

                    if (supplierCount == 0)
                    {
                        MessageBox.Show("Supplier ID not found. Please enter a valid Supplier ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Exit the method without executing the query
                    }
                }

                // If the SupplierId exists, proceed with the Item insertion
                string insertItemQuery = "INSERT INTO [Item] (itemName, quantity, price, unit, supplier_id) VALUES (@name, @quantity, @price, @unit, @suppID);";
                using (SqlCommand com = new SqlCommand(insertItemQuery, conn))
                {
                    com.Parameters.AddWithValue("@name", txtname.Text);
                    com.Parameters.AddWithValue("@quantity", int.Parse(txtquantity.Text));
                    com.Parameters.AddWithValue("@price", double.Parse(txtprice.Text));
                    com.Parameters.AddWithValue("@unit", cbounit.Text);
                    com.Parameters.AddWithValue("@suppID", supplierIdToCheck);

                    // Use ExecuteNonQuery for INSERT, UPDATE, DELETE, etc.
                    int rowsAffected = com.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Data Saved!");
                        buttonNew.Enabled = true;
                        buttonSave.Enabled = false;
                        Disable();
                        reload();
                    }
                    else
                    {
                        MessageBox.Show("Data not saved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                txtname.Clear();
                txtquantity.Clear();
                txtprice.Clear();
                textBox1.Clear();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            buttonSave.Enabled = true;
            textBox1.Enabled = true;
            txtname.Enabled = true;
            txtquantity.Enabled = true;
            txtprice.Enabled = true;
            cbounit.Enabled = true;
            buttonNew.Enabled = false;
        }

        public void Disable()
        {
            textBox1.Enabled = false;
            txtname.Enabled = false;
            txtquantity.Enabled = false;
            txtprice.Enabled = false;
            cbounit.Enabled = false;
        }
    }
}
