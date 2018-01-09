namespace VisualCode.Entities
{
    public class Sellers
    {
        public int IdSeller { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{nameof(this.Name)}: {this.Name}";
        }
    }
}