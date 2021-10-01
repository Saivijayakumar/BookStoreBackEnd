using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Interface
{
    public interface IMyWishListManager
    {
        bool AddBookToMyWishList(MyWishListModel myWishList);
        public List<GetWishListModel> GetBookFromMyWishList(int userId);
        public bool RemoveBookFromMyWishList(int myWishListId);
    }
}
