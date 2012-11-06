using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.Web.Administration;

namespace WebRole1
{
    public class WebRole : RoleEntryPoint
    {
        public override void Run()
        {
            using (var serverManager = new ServerManager())
            {
                var mainSite = serverManager.Sites[RoleEnvironment.CurrentRoleInstance.Id + "_Web"];
                var mainApplication = mainSite.Applications["/"];
                mainApplication["preloadEnabled"] = true;

                var mainApplicationPool = serverManager.ApplicationPools[mainApplication.ApplicationPoolName];
                mainApplicationPool["startMode"] = "AlwaysRunning";
                
                serverManager.CommitChanges();
            }

            base.Run();
        }

        public override bool OnStart()
        {
            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            return base.OnStart();
        }
    }
}
