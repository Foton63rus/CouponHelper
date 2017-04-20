using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FotonSoft.Entities
{
    public class OrderInfo
    {
        public string ID { get; private set; }
        public string OrderTime { get; private set; }
        public string Cost { get; private set; }
        public StoreInfo Store { get; private set; }

        public OrderInfo(
            string id, 
            string orderTime,
            string cost, 
            StoreInfo store)
        {
            ID = id;
            OrderTime = orderTime;
            Cost = cost.Split(' ')[1];
            Store = store;
        }
    }
}
