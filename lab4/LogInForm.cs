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
    public partial class LogInForm : Form
    {
        ApplicationContext db;
        public LogInForm()
        {
            InitializeComponent();
            db = new ApplicationContext();
            db.Users.Load();
        }

        private void LogIn_btn_Click(object sender, EventArgs e)
        {
            if(Login_tb.Text!="" && Password_tb.Text != "")
            {
                string login = Login_tb.Text;
                User user = db.Users.SingleOrDefault(m => m.Login == login);

                if (user == null)
                {
                    MessageBox.Show("Такого пользователя нет");
                }
                else if(user.Password != Password_tb.Text)
                {
                    MessageBox.Show("Неверный пароль");
                }
                else
                {
                    OrdersForm form = new OrdersForm(user.Login);
                    form.Show();

                    this.Close();
                }
            }
        }
    }
}
