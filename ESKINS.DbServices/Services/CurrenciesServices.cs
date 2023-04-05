using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;

namespace ESKINS.DbServices.Services
{
    public class CurrenciesServices :
        BaseServices<CurrenciesModel>,
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
