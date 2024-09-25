namespace CrudAngularWebApi.Models
{
    public class Employee
    {
        public int IdEmployee { get; set; }
        public string? FullName {  get; set; }
        public string? Email { get; set; }
        public double Salary { get; set; }
        public string? ContractDate { get; set; }

    }
}
