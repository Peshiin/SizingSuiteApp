using EngineeringUnits;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SizingSuiteControlLibrary.Model.Piping.BasePipe;

namespace SizingSuiteControlLibrary.Model.Piping
{
    public class DN: INotifyPropertyChanged
    {
        private Standards _Standard;
        public bool IsEN;
        public bool IsASME;
        public Standards Standard
        {
            get
            {
                return _Standard;
            }
            set
            {
                _Standard = value;
                if (value == Standards.EN)
                {
                    IsEN = true;
                    IsASME = false;
                }
                else if (value == Standards.ASME)
                {
                    IsEN = false;
                    IsASME = true;
                }
            }
        }
        public AvailableDNs SelectedDN = AvailableDNs.DN25;
        private Length _outerDiameter = new Length(Length.Zero);
        public double outerDiameter
        {
            get
            {
                return _outerDiameter.Millimeter;
            }
            set
            {
                _outerDiameter = Length.FromMillimeter(value);
            }
        }

        private Length _wallThickness = new Length(Length.Zero);
        public double wallThickness
        {
            get
            {
                return _wallThickness.Millimeter;
            }
            set
            {
                _wallThickness = Length.FromMillimeter(value);
            }
        }

        private List<Length> _availableWallThickness = new List<Length>();
        public List<double> availableWallThickness 
        {
            get 
            {
                List<double> result = new List<double>();
                foreach(Length n in _availableWallThickness)
                    result.Add(n.Millimeter);
                return result;
            }
            set
            {
                _availableWallThickness = new List<Length>();
                foreach(double n in value)
                    _availableWallThickness.Add(Length.FromMillimeter(n));
            }
        }

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        protected void InvokeChange(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
        #endregion

        #region Constructor
        public DN(){ }

        public DN(Standards standard, double outerDiameter)
        {
            this.Standard = standard;
            this.outerDiameter = outerDiameter;
        }

        public DN(Standards standard, double outerDiameter, IEnumerable<double>availableWallThickness)
        {
            this.Standard = standard;
            this.outerDiameter = outerDiameter;

            foreach(double wt in availableWallThickness)
                this.availableWallThickness = availableWallThickness.ToList();
        }

        public DN(Standards standard, AvailableDNs selectedDN, double outerDiameter, IEnumerable<double> availableWallThickness)
        {
            this.Standard = standard;
            this.SelectedDN = selectedDN;
            this.outerDiameter = outerDiameter;
            this.availableWallThickness = availableWallThickness.ToList();
        }
        #endregion

        #region Methods
        public static IEnumerable<AvailableDNs> GetAvailableDNs()
        {
            return Enum.GetValues(typeof(AvailableDNs)).Cast<AvailableDNs>();
        }

        public override string ToString()
        {
            return SelectedDN.ToString();
        }
        #endregion

        #region Enums
        public enum AvailableDNs: int
        {
            DN6 = 6,
            DN8 = 8,
            DN10 = 10,
            DN15 = 15,
            DN20 = 20,
            DN25 = 25,
            DN32 = 32,
            DN40 = 40,
            DN50 = 50,
            DN65 = 65,
            DN80 = 80,
            DN100 = 100,
            DN125 = 125,
            DN150 = 150,
            DN200 = 200,
            DN250 = 250,
            DN300 = 300,
            DN350 = 350,
            DN400 = 400,
            DN450 = 450,
            DN500 = 500,
            DN600 = 600,
            DN700 = 700,
            DN800 = 800,
            DN900 = 900,
            DN1000 = 1000,
            DN1050 = 1050,
            DN1100 = 1100,
            DN1200 = 1200,
            DN1400 = 1400,
            DN1600 = 1600,
            DN1800 = 1800,
            DN2000 = 2000,
            DN2200 = 2200,
            DN2500 = 2500
        }
        public enum Standards
        {
            EN,
            ASME
        }
        #endregion
    }
}
