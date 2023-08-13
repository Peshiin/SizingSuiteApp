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

namespace SizingSuiteControlLibrary.Model
{
    public class ExcelToXMLConverter
    {
        public void ExcelToDatatableToXml()
        {
            string path = "C:\\Users\\pechm\\Desktop\\Potrubí\\Rozměry a hmotnosti podle EN 10220.xlsx";
            string output = "Pipes.xml";
            Application excel = new Application();
            Workbook wb = excel.Workbooks.Open(path);
            Worksheet sheet = wb.Worksheets["List1"];

            List<BasePipe> Pipes = new List<BasePipe>();
            List<double> WT;
            string[] wtArray;
            string availableThickness;
            string strDN;
            int DN;
            string strOD;
            double OD;

            for(int i = 2; i <= 36; i++)
            {
                WT = new List<double>();
                availableThickness = sheet.Range["C" + i].Value;
                wtArray = availableThickness.Split(';');
                foreach(string wt in wtArray)
                    WT.Add(double.Parse(wt));

                //strDN = sheet.Range["A" + i].Value;
                DN = (int)sheet.Range["A" + i].Value;
                //strOD = sheet.Range["B" + i].Value;
                OD = sheet.Range["B" + i].Value;

                Pipes.Add(new BasePipe(BasePipe.PipeStandard.EN, DN, OD, WT));
            }

            wb.Close();
            excel.Quit();

            XmlSerializer serializer = new XmlSerializer(Pipes.GetType());
            using (StreamWriter sw = new StreamWriter(output))
            {
                serializer.Serialize(sw, Pipes);
            }
        }
    }
}
