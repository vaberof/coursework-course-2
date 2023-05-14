using CourseWork.Domain.TechnogenicObject;
using CourseWork.Handlers.Application.ToolStrip.Project;
using CourseWork.Service.Calculation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // initialize handlers for forms
            ProjectToolHandler projectToolHandler = new ProjectToolHandler();

            ICalculationService calculationsService = new CalculationService();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ApplicationForm(projectToolHandler, calculationsService));
        }
    }
}
