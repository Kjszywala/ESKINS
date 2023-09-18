using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models.CMS;

namespace ESKINS.DbServices.Services
{
    public class CategoriesServices : 
        BaseServices<Categories>,
        ICategoriesServices
    {
        public CategoriesServices() 
            : base("/api/v1.0/Categories/")
        {
        }
    }
}
