using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;

namespace ESKINS.DbServices.Services
{
    public class UsersServices :
        BaseServices<UsersModels>,
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
