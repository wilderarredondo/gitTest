namespace VisualCode.Entities
{
    public class InvoiceDetails
    {
        public int IdInvoiceDetail { get; set; }
        public Products Products { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}