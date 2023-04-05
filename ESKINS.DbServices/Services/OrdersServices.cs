using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;

namespace ESKINS.DbServices.Services
{
    public class OrdersServices :
        BaseServices<OrdersModels>,
        IOrdersServices
    {
        public OrdersServices() 
            : base("/api/v1.0/Orders/")
        {
        }
    }
}
