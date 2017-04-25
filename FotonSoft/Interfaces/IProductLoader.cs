using System.Collections.Generic;
using FotonSoft.Entities;

namespace FotonSoft.Interfaces
{
    interface IProductLoader
    {
        List<ProductInfo> getProductInfoList();
    }
}
