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
    /// Interaction logic for PipingCalculationLineView.xaml
    /// </summary>
    public partial class PipingCalcView : UserControl
    {
        public PipingViewModel viewModel { get; set; }

        public PipingCalcView(PipingViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            DataContext = viewModel.UnitManager;
            CalculationListView.DataContext = viewModel;
        }

        private void ScrollViewers_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (sender == CalculationListView)
            {
                HeaderScrollViewer.ScrollToVerticalOffset(e.VerticalOffset);
                HeaderScrollViewer.ScrollToHorizontalOffset(e.HorizontalOffset);
            }
        }
    }
}
