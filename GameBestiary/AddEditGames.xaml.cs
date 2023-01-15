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
    public partial class AddEditGames : Window
    {
        private BeastManager _beastManager = new BeastManager();

        private Game _game = null;
        private List<GameCompany> _gameCompanies = null;
        bool _addMode = false;
        public AddEditGames()
        {
            _addMode = true;
            InitializeComponent();
        }
        public AddEditGames(Game game)
        {
            if (game == null)
            {
                _addMode = true;
            }
            else
            {
                _addMode = false;
            }
            _game = game;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _gameCompanies = _beastManager.RetrieveAllActiveGameCompanies();
            cboGameCompanyID.ItemsSource = from m in _gameCompanies
                                    orderby m.GameCompanyID
                                    select m.GameCompanyID;
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
            txtGameID.Text = _game.GameID.ToString();
            cboGameCompanyID.SelectedItem = _game.GameCompanyID;
            txtGameVersion.Text = _game.GameVersion;
            chkActive.IsChecked = _game.Active;
        }

        private void setDetailAddMode()
        {
            txtGameID.IsEnabled = false;
            cboGameCompanyID.IsEnabled = false;
            txtGameVersion.IsEnabled = false;
            chkActive.IsEnabled = false;

            btnEditSave.Content = "Begin";
            btnCancel.Content = "Close";
        }

        private void setDetailMode()
        {
            populateControls();

            txtGameID.IsEnabled = false;
            cboGameCompanyID.IsEnabled = false;
            txtGameVersion.IsEnabled = false;
            chkActive.IsEnabled = false;

            btnEditSave.Content = "Edit";
            btnCancel.Content = "Close";
        }

        private void setBeginMode()
        {
            txtGameID.IsEnabled = true;
            cboGameCompanyID.IsEnabled = true;
            txtGameVersion.IsEnabled = true;
            chkActive.IsEnabled = true;

            btnEditSave.Content = "Save";
            btnCancel.Content = "Cancel";
        }

        private void setEditMode()
        {
            cboGameCompanyID.IsEnabled = true;
            txtGameVersion.IsEnabled = true;
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
                    if (String.IsNullOrEmpty(txtGameID.Text))
                    {
                        MessageBox.Show("You must enter a Game.");
                        return;
                    }
                    if (String.IsNullOrEmpty(cboGameCompanyID.Text))
                    {
                        MessageBox.Show("You must select a Game Company.");
                        return;
                    }
                    if (String.IsNullOrEmpty(txtGameVersion.Text))
                    {
                        MessageBox.Show("You must enter a Game Version.");
                        return;
                    }
                    Game game = new Game();
                    game.GameID = txtGameID.Text;
                    game.GameCompanyID = cboGameCompanyID.Text;
                    game.GameVersion = txtGameVersion.Text;
                    game.Active = (bool)chkActive.IsChecked;
                    try
                    {
                        if (_beastManager.CreateGame(game))
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
                    if (String.IsNullOrEmpty(cboGameCompanyID.Text))
                    {
                        MessageBox.Show("You must select a Game Company.");
                        return;
                    }
                    if (String.IsNullOrEmpty(txtGameVersion.Text))
                    {
                        MessageBox.Show("You must select a Game Version.");
                        return;
                    }
                    // build a new Game object
                    Game game = new Game()
                    {
                        GameID = _game.GameID,
                        GameCompanyID = cboGameCompanyID.SelectedItem.ToString(),
                        GameVersion = txtGameVersion.Text,
                        Active = (bool)chkActive.IsChecked
                    };
                    try
                    {
                        if (_beastManager.EditGame(_game, game))
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