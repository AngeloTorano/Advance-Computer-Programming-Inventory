using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory
{
    internal class connection
    {
        public string conn()
        {
            return ConfigurationManager.ConnectionStrings["databaseCon"].ConnectionString;
        }
    }
}


