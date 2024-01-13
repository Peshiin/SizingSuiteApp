using SizingSuiteControlLibrary.Model.Piping;
using SizingSuiteControlLibrary.Model;
using SizingSuiteControlLibrary.ViewModels.Piping;
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
using System.IO;
using Microsoft.VisualBasic;

namespace SizingSuiteControlLibrary.Views.Piping
{
    /// <summary>
    /// Interaction logic for PipingLoaderView.xaml
    /// </summary>
    public partial class PipingLoaderView : UserControl
    {
        public PipingViewModel viewModel { get; set; }
        public string delimiter { get; set; } = ";";

        public PipingLoaderView(PipingViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            CrossSelectionCBox.DataContext = this.viewModel;
            DelimiterTextBox.DataContext = this;
        }

        private void LoadCrossesBtn_Click(object sender, RoutedEventArgs e)
        {
            string filePath = FileHandler.OpenDialog();
            if (filePath != null)
                viewModel.crosses = FileHandler.LoadCrosses(filePath, delimiter, viewModel.UnitManager, viewModel.dnCatalogue);
            if(viewModel.crosses != null)
                CrossSelectionCBox.SelectedItem = viewModel.crosses.First();
        }

        private void CrossSelectionCBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.SelectedCross = (CalculationCross)CrossSelectionCBox.SelectedItem;
        }

        private void AddCrossCaseButton_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedCross == null)
            {
                MessageBox.Show("First, select a cross.");
                return;
            }

            string newCaseName = Interaction.InputBox("Select case name", Title: "New case name");
            if (newCaseName == null)
                return;
                
            viewModel.SelectedCross.Cases.Add(new CalculationCrossCase(viewModel.SelectedCross,
                newCaseName, 1.0, 120, 1.0, 1.0, viewModel.UnitManager, viewModel.dnCatalogue));
        }

        private void AddCrossButton_Click(object sender, RoutedEventArgs e)
        {
            string newCrossName = Interaction.InputBox("Select cross name", Title: "New cross name");
            if(newCrossName == null)
                return;

            var newCross = new CalculationCross(newCrossName, "Description",
                new System.Collections.ObjectModel.ObservableCollection<CalculationCrossCase>());
            viewModel.crosses.Add(newCross);
            CrossSelectionCBox.SelectedItem = newCross;
        }
    }
}
