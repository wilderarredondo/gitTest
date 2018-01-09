namespace VisualCode.Entities
{
    public class Customers
    {
        public int IdCustomer { get; set; }
        public string Name { get; set; }
        public string BirthDate { get; set; }

        public override string ToString()
        {
            return $"{nameof(this.Name)}: {this.Name}   {nameof(this.BirthDate)}: {this.BirthDate}";
        }
    }
}