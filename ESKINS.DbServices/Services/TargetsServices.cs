using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;

namespace ESKINS.DbServices.Services
{
    public class TargetsServices :
        BaseServices<TargetsModels>,
        ITargetsServices
    {
        #region Constructor

        public TargetsServices() 
            : base("/api/v1.0/Targets/")
        {
        }

        #endregion
    }
}
