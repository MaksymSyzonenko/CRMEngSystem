using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace CRMEngSystem.Word
{
    public sealed class CommercialOfferCreater
    {
        private readonly byte[] _fileBytes;
        private readonly Recipient _recipient;
        private readonly Sender _sender;
        private readonly DateTime _date;
        private readonly string _currency;
        private readonly PriceValues _priceValues;
        private readonly IList<EquipmentEntry> _equipmentList;
        private readonly Dictionary<string, string> _replacements;
        public CommercialOfferCreater(byte[] fileBytes, Recipient recipient, Sender sender, DateTime date, string currency, PriceValues priceValues, IList<EquipmentEntry> equipmentList, int orderId)
        {
            _fileBytes = fileBytes;
            _recipient = recipient;
            _sender = sender;
            _date = date;
            _currency = currency;
            _priceValues = priceValues;
            _equipmentList = equipmentList;
            _replacements = CreateReplacements(orderId);
        }
        private Dictionary<string, string> CreateReplacements(int orderId)
            => new(){
                { "<OrderId>",  orderId.ToString() },
                { "<Date>", _date.ToShortDateString().ToString() },
                { "<RecipientFirstName>", _recipient.RecipientFirstName },
                { "<RecipientLastName>", _recipient.RecipientLastName },
                { "<RecipientOrganizationName>", _recipient.RecipientOrganizationName },
                { "<RecipientPhoneNumber>", _recipient.RecipientPhoneNumber },
                { "<RecipientEmail>", _recipient.RecipientEmail },
                { "<SenderProducerName>", _sender.SenderProducerName},
                { "<SenderFirstName>", _sender.SenderFirstName },
                { "<SenderLastName>", _sender.SenderLastName },
                { "<SenderPhoneNumber>", _sender.SenderPhoneNumber },
                { "<SenderEmail>", _sender.SenderEmail },
                { "<Currency>", _currency},
                { "<TotalSum>", _priceValues.TotalSum.ToString() },
                { "<ShippingCost>", _priceValues.ShippingCost.ToString() },
                { "<TotalWithShippingCost>", _priceValues.TotalWithShippingCost.ToString()}
            };
        public byte[] CreateCommercialOffer()
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                memoryStream.Write(_fileBytes, 0, _fileBytes.Length);
                memoryStream.Position = 0;

                using (WordprocessingDocument doc = WordprocessingDocument.Open(memoryStream, true))
                {
                    var body = doc.MainDocumentPart.Document.Body;
                    Table table = body.Elements<Table>().ElementAt(0);
                    ReplaceDocumentTags(body);
                    if (table != null)
                    {
                        AddEquipmentToTable(table);
                    }
                    doc.MainDocumentPart.Document.Save();
                }

                return memoryStream.ToArray();
            }
        }
        private void ReplaceDocumentTags(Body body)
        {
            foreach (var textElement in body.Descendants<Run>().SelectMany(run => run.Descendants<Text>()))
                foreach (var replacement in _replacements)
                    if (textElement.Text.Contains(replacement.Key))
                        textElement.Text = textElement.Text.Replace(replacement.Key, replacement.Value);
        }
        private void AddEquipmentToTable(Table table)
        {
            for (int j = 0; j < _equipmentList.Count; j++)
            {
                EquipmentEntry equipmentEntry = _equipmentList[j];

                TableRow row = new();
                TableRowProperties rowProperties = new(new TableRowHeight() { Val = 568, HeightType = HeightRuleValues.Exact });
                row.AppendChild(rowProperties);

                for (int i = 0; i < 5; i++)
                {
                    RunProperties runProperties = new()
                    {
                        RunFonts = new RunFonts() { Ascii = "Calibri", HighAnsi = "Calibri" },
                        FontSize = new FontSize() { Val = "22" }
                    };

                    Justification justification = new() { Val = JustificationValues.Center };

                    string columnText = string.Empty;

                    switch (i)
                    {
                        case 0:
                            columnText = (j + 1).ToString();
                            break;
                        case 1:
                            columnText = equipmentEntry.Name;
                            justification.Val = JustificationValues.Left;
                            break;
                        case 2:
                            columnText = equipmentEntry.Quantity.ToString();
                            runProperties.FontSize = new FontSize() { Val = "20" };
                            runProperties.Italic = new Italic() { Val = OnOffValue.FromBoolean(true) };
                            break;
                        case 3:
                            columnText = equipmentEntry.Price.ToString();
                            break;
                        case 4:
                            columnText = (equipmentEntry.Quantity * equipmentEntry.Price).ToString();
                            break;
                    }

                    TableCell cell = new();

                    cell.AppendChild(new ParagraphProperties(justification))
                        .AppendChild(new Paragraph(new Run(new Text(columnText)) { RunProperties = runProperties }))
                        .AppendChild(CopyTableCellProperties(table, i).CloneNode(true));

                    row.AppendChild(cell);
                }

                table.Elements<TableRow>().ElementAt(j).InsertAfterSelf(row);
            }
        }
        private static TableCellProperties CopyTableCellProperties(Table table, int i)
            => table.Elements<TableRow>()
                    .ElementAt(0)
                    .Elements<TableCell>()
                    .ElementAt(i)
                    .Elements<TableCellProperties>()
                    .FirstOrDefault()!;
    }
}
