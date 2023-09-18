using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models.CMS;

namespace ESKINS.DbServices.Services
{
    public class PhasesServices :
        BaseServices<Phases>,
        IPhasesServices
    {
        #region Constructor

        public PhasesServices() 
            : base("/api/v1.0/Phases/")
        {
        }

        #endregion
    }
}
