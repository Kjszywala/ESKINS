using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models.CMS;

namespace ESKINS.DbServices.Services
{
    public class CartServices :
        BaseServices<BuyCart>,
        ICartServices
    {
        public CartServices() 
            : base("/api/v1.0/BuyCart/")
        {
        }
    }
}
