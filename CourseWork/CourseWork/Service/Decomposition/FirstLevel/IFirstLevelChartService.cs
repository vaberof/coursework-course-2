using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace CourseWork.Service.Decomposition.FirstLevel
{
    internal interface IFirstLevelChartService
    {
        void AddXYLine(string serieName, List<double> xValues, List<double> yValues, Chart chart);
        void RemoveLine(Chart chart, string serieName);
    }
}
