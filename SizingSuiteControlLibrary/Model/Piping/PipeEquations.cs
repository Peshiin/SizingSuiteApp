using EngineeringUnits;
using Jace;
using SharpFluids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SizingSuiteControlLibrary.Model.Piping
{
    public static class PipeEquations
    {
        public static Speed GetSpeed(Fluid fluid, Pipe pipe)
        {
            CalculationEngine engine = new CalculationEngine();

            string AExpression = "m / (rho * S)";

            try
            {
                Dictionary<string, double> variables = new Dictionary<string, double>();
                variables.Add("w", pipe.CrossSection.SquareMeter);
                variables.Add("m", fluid.MassFlow.KilogramPerSecond);
                variables.Add("rho", fluid.Density.KilogramPerCubicMeter);
                return Speed.FromMeterPerSecond(engine.Calculate(AExpression, variables));
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
