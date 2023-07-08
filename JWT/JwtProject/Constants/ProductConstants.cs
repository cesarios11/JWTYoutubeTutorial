using JwtProject.Models;

namespace JwtProject.Constants
{
    public class ProductConstants
    {
        public static List<ProductModel> Products = new List<ProductModel>() 
        { 
            new ProductModel(){ Name = "Coca Cola", Description = "Gaseosa con alto contenido de azucar"},
            new ProductModel(){ Name = "Leche Colanta", Description = "Bebida que proviene de la vaca"},
            new ProductModel(){ Name = "Agua Brisa", Description = "Agua mineral de 2 litros"},
            new ProductModel(){ Name = "Cerveza", Description = "Bebida alcohólica para campeones"}
        };
    }
}
