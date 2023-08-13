using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SizingSuiteControlLibrary.Model.Piping
{
    public class CalculationCross
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ObservableCollection<CalculationCrossCase> Cases { get; set; }


        public CalculationCross(string name, string description,
            ObservableCollection<CalculationCrossCase> cases)
        {
            Name = name;
            Description = description;
            Cases = cases;
        }
    }

}
