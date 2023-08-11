using EngineeringUnits.Units;
using SizingSuiteApp.ViewModels;
using SizingSuiteControlLibrary.Model.Piping;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SizingSuiteControlLibrary.ViewModels.Piping
{
    public class PipingViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public UnitManager UnitManager = new UnitManager();


        #region Constructor
        public PipingViewModel()
        {
        }
        #endregion


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
    }
}
