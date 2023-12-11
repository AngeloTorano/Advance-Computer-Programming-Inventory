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
    public partial class customerForm : Form
    {
        connection connection = new connection();
        public customerForm()
        {
            InitializeComponent();
            reload();
        }

        private void dtgCus_itemlist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public void reload()
        {
            SqlConnection conn = new SqlConnection(connection.conn());
            string query = "SELECT DISTINCT(FirstName), LastName, address, contact, type FROM [Customer];";
            conn.Open();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);

                    // Assuming dataGridView1 is the name of your DataGridView control
                    customerList.DataSource = dataTable;
                    conn.Close();
                }
            }
        }

        private void search_TextChanged(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connection.conn());
            string query = "SELECT DISTINCT(FirstName), LastName, address, contact, type FROM [Customer] WHERE FirstName = @Fname";
            conn.Open();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                command.Parameters.AddWithValue("@Fname", search.Text);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);

                    // Assuming dataGridView1 is the name of your DataGridView control
                    customerList.DataSource = dataTable;
                }
                conn.Close(); // Move the connection close outside the inner using block
            }
        }
    }
}
