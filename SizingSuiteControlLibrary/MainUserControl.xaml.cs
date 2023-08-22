using SizingSuiteApp.ViewModels;
using SizingSuiteApp.ViewModels.SafetyValve;
using SizingSuiteControlLibrary.Model;
using SizingSuiteControlLibrary.Views;
using SizingSuiteControlLibrary.Views.Piping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace SizingSuiteControlLibrary
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class MainUserControl : UserControl //, INotifyPropertyChanged
    {
        public Dictionary<UserControl, string> ControlList = new Dictionary<UserControl, string>();

        public MainUserControl()
        {
            InitializeComponent();
            ControlList.Add(new APISteamReliefView(), "Safety valve - API 520 steam relief");
            ControlList.Add(new PipingView(), "Piping");
            ControlList.Add(new AdminView(), "Administration");
            ControlSelection.ItemsSource = ControlList;
        }

        //public event PropertyChangedEventHandler PropertyChanged;

        private void ControlSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            contentControl.Content = ControlSelection.SelectedValue;
        }
    }
}
