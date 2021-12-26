using System;
using System.Windows;

namespace PackageManager
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            AppDomain.CurrentDomain.UnhandledException -= OnUnhandledException;

            base.OnExit(e);
        }

        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception exception = (Exception)e.ExceptionObject;

            string message = exception.Message;

            if (exception.StackTrace is not null)
            {
                message += "\n\n";
                message += exception.StackTrace;
            }

            MessageBox.Show(message, "PackageManager - Fatal Error!", MessageBoxButton.OK, MessageBoxImage.Error);

            if (!e.IsTerminating)
                Shutdown();
        }
    }
}
