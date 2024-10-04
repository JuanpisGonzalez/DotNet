namespace DotNetMvcIdentity.Models
{
    public class ClaimsUserViewModel
    {
        public string IdUser { get; set; }
        public List<UserClaim> Claims { get; set; }

        public ClaimsUserViewModel()
        {
            Claims = new List<UserClaim>();
        }

        public class UserClaim
        {
            public string ClaimType { get; set; }
            public bool Selected { get; set; }
        }
    }
}
