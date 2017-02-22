﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MFA_Main
{
    public partial class Pin : Form
    {
        public Pin()
        {
            InitializeComponent();
        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            textBoxNewPin.Clear();
            textBoxNewPin.Focus();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            int newPin;
            try
            {
                newPin = int.Parse(textBoxNewPin.Text);
                using (EntityModel db = new EntityModel())
                {
                    LoggedInUser.User.pin = Misc.SHA1HashStringForUTF8String(newPin.ToString());
                    db.Entry(LoggedInUser.User).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                this.Hide();
                MFA_Bank form = new MFA_Bank();
                form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
