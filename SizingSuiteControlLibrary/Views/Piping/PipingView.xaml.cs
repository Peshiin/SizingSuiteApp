using EngineeringUnits.Units;
using SizingSuiteApp.ViewModels;
using SizingSuiteControlLibrary.Model;
using SizingSuiteControlLibrary.Model.Piping;
using SizingSuiteControlLibrary.ViewModels.Piping;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
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
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class PipingView : UserControl
    {
        public PipingViewModel PipingViewModel = new PipingViewModel();

        public PipingView()
        {
            InitializeComponent();
            DataContext = PipingViewModel.UnitManager;
            CrossSelectionCBox.DataContext = PipingViewModel;
            CalculationItemsControl.DataContext = PipingViewModel;
        }

        private void LoadCrossesBtn_Click(object sender, RoutedEventArgs e)
        {
            string filePath = FileHandler.OpenDialog();
            PipingViewModel.LoadCrosses(filePath);      
        }

        private void CrossSelectionCBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PipingViewModel.SelectedCross = (CalculationCross)CrossSelectionCBox.SelectedItem;
        }
    }
}
