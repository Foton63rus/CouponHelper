using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace ExcelHelper
{
    public class ExcelHelper
    {
        private string DataFilePath = @"\Save\Data.xlsx";

        private Excel.Application xlApp;
        private Excel.Workbook wb;
        private Excel.Worksheet ws;

        private string SHEET_PRODUCTS;
        private string SHEET_ACCOUNTS;
        private string SHEET_SM;

        public ExcelHelper()
        {
            openApplication();
            addDocument();
        }
        public void addDocument()
        {

            workbookOpen();


        }
        private void openApplication()
        {
            xlApp = new Excel.Application();
            xlApp.Visible = true;
        }
        /// <summary>
        /// Открыть эксель-книгу с указанным путем к файлу
        /// </summary>
        /// <param name="path"></param>
        public void workbookOpen(string path)
        {
            bool xlFileIsExist = new FileInfo(DataFilePath).Exists;
            if (xlFileIsExist)
            {
                wb = xlApp.Workbooks.Open(DataFilePath);
            }
            else
            {
                workbookCreateTemplate();
            }
        }
        /// <summary>
        /// Создает новый эксель документ
        /// </summary>
        private void workbookCreateTemplate()
        {
            wb = xlApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
            wb.Worksheets.Add();
            wb.Worksheets.Add();

            xlApp.Worksheets[1].Name = SHEET_PRODUCTS;
            xlApp.Worksheets[2].Name = SHEET_ACCOUNTS;
            xlApp.Worksheets[3].Name = SHEET_SM;

            ws = xlApp.Worksheets[1];
        }
        public void workbookOpen()
        {
            workbookOpen(DataFilePath);
        }
        /// <summary>
        /// Проверка на существование листа в текущем документе
        /// </summary>
        /// <param name="wsName"></param>
        /// <returns></returns>
        public bool WorkSheetIsExist(string wsName)
        {
            bool isExist = false;
            if (xlApp != null && wb != null)
            {
                foreach (Excel.Worksheet sheet in wb.Worksheets)
                {
                    if (sheet.Name == wsName)
                    {
                        isExist = true;
                        break;
                    }
                }
            }
            return isExist;
        }
        public void worksheetOpen(string sheetName)
        {
            if (WorkSheetIsExist(sheetName))
            {
                ws = wb.Worksheets[sheetName];
            }
        }

    }
}
