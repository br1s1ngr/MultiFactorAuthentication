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
    public partial class OTPLogin : Form
    {
        user u;
        public OTPLogin(user u)
        {
            InitializeComponent();
            this.u = u;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            textBoxOTP.Clear();
            textBoxOTP.Focus();
        }

        private void buttonResendOTP_Click(object sender, EventArgs e)
        {
            sendOTP();
            MessageBox.Show("OTP resent, please wait");
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

        private void btnEnter_Click(object sender, EventArgs e)
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
                        if (LoggedInUser.User == u)
                        {
                            MessageBox.Show("Welcome " + u.lastname.ToUpperInvariant() + " " + u.firstname.ToUpperInvariant() + " !", "Login success", MessageBoxButtons.OK, MessageBoxIcon.None);
                            MFA_Bank mainForm = new MFA_Bank();
                            mainForm.Show();
                        }
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
