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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfPresentation;

namespace GameBestiary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UserAccount _userAccount = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            hideAllUserTabs();
            txtEmail.Focus();
            btnLogin.IsDefault = true;
        }

        private void updateUIforLogOut()
        {
            _userAccount = null;

            lblGreeting.Content = "You are not logged in.";
            statMessage.Content = "Welcome. Please log in.";

            txtEmail.Visibility = Visibility.Visible;
            lblEmail.Visibility = Visibility.Visible;
            pwdPassword.Visibility = Visibility.Visible;
            lblPassword.Visibility = Visibility.Visible;

            btnLogin.Content = "Login";
            btnLogin.IsDefault = true;

            hideAllUserTabs();

            txtEmail.Focus();
        }

        private void updateUIforUser()
        {
            string rolesList = "";
            for (int i = 0; i < _userAccount.Roles.Count; i++)
            {
                rolesList += " " + _userAccount.Roles[i];
                if (i == _userAccount.Roles.Count - 2)
                {
                    if (_userAccount.Roles.Count > 2)
                    {
                        rolesList += ",";
                    }
                    rolesList += " and";
                }
                else if (i < _userAccount.Roles.Count - 2)
                {
                    rolesList += ",";
                }
            }
            lblGreeting.Content = "Welcome, " + _userAccount.GivenName +
                ". You are logged in as: " + rolesList + ".";

            statMessage.Content = "Logged in on " +
                DateTime.Now.ToLongDateString() + ", " +
                DateTime.Now.ToShortTimeString() +
                ". Please remember to log out before you leave.";

            txtEmail.Text = "";
            txtEmail.Visibility = Visibility.Hidden;
            lblEmail.Visibility = Visibility.Hidden;
            pwdPassword.Password = "";
            pwdPassword.Visibility = Visibility.Hidden;
            lblPassword.Visibility = Visibility.Hidden;

            btnLogin.Content = "Log Out";
            btnLogin.IsDefault = false;

            showTabsForUser();
        }

        private void showTabsForUser()
        {
            foreach (var role in _userAccount.Roles)
            {
                switch (role)
                {
                    case "ChiefEditor":
                        tabBeast.Visibility = Visibility.Visible;
                        tabBeast.IsSelected = true;
                        break;
                    case "Admin":
                        tabGame.Visibility = Visibility.Visible;
                        tabGameCompany.Visibility = Visibility.Visible;
                        tabAdmin.Visibility = Visibility.Visible;
                        tabAdmin.IsSelected = true;
                        break;
                }
            }
            tabsetMain.Visibility = Visibility.Visible;
        }

        private void hideAllUserTabs()
        {
            foreach (var tab in tabsetMain.Items)
            {
                ((TabItem)tab).Visibility = Visibility.Collapsed;
            }
            tabsetMain.Visibility = Visibility.Collapsed;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (btnLogin.Content.ToString() == "Login")
            {
                var userManager = new UserManager();

                string email = txtEmail.Text;
                string password = pwdPassword.Password;

                if (email.isValidEmail() != true)
                {
                    MessageBox.Show("Invalid email address.");
                    return;
                }

                try
                {
                    _userAccount = userManager.LoginUser(email, password);

                    if (_userAccount != null && password == "newuser")
                    {
                        string instructions =
                            "On first login, all new users must choose " +
                            "a password to continue.";
                        var updateWindow = new UpdatePasswordWindow(
                            userManager, _userAccount, instructions, true);
                        bool? didUpdate = updateWindow.ShowDialog();

                        if (didUpdate == true)
                        {
                            updateUIforUser();
                        }
                        else
                        {
                            updateUIforLogOut();
                            MessageBox.Show("You did not update your password.\n\n"
                                + "You have been logged out.");
                            txtEmail.Text = "";
                            pwdPassword.Password = "";
                            txtEmail.Focus();
                        }
                    }
                    else
                    {
                        updateUIforUser();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                updateUIforLogOut();
            }
        }

        private void tabBeast_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (datAllBeasts.Items.Count == 0)
                {
                    var beastManager = new BeastManager();
                    datAllBeasts.ItemsSource = beastManager.RetrieveAllBeasts();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void datAllBeasts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var beast = (Beast)datAllBeasts.SelectedItem;
            var editWindow = new AddEditBeasts(beast);
            bool result = (bool)editWindow.ShowDialog();

            if (result)
            {
                var beastManager = new BeastManager();
                datAllBeasts.ItemsSource = beastManager.RetrieveAllBeasts();
            }
        }

        private void tabAdmin_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (datAllUsers.Items.Count == 0)
                {
                    var userManager = new UserManager();
                    datAllUsers.ItemsSource = userManager.GetAllUserAccounts();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void datAllUsers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var userAccount = (UserAccount)datAllUsers.SelectedItem;
            var editWindow = new AddEditUser(userAccount);
            bool result = (bool)editWindow.ShowDialog();
            if (result)
            {
                var userManager = new UserManager();
                datAllUsers.ItemsSource = userManager.GetAllUserAccounts();
            }
        }

        private void tabGameCompany_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (datAllGameCompanies.Items.Count == 0)
                {
                    var beastManager = new BeastManager();
                    datAllGameCompanies.ItemsSource = beastManager.RetrieveAllGameCompanies();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void datAllGameCompanies_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var gameCompany = (GameCompany)datAllGameCompanies.SelectedItem;
            var editWindow = new AddEditGameCompanies(gameCompany);
            bool result = (bool)editWindow.ShowDialog();
            if (result)
            {
                var beastManager = new BeastManager();
                datAllGameCompanies.ItemsSource = beastManager.RetrieveAllGameCompanies();
            }
        }

        private void tabGame_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (datAllGames.Items.Count == 0)
                {
                    var beastManager = new BeastManager();
                    datAllGames.ItemsSource = beastManager.RetrieveAllGames();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void datAllGames_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var game = (Game)datAllGames.SelectedItem;
            var editWindow = new AddEditGames(game);
            bool result = (bool)editWindow.ShowDialog();
            if (result)
            {
                var beastManager = new BeastManager();
                datAllGames.ItemsSource = beastManager.RetrieveAllGames();
            }
        }
    }
}
