using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_School.Models
{
    public class Docuement
    {
        public static async Task ExportToExcel(DataGridView dataGridView, string filePath)
        {
            // Create a new Excel document
            var spreadsheetDoc = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook);

            // Add a WorkbookPart to the document
            WorkbookPart workbookPart = spreadsheetDoc.AddWorkbookPart();
            workbookPart.Workbook = new Workbook();

            // Add a WorksheetPart to the WorkbookPart
            WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
            worksheetPart.Worksheet = new Worksheet(new SheetData());

            // Add a new sheet to the workbook
            Sheets sheets = spreadsheetDoc.WorkbookPart.Workbook.AppendChild(new Sheets());
            Sheet sheet = new Sheet() { Id = spreadsheetDoc.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Sheet1" };
            sheets.Append(sheet);

            // Get the data from the DataGridView
            DataTable dataTable = new DataTable();
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                dataTable.Columns.Add(column.HeaderText, column.ValueType);
            }
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                DataRow dataRow = dataTable.NewRow();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    dataRow[cell.ColumnIndex] = cell.Value;
                }
                dataTable.Rows.Add(dataRow);
            }

            // Add the column headers to the worksheet
            SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();
            Row headerRow = new Row();
            foreach (DataColumn column in dataTable.Columns)
            {
                Cell cell = new Cell();
                cell.DataType = CellValues.String;
                cell.CellValue = new CellValue(column.ColumnName);
                headerRow.Append(cell);
            }
            sheetData.Append(headerRow);

            // Add the data to the worksheet
            foreach (DataRow row in dataTable.Rows)
            {
                Row worksheetRow = new Row();
                foreach (DataColumn column in dataTable.Columns)
                {
                    Cell cell = new Cell();
                    cell.DataType = GetOpenXmlDataType(column.DataType);
                    cell.CellValue = new CellValue(row[column].ToString());
                    worksheetRow.Append(cell);
                }
                sheetData.Append(worksheetRow);
            }

            // Save the document
            workbookPart.Workbook.Save();
        }

        private static CellValues GetOpenXmlDataType(Type dataType)
        {
            if (dataType == typeof(int) || dataType == typeof(decimal) || dataType == typeof(double))
            {
                return CellValues.Number;
            }
            else if (dataType == typeof(bool))
            {
                return CellValues.Boolean;
            }
            else if (dataType == typeof(DateTime))
            {
                return CellValues.Date;
            }
            else
            {
                return CellValues.String;
            }
        }
    
    }
}
