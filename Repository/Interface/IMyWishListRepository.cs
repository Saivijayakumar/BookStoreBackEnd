using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interface
{
    public interface IMyWishListRepository
    {
        bool AddBookToMyWishList(MyWishListModel myWishList);
        public List<GetWishListModel> GetBookFromMyWishList(int userId);
        public bool RemoveBookFromMyWishList(int myWishListId);
    }
}
