using EngineeringUnits;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpFluids;
using SizingSuiteControlLibrary.Model;
using System.Diagnostics;
using Jace;
using SizingSuiteControlLibrary.Model.SafetyValve;

namespace SizingSuiteApp.ViewModels.SafetyValve
{
    public class APISteamReliefViewModel: BaseViewModel, INotifyPropertyChanged
    {
        public SafetyValveAPISteamSizingCase valve = new SafetyValveAPISteamSizingCase();

        public void SetSaturationTemperature()
        {
            valve.IsSaturated = true;
            InvokeChange(nameof(valve.Enthalpy_SI));
            InvokeChange(nameof(valve.VapourFraction));
        }
    }
}
