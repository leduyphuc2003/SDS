using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using CallPlan2015.Common;
using CallPlan2015.DataModel;

namespace CallPlan2015.WebApp.Common
{
	public class BasePage : Page
	{
		public string RedirectUrl(string url)
		{
			return ResolveUrl(url);
		}

        //css for icon checked 
	    protected string ChangeIcon(CallPlanData cp)
	    {
	        if (cp.check)
	        {
                return "background: white; color: green;";
            }
	        return "background: white; color: white;";}

        //doi mau sac
        protected string GenerateClass(CallPlanData className, int index)
        {
            double a = (className.MtdAllSources/className.Ave6LastMonth)*100;
            float aa = className.PassedWD;
            float bb = className.LeftWD + className.PassedWD;
            float b = (aa / bb) * 100;
 
            if ((a>=b) && (className.Class == "A"))
            {
                return "list-item-bgA";
            }
            if ((a >= b) && (className.Class == "B"))
            {
                return "list-item-bgB";
            }
            if ((a >= b) && (className.Class == "C"))
            {
                return "list-item-bgC";
            }
            if ((a >= b) && (className.Class == "D"))
            {
                return "list-item-bgD";
            }

            //return "list-item-bgE"; mau do
            return "list-item-bgF";
        }

	    protected string DoiMau(PharmacyCustomerData data, int index)
	    {
	        double a = (data.MtdAllSourcesValue/data.Ave6LastMonthValue)*100;
	       
            double r;
            double.TryParse(Session[Constants.SESSION_OF_Month_Gone_By].ToString(), NumberStyles.Any, CultureInfo.CurrentCulture, out r);

	        if (a >= r)
	        {
                return "background-green";
                //return "foreground-green";
	        }
	        return "";
	    }

        //check mau sac
        //protected string GenerateClass(string className, int index)
        //{
        //    if (className == "A" && className.)
        //    {
        //        return "list-item-bgA";
        //    }
        //    if (className == "B")
        //    {
        //        return "list-item-bgB";
        //    }

        //    return "list-item-bg" + (index%2 + 1);
        //}

        //doi mau sac
        //protected string GenerateClass(CallPlanData className, int index)
        //{
        //    if ((className.MtdAllSources >= className.Zlvl) && (className.Class=="A"))
        //    {
        //        return "list-item-bgA";
        //    }
        //    if ((className.MtdAllSources >= className.Zlvl) && (className.Class == "B"))
        //    {
        //        return "list-item-bgB";
        //    }
        //    if ((className.MtdAllSources >= className.Zlvl) && (className.Class == "C"))
        //    {
        //        return "list-item-bgC";
        //    }
        //    if ((className.MtdAllSources >= className.Zlvl) && (className.Class == "D"))
        //    {
        //        return "list-item-bgD";
        //    }

        //    return "list-item-bg" + (index%2 + 1);
        //}
 

        //doi mau sac
        //protected string GenerateClass(CallPlanData className, int index)
        //{
        //    if ((((className.MtdAllSources / className.Ave6LastMonth) * 100) >= 100)
        //        && (className.Class == "A"))
        //    {
        //        return "list-item-bgA";
        //    }
        //    if ((((className.MtdAllSources / className.Ave6LastMonth) * 100) >= 100)
        //        && (className.Class == "B"))
        //    {
        //        return "list-item-bgB";
        //    }
        //    if ((((className.MtdAllSources / className.Ave6LastMonth) * 100) >= 100)
        //        && (className.Class == "C"))
        //    {
        //        return "list-item-bgC";
        //    }
        //    if ((((className.MtdAllSources / className.Ave6LastMonth) * 100) >= 100)
        //        && (className.Class == "D"))
        //    {
        //        return "list-item-bgD";
        //    }

        //    return "list-item-bgE";

        //}
	}
}