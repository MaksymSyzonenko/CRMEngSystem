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
                    int cellCount = 0;
                    bool hasEmptyCell = false;

                    foreach (Cell cell in row.Elements<Cell>())
                    {
                        string cellValue = GetCellValue(cell, workbookPart);
                        if (string.IsNullOrEmpty(cellValue))
                        {
                            hasEmptyCell = true;
                            break;
                        }

                        cellValues.Add(cellValue);
                        cellCount++;

                        if (cellCount >= 3)
                        {
                            break;
                        }
                    }

                    if (hasEmptyCell || cellValues.Count < 3)
                    {
                        continue;
                    }

                    excelDataList.Add((cellValues[0], Math.Round(decimal.Parse(cellValues[2], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture), 2)));
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
