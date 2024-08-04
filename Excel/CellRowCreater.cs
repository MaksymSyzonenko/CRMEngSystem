using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace CRMEngSystem.Excel
{
    public sealed class CellRowCreater
    {
        private readonly SpreadsheetDocument _spreadsheetDocument;
        private readonly Row _row;
        private readonly CellsFormatter _cellsFormatter;
        public CellRowCreater(SpreadsheetDocument spreadsheetDocument, Row row)
        {
            _spreadsheetDocument = spreadsheetDocument;
            _row = row;
            _cellsFormatter = new(_spreadsheetDocument);
        }
        public void AddCellToRow(object data, CellFormattingProperties cellFormattingProperties)
        {
            _row.AppendChild(_cellsFormatter.ApplyCellFormating(CreateCell(data), cellFormattingProperties));
        }
        private static Cell CreateCell(object data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));


            Cell cell = new();

            switch (data)
            {
                case string stringData:
                    cell.DataType = CellValues.String;
                    cell.CellValue = new CellValue(stringData);
                    break;
                case int intData:
                    cell.DataType = CellValues.Number;
                    cell.CellValue = new CellValue(intData);
                    break;
                case decimal decimalData:
                    cell.DataType = CellValues.Number;
                    cell.CellValue = new CellValue(decimalData);
                    break;
                default:
                    throw new ArgumentException("Unsupported data type", nameof(data));
            }

            return cell;
        }
    }
}
