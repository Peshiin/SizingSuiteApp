using EngineeringUnits.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace SizingSuiteControlLibrary.Model.Piping
{
    public class BasePipe
    {
        private PipeStandard _Standard;

        public bool IsEN;
        public bool IsASME;
        public PipeStandard Standard
        {
            get
            {
                return _Standard;
            }
            set
            {
                _Standard = value;
                if (value == PipeStandard.EN)
                {
                    IsEN = true;
                    IsASME = false;
                }
                else if(value == PipeStandard.ASME)
                {
                    IsEN = false;
                    IsASME = true;
                }
            }
        }
        public int DN;
        public double OD;
        public List<double> AvailableWallThickness;

        #region Constructor
        public BasePipe()
        {

        }
        public BasePipe(PipeStandard standard, int dN, double oD, List<double> availableWallThickness)
        {
            Standard = standard;
            DN = dN;
            OD = oD;
            AvailableWallThickness = availableWallThickness;
        }
        #endregion

        public enum PipeStandard
        {
            EN,
            ASME
        }
    }
}
