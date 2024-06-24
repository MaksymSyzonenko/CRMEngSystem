using DocumentFormat.OpenXml.Spreadsheet;

namespace CRMEngSystem.Excel
{
    public sealed class CellFormattingProperties
    {
        public Alignment Alignment { get; set; }
        public FontSize FontSize { get; set; }
        public FontName FontName { get; set; }
        public bool IsBold { get; set; }
        public bool HasBorders { get; set; }
        public CellFormattingProperties(Alignment alignment, FontSize fontSize, FontName fontName, bool isBold, bool hasBorders)
        {
            Alignment = alignment;
            FontSize = fontSize;
            FontName = fontName;
            IsBold = isBold;
            HasBorders = hasBorders;
        }
    }
}
