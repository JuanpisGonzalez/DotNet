namespace DotNetMvcInnerQuery.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public Client? Client { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int Amount { get; set; }
        public int Total { get; set; }

        public void CalculateTotal(int price)
        {
            Total = price*Amount;
        }
    }
}
