namespace DotNetMvcInnerQuery.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int ProviderId { get; set; }
        public Provider? Provider { get; set; }
    }
}
