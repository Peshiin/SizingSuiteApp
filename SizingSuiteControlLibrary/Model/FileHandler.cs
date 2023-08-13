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

        public static ICollection<T> LoadXMLCollection<T>(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<T>));
            if (File.Exists(path))
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    return(ICollection<T>)serializer.Deserialize(reader);
                }
            }
            else
                return null;
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
