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
    /// Interaction logic for AddEditGames.xaml
    /// </summary>
    public partial class AddEditGameCompanies : Window
    {
        private BeastManager _beastManager = new BeastManager();

        private GameCompany _gameCompany = null;
        bool _addMode = false;
        public AddEditGameCompanies()
        {
            _addMode = true;
            InitializeComponent();
        }
        public AddEditGameCompanies(GameCompany gameCompany)
        {
            if (gameCompany == null)
            {
                _addMode = true;
            }
            else
            {
                _addMode = false;
            }
            _gameCompany = gameCompany;
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
            txtGameCompanyID.Text = _gameCompany.GameCompanyID;
            txtEmail.Text = _gameCompany.Email;
            txtWebsite.Text = _gameCompany.Website;
            chkActive.IsChecked = _gameCompany.Active;
        }

        private void setDetailAddMode()
        {
            txtGameCompanyID.IsEnabled = false;
            txtEmail.IsEnabled = false;
            txtWebsite.IsEnabled = false;
            chkActive.IsEnabled = false;

            btnEditSave.Content = "Begin";
            btnCancel.Content = "Close";
        }

        private void setDetailMode()
        {
            populateControls();

            txtGameCompanyID.IsEnabled = false;
            txtEmail.IsEnabled = false;
            txtWebsite.IsEnabled = false;
            chkActive.IsEnabled = false;

            btnEditSave.Content = "Edit";
            btnCancel.Content = "Close";
        }

        private void setBeginMode()
        {
            txtGameCompanyID.IsEnabled = true;
            txtEmail.IsEnabled = true;
            txtWebsite.IsEnabled = true;
            chkActive.IsEnabled = true;

            btnEditSave.Content = "Save";
            btnCancel.Content = "Cancel";
        }

        private void setEditMode()
        {
            txtEmail.IsEnabled = true;
            txtWebsite.IsEnabled = true;
            chkActive.IsEnabled = true;

            btnEditSave.Content = "Save";
            btnCancel.Content = "Cancel";
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
                    if (String.IsNullOrEmpty(txtGameCompanyID.Text))
                    {
                        MessageBox.Show("You must enter a Game Company.");
                        return;
                    }
                    if (String.IsNullOrEmpty(txtWebsite.Text))
                    {
                        MessageBox.Show("You must enter a Website.");
                        return;
                    }
                    if (String.IsNullOrEmpty(txtEmail.Text))
                    {
                        MessageBox.Show("You must enter an Email.");
                        return;
                    }
                    try
                    {
                        GameCompany gameCompany = new GameCompany();
                        gameCompany.GameCompanyID = txtGameCompanyID.Text;
                        gameCompany.Email = txtEmail.Text;
                        gameCompany.Website = txtWebsite.Text;
                        gameCompany.Active = (bool)chkActive.IsChecked;
                        if (_beastManager.CreateGameCompany(gameCompany))
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
                    if (String.IsNullOrEmpty(txtWebsite.Text))
                    {
                        MessageBox.Show("You must enter a Website.");
                        return;
                    }
                    if (String.IsNullOrEmpty(txtEmail.Text))
                    {
                        MessageBox.Show("You must enter an Email.");
                        return;
                    }
                    // build a new GameCompany object
                    GameCompany gameCompany = new GameCompany()
                    {
                        GameCompanyID = _gameCompany.GameCompanyID,
                        Email = txtEmail.Text,
                        Website = txtWebsite.Text,
                        Active = (bool)chkActive.IsChecked
                    };
                    try
                    {
                        if (_beastManager.EditGameCompany(_gameCompany, gameCompany))
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