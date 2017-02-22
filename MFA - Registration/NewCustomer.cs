using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MultiFaceRec
{
    public partial class NewCustomer : Form
    {
        public NewCustomer()
        {
            InitializeComponent();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            bool done = confirmDetails();
            if (done)
                addCustomer();
        }

        private void addCustomer()
        {
            EntityModel db = new EntityModel();
            int defaultPin = Misc.GenerateOTP(4);
            int accountNo;
            do
            {
                accountNo = generateAccountNumber();
            } while (db.User.SingleOrDefault(x => x.account_number == accountNo) != null);

            user u = new user
            {
                lastname = textBoxLastName.Text,
                firstname = textBoxFirstName.Text,
                phone = textBoxPhone.Text,
                email = textBoxEmail.Text,
                pin = Misc.SHA1HashStringForUTF8String(defaultPin.ToString()),
                balance = 500000,
                date_created = DateTime.Now,
                account_number = accountNo
            };
            try
            {
                string message = "hello " + u.lastname.ToUpper() /*+ " " + u.firstname*/ + ", your account number is: " + accountNo + " and your default pin is " + defaultPin + ", feel free to change it anytime";
                bool msgSent = false;
                msgSent = Misc.SendText(u.phone, message);

                if (msgSent)
                {
                    db.User.Add(u);
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("Error creating account. Please retry");
                }

                FaceDetect form = new FaceDetect(u);
                form.ShowDialog();
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private int generateAccountNumber()
        {
            string acct = "010" + Misc.GenerateOTP(7);
            return Convert.ToInt32(acct);
        }

        private bool confirmDetails()
        {
            if (textBoxPhone.Text != "" && textBoxLastName.Text != "" && textBoxFirstName.Text != "")
                return true;

            MessageBox.Show("incomplete details");
            return false;
        }

        private void textBoxPhone_Leave(object sender, EventArgs e)
        {
            if (!validPhoneNumber(textBoxPhone.Text))
            {
                MessageBox.Show("phone number entered is invalid", "Invalid Phone", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxPhone.Clear();
                textBoxPhone.Focus();
            }
        }

        private bool validPhoneNumber(string p)
        {
            if (p != "")
            {
                Regex regex = new Regex(@"[+]?\d{11,13}");
                if (!regex.IsMatch(p))
                    return false;
            }
            return true;
        }

        private void textBoxEmail_Leave(object sender, EventArgs e)
        {
            if (!validEmail(textBoxEmail.Text))
            {
                MessageBox.Show("email entered is invalid", "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxEmail.Clear();
                textBoxEmail.Focus();
            }
        }

        private bool validEmail(string p)
        {
            if (p != "")
            {
                Regex regex = new Regex(@"([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})");
                if (!regex.IsMatch(p))
                    return false;
            }
            return true;
        }

    }
}
