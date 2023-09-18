using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models.CMS;

namespace ESKINS.DbServices.Services
{
    public class InvoicesServices :
        BaseServices<Invoices>,
        IInvoicesServices
    {
        #region Constructor

        public InvoicesServices() 
            : base("/api/v1.0/Invoices/")
        {
        }

        #endregion

    }
}
