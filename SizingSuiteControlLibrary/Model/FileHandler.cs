using SizingSuiteControlLibrary.Model.Piping;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CsvHelper;
using System.Diagnostics;
using CsvHelper.Configuration;
using System.CodeDom;
using System.Xml.Linq;
using EngineeringUnits;
using Microsoft.VisualBasic.FileIO;

namespace SizingSuiteControlLibrary.Model
{
    public static class FileHandler
    {
        public static string OpenDialog(string filter = "All files|*.*")
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Title = "Select file";
            dialog.Filter = filter ; //e.g.: "Excel Files|*.xls;*.xlsx;*.xlsm"

            if (dialog.ShowDialog() == true)
                return dialog.FileName;
            else
                return null;
        }

        public static ObservableCollection<DN> LoadDNCollection(string path)
        {
            ObservableCollection<DN> result = new ObservableCollection<DN>();

            string xmlData = File.ReadAllText(path);

            // Parse the XML data using XDocument
            XDocument xDocument = XDocument.Parse(xmlData);
            XElement rootElement = xDocument.Root;

            if (rootElement != null)
            {
                foreach (XElement dnElement in rootElement.Elements("DN"))
                {
                    XElement avWT = dnElement.Element("availableWallThickness");
                    Trace.WriteLine(dnElement.Element("availableWallThickness")
                                .Elements("wallThickness").Count());
                    DN dn = new DN()
                    {
                        Name = (string)dnElement.Element("Name"),
                        IsEN = (bool)dnElement.Element("IsEN"),
                        IsASME = (bool)dnElement.Element("IsASME"),
                        Standard = (DN.Standards)Enum.Parse(typeof(DN.Standards), (string)dnElement.Element("Standard")),
                        availableWallThickness = new ObservableCollection<double>(
                            dnElement.Element("availableWallThickness")
                                .Elements("wallThickness")
                                .Select(x => (double)x)),
                        outerDiameter = (double)dnElement.Element("outerDiameter"),
                        wallThickness = (double)dnElement.Element("wallThickness")
                    };

                    result.Add(dn);
                }
            }

            return result;
        }

        public static IEnumerable<T> LoadCSVCollection<T, M>(string path) where M : ClassMap
        {
            IReaderConfiguration configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                Comment = '%'
            };

            try
            {
                using (var reader = new StreamReader(path))
                using (var csv = new CsvReader(reader, configuration))
                {
                    csv.Context.RegisterClassMap<M>();
                    return csv.GetRecords<T>().ToList();
                }
            }
            catch(Exception ex)
            {
                Trace.WriteLine(ex.Message);
                return null;
            }
        }


        public static ObservableCollection<CalculationCross> LoadCrosses(string filepath, string delimiter,
            UnitManager unitManager)
        {
            ObservableCollection <CalculationCross> Crosses = new ObservableCollection<CalculationCross>();

            using (TextFieldParser parser = new TextFieldParser(filepath))
            {
                ObservableCollection<CalculationCrossCase> caseList =
                    new ObservableCollection<CalculationCrossCase>();
                CalculationCross cross = new CalculationCross("dummyCross", "", null);
                CalculationCrossCase crossCase;
                CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture("en-US");

                long headerRows = 1;

                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(delimiter);

                // ignoring header rows
                for (long i = 0; i < headerRows; i++)
                    Trace.WriteLine(parser.ReadLine());

                while (!parser.EndOfData)
                {
                    //Processing row
                    string[] fields = parser.ReadFields();

                    // if cross name does not exist in Crosses
                    if (!Crosses.Any(x => x.Name == fields[0]))
                    {
                        caseList = new ObservableCollection<CalculationCrossCase>();
                        cross = new CalculationCross(fields[0], fields[1], caseList);
                        Crosses.Add(cross);
                    }

                    try
                    {
                        crossCase = new CalculationCrossCase(
                            cross,
                            fields[1],
                            double.Parse(fields[2], cultureInfo),
                            double.Parse(fields[3], cultureInfo),
                            double.Parse(fields[4], cultureInfo),
                            double.Parse(fields[5], cultureInfo),
                            unitManager);
                        caseList.Add(crossCase);
                    }
                    catch(Exception)
                    {
                        Trace.WriteLine("Invalid case");
                    }
                }
            }
            return Crosses;
        }
        
    }
}
