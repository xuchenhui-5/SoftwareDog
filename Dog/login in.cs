using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dog
{
    public partial class login_in : Form
    {
        public login_in()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (tbAccount .Text == "Xugq1123" && tbPassword.Text == "Snake34760503,./")
            {
                //MessageBox.Show("登录成功");
                this.Visible = false;
                Form1 fm = new Form1();
                fm.CloseLoginEvent += new Form1.CloseLoginForm(this.close_form);//事件的注册
                fm.ShowDialog();
                
                
            }
            else if (tbAccount.Text == "Xugq1123" && tbPassword.Text != "Snake34760503,./")
            {
                MessageBox.Show("密码有误请重新输入密码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (tbAccount.Text != "Xugq1123")
            {
                MessageBox.Show("账户名有误请重新登录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        //事件处理程序
        public void close_form(object sender,FormClosedEventArgs e)
        {
            this.Close();
        }
    }
}
