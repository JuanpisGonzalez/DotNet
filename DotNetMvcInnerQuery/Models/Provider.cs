namespace DotNetMvcInnerQuery.Models
{
    public class Provider
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}