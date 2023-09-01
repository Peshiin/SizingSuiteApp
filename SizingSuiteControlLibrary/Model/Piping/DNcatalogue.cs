using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SizingSuiteControlLibrary.Model.Piping
{
    public class DNcatalogue
    {
        #region Properties
        private string SourceFilePath = Path.Combine(Environment.CurrentDirectory, @"DataStorage\", "AvailableDNs.xml");
        public ObservableCollection<DN> AvailableDNs { get; private set; }
        #endregion

        #region Constructor
        public DNcatalogue()
        {
            AvailableDNs = FileHandler.LoadDNCollection(SourceFilePath);
        }

        public DNcatalogue(IEnumerable<DN> availableDNs)
        {
            AvailableDNs = (ObservableCollection<DN>)availableDNs;
        }
        #endregion

        #region Events
        #endregion

        #region Methods
        #endregion
    }
}
