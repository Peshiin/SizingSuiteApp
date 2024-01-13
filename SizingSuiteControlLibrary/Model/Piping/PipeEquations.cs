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
        public static Speed GetSpeed(MassFlow flowRate, Density density, Area crossSection)
        {
            return flowRate / (density * crossSection);
        }

        public static Area GetCrossSectionalAreaByDxt(Length outerDiameter, Length wallThickness)
        {
            return Math.PI / 4 * (outerDiameter - 2 * wallThickness).Pow(2);
        }

        public static Area GetRecommendedCrossSectionalArea(MassFlow flowrate, Density density, Speed recommendedVelocity)
        {
            return flowrate / (density * recommendedVelocity);
        }
    }
}
