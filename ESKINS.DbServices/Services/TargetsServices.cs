using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models.CMS;

namespace ESKINS.DbServices.Services
{
    public class TargetsServices :
        BaseServices<Targets>,
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
