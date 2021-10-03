﻿using Manager.Interface;
using Models;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Manager
{
    public class MyOrdersManager : IMyOrdersManager
    {
        private readonly IMyOrderRepository repository;

        public MyOrdersManager(IMyOrderRepository repository)
        {
            this.repository = repository;
        }

        public GetMyOrdersModel AddOrder(MyOrdersModel orderData)
        {
            try
            {
                return this.repository.AddOrder(orderData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<GetMyOrdersModel> GetMyOrders(int userId)
        {
            try
            {
                return this.repository.GetMyOrders(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
