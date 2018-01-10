namespace VisualCode.Entities
{
    public class InvoiceDetails
    {
        public int IdInvoiceDetail { get; set; }
        public Products Products { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public string FullDetails()
        {
            return $"{nameof(this.IdInvoiceDetail)}: {this.IdInvoiceDetail} {nameof(this.Products)}: {this.Products.Description} {nameof(this.Price)}: {this.Price} {nameof(this.Quantity)}: {this.Quantity} Total: {Price * Quantity}";
        }
    }
}