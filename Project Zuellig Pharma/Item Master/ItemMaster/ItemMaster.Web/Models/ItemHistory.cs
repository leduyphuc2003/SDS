using System;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ItemMaster.Web.Models
{
    public class ItemHistory : BaseEntity
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Content { get; set; }

        public string UrlAttachFile { get; set; }
        public string AttachfileName { get; set; }

        public string ReasonCode { get; set; }
        public DateTime UpdatedDate { get; set; }
        
        public ZppStock Item { get; set; }

        public ItemHistory()
        {
            UpdatedDate = DateTime.Now;
        }
    }
}