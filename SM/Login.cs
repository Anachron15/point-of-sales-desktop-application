using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SMLIB.Initializer;
using SMLIB.Repository;
using SMLIB.View;
using SMLIB.Context;
using System.Diagnostics;

namespace SM
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            txtEmail.Focus();
            Database.SetInitializer<SMLIB.Context.Context>(new Initializer());
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtEmail.Text != "" && txtPassword.Text != "")
                {
                    List<vwUserDetail> user = UserRepo.login(txtEmail.Text, txtPassword.Text);

                    if (user.Count > 0)
                    {
                        if (user[0].UserRole == "cashier" && user[0].UserStatus == "active")
                        {
                            Cashier cashier = new Cashier(user);
                            cashier.Show();
                            this.Hide();

                        }
                        else if (user[0].UserRole == "admin" && user[0].UserStatus == "active")
                        {
                            Admin admin = new Admin(user);
                            admin.Show();
                            this.Hide();

                        }
                    }
                    else
                    {
                        lblWarning.Visible = true;
                        lblWarning.Text = ("Login has failed, please try again.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An erro has occured. Please contact your system administrator" + ex.Message);
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                this.Hide();
                Process[] process = Process.GetProcessesByName("SM");
                process[0].Kill();

            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

            if (txtPassword.PasswordChar == '*')
            {
                txtPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '*';
            }
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }
    }
}
