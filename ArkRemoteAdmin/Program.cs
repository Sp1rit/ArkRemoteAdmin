using System;
using System.Threading;
using System.Windows.Forms;
using ArkRemoteAdmin.Data;
using ArkRemoteAdmin.UserInterface;
using BssFramework.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;

namespace ArkRemoteAdmin
{
    class Program : WindowsFormsApplicationBase
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.ThreadException += Application_ThreadException;
            Settings.Load();
            Data.Data.Load();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new WizardAddSchedule());
            new Program().Run(args);
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            ErrorHandler.CaptureUnhandledException(e.Exception);

            TaskDialog taskDialog = new TaskDialog();
            taskDialog.WindowTitle = "Unhandled Error";
            taskDialog.MainInstruction = "An unhandled error occured.";
            taskDialog.Content = "An error report was generated and sent to the developer of this application." + Environment.NewLine + "The error report does not contain any personal information.";
            taskDialog.CommonButtons = TaskDialogCommonButtons.Ok;
            taskDialog.MainIcon = TaskDialogIcon.Error;
            taskDialog.PositionRelativeToWindow = true;
            taskDialog.Show();
        }

        public App App { get; private set; }

        public Program()
        {
            IsSingleInstance = !Settings.General.AllowMultipleInstances;
        }

        protected override bool OnStartup(StartupEventArgs e)
        {
            App = new App();
            App.Run();
            return false;
        }

        protected override void OnStartupNextInstance(
          StartupNextInstanceEventArgs eventArgs)
        {
            base.OnStartupNextInstance(eventArgs);
            App.Focus();
        }
    }
}
