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
    public partial class Withdrawal : Form
    {
        public Withdrawal()
        {
            InitializeComponent();
        }

        private void btnWithdrawalOk_Click(object sender, EventArgs e)
        {
            decimal withdrawalAmount = 0;
            try
            {
                withdrawalAmount = decimal.Parse(textBoxWithdrawalAmount.Text);

                if (withdrawalAmount > LoggedInUser.User.balance)
                    throw new Exception("Insufficient Balance");
                else
                {
                    LoggedInUser.User.balance -= withdrawalAmount;
                    using (EntityModel db = new EntityModel())
                    {
                        var u = db.User.Find(LoggedInUser.User.id);
                        u.balance = LoggedInUser.User.balance;
                        db.Entry(u).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    string message = "Acct: " + Misc.AccNoForText(LoggedInUser.User.account_number, 3) + "Amt: NGN" + withdrawalAmount.ToString() + " Dr\r\nDesc: Cash WDL FROM ATM\r\nAvail Bal: NGN" + LoggedInUser.User.balance.ToString();
                    var x = Misc.SendText(LoggedInUser.User.phone, message);
                    MessageBox.Show("Collect Cash");
                }
                this.Hide();
                MFA_Bank form = new MFA_Bank();
                form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PinLogin form = new PinLogin();
            form.Show();
            this.Hide();
        }
    }
}
