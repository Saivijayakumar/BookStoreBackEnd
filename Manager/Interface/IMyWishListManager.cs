using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Interface
{
    public interface IMyWishListManager
    {
        bool AddBookToMyWishList(MyWishListModel myWishList);
        public List<BookModel> GetBookFromMyWishList(int userId);
    }
}
