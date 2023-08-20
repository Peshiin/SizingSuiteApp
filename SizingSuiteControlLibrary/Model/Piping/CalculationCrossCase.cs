using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using EngineeringUnits;
using EngineeringUnits.Units;
using SharpFluids;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SizingSuiteControlLibrary.Model.Piping
{
    public class CalculationCrossCase: INotifyPropertyChanged
    {
        private Fluid fluid = new Fluid(FluidList.Water);
        public UnitManager UnitManager = new UnitManager();
        public UserControl control;

        public string CrossName { get; set; }
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
        public int NoOfLines { get; set; }
        public double Reserve { get; set; }
        public string DN { get; set; }
        public double OuterDiameter { get; set; }
        public double WallThickness { get; set; }
        public double SelectedVelocity { get; set; }
        public double ActualVelocity { get; set; }

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        protected void InvokeChange(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

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
        #endregion

        #region Constructor
        public CalculationCrossCase(string crossName, string name, double pressure, double temperature, double enthalpy, double flowRate, UnitManager unitManager)
        {
            CrossName = crossName;
            Name = name;
            UnitManager = unitManager;

            this.fluid.UpdatePT(Pressure.From(pressure, UnitManager.PressureSelectedUnit),
                Temperature.From(temperature, UnitManager.TemperatureSelectedUnit));
            if(fluid.Quality != -1)
                this.fluid.UpdatePH(Pressure.From(pressure, UnitManager.PressureSelectedUnit),
                    Enthalpy.From(enthalpy, UnitManager.EnthalpySelectedUnit));
            this.fluid.MassFlow = MassFlow.From(flowRate, UnitManager.FlowRateSelectedUnit);

            control = new Views.Piping.PipingCalculationLineView();
            control.DataContext = this;

            UnitManager.PropertyChanged += UnitManager_PropertyChanged;
        }
        #endregion
    }
}
