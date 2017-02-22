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
    public partial class PinLogin : Form
    {
        user u;
        public PinLogin()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            textBoxAcctNumber.Clear();
            textBoxPin.Clear();
            textBoxAcctNumber.Focus();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (customerExists())
            {
                sendOTP();
                OTPLogin form = new OTPLogin(u);
                this.Hide();
                form.Show();
            }
            else {
                MessageBox.Show("Invalid Details");
            }
        }

        private void sendOTP()
        {
            int OTP = Misc.GenerateOTP(8);
            string message = "Please use the OTP code: " + OTP.ToString() + " to complete your login. OTP code expires after 10 minutes.";
            var x = Misc.SendText(u.phone, message);
            otp o = new otp
            {
                otp1 = OTP,
                userId = u.id,
                timesent = DateTime.Now,
                status = "pending",
            };
            EntityModel db = new EntityModel();
            db.OTP.Add(o);
            db.SaveChanges();
            //MessageBox.Show("message sent");
        }

        private bool customerExists()
        {
            EntityModel db = new EntityModel();
            try
            {
                int accNo = int.Parse(textBoxAcctNumber.Text);
                string pin = Misc.SHA1HashStringForUTF8String(textBoxPin.Text);
                u = db.User.SingleOrDefault(x => x.account_number == accNo && x.pin == pin);

                if (u != null)
                    return true;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            return false;
        }
    }
}
