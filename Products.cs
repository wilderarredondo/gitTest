namespace VisualCode.Entities
{
    public class Products
    {
        public int IdProduct { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public override string ToString()
        {
            return $"{nameof(this.Description)}: {this.Description}     {nameof(this.Price)} {this.Price}";
        }
    }
}