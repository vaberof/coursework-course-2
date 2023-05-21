using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork.Domain.TechnogenicObject
{
    public interface ITechogenicObjectStorage
    {
        void OpenConnection();
        void CloseConnection();
        void CreateTechnogenicObjectValuesTable(double alpha, double epsilon, byte[] image, int nextEpochValue);
        void GetTechnogenicObjectValues(ref double alpha, ref double epsilon, ref byte[] imageBytes, ref int nextEpochValue);
        void UpdateAlphaAndEpsilon(double alpha, double epsilon); 
        void UpdateNextEpochValue(int epochCount);
        int GetNextEpochValue();
        void FillDataTable(DataTable table);
        void AddRow(double value);
        void AddValuesInRow(int column, int row, double value);
        void DeleteRowFromTable(List<int> selectedRowsIndexes);
    }
}
