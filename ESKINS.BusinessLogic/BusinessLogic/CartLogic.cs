using ESKINS.BusinessLogic.Interfaces;
using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;
using System.Configuration;

namespace ESKINS.BusinessLogic.BusinessLogic
{
    public class CartLogic : ICartLogic
    {
        #region Properties

        IItemsServices itemsServices;
        ICartServices cartServices;

        #endregion

        #region Constructor

        public CartLogic(IItemsServices _itemsServices, ICartServices cartServices)
        {
            itemsServices = _itemsServices;
            this.cartServices = cartServices;

        }

        #endregion

        #region Methods

        /// <Inheritdoc />
        public async Task<bool> AddToCart(int ItemId)
        {
            try
            {
                var item = await itemsServices.GetAsync(ItemId);
                if (item != null)
                {
                    CartModels cartModels = new CartModels()
                    {
                        CreationDate = DateTime.Now,
                        Item = item,
                        Quantity = GetQuantity().Result,
                        ItemId = ItemId,
                        SessionId = BussinesLogicConfig.SessionId
                    };
                    await cartServices.AddAsync(cartModels);
                }
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<int> GetQuantity()
        {
            try
            {
                var list = cartServices.GetAllAsync().Result;
                return list.Where(item=>item.SessionId == BussinesLogicConfig.SessionId).ToList().Count;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> RemoveAll()
        {
            try
            {
                var list = cartServices.GetAllAsync().Result.Where(item=>item.SessionId == BussinesLogicConfig.SessionId).ToList();
                foreach (var item in list)
                {
                    await cartServices.RemoveAsync(item.Id);
                }
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <Inheritdoc />
        public async Task<bool> RemoveFromCart(int ItemId)
        {
            try
            {
                await cartServices.RemoveAsync(ItemId);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        #endregion
    }
}
