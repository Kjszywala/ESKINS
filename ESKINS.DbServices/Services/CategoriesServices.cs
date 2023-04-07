using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;

namespace ESKINS.DbServices.Services
{
    public class CategoriesServices : 
        BaseServices<CategoriesModels>,
        ICategoriesServices
    {
        public CategoriesServices() 
            : base("/api/v1.0/Categories/")
        {
        }
    }
}
