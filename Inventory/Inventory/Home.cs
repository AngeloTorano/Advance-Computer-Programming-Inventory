using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Inventory
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
            Disable();
            InitializeTimer();
        }

        private void changeUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Disable();
            login.Enabled = true;
            panel2.Controls.Clear();
            loginForm newForm = new loginForm(this);
            newForm.ShowDialog();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            stockForm newForm = new stockForm();
            newForm.TopLevel = false;
            panel2.Controls.Add(newForm);
            newForm.BringToFront();
            newForm.Show();

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            loginForm newForm = new loginForm(this);
            newForm.ShowDialog();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            customerForm newForm = new customerForm();
            panel2.Controls.Clear();
            newForm.TopLevel = false;
            panel2.Controls.Add(newForm);
            newForm.BringToFront();
            newForm.Show();

        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            accountForm newForm = new accountForm();
            panel2.Controls.Clear();
            newForm.TopLevel = false;
            panel2.Controls.Add(newForm);
            newForm.BringToFront();
            newForm.Show();

        }

        public void Disable()
        {
            accountToolStripMenuItem.Enabled = false;
            stock.Enabled = false;
            customer.Enabled = false;
            items.Enabled = false;
        }

        public void EnableAllButtons()
        {
            accountToolStripMenuItem.Enabled = true;
            stock.Enabled = true;
            customer.Enabled = true;
            items.Enabled = true;
        }
        public void DisableLogin()
        {
            login.Enabled = false;
        }

        

        public void EnableStaffButtons()
        {
            //button enable for staff
            items.Enabled = true;
            accountToolStripMenuItem.Enabled = true;

            //button Disable for staff
            customer.Enabled = false;
            addToolStripMenuItem.Enabled = false;
            stock.Enabled = false;

        }

        private Timer timer;

        private void InitializeTimer()
        {
            // Create and configure the Timer
            timer = new Timer();
            timer.Interval = 1000; // Set the interval to 1 second (1000 milliseconds)
            timer.Tick += timer1_Tick;
            toolStripStatusLabel4.Text = DateTime.Now.ToString("M-dd-yyyy");
            // Start the Timer
            timer.Start();
        }

        
        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = DateTime.Now.ToString("HH:mm:ss");

        }

        private void items_Click(object sender, EventArgs e)
        {
            
            Item newForm = new Item();
            panel2.Controls.Clear();
            newForm.TopLevel = false;
            panel2.Controls.Add(newForm);
            newForm.BringToFront();
            newForm.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
