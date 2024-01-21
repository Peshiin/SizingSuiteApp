using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SizingSuiteControlLibrary.Model.DataWriter
{
    internal interface IDataWriter
    {
        bool WriteToCsv();
        bool WriteToXml();
        bool WriteToExcel();
    }
}
