using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DataObjects;
using LogicLayer;

namespace GameBestiary
{
    /// <summary>
    /// Interaction logic for UpdatePasswordWindow.xaml
    /// </summary>
    public partial class UpdatePasswordWindow : Window
    {
        UserManager _userManager = null;
        UserAccount _userAccount = null;
        bool isNewUser = false;

        public UpdatePasswordWindow(UserManager userManager,
                                    UserAccount userAccount,
                                    string instructions,
                                    bool newUser = false)
        {
            _userManager = userManager;
            _userAccount = userAccount;
            isNewUser = newUser;

            InitializeComponent();

            txtInstructions.Text = instructions;
            pwdOldPassword.Focus();

            if (isNewUser)
            {
                pwdOldPassword.Password = "newuser";
                pwdOldPassword.IsEnabled = false;
                pwdNewPassword.Focus();
            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string newPassword = pwdNewPassword.Password;
                string oldPassword = pwdOldPassword.Password;
                string retypePassword = pwdRetypePassword.Password;

                if (newPassword != retypePassword)
                {
                    MessageBox.Show("Passwords must match.");
                    pwdNewPassword.Password = "";
                    pwdRetypePassword.Password = "";
                    pwdNewPassword.Focus();

                    return;
                }

                if (_userManager.ResetPassword(_userAccount.Email, oldPassword, newPassword))
                {
                    // worked!
                    MessageBox.Show("Password was reset.");
                    this.DialogResult = true;
                }
                else
                {
                    throw new ApplicationException("Reset failed.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}