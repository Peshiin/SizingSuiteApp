using EngineeringUnits;
using EngineeringUnits.Units;
using SizingSuiteControlLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SizingSuiteControlLibrary.Model.Piping
{
    public class UnitManager: BaseViewModel
    {
        public ObservableCollection<MassFlowUnit> FlowRateUnits { get; private set; }
        public ObservableCollection<PressureUnit> PressureUnits { get; private set; }
        public ObservableCollection<TemperatureUnit> TemperatureUnits { get; private set; }
        public ObservableCollection<EnthalpyUnit> EnthalpyUnits { get; private set; }
        public ObservableCollection<DensityUnit> DensityUnits { get; private set; }
        public ObservableCollection<SpeedUnit> VelocityUnits { get; private set; }

        private MassFlowUnit _flowRateSelectedUnit;
        public MassFlowUnit FlowRateSelectedUnit
        {
            get 
            { 
                return _flowRateSelectedUnit;
            }
            set 
            { 
                _flowRateSelectedUnit = value;
                InvokeChange(nameof(FlowRateSelectedUnit));
            }
        }

        private PressureUnit _pressureSelectedUnit;
        public PressureUnit PressureSelectedUnit
        {
            get 
            {
                return _pressureSelectedUnit;
            }
            set
            { 
                _pressureSelectedUnit = value;
                InvokeChange(nameof(PressureSelectedUnit));
            }
        }

        private TemperatureUnit _temperatureSelectedUnit;
        public TemperatureUnit TemperatureSelectedUnit
        {
            get
            {
                return _temperatureSelectedUnit;
            }
            set
            {
                _temperatureSelectedUnit = value;
                InvokeChange(nameof(TemperatureSelectedUnit));
            }
        }

        private EnthalpyUnit _enthalpySelectedUnit;
        public EnthalpyUnit EnthalpySelectedUnit
        {
            get
            {
                return _enthalpySelectedUnit;
            }
            set
            {
                _enthalpySelectedUnit = value;
                InvokeChange(nameof(EnthalpySelectedUnit));
            }
        }

        private DensityUnit _densitySelectedUnit;
        public DensityUnit DensitySelectedUnit
        {
            get
            {
                return _densitySelectedUnit;
            }
            set
            {
                _densitySelectedUnit = value;
                InvokeChange(nameof(DensitySelectedUnit));
            }
        }

        private SpeedUnit _selectedVelocitySelectedUnit;
        public SpeedUnit SelectedVelocitySelectedUnit
        {
            get
            {
                return _selectedVelocitySelectedUnit;
            }
            set
            {
                _selectedVelocitySelectedUnit = value;
                InvokeChange(nameof(SelectedVelocitySelectedUnit));
            }
        }

        private SpeedUnit _actualVelocityUnit;
        public SpeedUnit ActualVelocitySelectedUnit
        {
            get
            {
                return _actualVelocityUnit;
            }
            set
            {
                _actualVelocityUnit = value;
                InvokeChange(nameof(ActualVelocitySelectedUnit));
            }
        }

        #region Events
        #endregion

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
