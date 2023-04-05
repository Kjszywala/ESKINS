using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;

namespace ESKINS.DbServices.Services
{
    public class PhasesServices :
        BaseServices<PhasesModels>,
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
