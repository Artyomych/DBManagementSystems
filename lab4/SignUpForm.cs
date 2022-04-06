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
    public partial class SignUpForm : Form
    {
        ApplicationContext db;
        public SignUpForm()
        {
            InitializeComponent();

            db = new ApplicationContext();
            db.Users.Load();
        }

        private void SignUp_btn_Click(object sender, EventArgs e)
        {
            if(Login_tb.Text!="" && Password_tb.Text!="" && Role_tb.Text!="" && Name_tb.Text != "")
            {
                User temp = new User();
                temp.Login = Login_tb.Text;
                temp.Password = Password_tb.Text;
                temp.Role = Role_tb.Text;
                temp.Name = Name_tb.Text;
                //temp.Order = null;
                db.Users.Add(temp);
                db.SaveChanges();
                MessageBox.Show("Регистрация прошла успешно");
                this.Close();
            }
            else
            {
                MessageBox.Show("Не удалось зарегистрировать пользователя");
            }
        }
    }
}
