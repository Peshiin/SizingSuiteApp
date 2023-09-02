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
using System.Windows.Shapes;

namespace SizingSuiteControlLibrary.Views.Piping
{
    /// <summary>
    /// Interaction logic for PipingLoaderView.xaml
    /// </summary>
    public partial class PipingLoaderView : UserControl
    {
        public PipingViewModel viewModel { get; set; }

        public PipingLoaderView(PipingViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            CrossSelectionCBox.DataContext = this.viewModel;
        }

        private void LoadCrossesBtn_Click(object sender, RoutedEventArgs e)
        {
            string filePath = FileHandler.OpenDialog();
            viewModel.crosses = FileHandler.LoadCrosses(filePath, ";", viewModel.UnitManager);
            CrossSelectionCBox.SelectedItem = viewModel.crosses.First();
        }

        private void CrossSelectionCBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.SelectedCross = (CalculationCross)CrossSelectionCBox.SelectedItem;
        }
    }
}
