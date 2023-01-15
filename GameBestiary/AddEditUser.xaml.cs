using DataObjects;
using LogicLayer;
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

namespace GameBestiary
{
    /// <summary>
    /// Interaction logic for AddEditUser.xaml
    /// </summary>
    public partial class AddEditUser : Window
    {

        private UserManager _userManager = new UserManager();

        private UserAccount _userAccount = null;
        bool _addMode = false;
        public AddEditUser()
        {
            _addMode = true;
            InitializeComponent();
        }
        public AddEditUser(UserAccount userAccount)
        {
            if (userAccount == null)
            {
                _addMode = true;
            }
            else
            {
                _addMode = false;
            }
            _userAccount = userAccount;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_addMode)
            {
                setDetailAddMode();
            }
            else
            {
                setDetailMode();
            }
        }

        // helper methods
        private void populateControls()
        {
            txtUserAccountID.Text = _userAccount.UserAccountID.ToString();
            txtUserAccountID.IsEnabled = false;
            txtGivenName.Text = _userAccount.GivenName;
            txtFamilyName.Text = _userAccount.FamilyName;
            txtEmail.Text = _userAccount.Email;
            chkActive.IsChecked = _userAccount.Active;
        }

        private void setDetailAddMode()
        {
            txtUserAccountID.Visibility = Visibility.Hidden;
            txtUserAccountID.IsEnabled = false;
            txtGivenName.IsEnabled = false;
            txtFamilyName.IsEnabled = false;
            txtEmail.IsEnabled = false;
            lblActive.Visibility = Visibility.Hidden;
            chkActive.Visibility = Visibility.Hidden;

            btnEditSave.Content = "Begin";
            btnCancel.Content = "Close";
        }

        private void setDetailMode()
        {
            populateControls();

            txtUserAccountID.IsEnabled = false;
            txtGivenName.IsEnabled = false;
            txtFamilyName.IsEnabled = false;
            txtEmail.IsEnabled = false;
            chkActive.IsEnabled = false;

            btnEditSave.Content = "Edit";
            btnCancel.Content = "Close";
        }

        private void setBeginMode()
        {
            txtGivenName.IsEnabled = true;
            txtFamilyName.IsEnabled = true;
            txtEmail.IsEnabled = true;
            lblActive.Visibility = Visibility.Hidden;
            chkActive.Visibility = Visibility.Hidden;

            btnEditSave.Content = "Create";
            btnCancel.Content = "Exit";
        }

        private void setEditMode()
        {
            txtGivenName.IsEnabled = true;
            txtFamilyName.IsEnabled = true;
            txtEmail.IsEnabled = true;
            chkActive.IsEnabled = true;

            btnEditSave.Content = "Save";
            btnCancel.Content = "Exit";
        }

        private void btnEditSave_Click(object sender, RoutedEventArgs e)
        {
            if (btnEditSave.Content.ToString() == "Edit")
            {
                setEditMode();
                return;
            }
            if (btnEditSave.Content.ToString() == "Begin")
            {
                setBeginMode();
            }
            else
            {
                if (_addMode)
                {
                    if (String.IsNullOrEmpty(txtGivenName.Text))
                    {
                        MessageBox.Show("You must enter a Given Name.");
                        return;
                    }
                    if (String.IsNullOrEmpty(txtFamilyName.Text))
                    {
                        MessageBox.Show("You must enter a Family Name.");
                        return;
                    }
                    if (String.IsNullOrEmpty(txtEmail.Text))
                    {
                        MessageBox.Show("You must select a Email.");
                        return;
                    }
                    UserAccount userAccount = new UserAccount();
                    userAccount.GivenName = txtGivenName.Text;
                    userAccount.FamilyName = txtFamilyName.Text;
                    userAccount.Email = txtEmail.Text;
                    try
                    {
                        if (_userManager.CreateUser(userAccount))
                        {
                            this.DialogResult = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    if (String.IsNullOrEmpty(txtGivenName.Text))
                    {
                        MessageBox.Show("You must enter a Given Name.");
                        return;
                    }
                    if (String.IsNullOrEmpty(txtFamilyName.Text))
                    {
                        MessageBox.Show("You must enter a Family Name.");
                        return;
                    }
                    if (String.IsNullOrEmpty(txtEmail.Text))
                    {
                        MessageBox.Show("You must enter an Email.");
                        return;
                    }
                    // build a new UserAccount object
                    UserAccount userAccount = new UserAccount()
                    {
                        UserAccountID = _userAccount.UserAccountID,
                        GivenName = txtGivenName.Text,
                        FamilyName = txtFamilyName.Text,
                        Email = txtEmail.Text,
                        PasswordHash = _userAccount.PasswordHash,
                        Active = (bool)chkActive.IsChecked
                    };
                    try
                    {
                        if (_userManager.EditUser(_userAccount, userAccount))
                        {
                            this.DialogResult = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
