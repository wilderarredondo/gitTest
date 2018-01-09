using System.Collections.Generic;

namespace VisualCode.Entities
{
    public class Invoice
    {
        public int IdInvoice { get; set; }
        public decimal Amount { get; set; }
        public Customers Customer { get; set; }
        public Seller Seller { get; set; }
        public List<InvoiceDetail> InvoiceDetails { get; set; }
    }
}