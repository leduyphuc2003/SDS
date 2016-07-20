using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeSurvey.Web.Models
{
    public class SurveyViewModel
    {
        public string FullName { get; set; }
        public string EmployeeCode { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public string Institution { get; set; }

        public string Country { get; set; }
        
        public DateTime DateJoined { get; set; }

        public List<SurveyAnswerModel> SurveyAnswers { get; set; }

        public SurveyViewModel()
        {
            SurveyAnswers = new List<SurveyAnswerModel>();
            for (int i = 0; i < 11; i++)
            {
                SurveyAnswers.Add(new SurveyAnswerModel());
            }
        }
    }
}