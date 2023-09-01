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

    }
}
