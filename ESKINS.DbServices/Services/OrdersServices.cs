using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models.CMS;

namespace ESKINS.DbServices.Services
{
    public class OrdersServices :
        BaseServices<Orders>,
        IOrdersServices
    {
        public OrdersServices() 
            : base("/api/v1.0/Orders/")
        {
        }
    }
}
