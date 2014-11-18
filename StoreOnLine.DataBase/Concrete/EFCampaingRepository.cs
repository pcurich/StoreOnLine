using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using StoreOnLine.DataBase.Abstract;
using StoreOnLine.DataBase.Configuration;
using StoreOnLine.DataBase.Entities;
using StoreOnLine.DataBase.Model.Products;

namespace StoreOnLine.DataBase.Concrete
{
    public class CampaingRepository : ICampaingRepository
    {
        private readonly StoreOnLineContext _context = new StoreOnLineContext();

        public IEnumerable<Campaign> Campaigns
        {
            get
            {
                return _context.Campaigns
                    .Include(o => o.CampaignPhoto)
                    .Where(o => !o.IsDeleted);
            }
        }

        public int SaveCampaign(Campaign campaign)
        {
            if (campaign.Id == 0)
            {
                _context.Campaigns.Add(campaign);
            }
            else
            {
                var dbEntry = _context.Campaigns.Find((campaign.Id));
                foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(campaign))
                {
                    if (!property.Name.Equals("Id") && property.GetValue(campaign) != null)
                    {
                        var value = property.GetValue(campaign);
                        if (value != null) property.SetValue(dbEntry, value);
                    }
                }

            }
            _context.SaveChanges();
            return campaign.Id;
        }

        public Campaign DeleteCampaign(int campaignId, bool physical = false)
        {
            var dbEntry = _context.Campaigns.Find(campaignId);
            if (dbEntry == null) return null;
            if (physical)
            {
                _context.Campaigns.Remove(dbEntry);
            }
            else
            {
                dbEntry.IsDeleted = true;
                dbEntry.Active = false;
                _context.Entry(dbEntry).State = EntityState.Modified;
            }

            _context.SaveChanges();
            return dbEntry;
        }
    }
}
