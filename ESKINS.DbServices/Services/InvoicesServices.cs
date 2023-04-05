using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;

namespace ESKINS.DbServices.Services
{
    public class InvoicesServices :
        BaseServices<InvoicesModels>,
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
