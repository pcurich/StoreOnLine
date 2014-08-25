using System.Collections.Generic;
using StoreOnLine.DataBase.Model.Products;

namespace StoreOnLine.DataBase.Abstract
{
    public interface ICampaingRepository
    {
        IEnumerable<Campaign> Campaigns { get; }
    }
}
