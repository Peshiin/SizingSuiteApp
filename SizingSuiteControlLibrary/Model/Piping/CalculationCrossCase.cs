using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using EngineeringUnits;
using SharpFluids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SizingSuiteControlLibrary.Model.Piping
{
    public class CalculationCrossCase
    {
        private Fluid fluid = new Fluid(FluidList.Water);
        private UnitManager _unitManager;
        public UserControl control;

        public string CrossName { get; set; }
        public string Name { get; set; }
        public double Pressure { get; set; }
        public double Temperature { get; set; }
        public double Enthalpy { get; set; }
        public double FlowRate { get; set; }
        public double Density { get; set; }
        public double Quality { get; set; }
        public int NoOfLines { get; set; }
        public double Reserve { get; set; }
        public string DN { get; set; }
        public double OuterDiameter { get; set; }
        public double WallThickness { get; set; }
        public double SelectedVelocity { get; set; }
        public double ActualVelocity { get; set; }

        #region Constructor
        public CalculationCrossCase()
        {
            control = new Views.Piping.PipingCalculationLineView();
            control.DataContext = this;
        }
        public CalculationCrossCase(string crossName, string name, double pressure, double temperature, double enthalpy, double flowRate, UnitManager unitManager)
        {
            CrossName = crossName;
            Name = name;
            Pressure = pressure;
            Temperature = temperature;
            Enthalpy = enthalpy;
            FlowRate = flowRate;
            _unitManager = unitManager;

            control = new Views.Piping.PipingCalculationLineView();
            control.DataContext = this;
        }
        #endregion
    }

    public class CalculationCrossCaseMap: ClassMap<CalculationCrossCase>
    {        
        public CalculationCrossCaseMap()
        {
            Map(p => p.CrossName).Index(0);
            Map(p => p.Name).Index(1);
            Map(p => p.Pressure).Index(2);
            Map(p => p.Temperature).Index(3);
            Map(p => p.Enthalpy).Index(4);
            Map(p => p.FlowRate).Index(5);
        }
    }
}
