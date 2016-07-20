using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeSurvey.Web.Models
{
    public class UploadUserModel
    {
        public string EmployeeId { get; set; }
        public string FullName { get; set; }
        public string PayrollGroup { get; set; }
        public string Department { get; set; }
        public string CostLocation { get; set; }
        public string Position { get; set; }
        public DateTime StartingDate { get; set; }
        public string Password { get; set; }
        public string Level { get; set; }

        public string Country { get; set; }
    }
}