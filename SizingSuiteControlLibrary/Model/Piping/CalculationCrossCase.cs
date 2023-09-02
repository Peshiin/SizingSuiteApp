using EngineeringUnits;
using EngineeringUnits.Units;
using SharpFluids;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using SizingSuiteControlLibrary.Model.Piping;
using SizingSuiteApp.ViewModels;

namespace SizingSuiteControlLibrary.Model.Piping
{
    public class CalculationCrossCase : BaseViewModel
    {
        private Fluid fluid = new Fluid(FluidList.Water);
        public UnitManager UnitManager = new UnitManager();
        public UserControl control;
        public DNcatalogue dnCatalogue { get; set; }
        private DN _dn;
        public DN dn
        {
            get
            {
                return _dn;
            }
            set
            {
                _dn = value;
                dn.wallThickness = dn.availableWallThickness.First();
                InvokeChange(nameof(dn));
                InvokeChange(nameof(dn.outerDiameter));
                InvokeChange(nameof(dn.availableWallThickness));


                dn.PropertyChanged += Dn_PropertyChanged;
            }
        }

        public CalculationCross Cross { get; set; }
        public string Name { get; set; }
        public double pressure
        {
            get
            {
                return fluid.Pressure.As(UnitManager.PressureSelectedUnit);
            }
            set
            {
                fluid.UpdatePT(Pressure.From(value, UnitManager.PressureSelectedUnit),
                    Temperature.From(temperature, UnitManager.TemperatureSelectedUnit));
                InvokeChange(nameof(pressure));
                InvokeChange(nameof(enthalpy));
                InvokeChange(nameof(density));
                InvokeChange(nameof(quality));
            }
        }
        public double temperature
        {
            get
            {
                return fluid.Temperature.As(UnitManager.TemperatureSelectedUnit);
            }
            set
            {
                fluid.UpdatePT(Pressure.From(pressure, UnitManager.PressureSelectedUnit),
                    Temperature.From(value, UnitManager.TemperatureSelectedUnit));
                InvokeChange(nameof(temperature));
                InvokeChange(nameof(enthalpy));
                InvokeChange(nameof(density));
                InvokeChange(nameof(quality));
            }
        }
        public double enthalpy
        {
            get
            {
                return fluid.Enthalpy.As(UnitManager.EnthalpySelectedUnit);
            }
            set
            {
                fluid.UpdatePH(Pressure.From(pressure, UnitManager.PressureSelectedUnit),
                    Enthalpy.From(value, UnitManager.EnthalpySelectedUnit));
                InvokeChange(nameof(enthalpy));
                InvokeChange(nameof(temperature));
                InvokeChange(nameof(density));
                InvokeChange(nameof(quality));
            }
        }
        public double flowRate
        {
            get
            {
                return fluid.MassFlow.As(UnitManager.FlowRateSelectedUnit);
            }
            set
            {
                fluid.MassFlow = MassFlow.From(value, UnitManager.FlowRateSelectedUnit);
                InvokeChange(nameof(flowRate));
            }
        }
        public double density
        {
            get
            {
                return fluid.Density.As(UnitManager.DensitySelectedUnit);
            }
            set
            {
                fluid.UpdateDP(Density.From(value, UnitManager.DensitySelectedUnit),
                    Pressure.From(pressure, UnitManager.PressureSelectedUnit));
                InvokeChange(nameof(density));
                InvokeChange(nameof(enthalpy));
                InvokeChange(nameof(temperature));
                InvokeChange(nameof(quality));
            }
        }
        public double quality
        {
            get
            {
                return fluid.Quality;
            }
            set
            {
                fluid.UpdatePX(Pressure.From(pressure, UnitManager.PressureSelectedUnit),
                    value);
                InvokeChange(nameof(density));
                InvokeChange(nameof(enthalpy));
                InvokeChange(nameof(temperature));
                InvokeChange(nameof(quality));
            }
        }
        private int _noOfLines;
        public int noOfLines
        {
            get
            {
                return _noOfLines;
            }
            set
            {
                _noOfLines = value;
                InvokeChange(nameof(noOfLines));
            }
        }
        private double _reserve;
        public double reserve 
        { 
            get
            {
                return _reserve;
            }
            set
            {
                _reserve = value;
                InvokeChange(nameof(reserve));
            } 
        }

