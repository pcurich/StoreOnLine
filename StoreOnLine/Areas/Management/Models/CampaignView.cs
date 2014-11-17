using StoreOnLine.DataBase.Model.Products;
using StoreOnLine.DataBase.Model.Resources;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace StoreOnLine.Areas.Management.Models
{
    public class CampaignView
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Esta Activo?")]
        public bool IsStatus { get; set; }

        [Required(ErrorMessage = "Ingrese un nombre para la campaña")]
        [StringLength(20, ErrorMessage = "El nombre debe ser mayor a 5 caracteres y menor a 20", MinimumLength = 5)]
        [DataType(DataType.Text)]
        [Display(Name = "Nombre")]
        public string CampaignName { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Descripcion")]
        public string CampaignDescription { get; set; }


        public Campaign ToBd(CampaignView view, Imagen imagen)
        {
            return new Campaign
            {
                Id = view.Id,
                Active = view.IsStatus,
                CampaignName = view.CampaignName,
                CampaignDescription = view.CampaignDescription,
                CampaignPhoto = imagen
            };
        }

        public CampaignView ToView(Campaign db)
        {
            return new CampaignView
            {
                Id = db.Id,
                IsStatus = db.Active,
                CampaignName = db.CampaignName,
                CampaignDescription = db.CampaignDescription
            };
        }
    }
}