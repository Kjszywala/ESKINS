using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;

namespace ESKINS.DbServices.Services
{
    public class UsersAddressesServices :
        BaseServices<UsersAddressesModels>,
        IUserAddressesServices
    {
        #region Constructor

        public UsersAddressesServices() 
            : base("/api/v1.0/UsersAddresses/")
        {
        }

        #endregion
    }
}
