using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace CRMEngSystem.Excel
{
    public sealed class CommercialOrderCreater
    {
        private readonly byte[] _fileBytes;
        private readonly IList<OrderEntry> _entries;
        private readonly string _sender;
        private readonly decimal _totalPrice;
        private readonly int _startTableIndex;
        private readonly int _finishTableIndex;

        public CommercialOrderCreater(byte[] fileBytes, IList<OrderEntry> entries, string sender)
        {
            _fileBytes = fileBytes;
            _entries = entries;
            _sender = sender;
            _totalPrice = 0;
            _startTableIndex = 17;
            _finishTableIndex = _startTableIndex + _entries.Count;
            foreach (var entry in _entries)
                _totalPrice += entry.Price * entry.Quantity;
        }
        public byte[] CreateCommercialOrder()
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                memoryStream.Write(_fileBytes, 0, _fileBytes.Length);
                memoryStream.Position = 0;
                using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(memoryStream, true))
                {
                    AddDataToDocument(spreadsheetDocument);
                    ApplyMerges(spreadsheetDocument);
                }
                return memoryStream.ToArray();
            }
        }

        public void AddDataToDocument(SpreadsheetDocument spreadsheetDocument)
        {
            Sheet sheet = spreadsheetDocument.WorkbookPart!.Workbook.Descendants<Sheet>().FirstOrDefault()!;
            Worksheet worksheet = ((WorksheetPart)spreadsheetDocument.WorkbookPart.GetPartById(sheet!.Id!)).Worksheet;
            SheetData sheetData = worksheet.GetFirstChild<SheetData>()!;
            Row headerRow = worksheet.Descendants<Row>().FirstOrDefault(r => r.RowIndex! == _startTableIndex)!;

            Alignment alignment = new()
            {
                Horizontal = HorizontalAlignmentValues.Center,
                Vertical = VerticalAlignmentValues.Center,
                WrapText = true
            };
            FontSize fontSize = new() { Val = 10 };
            FontName fontName = new() { Val = "Arial Cyr" };
            CellFormattingProperties cellFormattingProperties = new(alignment, fontSize, fontName, false, true);


            for (int i = 0; i < _entries.Count; i++)
            {
                Row tableRow = new()
                {
                    RowIndex = (headerRow!.RowIndex!.HasValue ? headerRow.RowIndex.Value : 0) + 1,
                    CustomHeight = true,
                    Height = 30
                };
                CellRowCreater tableCellRowCreater = new(spreadsheetDocument, tableRow);

                tableCellRowCreater.AddCellToRow(i + 1, cellFormattingProperties);
                tableCellRowCreater.AddCellToRow(_entries[i].PartNumber, cellFormattingProperties);
                cellFormattingProperties.Alignment.Horizontal = HorizontalAlignmentValues.Left;
                tableCellRowCreater.AddCellToRow(_entries[i].Description, cellFormattingProperties);
                cellFormattingProperties.Alignment.Horizontal = HorizontalAlignmentValues.Center;
                tableCellRowCreater.AddCellToRow(_entries[i].Quantity, cellFormattingProperties);
                tableCellRowCreater.AddCellToRow(_entries[i].Price, cellFormattingProperties);
                tableCellRowCreater.AddCellToRow(_entries[i].Quantity * _entries[i].Price, cellFormattingProperties);
                tableCellRowCreater.AddCellToRow(_entries[i].Customer, cellFormattingProperties);
                tableCellRowCreater.AddCellToRow(_entries[i].TradeGroup, cellFormattingProperties);
                tableCellRowCreater.AddCellToRow(_entries[i].OrderNumber, cellFormattingProperties);
                tableCellRowCreater.AddCellToRow(string.Empty, cellFormattingProperties);

                sheetData.InsertAfter(tableRow, headerRow);
                headerRow = tableRow;
            }

            Row sumRow = new()
            {
                RowIndex = Convert.ToUInt32(_finishTableIndex + 1),
                CustomHeight = true,
                Height = 30
            };

            cellFormattingProperties.Alignment.Horizontal = HorizontalAlignmentValues.Right;
            cellFormattingProperties.IsBold = true;
            CellRowCreater sumCellRowCreater = new(spreadsheetDocument, sumRow);
            sumCellRowCreater.AddCellToRow("Total Price EURO: ", cellFormattingProperties);
            for (int i = 0; i < 4; i++)
                sumCellRowCreater.AddCellToRow(string.Empty, cellFormattingProperties);
            cellFormattingProperties.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            sumCellRowCreater.AddCellToRow(_totalPrice, cellFormattingProperties);


            Row greetingsRow = new()
            {
                RowIndex = Convert.ToUInt32(_finishTableIndex + 3),
                CustomHeight = true,
                Height = 15
            };

            cellFormattingProperties.Alignment.Horizontal = HorizontalAlignmentValues.Left;
            cellFormattingProperties.IsBold = false;
            cellFormattingProperties.HasBorders = false;

            CellRowCreater greetingsCellRowCreater = new(spreadsheetDocument, greetingsRow);
            greetingsCellRowCreater.AddCellToRow(string.Empty, cellFormattingProperties);
            greetingsCellRowCreater.AddCellToRow("Best regards,", cellFormattingProperties);


            Row senderRow = new()
            {
                RowIndex = Convert.ToUInt32(_finishTableIndex + 4),
                CustomHeight = true,
                Height = 30
            };

            CellRowCreater senderRowCreater = new(spreadsheetDocument, senderRow);
            senderRowCreater.AddCellToRow(string.Empty, cellFormattingProperties);
            senderRowCreater.AddCellToRow(_sender, cellFormattingProperties);

            sheetData.InsertAfter(sumRow, headerRow);
            sheetData.InsertAfter(greetingsRow, sumRow);
            sheetData.InsertAfter(senderRow, greetingsRow);
        }
        private void ApplyMerges(SpreadsheetDocument spreadsheetDocument)
        {
            Dictionary<string, List<int>> entriesByCustomer = new();

            for (int i = 0; i < _entries.Count; i++)
            {
                if (!entriesByCustomer.ContainsKey(_entries[i].Customer))
                    entriesByCustomer[_entries[i].Customer] = new List<int>();

                entriesByCustomer[_entries[i].Customer].Add(i);
            }

            CellsMerger cellsMerger = new(spreadsheetDocument);
            foreach (var entry in entriesByCustomer)
            {
                cellsMerger.MergeCol(7, entry.Value.First() + 18, entry.Value.Last() + 18);
                cellsMerger.MergeCol(8, entry.Value.First() + 18, entry.Value.Last() + 18);
                cellsMerger.MergeCol(9, entry.Value.First() + 18, entry.Value.Last() + 18);
                cellsMerger.MergeCol(10, entry.Value.First() + 18, entry.Value.Last() + 18);
            }
            cellsMerger.MergeRow(_finishTableIndex + 1, 1, 5);
        }
    }
}
