using EngineeringUnits;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SizingSuiteControlLibrary.Model.Piping
{
    public class DN: INotifyPropertyChanged
    {
        public string Name { get; set; }
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

        public DN(Standards standard)
        {
            this.Standard = standard;
        }

        public DN(string name, Standards standard, double outerDiameter, IEnumerable<double> availableWallThickness)
        {
            this.Name = name;
            this.Standard = standard;
            this.outerDiameter = outerDiameter;
            this.availableWallThickness = availableWallThickness.ToList(); 
        }
        #endregion

        #region Methods
        #endregion

        #region Enums
        public enum Standards
        {
            EN,
            ASME
        }
        #endregion
    }
}
