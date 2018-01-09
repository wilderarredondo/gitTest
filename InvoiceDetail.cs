namespace VisualCode.Entities
{
    public class InvoiceDetail
    {
        public int IdInvoiceDetail { get; set; }
        public Product Product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}