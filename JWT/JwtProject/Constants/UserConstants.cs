using JwtProject.Models;

namespace JwtProject.Constants
{
    public class UserConstants
    {
        public static List<UserModel> Users = new List<UserModel>() 
        {
            new UserModel(){ UserName = "cesarios", Password="abc123", Rol ="Administrador", EmailAddress="cesarios11@gmail.com", FirstName ="Cesar", LastName ="Rios" },
            new UserModel(){ UserName = "oscarios", Password="def123", Rol ="Vendedor", EmailAddress="oscarios11@gmail.com", FirstName ="Oscar", LastName ="Rios" },
            new UserModel(){ UserName = "carolinarios", Password="ghi123", Rol ="Nutricionista", EmailAddress="carolina11@gmail.com", FirstName ="Carolina", LastName ="Rios" }
        };
    }
}
