using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeSurvey.Web.Models
{
    public class SurveyAnswerModel
    {
        public YesNoAnswer? YesNoOption { get; set; }
        public string Content { get; set; }
    }

    public enum YesNoAnswer
    {
        Yes,
        No
    }
}