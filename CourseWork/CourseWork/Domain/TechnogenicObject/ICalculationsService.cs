using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork.Domain.TechnogenicObject
{
    public interface ICalculationsService
    {
        List<double> CalculateMValues(DataGridView dataGridTable);
        List<double> CalculateAlphaValues(DataGridView dataGridTable, List<double> MValues);
        List<double> CalculateMValuesWithMarks(DataGridView dataGridTable, List<string> marks);
        List<double> CalculateAlphaValuesWithMarks(DataGridView dataGridTable, List<double> MValues, List<string> marks);
        DataGridView CalculateLowerOrUpperBound(DataTable dataTable, double epsilon, DataGridView dataGridTable, string arithmeticOperator);
        DataGridView CalculateLowerOrUpperBoundWithMarks(DataGridView dataGridTable, List<string> marks, double epsilon, string arithmeticOperator);
        List<double> GetPredictedValues(List<double> values, double exponentialCoefficient);
        void CalculateAndFillDiameterColumn(
            ref Dictionary<string, List<double>> calculatedAlphaAndMValues,
            DataGridView dataGridView);

        void CalculateAndFillLColumn(
            ref Dictionary<string, List<double>> calculatedAlphaAndMValues,
            DataGridView dataGridView);
    }
}
