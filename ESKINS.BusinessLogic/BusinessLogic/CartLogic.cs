using ESKINS.BusinessLogic.Interfaces;

namespace ESKINS.BusinessLogic.BusinessLogic
{
    public class CartLogic : ICartLogic
    {
        #region Methods

        /// <Inheritdoc />
        public Task<bool> AddToCart(int ItemId)
        {
            throw new NotImplementedException();
        }

        /// <Inheritdoc />
        public Task<bool> RemoveFromCart(int ItemId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
