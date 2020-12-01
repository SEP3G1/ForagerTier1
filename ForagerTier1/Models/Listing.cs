using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ForagerTier1.Models
{
    public class Listing
    {
        [Key]
        public int ListingId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int CompanyId { get; set; }

        public Product Product { get; set; }

        [Required(ErrorMessage = "Price has to be specified"), Range(0, 999999, ErrorMessage = "Price has to be between 0 and 999.999")]
        public double? Price { get; set; }

        [Required(ErrorMessage = "Quantity has to be specified"), Range(0, 999999, ErrorMessage = "Quantity has to be between 0 and 999.999")]
        public double? Quantity { get; set; }

        [Required(ErrorMessage = "Unit has to be specified"), MaxLength(16)]
        public string Unit { get; set; }

        public string StartDate { get; set; }

        [Required(ErrorMessage = "Expiration date has to be specifed")]
        public string BestBefore { get; set; }

        [Required(ErrorMessage = "An Address has to be specified"), MaxLength(256)]
        public string PickupAddress { get; set; }

        [Required(ErrorMessage = "Postcode has to be specified"), MaxLength(16)]
        public string Postcode { get; set; }

        public bool HasDelivery { get; set; }

        public string PictureList { get; set; }
        public int NumberOfViews { get; set; }
        [Required]
        public bool IsArchived { get; set; }
        [MaxLength(512)]
        public string Comment { get; set; }
        [NotMapped] public List<string> Pictures { get; set; }

        public Listing()
        {

        }
        public string getDate()
        {
            DateTime dateTime = new DateTime(Int64.Parse(BestBefore));
            return dateTime.ToString("d", CultureInfo.CreateSpecificCulture("da-DK"));
        }
        public String getCover()
        {
            if (Pictures == null)
                Pictures = new List<string>();
            if (Pictures.Count == 0)
            {
                if (PictureList != null && !PictureList.Equals(""))
                {
                    Pictures.Add(PictureList);
                }

                if (Product.ImagesString != "")
                    Pictures.Add(Product.ImagesString);
            }
            return Pictures[0];
        }
        public string pricePrUnit()
        {
            double d = ((double)(Price / Quantity));
            return Math.Round(d, 2) + "";

        }
        public List<string> getPictures()
        {
            if (Pictures == null)
                Pictures = new List<string>();
            if (Pictures.Count == 0)
            {
                if (PictureList != null && !PictureList.Equals(""))
                {
                    Pictures.Add(PictureList);
                }

                if (Product.ImagesString != "")
                    Pictures.Add(Product.ImagesString);
            }
            return Pictures;
        }
        public override string ToString()
        {
            return $"{nameof(ListingId)}: {ListingId}, {nameof(UserId)}: {UserId}, {nameof(ProductId)}: {ProductId}, {nameof(Product)}: {Product}, {nameof(Price)}: {Price}, {nameof(Quantity)}: {Quantity}, {nameof(Unit)}: {Unit}, {nameof(StartDate)}: {StartDate}, {nameof(BestBefore)}: {BestBefore}, {nameof(PickupAddress)}: {PickupAddress}, {nameof(Postcode)}: {Postcode}, {nameof(HasDelivery)}: {HasDelivery}, {nameof(PictureList)}: {PictureList}, {nameof(NumberOfViews)}: {NumberOfViews}, {nameof(IsArchived)}: {IsArchived}, {nameof(Comment)}: {Comment}, {nameof(Pictures)}: {Pictures}";
        }
    }
}
