using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork.Domain.TechnogenicObject
{
    public interface ITechnogenicObjectService
    {
        void FillDataGridTable(DataGridView dataGridTable);
        void AddEpoch(DataGridView dataGridTable);
        void DeleteEpoches(DataGridView dataGridTable, List<int> selectedRowsIndexes);
        DataTable GetDataTable();
    }
}
