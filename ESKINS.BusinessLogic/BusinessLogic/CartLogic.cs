using ESKINS.BusinessLogic.Interfaces;
using ESKINS.DbServices.Interfaces;

namespace ESKINS.BusinessLogic.BusinessLogic
{
    public class CartLogic : ICartLogic
    {
        #region Properties

        IItemsServices itemsServices;

        #endregion

        #region Constructor

        public CartLogic(IItemsServices _itemsServices)
        {
            itemsServices = _itemsServices;
        }

        #endregion

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
