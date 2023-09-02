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
        public PipingViewModel viewModel = new PipingViewModel();
        public PipingCalcView calcView { get; set; }
        public PipingLoaderView loaderView { get; set; }

        public PipingView()
        {
            InitializeComponent();

            calcView = new PipingCalcView(viewModel);
            CalcView.Content = calcView;

            loaderView = new PipingLoaderView(viewModel);
            LoaderView.Content = loaderView;
        }
    }
}
