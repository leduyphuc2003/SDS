using System;

namespace EmployeeSurvey.Web.Models
{
    public class Survey
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string YesNoOption1 { get; set; }
        public string YesNoOption2 { get; set; }
        public string YesNoOption3 { get; set; }
        public string YesNoOption4 { get; set; }
        public string YesNoOption5 { get; set; }
        public string YesNoOption6 { get; set; }
        public string YesNoOption7 { get; set; }
        public string YesNoOption8 { get; set; }
        public string YesNoOption9 { get; set; }
        public string YesNoOption10 { get; set; }
        public string YesNoOption11 { get; set; }
        public string Content1 { get; set; }
        public string Content2 { get; set; }
        public string Content3 { get; set; }
        public string Content4 { get; set; }
        public string Content5 { get; set; }
        public string Content6 { get; set; }
        public string Content7 { get; set; }
        public string Content8 { get; set; }
        public string Content9 { get; set; }
        public string Content10 { get; set; }
        public string Content11 { get; set; }

        public DateTime SurveyUpdateDate { get; set; }

        public void CopySurveyFrom(Survey source)
        {
            this.UserId = source.UserId;
            this.YesNoOption1 = source.YesNoOption1;
            this.YesNoOption2 = source.YesNoOption2;
            this.YesNoOption3 = source.YesNoOption3;
            this.YesNoOption4 = source.YesNoOption4;
            this.YesNoOption5 = source.YesNoOption5;
            this.YesNoOption6 = source.YesNoOption6;
            this.YesNoOption7 = source.YesNoOption7;
            this.YesNoOption8 = source.YesNoOption8;
            this.YesNoOption9 = source.YesNoOption9;
            this.YesNoOption10 = source.YesNoOption10;
            this.YesNoOption11 = source.YesNoOption11;
            this.Content1 = source.Content1;
            this.Content2 = source.Content2;
            this.Content3 = source.Content3;
            this.Content4 = source.Content4;
            this.Content5 = source.Content5;
            this.Content6 = source.Content6;
            this.Content7 = source.Content7;
            this.Content8 = source.Content8;
            this.Content9 = source.Content9;
            this.Content10 = source.Content10;
            this.Content11 = source.Content11;
            this.SurveyUpdateDate = source.SurveyUpdateDate;
        }
    }
}