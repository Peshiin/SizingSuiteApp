using EngineeringUnits.Units;
using Microsoft.VisualBasic.FileIO;
using SizingSuiteApp.ViewModels;
using SizingSuiteControlLibrary.Model;
using SizingSuiteControlLibrary.Model.Piping;
using SizingSuiteControlLibrary.Views.Piping;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace SizingSuiteControlLibrary.ViewModels.Piping
{
    public class PipingViewModel : BaseViewModel
    {
        private CalculationCross _selectedCross;
        //public UserControl CalculationControlTemplate = new PipingCalcView();
        public UnitManager UnitManager = new UnitManager();
        private ObservableCollection<CalculationCross> _crosses;
        public ObservableCollection<CalculationCross> crosses 
        { 
            get
            {
                return _crosses;
            }
            set
            {
                _crosses = value;
                InvokeChange(nameof(crosses));
            }
        }
        public CalculationCross SelectedCross
        {
            get
            {
                return _selectedCross;
            }
            set
            {
                _selectedCross = value;
                InvokeChange(nameof(SelectedCross));
            }
        }

        #region Constructor
        public PipingViewModel()
        {
            //Crosses = new ObservableCollection<CalculationCross>();
        }
        #endregion

        #region Events
        #endregion

        #region Methods
        #endregion
    }
}
