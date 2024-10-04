using System.Security.Claims;

namespace DotNetMvcIdentity.Claims
{
    public static class ClaimsManager
    {
        //Claim === permission
        //"Type","Value"
        public static List<Claim> Claims = new List<Claim>()
        {
            new Claim("Create", "Create"),
            new Claim("Update", "Update"),
            new Claim("Delete", "Delete"),
        };
    }
}
