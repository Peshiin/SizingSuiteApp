using EngineeringUnits;
using EngineeringUnits.Units;
using SharpFluids;
using SizingSuiteControlLibrary.Model;
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

namespace SizingSuiteControlLibrary.Views
{
    /// <summary>
    /// Interaction logic for QuantityControl.xaml
    /// </summary>
    public partial class QuantityControlView : UserControl
    {
        #region Constructor
        public QuantityControlView()
        {
            InitializeComponent();
            DescriptionLabel.DataContext = this;
        }
        #endregion

        #region Dependency properties
        /// <summary>
        /// Description of the quantity used as label in the control
        /// </summary>
        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }
        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(QuantityControlView),
                new PropertyMetadata("Description"));

        /// <summary>
        /// Text for the help button
        /// </summary>
        public string HelpText
        {
            get { return (string)GetValue(HelpTextProperty); }
            set { SetValue(HelpTextProperty, value); }
        }
        public static readonly DependencyProperty HelpTextProperty =
            DependencyProperty.Register("HelpText", typeof(string), typeof(QuantityControlView),
                new PropertyMetadata("No help here :("));
        #endregion

        #region Methods
        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(HelpText, "Help", MessageBoxButton.OK, MessageBoxImage.Question);
        }
        #endregion
    }
}
