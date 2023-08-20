using EngineeringUnits;
using EngineeringUnits.Units;
using SharpFluids;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace SizingSuiteControlLibrary.Model.Piping
{
    public class BasePipe: INotifyPropertyChanged
    {
        public ObservableCollection<DN> DNs {get; set;}

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        protected void InvokeChange(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
        #endregion

        #region Constructor
        public BasePipe()
        {
            //DNs = (ObservableCollection<DN>)DN.GetAvailableDNs();
        }
        #endregion

    }
}
