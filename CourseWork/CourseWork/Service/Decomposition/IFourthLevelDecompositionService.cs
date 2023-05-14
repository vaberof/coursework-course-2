using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork.Service.Decomposition
{
    // TODO: все методы переименовать в camelCase
    public interface IFourthLevelDecompositionService
    {
        void FillMarksInCheckedListBox(int marksCount, CheckedListBox checkedListBox);
        List<double> getPredictedValues(List<double> marksValues, double alpha);
    }
}
