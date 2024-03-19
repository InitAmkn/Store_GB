namespace Store_GB.Models
{
    public class Storage : BaseModel
    {
        public List<Product>? Products { get; set; } = new List<Product>();
        public int Count { get; set; }
    }
}