        private Speed _SelectedVelocity;
        private bool velocityIsSetManually;
        public double? SelectedVelocity
        {
            get
            {
                if (velocityIsSetManually)
                    return _SelectedVelocity.As(UnitManager.SelectedVelocitySelectedUnit);
                else
                    if (fluid.Phase == Phases.Liquid)
                    return Speed.FromMeterPerSecond(2.5).As(UnitManager.SelectedVelocitySelectedUnit);
                else if (fluid.Phase == Phases.Gas)
                    return Speed.FromMeterPerSecond(40).As(UnitManager.SelectedVelocitySelectedUnit);
                else
                    return Speed.FromMeterPerSecond(30).As(UnitManager.SelectedVelocitySelectedUnit);
            }
            set
            {
                _SelectedVelocity = Speed.From(value, UnitManager.SelectedVelocitySelectedUnit);
                if (value == null)
                {
                    velocityIsSetManually = false;
                    InvokeChange(nameof(SelectedVelocity));
                }
                else
                {
                    velocityIsSetManually = true;
                    InvokeChange(nameof(SelectedVelocity));
                }

            }
        }

        private Speed _ActualVelocity = Speed.Zero;
        public double ActualVelocity
        {
            get
            {
                return _ActualVelocity.As(UnitManager.ActualVelocitySelectedUnit);
            }
            set
            {
                _ActualVelocity = Speed.From(value, UnitManager.ActualVelocitySelectedUnit);
                InvokeChange(nameof(ActualVelocity));
            }
        }

        private List<string> velocityTriggers;

        #region Events

        private void UnitManager_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case "FlowRateSelectedUnit":
                    InvokeChange(nameof(flowRate));
                    break;
                case "PressureSelectedUnit":
                    InvokeChange(nameof(pressure));
                    break;
                case "TemperatureSelectedUnit":
                    InvokeChange(nameof(temperature));
                    break;
                case "EnthalpySelectedUnit":
                    InvokeChange(nameof(enthalpy));
                    break;
                case "DensitySelectedUnit":
                    InvokeChange(nameof(density));
                    break;
                case "SelectedVelocitySelectedUnit":
                    InvokeChange(nameof(SelectedVelocity));
                    break;
                case "ActualVelocitySelectedUnit":
                    InvokeChange(nameof(ActualVelocity));
                    break;
            }
        }

        private void CalculationCrossCase_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (velocityTriggers.Contains(e.PropertyName))
                ActualVelocity = PipeEquations.GetSpeed(fluid.MassFlow, fluid.Density, dn.crossSection)
                    .As(UnitManager.ActualVelocitySelectedUnit) * (reserve / noOfLines);
        }

        private void Dn_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (velocityTriggers.Contains(e.PropertyName))
                ActualVelocity = PipeEquations.GetSpeed(fluid.MassFlow, fluid.Density, dn.crossSection)
                    .As(UnitManager.ActualVelocitySelectedUnit) * (reserve / noOfLines);
        }
        #endregion

        #region Constructor
        public CalculationCrossCase(CalculationCross cross, string name, double pressure,
            double temperature, double enthalpy, double flowRate,
            UnitManager unitManager, DNcatalogue dNcatalogue)
        {
            Cross = cross;
            Name = name;
            UnitManager = unitManager;
            this.dnCatalogue = dNcatalogue;
            noOfLines = 1;
            reserve = 1;

            dn = dnCatalogue.AvailableDNs.First();

            this.fluid.UpdatePT(Pressure.From(pressure, UnitManager.PressureSelectedUnit),
                Temperature.From(temperature, UnitManager.TemperatureSelectedUnit));
            if(fluid.Quality != -1)
                this.fluid.UpdatePH(Pressure.From(pressure, UnitManager.PressureSelectedUnit),
                    Enthalpy.From(enthalpy, UnitManager.EnthalpySelectedUnit));
            this.fluid.MassFlow = MassFlow.From(flowRate, UnitManager.FlowRateSelectedUnit);

            velocityTriggers = new List<string>(){ nameof(dn.crossSection),
                nameof(dn), nameof(noOfLines), nameof(reserve),
                nameof(this.flowRate), nameof(this.pressure),
                nameof(this.temperature), nameof(this.enthalpy)};

            UnitManager.PropertyChanged += UnitManager_PropertyChanged;
            this.PropertyChanged += CalculationCrossCase_PropertyChanged;
        }
        #endregion
    }
}
