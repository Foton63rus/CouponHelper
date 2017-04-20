using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FotonSoft.Entities
{
    public class ProductInfo
    {
        public string ProductID { get; private set; }
        public string Title { get; private set; }
        public string WebLink { get; private set; }
        public string PicLink { get; private set; }
        public string ProductCost { get; private set; }
        public string ProductAmount { get; private set; }

        public ProductInfo(
            string productID,
            string title,
            string webLink,
            string picLink,
            string productCost,
            string productAmount
            )
        {
            Title = title;
            WebLink = webLink;
            PicLink = picLink;
            ProductID = productID;
            ProductAmount = productAmount;
            ProductCost = productCost;
        }
    }
}
