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
        public void LoadCrosses(string filepath, string delimiter)
        {
            using (TextFieldParser parser = new TextFieldParser(filepath))
            {
                ObservableCollection<CalculationCrossCase> caseList =
                    new ObservableCollection<CalculationCrossCase>();
                CalculationCrossCase crossCase;
                CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture("en-US");

                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(delimiter);
                while (!parser.EndOfData)
                {
                    //Processing row
                    string[] fields = parser.ReadFields();

                    // ignores header
                    if (parser.LineNumber <= 2)
                        continue;

                    // if cross name does not exist in Crosses
                    if (!Crosses.Any(x => x.Name == fields[0]))
                    {
                        caseList = new ObservableCollection<CalculationCrossCase>();
                        Crosses.Add(new CalculationCross(fields[0], fields[1], caseList));
                    }

                    crossCase = new CalculationCrossCase(
                        fields[0],
                        fields[1],
                        double.Parse(fields[2], cultureInfo),
                        double.Parse(fields[3], cultureInfo),
                        double.Parse(fields[4], cultureInfo),
                        double.Parse(fields[5], cultureInfo),
                        UnitManager);
                    caseList.Add(crossCase);
                }
            }
            InvokeChange(nameof(Crosses));
        }
        #endregion
    }
}
