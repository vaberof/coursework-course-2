using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork.Domain.TechnogenicObject
{
    public interface IDecompositionService
    {
        List<double> CalculateMValues(DataGridView dataGridTable);
        List<double> CalculateAlphaValues(DataGridView dataGridTable, List<double> MValues);
        DataGridView CalculateLowerOrUpperBound(DataTable dataTable, double epsilon, DataGridView dataGridTable, string arithmeticOperator);
        List<double> GetPredictedValues(List<double> values, double exponentialCoefficient);
    }
}
