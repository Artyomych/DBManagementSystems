using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;

namespace EntityFrameworkApp
{
    public partial class OrdersForm : Form
    {
        ApplicationContext db;
        public OrdersForm()
        {
            InitializeComponent();
        }

        public OrdersForm(string login)
        {
            InitializeComponent();

            db = new ApplicationContext();
            db.Orders.Load();
            MessageBox.Show(login);
            OrdersDG.DataSource = db.Orders.Local.Where(m => m.CustomerLogin == login).ToList();
            username.Text = login;
        }
    }
}
