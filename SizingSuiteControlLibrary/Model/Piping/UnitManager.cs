using EngineeringUnits.Units;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SizingSuiteControlLibrary.Model.Piping
{
    public class UnitManager
    {
        public ObservableCollection<MassFlowUnit> FlowRateUnits { get; private set; }
        public ObservableCollection<PressureUnit> PressureUnits { get; private set; }
        public ObservableCollection<TemperatureUnit> TemperatureUnits { get; private set; }
        public ObservableCollection<EnthalpyUnit> EnthalpyUnits { get; private set; }
        public ObservableCollection<DensityUnit> DensityUnits { get; private set; }
        public ObservableCollection<SpeedUnit> VelocityUnits { get; private set; }

        public MassFlowUnit FlowRateSelectedUnit { get; set; }
        public PressureUnit PressureSelectedUnit { get; set; }
        public TemperatureUnit TemperatureSelectedUnit { get; set; }
        public EnthalpyUnit EnthalpySelectedUnit { get; set; }
        public DensityUnit DensitySelectedUnit { get; set; }
        public SpeedUnit SelectedVelocitySelectedUnit { get; set; }
        public SpeedUnit ActualVelocitySelectedUnit { get; set; }


        #region Constructor
        public UnitManager()
        {
            FlowRateUnits = new ObservableCollection<MassFlowUnit>(
                EngineeringUnits.UnitTypebase.ListOf<MassFlowUnit>());
            FlowRateSelectedUnit = MassFlowUnit.KilogramPerSecond;

            PressureUnits = new ObservableCollection<PressureUnit>(
                EngineeringUnits.UnitTypebase.ListOf<PressureUnit>());
            PressureSelectedUnit = PressureUnit.Bar;

            TemperatureUnits = new ObservableCollection<TemperatureUnit>(
                EngineeringUnits.UnitTypebase.ListOf<TemperatureUnit>());
            TemperatureSelectedUnit = TemperatureUnit.DegreeCelsius;

            EnthalpyUnits = new ObservableCollection<EnthalpyUnit>(
                EngineeringUnits.UnitTypebase.ListOf<EnthalpyUnit>());
            EnthalpySelectedUnit = EnthalpyUnit.KilojoulePerKilogram;

            DensityUnits = new ObservableCollection<DensityUnit>(
                EngineeringUnits.UnitTypebase.ListOf<DensityUnit>());
            DensitySelectedUnit = DensityUnit.KilogramPerCubicMeter;

            VelocityUnits = new ObservableCollection<SpeedUnit>(
                EngineeringUnits.UnitTypebase.ListOf<SpeedUnit>());
            SelectedVelocitySelectedUnit = SpeedUnit.MeterPerSecond;
            ActualVelocitySelectedUnit = SpeedUnit.MeterPerSecond;
        }
        #endregion
    }
}
