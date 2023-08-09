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

namespace SizingSuiteApp.ViewModels.SafetyValve
{
    public class APISteamReliefViewModel: BaseViewModel, INotifyPropertyChanged
    {
        #region Constants
        private readonly Pressure AtmosphericPressure = Pressure.FromKilopascal(101);
        private double AllowableOverpressure = 1.1;
        #endregion

        #region DataStorage
        private Pressure _p1 = new Pressure();
        private double _vapourFraction;
        private double _kd = 0.975;
        private double _kb = 1.0;
        private double _kc = 1.0;
        private double _kN;
        private KSH _kSH = new KSH();
        private double _ksh;
        private Area _dischargeArea = new Area(0, EngineeringUnits.Units.AreaUnit.SquareMillimeter);
        private bool _isSaturated = false;
        #endregion

        #region Properties
        private Fluid Steam = new Fluid(FluidList.Water);
        public double PSet_SI
        {
            get
            {
                return Steam.Pressure.Bar - 1;
            }
            set
            {
                Steam.UpdatePT(Pressure.FromBar(value + 1), Steam.Temperature);
                if (Steam.Quality == -1)
                    IsSaturated = false;
                P1_SI = (PSet_SI + 1) * AllowableOverpressure;
                KSH = _kSH.GetKSH(PSet_SI, Temperature_SI);
                InvokeChange(nameof(PSet_SI));
                InvokeChange(nameof(VapourFraction));
                InvokeChange(nameof(Enthalpy_SI));
                EnthalpySetManually = false;
            }
        } // set pressure bar(g) - that is why +- 1 in getter and setter
        public double Temperature_SI
        {
            get
            {
                if (IsSaturated)
                    return Steam.Tsat.DegreesCelsius;
                else
                    return Steam.Temperature.DegreesCelsius;
            }
            set
            {
                Steam.UpdatePT(Steam.Pressure, Temperature.FromDegreesCelsius(value));
                if(Steam.Quality == -1)
                    IsSaturated = false;
                KSH = _kSH.GetKSH(P1_SI, Temperature_SI);
                InvokeChange(nameof(Temperature_SI));
                InvokeChange(nameof(Enthalpy_SI));
                EnthalpySetManually = false;
            }
        } // Valve inlet steam temperature [°C]
        public double MassFlow_SI
        {
            get
            {
                return Steam.MassFlow.KilogramPerSecond;
            }
            set
            {
                Steam.MassFlow = MassFlow.FromKilogramPerSecond(value);
                InvokeChange(nameof(MassFlow_SI));
            }
        } // Required flow rate [kg/s]
        public double P1_SI
        {
            get
            {
                return _p1.Bar;
            }
            set
            {
                _p1 = Pressure.FromBar(value);
                KN = GetKN(P1_SI);
                InvokeChange(nameof(P1_SI));
            }
        } // upstream relieving pressure [bar(a)] (set pressure + atmospheric pressure + allowable overpressure)
        public double Enthalpy_SI
        {
            get
            {
                if (IsSaturated && !EnthalpySetManually)
                    return double.NaN;
                else
                    return Steam.Enthalpy.KilojoulePerKilogram;
            }
            set
            {
                Steam.UpdatePH(Steam.Pressure, Enthalpy.FromKilojoulePerKilogram(value));
                if (Steam.Temperature.DegreesCelsius == Steam.Tsat.DegreesCelsius)
                    IsSaturated = true;
                else
                {
                    IsSaturated = false;
                    InvokeChange(nameof(Temperature_SI));
                }
                EnthalpySetManually = true;
                InvokeChange(nameof(Enthalpy_SI));
                InvokeChange(nameof(VapourFraction));
            }
        } // upstream relieving enthalpy [kJ/kg] 
        public double VapourFraction
        {
            get
            {
                return Steam.Quality;
            }
            set
            {
                _vapourFraction = value;
                InvokeChange(nameof(VapourFraction));
            }
        } // steam dryness factor [-] 
        public double Kd
        {
            get
            {
                return _kd;
            }
            set
            {
                _kd = value;
                InvokeChange(nameof(Kd));
            }
        } // effective coefficient of discharge (0.65 for rupture disc)
        public double Kb
        {
            get
            {
                return _kb;
            }
            set
            {
                _kb = value;
                InvokeChange(nameof(Kb));
            }
        } // capacity correction factor due to backpressure (different only if bellows are used)
        public double Kc
        {
            get
            {
                return _kc;
            }
            set
            {
                _kc = value;
                InvokeChange(nameof(Kc));
            }
        } // combination correction factor for installations with a rupture disk upstream of the PRV (0.9 with RD)
        public double KN
        {
            get
            {
                return _kN;
            }
            set
            {
                _kN = value;
                InvokeChange(nameof(KN));
            }
        } // correction factor for the Napier equation
        public double KSH
        {
            get
            {
                return _kSH.GetKSH(P1_SI, Temperature_SI);
            }
            set
            {
                _ksh = value;
                InvokeChange(nameof(KSH));
            }
        } // superheat correction factor
        public double DischargeArea_SI
        {
            get
            {
                return _dischargeArea.SquareMillimeter;
            }
            set
            {
                _dischargeArea = Area.FromSquareMillimeter(value);
                InvokeChange(nameof(DischargeArea_SI));
            }
        }
        public bool IsSaturated
        {
            get
            {
                return _isSaturated;
            }
            set
            {
                _isSaturated = value;
                InvokeChange(nameof(IsSaturated));
                InvokeChange(nameof(Temperature_SI));
            }
        }
        public bool EnthalpySetManually = false;
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;

