using JwtProject.Models;

namespace JwtProject.Constants
{
    public class CountryConstants
    {
        public static List<CountryModel> Countries = new List<CountryModel>()
        {
            new CountryModel(){ Name = "Colombia"},
            new CountryModel(){ Name = "Peru"},
            new CountryModel(){ Name = "Ecuador"},
            new CountryModel(){ Name = "Argentina"},
            new CountryModel(){ Name = "Chile"},
            new CountryModel(){ Name = "Brasil"}
        };
    }
}
