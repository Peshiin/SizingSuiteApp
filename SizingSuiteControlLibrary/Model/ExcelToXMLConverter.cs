using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Controls;
using System.Xml.Linq;
using Apache.Arrow;
using System.Reflection;
using System.Xml.Serialization;
using SizingSuiteControlLibrary.Model.Piping;
using System.Windows.Shapes;
using Microsoft.Office.Interop.Excel;
using System.IO.Pipes;
using EngineeringUnits;

namespace SizingSuiteControlLibrary.Model
{
    public class ExcelToXMLConverter
    {
        public void BasePipesExcelToXML()
        {
            string path = @"C:\Users\pechm\Desktop\Potrubí\Rozměry a hmotnosti podle EN 10220.xlsx";
            string output = @"C:\Users\pechm\Desktop\Other Projects\SizingSuiteApp\SizingSuiteControlLibrary\Model\Piping\Pipes.xml";
            Application excel = new Application();
            Workbook wb = excel.Workbooks.Open(path);
            Worksheet sheet = wb.Worksheets["List1"];

            List<double> WTs;
            List<DN> DNs = new List<DN>();
            string availableThickness;

            for(int i = 2; i <= sheet.UsedRange.Rows.Count; i++)
            {
                WTs = new List<double>();

                availableThickness = sheet.Range["C" + i].Value;
                foreach(string wt in availableThickness.Split(';'))
                    WTs.Add(double.Parse(wt));

                DNs.Add(new DN(
                    DN.Standards.EN,
                    (DN.AvailableDNs)sheet.Range["A" + i].Value,
                    sheet.Range["B" + i].Value,
                    WTs.AsEnumerable()));
            }

            wb.Close();
            excel.Quit();

            XmlSerializer serializer = new XmlSerializer(DNs.GetType());
            using (StreamWriter sw = new StreamWriter(output))
            {
                serializer.Serialize(sw, DNs);
            }
        }
    }
}
