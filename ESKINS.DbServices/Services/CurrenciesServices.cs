using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models.CMS;

namespace ESKINS.DbServices.Services
{
    public class CurrenciesServices :
        BaseServices<Currencies>,
        ICurrenciesServices
    {
        #region Constructor

        public CurrenciesServices()
            : base("/api/v1.0/Currencies/")
        {
        }

        #endregion
    }
}
