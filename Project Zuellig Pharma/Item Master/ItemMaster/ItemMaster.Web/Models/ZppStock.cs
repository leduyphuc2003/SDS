using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace ItemMaster.Web.Models
{
    public class ZppStock : BaseEntity
    {
        public string PRNCode { get; set; }
        public string ZpItemCode { get; set; }
        public string ItemDescription { get; set; }
        public string ItemStorageCode { get; set; }
        public string Template { get; set; }
        public string ControlledTemperture { get; set; }
        public string DeliveryMode { get; set; }
        public string StorageCondition { get; set; }

        public List<ItemImage> Images { get; set; }
        public List<ItemHistory> Histories { get; set; }

        public string ChangeReason { get; set; }
        public string StrActive { get; set; }

        public void CopyFrom(ZppStock stock)
        {
            this.PRNCode = stock.PRNCode;
            this.ZpItemCode = stock.ZpItemCode ?? this.ZpItemCode;
            this.ItemDescription = stock.ItemDescription;
            this.ItemStorageCode = stock.ItemStorageCode;
            this.Template = stock.Template;
            this.ControlledTemperture = stock.ControlledTemperture;
            this.DeliveryMode = stock.DeliveryMode;
            this.StorageCondition = stock.StorageCondition;
            this.ChangeReason = stock.ChangeReason;
            this.StrActive = stock.StrActive;
        }
    }
}