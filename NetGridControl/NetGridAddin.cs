using Aveva.ApplicationFramework;
using Aveva.ApplicationFramework.Presentation;

namespace NetGridControl
{
    public class NetGridAddin : IAddin
    {
        public string Name => "NetGridControlAddin";

        public string Description => "Test of Netgrid";

        public void Start(ServiceManager serviceManager)
        {
            ICommandManager commandManager = DependencyResolver.GetImplementationOf<ICommandManager>();
            IWindowManager windowManager = DependencyResolver.GetImplementationOf<IWindowManager>();

            NetGridCmd netGridCmd = new NetGridCmd(windowManager);

            commandManager.Commands.Add(netGridCmd);
        }

        public void Stop()
        {
            
        }
    }
}
