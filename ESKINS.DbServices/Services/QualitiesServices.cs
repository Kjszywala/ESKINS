using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models.CMS;

namespace ESKINS.DbServices.Services
{
    public class QualitiesServices :
        BaseServices<Qualities>,
        IQualitiesServices
    {
        #region Constructor

        public QualitiesServices() 
            : base("/api/v1.0/Qualities/")
        {
        }

        #endregion
    }
}
