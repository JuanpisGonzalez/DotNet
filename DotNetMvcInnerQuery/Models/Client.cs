namespace DotNetMvcInnerQuery.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public List<Sale> Sales { get; set; }
    }
}