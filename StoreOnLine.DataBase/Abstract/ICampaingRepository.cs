using System.Collections.Generic;
using StoreOnLine.DataBase.Model.Products;

namespace StoreOnLine.DataBase.Abstract
{
    public interface ICampaingRepository
    {
        IEnumerable<Campaign> Campaigns { get; }
        int SaveCampaign(Campaign campaign);
        Campaign DeleteCampaign(int campaignId, bool physical = false);
    }
}
