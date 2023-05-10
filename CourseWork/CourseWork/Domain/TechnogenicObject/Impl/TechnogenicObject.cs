using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Domain.TechnogenicObject
{
    public class TechnogenicObject
    {
        public int TaskOption; // вариант задания 
        public string Type; // тип объекта
        public int StructuralBlocksCount; // количество структурных блоков 
        public int GeodeticMarksCount; // количество геодезических марок
        public int MeasurementEpochsCount; // количество измерений эпох
        public double MeasurementAccuracy; // точность измерения (погрешность)
    
        public TechnogenicObject(int taskOption, string type, int structuralBlocksCount, int geodeticMarksCount, int measurementEpochsCount, double measurementAccuracy)
        {
            TaskOption = taskOption;
            Type = type;
            StructuralBlocksCount = structuralBlocksCount;
            GeodeticMarksCount = geodeticMarksCount;
            MeasurementEpochsCount = measurementEpochsCount;
            MeasurementAccuracy = measurementAccuracy;
        }
    }

    
}
