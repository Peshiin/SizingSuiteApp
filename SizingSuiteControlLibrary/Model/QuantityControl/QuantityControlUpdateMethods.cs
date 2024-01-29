using EngineeringUnits.Units;
using EngineeringUnits;
using SizingSuiteControlLibrary.ViewModels.QuantityControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SizingSuiteControlLibrary.Model.QuantityControl
{
    public static class QuantityControlUpdateMethods
    {

        public static void PressureUpdateMethod(double value, QuantityControlViewModel viewModel)
        {
            viewModel.Fluid.UpdatePT(Pressure.From(value, (PressureUnit)viewModel.Unit),
                viewModel.Fluid.Temperature);
        }

        public static void TemperatureUpdateMethod(double value, QuantityControlViewModel viewModel)
        {
            viewModel.Fluid.UpdatePT(viewModel.Fluid.Pressure,
                Temperature.From(value, (TemperatureUnit)viewModel.Unit));
        }

        public static void EnthalpyUpdateMethod(double value, QuantityControlViewModel viewModel)
        {
            viewModel.Fluid.UpdatePH(viewModel.Fluid.Pressure,
                Enthalpy.From(value, (EnthalpyUnit)viewModel.Unit));
        }

        public static void BasicQuantityUpdateMethod<T>(double value, QuantityControlViewModel viewModel)
            where T: BaseUnit
        {
            viewModel.Quantity = (T)Activator.CreateInstance(typeof(T), value, viewModel.Unit);
        }
    }
}
