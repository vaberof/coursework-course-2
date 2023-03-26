using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork.Domain.TechnogenicObject
{
    internal interface ITechnogenicObjectService
    {
        void FillDataGridTable(DataGridView dataGridTable);
        void DeleteEpoches(List<int> selectedRowsIndexes);
    }
}
