using EngineeringUnits.Units;
using EngineeringUnits;
using SharpFluids;
using SizingSuiteControlLibrary.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SizingSuiteControlLibrary.ViewModels.QuantityControl
{
    public class PressureControlViewModel: BaseViewModel
    {
        #region Properties
        public QuantityControlView Control { get; }
        public Fluid Fluid { get; }
        public UnitTypebase OriginalUnit { get; }
        public UnitTypebase Unit { get; set; }
        public ObservableCollection<UnitTypebase> Units { get; }
        public double OriginalValue { get; }
        public double Value 
        {
            get { return Fluid.Pressure.As(Unit); }
            set 
            {
                Fluid.UpdatePT(Pressure.From(value, (PressureUnit)Unit),
                    Fluid.Temperature);
                InvokeChange(nameof(Pressure));
            }
        }
        #endregion

        #region Constructor
        public PressureControlViewModel(QuantityControlView control, Fluid fluid, UnitTypebase unit,
            List<UnitTypebase> units = null)
        {
            if (control is null || fluid is null || unit is null)
                return;

            Control = control;
            Fluid = fluid;
            Unit = unit;
            OriginalUnit = Unit;
            Value = Fluid.Pressure.As(Unit);
            OriginalValue = Value;

            if (units != null)
                Units = new ObservableCollection<UnitTypebase>(units);
            else
                Units = new ObservableCollection<UnitTypebase>(UnitTypebase.ListOf<PressureUnit>());

            Control.UnitCBox.SelectionChanged += UnitCBox_SelectionChanged;

            InvokeChange(nameof(Units));
        }

        private void UnitCBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            InvokeChange(nameof(Value)); 
        }
        #endregion
    }
}
