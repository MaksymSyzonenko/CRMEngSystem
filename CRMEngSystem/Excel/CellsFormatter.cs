using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace CRMEngSystem.Excel
{
    public sealed class CellsFormatter
    {
        private readonly SpreadsheetDocument _spreadsheetDocument;
        public CellsFormatter(SpreadsheetDocument spreadsheetDocument)
        {
            _spreadsheetDocument = spreadsheetDocument;
        }
        public Cell ApplyCellFormating(Cell cell, CellFormattingProperties cellFormattingProperties)
        {
            WorkbookStylesPart stylesPart = _spreadsheetDocument.WorkbookPart!.WorkbookStylesPart!;
            if (stylesPart == null)
            {
                stylesPart = _spreadsheetDocument.WorkbookPart.AddNewPart<WorkbookStylesPart>();
                stylesPart.Stylesheet = new Stylesheet();
            }

            CellFormat cellFormat = new();

            Alignment copyAlignment = new()
            {
                Horizontal = cellFormattingProperties.Alignment.Horizontal,
                Vertical = cellFormattingProperties.Alignment.Vertical,
                WrapText = cellFormattingProperties.Alignment.WrapText
            };
            cellFormat.Append(copyAlignment);

            Font font = new()
            {
                FontSize = new() { Val = cellFormattingProperties.FontSize.Val },
                FontName = new() { Val = cellFormattingProperties.FontName.Val },
                Bold = cellFormattingProperties.IsBold ? new() : null
            };
            CellFormats cellFormats = stylesPart.Stylesheet.CellFormats!;
            Fonts fonts = _spreadsheetDocument.WorkbookPart.WorkbookStylesPart!.Stylesheet.Fonts!;
            fonts.Append(font);
            cellFormat.FontId = Convert.ToUInt32(fonts.Elements<Font>().Count() - 1);

            cellFormat.BorderId = cellFormattingProperties.HasBorders ? Convert.ToUInt32(1) : Convert.ToUInt32(0);

            cell.StyleIndex = AddCellFormat(cellFormats, cellFormat);
            return cell;
        }
        private static uint AddCellFormat(CellFormats cellFormats, CellFormat cellFormat)
        {
            cellFormats.AppendChild(cellFormat);
            return (uint)cellFormats.Count!++;
        }
    }
}
