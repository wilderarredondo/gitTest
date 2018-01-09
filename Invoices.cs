using System.Collections.Generic;

namespace VisualCode.Entities
{
    public class Invoices
    {
        public int IdInvoice { get; set; }
        public decimal Amount { get; set; }
        public Customers Customers { get; set; }
        public Sellers Sellers { get; set; }
        public List<InvoiceDetails> InvoiceDetails { get; set; }
    }
}