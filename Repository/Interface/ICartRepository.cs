using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interface
{
    public interface ICartRepository
    {
        bool AddBookToCart(CartModel cartData);

        List<GetCartModel> GetCart(int userId);

        bool UpdateCountInCart(CartModel cartData);
        bool RemoveBookFromCart(int cartId);
    }
}
