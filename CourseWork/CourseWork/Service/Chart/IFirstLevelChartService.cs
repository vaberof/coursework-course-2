using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace CourseWork.Service.Chart.FirstLevel
{
    public interface IFirstLevelChartService
    {
        void AddXYLine(string serieName, List<double> xValues, List<double> yValues, System.Windows.Forms.DataVisualization.Charting.Chart chart);
        void RemoveLine(System.Windows.Forms.DataVisualization.Charting.Chart chart, string serieName);
    }
}
