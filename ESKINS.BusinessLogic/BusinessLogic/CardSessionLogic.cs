using Microsoft.AspNetCore.Http;
using System.Text;

namespace ESKINS.BusinessLogic.BusinessLogic
{
    public class CardSessionLogic
    {
        #region Properties

        public string CartSessionId { get; set; }

        #endregion

        #region Constructor

        public CardSessionLogic(HttpContext httpContext)
        {
            CartSessionId = GetCartSessionId(httpContext);
        }

        #endregion

        #region Methods

        /// <summary>
        /// If the shopping cart session ID is empty, meaning there is no specific browser identifier,
        /// we first check if we can read that identifier from the context. If the name in the context is not null, 
        /// then that name becomes the shopping cart session ID. However, if it is null, 
        /// we generate a unique browser number using a GUID and set it as the shopping cart session ID.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public string? GetCartSessionId(HttpContext httpContext)
        {
            if (httpContext.Session.GetString("CartSessionId") == null)
            {
                if (!string.IsNullOrWhiteSpace(httpContext.User.Identity.Name))
                {
                    httpContext.Session.SetString("CartSessionId", httpContext.User.Identity.Name);
                }
                else
                {
                    Guid guid = Guid.NewGuid();
                    httpContext.Session.SetString("CartSessionId", guid.ToString());
                }
            }
            return httpContext.Session.GetString("CartSessionId").ToString();
        }

        #endregion
    }
}
