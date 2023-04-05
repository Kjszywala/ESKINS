using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;

namespace ESKINS.DbServices.Services
{
    public class ExteriorsServices :
        BaseServices<ExteriorsModels>,
        IExteriorsServices
    {
        #region MyRegion

        public ExteriorsServices() 
            : base("/api/v1.0/Exteriors/")
        {
        }

        #endregion
    }
}
