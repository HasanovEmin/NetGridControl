using Aveva.ApplicationFramework.Presentation;

namespace NetGridControl
{
    class NetGridCmd : Command
    {
        private IWindowManager _windowManager;
        DockedWindow dockedWindow;

        public NetGridCmd(IWindowManager windowManager)
        {
            base.Key = "Emin.NetGridCmd";
            _windowManager = windowManager;

            dockedWindow = _windowManager.CreateDockedWindow("NetGridWindow","Spec Viewer", new NetGridCntrl(), DockedPosition.Right);
            dockedWindow.SaveLayout = true;
            dockedWindow.Width = 400;

            windowManager.WindowLayoutLoaded += WindowManager_WindowLayoutLoaded;
            dockedWindow.Closed += DockedWindow_Closed;
        }

        private void DockedWindow_Closed(object sender, System.EventArgs e)
        {
            this.Checked = false;
            dockedWindow.Hide();
        }

        private void WindowManager_WindowLayoutLoaded(object sender, System.EventArgs e)
        {
            this.Checked = dockedWindow.Visible;
        }

        public override void Execute()
        {
            if (!this.Checked)
            {
                dockedWindow.Show();
            }
            else
                dockedWindow.Hide();

            base.Execute();
        }

        public override bool Checked
        {
            get { return dockedWindow.Visible; }
            set => base.Checked = value;
        }
    }
}