        private void APISteamRelief_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Update of discharge area
            if (e.PropertyName != nameof(DischargeArea_SI))
                if(GetDischargeArea() != null)
                    DischargeArea_SI = GetDischargeArea().SquareMillimeter;
    }
        #endregion

        #region Constructor
        public APISteamReliefViewModel()
        {
            this.PropertyChanged += APISteamRelief_PropertyChanged;
            Steam.Media.BackendType = "IF97::Water";
            Steam.UpdatePT(Pressure.FromBar(1.5), Temperature.FromDegreesCelsius(0));
            Steam.MassFlow = new MassFlow(0, EngineeringUnits.Units.MassFlowUnit.KilogramPerSecond);
            P1_SI = (PSet_SI + AtmosphericPressure.Bar) * AllowableOverpressure;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Invokes change of class property
        /// </summary>
        /// <param name="property"></param>
        protected void InvokeChange(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        /// <summary>
        /// Gets correction factor for the Napier equation
        /// </summary>
        /// <param name="P1">Set pressure [psi(a)]</param>
        /// <returns></returns>
        private double GetKN(double P1)
        {
            Pressure Pressure = Pressure.FromBar(P1);
            double psia = Pressure.PoundForcePerSquareInch;

            if (psia <= 1500)
                return 1.0;
            else if (psia > 1500 && psia <= 3200)
                return (0.1906 * psia - 1000) / (0.2292 * psia - 1061);
            else
                return double.NaN;
        }

        public Area GetDischargeArea()
        {
            CalculationEngine engine = new CalculationEngine();

            double W_USC = Steam.MassFlow.PoundPerHour;
            double P1_USC = _p1.PoundForcePerSquareInch;
            string AExpression = "W / (51,5 * P1 * Kd * Kb * Kc * KN * KSH)";

            try
            {
                Dictionary<string, double> variables = new Dictionary<string, double>();
                variables.Add("W", W_USC);
                variables.Add("P1", P1_USC);
                variables.Add(nameof(Kd), Kd);
                variables.Add(nameof(Kb), Kb);
                variables.Add(nameof(Kc), Kc);
                variables.Add(nameof(KN), KN);
                variables.Add(nameof(KSH), KSH);
                return Area.FromSquareInch(engine.Calculate(AExpression, variables));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void SetSaturationTemperature()
        {
            IsSaturated = true;
            InvokeChange(nameof(Enthalpy_SI));
            InvokeChange(nameof(VapourFraction));
        }
        #endregion
    }
}
