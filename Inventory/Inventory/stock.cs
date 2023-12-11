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

namespace Inventory
{
    public partial class stockForm : Form
    {
        connection connection = new connection();
        public stockForm()
        {
            InitializeComponent();
            reload();
        }

        
        public void saveEnable()
        {
            if(stock.Text != string.Empty && 
                itemID.Text != string.Empty && suppID.Text != string.Empty)
            {
                buttonSave.Enabled = true;
            }
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {

        }

        public void reload()
        {
            SqlConnection conn = new SqlConnection(connection.conn());
            string query = "SELECT * FROM [Stocks];";
            conn.Open();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);

                    // Assuming dataGridView1 is the name of your DataGridView control
                    stockList.DataSource = dataTable;
                    conn.Close();
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {

            try
            {
                SqlConnection conn = new SqlConnection(connection.conn());
                conn.Open();

                // Check if the provided SupplierId exists in the Supplier table
                int supplierIdToCheck = int.Parse(suppID.Text);
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

                int itemIdToCheck = int.Parse(suppID.Text);
                string checkItemQuery = "SELECT COUNT(*) FROM [Item] WHERE itemId = @itemID;";
                using (SqlCommand checkItemCommand = new SqlCommand(checkItemQuery, conn))
                {
                    checkItemCommand.Parameters.AddWithValue("@itemID", itemIdToCheck);

                    int ItemCount = (int)checkItemCommand.ExecuteScalar();

                    if (ItemCount == 0)
                    {
                        MessageBox.Show("Item ID not found. Please enter a valid Item ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Exit the method without executing the query
                    }
                }

                // If the SupplierId exists, proceed with the Stocks insertion
                string insertStocksQuery = "INSERT INTO [Stocks] (stock, remarks, Item_id, supplier_id) VALUES (@stock, @remarks, @ItemID, @SuppID);";
                using (SqlCommand com = new SqlCommand(insertStocksQuery, conn))
                {
                    com.Parameters.AddWithValue("@stock", stock.Text);
                    com.Parameters.AddWithValue("@remarks", remarks.Text);
                    com.Parameters.AddWithValue("@ItemID", int.Parse(itemID.Text)); // Parse as integer
                    com.Parameters.AddWithValue("@SuppID", supplierIdToCheck); // Use the checked SupplierId

                    // Use ExecuteNonQuery for INSERT, UPDATE, DELETE, etc.
                    int rowsAffected = com.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Data Saved!");
                        reload();
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                   
                    SqlConnection conn = new SqlConnection(connection.conn());
                    conn.Open();
                    string query = "DELETE FROM [Stocks] WHERE stockId = @ID;";

                    using (SqlCommand com = new SqlCommand(query, conn))
                    {
                        com.Parameters.AddWithValue("@ID", textBox1.Text);

                        // Use ExecuteNonQuery for DELETE
                        int rowsAffected = com.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Stock Deleted!");
                            reload(); // Reload your data after deletion
                        }
                        else
                        {
                            MessageBox.Show("Item not deleted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    conn.Close();
                
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
