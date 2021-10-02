using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interface
{
    public interface IMyOrderRepository
    {
        GetMyOrdersModel AddOrder(MyOrdersModel orderData);
        List<GetMyOrdersModel> GetMyOrders(int userId);
    }
}
