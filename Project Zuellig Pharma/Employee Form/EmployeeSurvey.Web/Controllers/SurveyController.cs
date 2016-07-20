using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EmployeeSurvey.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace EmployeeSurvey.Web.Controllers
{
    [Authorize]
    public class SurveyController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Survey
        public async Task<ActionResult> Index()
        {
            // Load user infomation
            var userId = User.Identity.GetUserId();
            var user = await db.Users.Include(x => x.Survey).FirstOrDefaultAsync(x => x.Id == userId);

            var model = new SurveyViewModel()
            {
                EmployeeCode = user.EmployeeId,
                FullName = user.FullName,
                Department = user.Department,
                Position = user.Position,
                DateJoined = user.StartingDate,
                Country = user.Country
            };

            if (user.Survey != null)
            {
                model.SurveyAnswers = GetSurveyAnswers(user.Survey);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Save(SurveyViewModel model)
        {
            var userId = User.Identity.GetUserId();
            // Save survey to database
            var surveyResult = new Survey()
            {
                UserId = userId,
                YesNoOption1 = model.SurveyAnswers[0].YesNoOption.ToString(),
                Content1 = (model.SurveyAnswers[0].YesNoOption == YesNoAnswer.Yes) ? model.SurveyAnswers[0].Content : string.Empty,
                YesNoOption2 = model.SurveyAnswers[1].YesNoOption.ToString(),
                Content2 = (model.SurveyAnswers[1].YesNoOption == YesNoAnswer.Yes) ? model.SurveyAnswers[1].Content : string.Empty,
                YesNoOption3 = model.SurveyAnswers[2].YesNoOption.ToString(),
                Content3 = (model.SurveyAnswers[2].YesNoOption == YesNoAnswer.Yes) ? model.SurveyAnswers[2].Content : string.Empty,
                YesNoOption4 = model.SurveyAnswers[3].YesNoOption.ToString(),
                Content4 = (model.SurveyAnswers[3].YesNoOption == YesNoAnswer.Yes) ? model.SurveyAnswers[3].Content : string.Empty,
                YesNoOption5 = model.SurveyAnswers[4].YesNoOption.ToString(),
                Content5 = (model.SurveyAnswers[4].YesNoOption == YesNoAnswer.Yes) ? model.SurveyAnswers[4].Content : string.Empty,
                YesNoOption6 = model.SurveyAnswers[5].YesNoOption.ToString(),
                Content6 = (model.SurveyAnswers[5].YesNoOption == YesNoAnswer.Yes) ? model.SurveyAnswers[5].Content : string.Empty,
                YesNoOption7 = model.SurveyAnswers[6].YesNoOption.ToString(),
                Content7 = (model.SurveyAnswers[6].YesNoOption == YesNoAnswer.Yes) ? model.SurveyAnswers[6].Content : string.Empty,
                YesNoOption8 = model.SurveyAnswers[7].YesNoOption.ToString(),
                Content8 = (model.SurveyAnswers[7].YesNoOption == YesNoAnswer.Yes) ? model.SurveyAnswers[7].Content : string.Empty,
                YesNoOption9 = model.SurveyAnswers[8].YesNoOption.ToString(),
                Content9 = (model.SurveyAnswers[8].YesNoOption == YesNoAnswer.Yes) ? model.SurveyAnswers[8].Content : string.Empty,
                YesNoOption10 = model.SurveyAnswers[9].YesNoOption.ToString(),
                Content10 = (model.SurveyAnswers[9].YesNoOption == YesNoAnswer.Yes) ? model.SurveyAnswers[9].Content : string.Empty,
                YesNoOption11 = model.SurveyAnswers[10].YesNoOption.ToString(),
                Content11 = (model.SurveyAnswers[10].YesNoOption == YesNoAnswer.Yes) ? model.SurveyAnswers[10].Content : string.Empty,
                SurveyUpdateDate = DateTime.Now
                
            };

            var user = await db.Users.Include(x => x.Survey).FirstOrDefaultAsync(x => x.Id == userId);
            if (user.Survey == null)
            {
                user.Survey = new Survey();
            }

            user.Survey.CopySurveyFrom(surveyResult);

            await db.SaveChangesAsync();

            return View("SurveyResult");
        }

        private List<SurveyAnswerModel> GetSurveyAnswers(Survey survey)
        {
            var surveyAnswers = new List<SurveyAnswerModel>();
            var answer1 = new SurveyAnswerModel { YesNoOption = (YesNoAnswer)Enum.Parse(typeof(YesNoAnswer), survey.YesNoOption1), Content = survey.Content1 };
            surveyAnswers.Add(answer1);
            var answer2 = new SurveyAnswerModel { YesNoOption = (YesNoAnswer)Enum.Parse(typeof(YesNoAnswer), survey.YesNoOption2), Content = survey.Content2 };
            surveyAnswers.Add(answer2);
            var answer3 = new SurveyAnswerModel { YesNoOption = (YesNoAnswer)Enum.Parse(typeof(YesNoAnswer), survey.YesNoOption3), Content = survey.Content3 };
            surveyAnswers.Add(answer3);
            var answer4 = new SurveyAnswerModel { YesNoOption = (YesNoAnswer)Enum.Parse(typeof(YesNoAnswer), survey.YesNoOption4), Content = survey.Content4 };
            surveyAnswers.Add(answer4);
            var answer5 = new SurveyAnswerModel { YesNoOption = (YesNoAnswer)Enum.Parse(typeof(YesNoAnswer), survey.YesNoOption5), Content = survey.Content5 };
            surveyAnswers.Add(answer5);
            var answer6 = new SurveyAnswerModel { YesNoOption = (YesNoAnswer)Enum.Parse(typeof(YesNoAnswer), survey.YesNoOption6), Content = survey.Content6 };
            surveyAnswers.Add(answer6);
            var answer7 = new SurveyAnswerModel { YesNoOption = (YesNoAnswer)Enum.Parse(typeof(YesNoAnswer), survey.YesNoOption7), Content = survey.Content7 };
            surveyAnswers.Add(answer7);
            var answer8 = new SurveyAnswerModel { YesNoOption = (YesNoAnswer)Enum.Parse(typeof(YesNoAnswer), survey.YesNoOption8), Content = survey.Content8 };
            surveyAnswers.Add(answer8);
            var answer9 = new SurveyAnswerModel { YesNoOption = (YesNoAnswer)Enum.Parse(typeof(YesNoAnswer), survey.YesNoOption9), Content = survey.Content9 };
            surveyAnswers.Add(answer9);
            var answer10 = new SurveyAnswerModel { YesNoOption = (YesNoAnswer)Enum.Parse(typeof(YesNoAnswer), survey.YesNoOption10), Content = survey.Content10 };
            surveyAnswers.Add(answer10);
            var answer11 = new SurveyAnswerModel { YesNoOption = (YesNoAnswer)Enum.Parse(typeof(YesNoAnswer), survey.YesNoOption11), Content = survey.Content11 };
            surveyAnswers.Add(answer11);


            return surveyAnswers;
        }
    }
}