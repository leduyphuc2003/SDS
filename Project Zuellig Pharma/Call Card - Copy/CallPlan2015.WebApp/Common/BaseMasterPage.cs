using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace CallPlan2015.WebApp.Common
{
	public class BaseMasterPage : MasterPage
	{
		public string RedirectUrl(string url)
		{
			return ResolveUrl(url);
		}
	}
}