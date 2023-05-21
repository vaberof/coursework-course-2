using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork.Domain.TechnogenicObject
{
    public interface ITechnogenicObjectService
    {
        void CreateTechnogenicObjectValuesTable(double alpha, double epsilon, string pngFilePath, int nextEpochValue);
        void GetTechnogenicObjectValues(ref double alpha, ref double epsilon, ref Image image, ref int nextEpochCount);
        void UpdateAlphaAndEpsilon(double alpha, double epsilon);
        void FillMainCoordinatesTable(DataGridView mainCoordinatesTable);
        void AddEpoch(DataGridView mainCoordinatesTable, ref int currentEpochCount);
        void DeleteEpochs(DataGridView mainCoordinatesTable, List<int> selectedRowsIndexes, ref int currentEpochCount); 
        DataTable GetDataTable();
    }
}
