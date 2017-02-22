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
    public partial class MFA_Bank : Form
    {
        public MFA_Bank()
        {
            InitializeComponent();
        }

        private void buttonWithdrawal_Click(object sender, EventArgs e)
        {
            Withdrawal form = new Withdrawal();
            form.Show();
            this.Hide();
        }

        private void btnBalance_Click(object sender, EventArgs e)
        {
            Balance form = new Balance();
            form.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Pin form = new Pin();
            form.Show();
            this.Hide();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PinLogin form = new PinLogin();
            form.Show();
            this.Hide();
        }

        private void btnAirtime_Click(object sender, EventArgs e)
        {
            Airtime form = new Airtime();
            form.Show();
            this.Hide();
        }
    }
}
