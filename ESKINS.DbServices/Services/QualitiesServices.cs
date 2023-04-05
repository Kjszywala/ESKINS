using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;

namespace ESKINS.DbServices.Services
{
    public class QualitiesServices :
        BaseServices<QualitiesModels>,
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
