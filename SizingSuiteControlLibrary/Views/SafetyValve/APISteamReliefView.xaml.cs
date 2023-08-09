using SizingSuiteApp.ViewModels.SafetyValve;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace SizingSuiteControlLibrary.Views
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class APISteamReliefView : UserControl
    {
        public APISteamReliefViewModel viewModel;

        public APISteamReliefView()
        {
            InitializeComponent();
            viewModel = new APISteamReliefViewModel();
            DataContext = viewModel;

            viewModel.PropertyChanged += viewModel_PropertyChanged;
        }

        private void viewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(viewModel.IsSaturated))
                EnthalpyTBox.IsEnabled = viewModel.IsSaturated;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,-]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void SaturationTemperature_Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SetSaturationTemperature();
        }

        private void EnthalpyTBox_LostFocus(object sender, RoutedEventArgs e)
        {
            viewModel.EnthalpySetManually = true;
        }
    }
}
