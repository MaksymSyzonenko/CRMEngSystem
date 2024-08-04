using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;

namespace CRMEngSystem.Excel
{
    public sealed class CellsMerger
    {
        private readonly SpreadsheetDocument _spreadsheetDocument;
        public CellsMerger(SpreadsheetDocument spreadsheetDocument)
        {
            _spreadsheetDocument = spreadsheetDocument;
        }
        public void MergeRow(int rowNumber, int startColumn, int endColumn)
        {
            string cellReference1 = GetCellReference(startColumn, rowNumber);
            string cellReference2 = GetCellReference(endColumn, rowNumber);
            AppendMergeCell(cellReference1, cellReference2);
        }
        public void MergeCol(int columnNumber, int startRow, int endRow)
        {
            string cellReference1 = GetCellReference(columnNumber, startRow);
            string cellReference2 = GetCellReference(columnNumber, endRow);
            AppendMergeCell(cellReference1, cellReference2);
        }
        private void AppendMergeCell(string cellReference1, string cellReference2)
        {
            MergeCell mergeCell = new() { Reference = new StringValue($"{cellReference1}:{cellReference2}") };
            MergeCells mergeCells = GetMergeCells(_spreadsheetDocument);
            mergeCells.Append(mergeCell);
        }
        private static MergeCells GetMergeCells(SpreadsheetDocument spreadsheetDocument)
        {
            WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart!;
            Sheet sheet = spreadsheetDocument.WorkbookPart!.Workbook.Descendants<Sheet>().FirstOrDefault()!;
            WorksheetPart worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id!);
            SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();
            MergeCells mergeCells = worksheetPart.Worksheet.Elements<MergeCells>().FirstOrDefault()!;

            if (mergeCells == null)
            {
                mergeCells = new MergeCells();
                worksheetPart.Worksheet.InsertAfter(mergeCells, sheetData);
            }

            return mergeCells;
        }
        private string GetCellReference(int columnIndex, int rowIndex)
        {
            string columnName = GetColumnName(columnIndex);
            return $"{columnName}{rowIndex}";
        }
        private static string GetColumnName(int columnIndex)
        {
            int dividend = columnIndex;
            string columnName = string.Empty;

            while (dividend > 0)
            {
                int modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo) + columnName;
                dividend = (dividend - modulo) / 26;
            }

            return columnName;
        }
    }
}
