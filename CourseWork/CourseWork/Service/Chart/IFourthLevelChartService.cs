using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Service.Chart
{
    public interface IFourthLevelChartService
    {
        void AddXYLine(string serieName, List<double> xValues, List<double> yValues, System.Windows.Forms.DataVisualization.Charting.Chart chart);
        void AddPredictedValue(string serieName, List<double> xValues, List<double> yValues, System.Windows.Forms.DataVisualization.Charting.Chart chart);
        void RemoveLine(System.Windows.Forms.DataVisualization.Charting.Chart chart, string serieName);
        void ClearChart(System.Windows.Forms.DataVisualization.Charting.Chart chart);

    }
}
