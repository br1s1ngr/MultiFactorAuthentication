using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MFA_Main
{
    public partial class Balance : Form
    {
        public Balance()
        {
            InitializeComponent();
        }

        private void Balance_Load(object sender, EventArgs e)
        {
            labelBalance.Text = LoggedInUser.User.balance.ToString();
        }

        //press back button
        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            MFA_Bank form = new MFA_Bank();
            form.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PinLogin form = new PinLogin();
            form.Show();
            this.Hide();
        }
    }
}
