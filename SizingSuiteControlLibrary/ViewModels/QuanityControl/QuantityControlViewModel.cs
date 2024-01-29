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
using System.ComponentModel;
using Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.CodeDom;
using System.Reflection;
using System.Windows;

namespace SizingSuiteControlLibrary.ViewModels.QuantityControl
{
    public class QuantityControlViewModel: INotifyPropertyChanged
    {
        #region Properties
        public QuantityControlView Control { get; }
        public Fluid Fluid { get; }
        public BaseUnit Quantity { get; set; }
        public string QuantityName { get; set; }
        public UnitTypebase OriginalUnit { get; }
        public UnitTypebase Unit { get; set; }
        public ObservableCollection<UnitTypebase> Units { get; }
        public ValueUpdateMethod UpdateMethod;
        public double OriginalValue { get; }
        private double value;
        public double Value 
        {
            get 
            {
                if (!(Fluid is null))
                    return (Fluid.GetType().GetProperty(QuantityName).GetValue(Fluid, null)
                        as BaseUnit).As(Unit);
                else if (!(Quantity is null))
                    return Quantity.As(Unit);
                else
                    return value;
            }
            set 
            {
                if (!(UpdateMethod is null))
                    UpdateMethod(value, this);
                else
                    this.value = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
            }
        }
        #endregion

        #region Constructor
        public QuantityControlViewModel(QuantityControlView control, Fluid fluid, BaseUnit quantity,
            UnitTypebase unit, IEnumerable<UnitTypebase> units, ValueUpdateMethod updateMethod)
        {
            if (control is null)
                throw new ArgumentNullException(nameof(control));

            Control = control;
            Control.DataContext = this;
            Fluid = fluid;
            Quantity = quantity;
            Type unitType = unit.GetType();
            Unit = unit;
            OriginalUnit = Unit; 

            if (!(fluid is null))
            {
                QuantityName = quantity.GetType().Name;
                var propertyByName = Fluid.GetType().GetProperty(QuantityName).GetValue(Fluid, null);
                Value = (propertyByName as BaseUnit).As(Unit);
            }

            UpdateMethod = updateMethod;
            OriginalValue = Value;
            Units = new ObservableCollection<UnitTypebase>(units);

            Control.UnitCBox.SelectionChanged += UnitCBox_SelectionChanged;
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Methods
        private void UnitCBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
        }
        #endregion

        #region Delegates
        public delegate void ValueUpdateMethod(double value, QuantityControlViewModel viewModel);
        #endregion
    }
}
