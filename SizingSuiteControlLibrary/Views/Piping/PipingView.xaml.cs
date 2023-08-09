using EngineeringUnits.Units;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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

namespace SizingSuiteControlLibrary.Views.Piping
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class PipingView : UserControl
    {
        public List<MassFlowUnit> FlowRateUnits = EngineeringUnits.UnitTypebase.ListOf<MassFlowUnit>();
        public List<PressureUnit> PressureUnits = EngineeringUnits.UnitTypebase.ListOf<PressureUnit>();
        public List<TemperatureUnit> TemperatureUnits = EngineeringUnits.UnitTypebase.ListOf<TemperatureUnit>();
        public List<EnthalpyUnit> EnthalpyUnits = EngineeringUnits.UnitTypebase.ListOf<EnthalpyUnit>();
        public List<DensityUnit> DensityUnits = EngineeringUnits.UnitTypebase.ListOf<DensityUnit>();
        public List<SpeedUnit> VelocityUnits = EngineeringUnits.UnitTypebase.ListOf<SpeedUnit>();

        public PipingView()
        {
            InitializeComponent();

            FlowRateUnitCBox.ItemsSource = FlowRateUnits;
            FlowRateUnitCBox.SelectedItem = MassFlowUnit.KilogramPerSecond;

            PressureUnitCBox.ItemsSource = PressureUnits;
            PressureUnitCBox.SelectedItem = PressureUnit.Bar;

            TemperatureUnitCBox.ItemsSource = TemperatureUnits;
            TemperatureUnitCBox.SelectedItem = TemperatureUnit.DegreeCelsius;

            EnthalpyUnitCBox.ItemsSource = EnthalpyUnits;
            EnthalpyUnitCBox.SelectedItem = EnthalpyUnit.KilojoulePerKilogram;

            DensityUnitCBox.ItemsSource = DensityUnits;
            DensityUnitCBox.SelectedItem = DensityUnit.KilogramPerCubicMeter;

            SelectedVelocityUnitCBox.ItemsSource = VelocityUnits;
            SelectedVelocityUnitCBox.SelectedItem = SpeedUnit.MeterPerSecond;

            ActualVelocityUnitCBox.ItemsSource = VelocityUnits;
            ActualVelocityUnitCBox.SelectedItem = SpeedUnit.MeterPerSecond;
        }
    }
}
