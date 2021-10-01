using Manager.Interface;
using Models;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Manager
{
    public class MyWishListManager : IMyWishListManager
    { 

        private readonly IMyWishListRepository repository;

        public MyWishListManager(IMyWishListRepository repository)
        {
            this.repository = repository;
        }

        public bool AddBookToMyWishList(MyWishListModel myWishList)
        {
            try
            {
                return this.repository.AddBookToMyWishList(myWishList);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public List<GetWishListModel> GetBookFromMyWishList(int userId)
        {
            try
            {
                return this.repository.GetBookFromMyWishList(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool RemoveBookFromMyWishList(int myWishListId)
        {
            try
            {
                return this.repository.RemoveBookFromMyWishList(myWishListId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

    }
}
