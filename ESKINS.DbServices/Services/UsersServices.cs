using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models.CMS;

namespace ESKINS.DbServices.Services
{
    public class UsersServices :
        BaseServices<Users>,
        IUsersServices
    {
        #region Constructor

        public UsersServices() 
            : base("/api/v1.0/Users/")
        {
        }

        #endregion
    }
}
