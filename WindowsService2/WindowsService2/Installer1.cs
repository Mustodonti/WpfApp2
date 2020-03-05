using System.ComponentModel;
using System.ServiceProcess;
using System.Configuration.Install;

namespace FileWatcherService
{
    [RunInstaller(true)]
    public partial class Installer1 : Installer
    {
        ServiceInstaller serviceInstaller;
        ServiceProcessInstaller processInstaller;

        public Installer1()
        {
            InitializeComponent();
            /*
              для настройки значений для каждой из запускаемых служб.
              То есть если у нас запускается три службы, то для каждой службы
              создается свой объект ServiceInstaller. Но в нашем случае в
              мы определили только одну запускаемую службу, поэтому объекты 
              обоих классов у нас будут только в одном экземпляре.
             */
            serviceInstaller = new ServiceInstaller();
            processInstaller = new ServiceProcessInstaller();//управляет настройкой значений для всех запускаемых служб внутри одного процесса 

            processInstaller.Account = ServiceAccount.LocalSystem;
            serviceInstaller.StartType = ServiceStartMode.Manual;
            serviceInstaller.ServiceName = "CompareDates";
            Installers.Add(processInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}