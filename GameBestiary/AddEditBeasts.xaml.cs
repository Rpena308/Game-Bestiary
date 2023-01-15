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
    /// Interaction logic for AddEditBeasts.xaml
    /// </summary>
    public partial class AddEditBeasts : Window
    {
        private List<Game> _games = null;
        private List<Alignment> _alignments = null;
        private List<BeastType> _beastType = null;
        private List<BeastSubType> _beastSubType = null;
        private List<BeastSize> _beastSize = null;
        private List<Terrain> _terrain = null;
        private BeastManager _beastManager = new BeastManager();

        private Beast _beast = null;
        bool _addMode = false;

        public AddEditBeasts()
        {
            _addMode = true;
            InitializeComponent();
        }

        public AddEditBeasts(Beast beast)
        {
            if (beast == null)
            {
                _addMode = true;
            }
            else
            {
                _addMode = false;
            }
            _beast = beast;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _games = _beastManager.RetrieveAllActiveGames();
            cboGameID.ItemsSource = from m in _games
                                    orderby m.GameID
                                    select m.GameID;

            _alignments = _beastManager.RetrieveAllAlignments();
            cboAlignmentID.ItemsSource = from m in _alignments
                                         orderby m.AlignmentID
                                         select m.AlignmentID;

            _beastType = _beastManager.RetrieveAllBeastTypes();
            cboBeastTypeID.ItemsSource = from m in _beastType
                                         orderby m.BeastTypeID
                                         select m.BeastTypeID;

            _beastSubType = _beastManager.RetrieveAllBeastSubTypes();
            cboBeastSubTypeID.ItemsSource = from m in _beastSubType
                                            orderby m.BeastSubTypeID
                                            select m.BeastSubTypeID;

            _beastSize = _beastManager.RetrieveAllBeastSizes();
            cboBeastSizeID.ItemsSource = from m in _beastSize
                                         orderby m.BeastSizeID
                                         select m.BeastSizeID;

            _terrain = _beastManager.RetrieveAllTerrains();
            cboTerrainID.ItemsSource = from m in _terrain
                                       orderby m.TerrainID
                                       select m.TerrainID;
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
            txtBeastID.Text = _beast.BeastID.ToString();
            txtBeastID.IsEnabled = false;
            cboGameID.SelectedItem = _beast.GameID;
            cboAlignmentID.SelectedItem = _beast.AlignmentID;
            cboBeastTypeID.SelectedItem = _beast.BeastTypeID;
            cboBeastSubTypeID.SelectedItem = _beast.BeastSubTypeID;
            cboBeastSizeID.SelectedItem = _beast.BeastSizeID;
            cboTerrainID.SelectedItem = _beast.TerrainID;
            txtBeastName.Text = _beast.BeastName;
            txtChallengeRating.Text = _beast.ChallengeRating.ToString();
            txtTreasure.Text = _beast.Treasure;
            txtExperience.Text = _beast.Experience.ToString();
            txtDescription.Text = _beast.BeastDescription;
            chkActive.IsChecked = _beast.Active;
        }

        private void setBeginMode()
        {
            cboGameID.IsEnabled = true;
            cboAlignmentID.IsEnabled = true;
            cboBeastTypeID.IsEnabled = true;
            cboBeastSubTypeID.IsEnabled = true;
            cboTerrainID.IsEnabled = true;
            cboBeastSizeID.IsEnabled = true;
            txtBeastName.IsEnabled = true;
            txtChallengeRating.IsEnabled = true;
            txtTreasure.IsEnabled = true;
            txtExperience.IsEnabled = true;
            txtDescription.IsEnabled = true;
            chkActive.IsEnabled = true;

            btnEditSave.Content = "Save";
            btnCancel.Content = "Cancel";
        }


        private void setDetailAddMode()
        {
            cboGameID.IsEnabled = true;
            cboAlignmentID.IsEnabled = true;
            cboBeastTypeID.IsEnabled = true;
            cboBeastSubTypeID.IsEnabled = true;
            cboTerrainID.IsEnabled = true;
            cboBeastSizeID.IsEnabled = true;
            txtBeastName.IsEnabled = true;
            txtChallengeRating.IsEnabled = true;
            txtTreasure.IsEnabled = true;
            txtExperience.IsEnabled = true;
            txtDescription.IsEnabled = true;
            chkActive.IsEnabled = true;

            btnEditSave.Content = "Begin";
            btnCancel.Content = "Close";
        }


        private void setDetailMode()
        {
            populateControls();

            cboGameID.IsEnabled = false;
            cboAlignmentID.IsEnabled = false;
            cboBeastTypeID.IsEnabled = false;
            cboBeastSubTypeID.IsEnabled = false;
            cboTerrainID.IsEnabled = false;
            cboBeastSizeID.IsEnabled = false;
            txtBeastName.IsEnabled = false;
            txtChallengeRating.IsEnabled = false;
            txtTreasure.IsEnabled = false;
            txtExperience.IsEnabled = false;
            txtDescription.IsEnabled = false;
            chkActive.IsEnabled = false;

            btnEditSave.Content = "Edit";
            btnCancel.Content = "Close";
        }

        private void setEditMode()
        {
            cboGameID.IsEnabled = true;
            cboAlignmentID.IsEnabled = true;
            cboBeastTypeID.IsEnabled = true;
            cboBeastSubTypeID.IsEnabled = true;
            cboTerrainID.IsEnabled = true;
            cboBeastSizeID.IsEnabled = true;
            txtBeastName.IsEnabled = true;
            txtChallengeRating.IsEnabled = true;
            txtTreasure.IsEnabled = true;
            txtExperience.IsEnabled = true;
            txtDescription.IsEnabled = true;
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
                    if (String.IsNullOrEmpty(cboGameID.Text))
                    {
                        MessageBox.Show("You must select a Game.");
                        return;
                    }
                    if (String.IsNullOrEmpty(cboAlignmentID.Text))
                    {
                        MessageBox.Show("You must select an Alignment.");
                        return;
                    }
                    if (String.IsNullOrEmpty(cboBeastTypeID.Text))
                    {
                        MessageBox.Show("You must select a Beast Type.");
                        return;
                    }
                    if (String.IsNullOrEmpty(cboBeastSubTypeID.Text))
                    {
                        MessageBox.Show("You must select a Beast Sub Type.");
                        return;
                    }
                    if (String.IsNullOrEmpty(cboTerrainID.Text))
                    {
                        MessageBox.Show("You must select a Terrain.");
                        return;
                    }
                    if (String.IsNullOrEmpty(cboBeastSizeID.Text))
                    {
                        MessageBox.Show("You must select a Size for this Beast.");
                        return;
                    }
                    if (txtBeastName.Text.ToString() == "")
                    {
                        MessageBox.Show("You must enter a Beast Name.");
                        txtBeastName.Focus();
                        return;
                    }
                    int challengeRating;
                    try
                    {
                        challengeRating = Convert.ToInt32(txtChallengeRating.Text);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("You must enter a valid number.");
                        txtExperience.SelectAll();
                        txtExperience.Focus();
                        return;
                    }
                    if (txtTreasure.Text.ToString() == "")
                    {
                        MessageBox.Show("You must enter a treasure.");
                        txtTreasure.Focus();
                        return;
                    }
                    int experience;
                    try
                    {
                        experience = Convert.ToInt32(txtExperience.Text);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("You must enter a valid number.");
                        txtExperience.SelectAll();
                        txtExperience.Focus();
                        return;
                    }
                    if (txtDescription.Text.ToString() == "")
                    {
                        MessageBox.Show("You must enter a Description.");
                        txtDescription.Focus();
                        return;
                    }

                    try
                    {
                        Beast beast = new Beast();
                        beast.GameID = cboGameID.Text;
                        beast.AlignmentID = cboAlignmentID.Text;
                        beast.BeastTypeID = cboBeastTypeID.Text;
                        beast.BeastSubTypeID = cboBeastSubTypeID.Text;
                        beast.TerrainID = cboTerrainID.Text;
                        beast.BeastSizeID = cboBeastSizeID.Text;
                        beast.BeastName = txtBeastName.Text;
                        beast.ChallengeRating = challengeRating;
                        beast.Treasure = txtTreasure.Text;
                        beast.Experience = experience;
                        beast.BeastDescription = txtDescription.Text;
                        beast.Active = (bool)chkActive.IsChecked;
                        if (_beastManager.CreateBeast(beast))
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
                    if (String.IsNullOrEmpty(cboGameID.Text))
                    {
                        MessageBox.Show("You must select a Game.");
                        return;
                    }
                    if (String.IsNullOrEmpty(cboAlignmentID.Text))
                    {
                        MessageBox.Show("You must select an Alignment.");
                        return;
                    }
                    if (String.IsNullOrEmpty(cboBeastTypeID.Text))
                    {
                        MessageBox.Show("You must select a Beast Type.");
                        return;
                    }
                    if (String.IsNullOrEmpty(cboBeastSubTypeID.Text))
                    {
                        MessageBox.Show("You must select a Beast Sub Type.");
                        return;
                    }
                    if (String.IsNullOrEmpty(cboTerrainID.Text))
                    {
                        MessageBox.Show("You must select a Terrain.");
                        return;
                    }
                    if (String.IsNullOrEmpty(cboBeastSizeID.Text))
                    {
                        MessageBox.Show("You must select a Size for this Beast.");
                        return;
                    }
                    if (txtBeastName.Text.ToString() == "")
                    {
                        MessageBox.Show("You must enter a Beast Name.");
                        txtBeastName.Focus();
                        return;
                    }
                    int challengeRating;
                    try
                    {
                        challengeRating = Convert.ToInt32(txtChallengeRating.Text);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("You must enter a valid number.");
                        txtExperience.SelectAll();
                        txtExperience.Focus();
                        return;
                    }
                    if (txtTreasure.Text.ToString() == "")
                    {
                        MessageBox.Show("You must enter a treasure.");
                        txtTreasure.Focus();
                        return;
                    }
                    int experience;
                    try
                    {
                        experience = Convert.ToInt32(txtExperience.Text);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("You must enter a valid number.");
                        txtExperience.SelectAll();
                        txtExperience.Focus();
                        return;
                    }
                    if (txtDescription.Text.ToString() == "")
                    {
                        MessageBox.Show("You must enter a Description.");
                        txtDescription.Focus();
                        return;
                    }

                    // build a new Beast object
                    Beast beast = new Beast()
                    {
                        BeastID = _beast.BeastID,
                        GameID = cboGameID.SelectedItem.ToString(),
                        AlignmentID = cboAlignmentID.SelectedItem.ToString(),
                        BeastTypeID = cboBeastTypeID.SelectedItem.ToString(),
                        BeastSubTypeID = cboBeastSubTypeID.SelectedItem.ToString(),
                        TerrainID = cboTerrainID.SelectedItem.ToString(),
                        BeastSizeID = cboBeastSizeID.SelectedItem.ToString(),
                        BeastName = txtBeastName.Text,
                        ChallengeRating = challengeRating,
                        Treasure = txtTreasure.Text,
                        Experience = experience,
                        BeastDescription = txtDescription.Text,
                        Active = (bool)chkActive.IsChecked
                    };
                    try
                    {
                        if (_beastManager.EditBeast(_beast, beast))
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
