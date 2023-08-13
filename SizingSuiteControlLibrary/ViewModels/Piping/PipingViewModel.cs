using EngineeringUnits.Units;
using SizingSuiteApp.ViewModels;
using SizingSuiteControlLibrary.Model;
using SizingSuiteControlLibrary.Model.Piping;
using SizingSuiteControlLibrary.Views.Piping;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace SizingSuiteControlLibrary.ViewModels.Piping
{
    public class PipingViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private CalculationCross _selectedCross;
        public UserControl CalculationControlTemplate = new PipingCalculationLineView();
        public UnitManager UnitManager = new UnitManager();
        public ObservableCollection<CalculationCross> Crosses { get; set; }
        public ObservableCollection<BasePipe> Pipes { get; set; }
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
            Pipes = (ObservableCollection<BasePipe>)FileHandler.LoadXMLCollection<BasePipe>("Pipes.xml");
            Crosses = new ObservableCollection<CalculationCross>();
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Invokes change of class property
        /// </summary>
        /// <param name="property"></param>
        protected void InvokeChange(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
        #endregion

        #region Methods
        public void LoadCrosses(string filepath)
        {
            List<CalculationCrossCase> csvInput =
                FileHandler.LoadCSVCollection<CalculationCrossCase, CalculationCrossCaseMap>(filepath).ToList();
            IEnumerable<string> crossNames = csvInput.Select(x => x.CrossName).Distinct();
            ObservableCollection<CalculationCrossCase> caseList;

            foreach (string name in crossNames)
            {
                caseList = new ObservableCollection<CalculationCrossCase>(csvInput.Where(x => x.CrossName == name));
                Crosses.Add(new CalculationCross(name, "", caseList));
            }
            InvokeChange(nameof(Crosses));
        }
        #endregion
    }
}
