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
        public ObservableCollection<double> availableWallThickness { get; set; }

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
                InvokeChange(nameof(outerDiameter));
                crossSection = PipeEquations.GetCrossSectionalArea(_outerDiameter, _wallThickness);
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
                InvokeChange(nameof(wallThickness));
                crossSection = PipeEquations.GetCrossSectionalArea(_outerDiameter, _wallThickness);
            }
        }

        private Area _crossSection = Area.Zero;
        public Area crossSection
        {
            get
            {
                return _crossSection;
            }
            set 
            {
                _crossSection = value;
                InvokeChange(nameof(crossSection));
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
            this.availableWallThickness = new ObservableCollection<double>(availableWallThickness); 
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
