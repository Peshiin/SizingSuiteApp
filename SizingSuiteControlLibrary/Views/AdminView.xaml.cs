using EngineeringUnits;
using EngineeringUnits.Units;
using Newtonsoft.Json.Linq;
using SharpFluids;
using SizingSuiteControlLibrary.Model;
using UpdateMethods = SizingSuiteControlLibrary.Model.QuantityControl.QuantityControlUpdateMethods;
using SizingSuiteControlLibrary.ViewModels;
using SizingSuiteControlLibrary.ViewModels.QuantityControl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for AdminView.xaml
    /// </summary>
    public partial class AdminView : UserControl
    {
        public AdminView()
        {
            InitializeComponent();

            var fluid = new Fluid(FluidList.Water);
            var press = new Pressure(3.0, PressureUnit.Bar);
            var temp = new Temperature(150, TemperatureUnit.DegreeCelsius);
            fluid.UpdatePT(press, temp);

            var pVM = new QuantityControlViewModel(QuantControl, null, new Length(15, LengthUnit.Meter),
                LengthUnit.Meter, UnitTypebase.ListOf<LengthUnit>().AsEnumerable<UnitTypebase>(),
                UpdateMethods.BasicQuantityUpdateMethod<Length>);
        }

        private void LoadCrossesButton_Click(object sender, RoutedEventArgs e)
        {
            ExcelToXMLConverter convertor = new ExcelToXMLConverter();
            convertor.BasePipesExcelToXML();
        }

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
