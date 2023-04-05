using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;

namespace ESKINS.DbServices.Services
{
    internal class CategoriesService : 
        BaseServices<CategoriesModels>,
        ICategoriesServices
    {
        public CategoriesService() 
            : base("/api/v1.0/Categories/")
        {
        }
    }
}
