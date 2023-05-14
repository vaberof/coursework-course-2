using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork.Service.Calculation
{
    public interface ICalculationService
    {
        List<double> CalculateMValues(DataGridView mainCoordinatesTable);
        List<double> CalculateAlphaValues(DataGridView mainCoordinatesTable, List<double> MValues);
        List<double> CalculateMValuesWithMarks(DataGridView mainCoordinatesTable, List<string> marks);
        List<double> CalculateAlphaValuesWithMarks(DataGridView mainCoordinatesTable, List<double> MValues, List<string> marks);
        DataGridView CalculateLowerOrUpperBound(DataTable dataTable, double epsilon, DataGridView mainCoordinatesTable, string arithmeticOperator);
        DataGridView CalculateLowerOrUpperBoundWithMarks(DataGridView mainCoordinatesTable, List<string> marks, double epsilon, string arithmeticOperator);
        List<double> GetPredictedValues(List<double> values, double exponentialCoefficient);
        void CalculateAndFillDiameterColumn(
            ref Dictionary<string, List<double>> calculatedAlphaAndMValues,
            DataGridView dataGridView);

        void CalculateAndFillLColumn(
            ref Dictionary<string, List<double>> calculatedAlphaAndMValues,
            DataGridView dataGridView);
    }
}
