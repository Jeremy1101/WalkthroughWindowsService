using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

using System.ServiceProcess;

namespace WalkthroughWindowsService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();

            AfterInstall += (sender, e) =>
            {
                ServiceController sc = new ServiceController(serviceInstaller1.ServiceName);
                if (sc != null)
                    sc.Start();
            };
        }
    }
}
