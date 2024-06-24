using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Globalization;

namespace CRMEngSystem.Excel
{
    public static class PriceReader
    {
        public static List<(string, decimal)> ReadExcelData(string filePath)
        {
            List<(string, decimal)> excelDataList = new();

            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(filePath, false))
            {
                WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;
                WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();
                SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

                bool isFirstRow = true;

                foreach (Row row in sheetData.Elements<Row>())
                {
                    if (isFirstRow)
                    {
                        isFirstRow = false;
                        continue;
                    }

                    List<string> cellValues = new();

                    foreach (Cell cell in row.Elements<Cell>())
                    {
                        cellValues.Add(GetCellValue(cell, workbookPart));
                    }

                    if (cellValues.Count >= 3)
                    {
                        excelDataList.Add((cellValues[0], Math.Round(decimal.Parse(cellValues[2], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture), 2)));
                    }
                }
            }

            return excelDataList;
        }

        private static string GetCellValue(Cell cell, WorkbookPart workbookPart)
        {
            string cellValue = cell.InnerText;

            if (cell.DataType != null && cell.DataType == CellValues.SharedString)
            {
                SharedStringTablePart sharedStringPart = workbookPart.SharedStringTablePart;
                if (sharedStringPart != null)
                {
                    cellValue = sharedStringPart.SharedStringTable.ChildElements[int.Parse(cellValue)].InnerText;
                }
            }

            return cellValue;
        }
    }
}
