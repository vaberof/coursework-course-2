using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork.Service.Chart
{
    public interface IFirstLevelDecompositionService
    {
        void CalculateValuesInCoordinatesTable(
            ref List<List<double>> calculatedValues,
            ref Dictionary<string, List<double>> calculatedAlphaAndMValues,
            DataGridView dataGridTable,
            DataTable dataTable,
            double epsilon,
            double alpha);
        void FillValuesInEstimationTable(
            ref Dictionary<string, List<double>> calculatedAlphaAndMValues,
            DataGridView dataGridView);
        void CreateCoordinatesTableColumns(DataGridView coordinatesTable);
        void CreateEstimationTableColumns(DataGridView estimationTable);
    }
}
