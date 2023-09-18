using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models.CMS;

namespace ESKINS.DbServices.Services
{
    public class ExteriorsServices :
        BaseServices<Exteriors>,
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
