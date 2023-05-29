using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;

namespace ESKINS.DbServices.Services
{
    public class CartServices :
        BaseServices<CartModels>,
        ICartServices
    {
        public CartServices() 
            : base("/api/v1.0/BuyCart/")
        {
        }
    }
}
