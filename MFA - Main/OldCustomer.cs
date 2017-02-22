using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MFA_Main
{
    public partial class Old_Customer : Form
    {
        user u;
        public Old_Customer()
        {
            InitializeComponent();
        }

        private void buttonAddCustomer_Click(object sender, EventArgs e)
        {
            if (customerExists())
            {
                sendOTP();
                (tabPage2 as Control).Enabled = true;
                (tabPage1 as Control).Enabled = false;
                tabControl1.SelectedTab = tabPage2;
            }

            else
                MessageBox.Show("invalid login details");
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
                int accNo = int.Parse(textBoxAccNO.Text);
                string pin = Misc.SHA1HashStringForUTF8String(textBoxPin.Text);
                u = db.User.SingleOrDefault(x => x.account_number == accNo && x.pin == pin);

                if (u != null)
                    return true;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            return false;
        }


        private void Old_Customer_Load(object sender, EventArgs e)
        {
            (tabPage2 as Control).Enabled = true;
        }

        private void linkLabelResendOTP_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            sendOTP();
            MessageBox.Show("OTP resent, please wait");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EntityModel db = new EntityModel();
            var xxx = (from x in db.OTP
                      where x.userId == u.id
                      orderby x.timesent descending
                      select x).ToList();

            otp otpInDb = xxx.ElementAt(0);
            if ((DateTime.Now - otpInDb.timesent).Minutes < 10)
            {
                try
                {
                    int otpEntered = int.Parse(textBoxOTP.Text);
                    if (otpInDb.otp1 == otpEntered)
                    {
                        FaceRecog form = new FaceRecog(u);
                        form.ShowDialog();
                        this.Hide();
                    }
                    else
                        MessageBox.Show("invalid OTP");
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            else
                MessageBox.Show("OTP has expired");
        }

    }
}
