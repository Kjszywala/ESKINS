using Microsoft.AspNetCore.Http;
using System.Text;

namespace ESKINS.BusinessLogic.BusinessLogic
{
    public class CardSessionLogic
    {
        #region Properties

        public string CartSessionId { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion

        #region Constructor

        public CardSessionLogic(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            CartSessionId = GetCartSessionId();
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
        public string GetCartSessionId()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var cartSessionId = httpContext.Session.GetString("CartSessionId");

            if (string.IsNullOrWhiteSpace(cartSessionId))
            {
                if (!string.IsNullOrWhiteSpace(httpContext.User.Identity.Name))
                {
                    cartSessionId = httpContext.User.Identity.Name;
                }
                else
                {
                    cartSessionId = Guid.NewGuid().ToString();
                }

                httpContext.Session.SetString("CartSessionId", cartSessionId);
            }

            return cartSessionId;
        }

        #endregion
    }
}
