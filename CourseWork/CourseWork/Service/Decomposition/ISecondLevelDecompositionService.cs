using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork.Service.Chart
{
    public interface ISecondLevelDecompositionService
    {
        void CalculateValuesInMarksCoordinatesTable(
            ref List<List<double>> calculatedValues,
            ref Dictionary<string, List<double>> calculatedAlphaAndMValues,
            List<string> marks,
            DataGridView dataGridTable,
            double epsilon,
            double alpha);

        void FillValuesInEstimationTable(
            ref Dictionary<string, List<double>> calculatedAlphaAndMValues,
            DataGridView dataGridView);

        void CreateCoordinatesTableColumns(DataGridView coordinatesTable);
        void CreateEstimationTableColumns(DataGridView estimationTable);
    }
}
