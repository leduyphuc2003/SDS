using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace EmployeeSurvey.Web.SignalR
{
    public class ProgressBarHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}