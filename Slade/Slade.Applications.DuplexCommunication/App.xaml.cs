namespace Slade.Applications.DuplexCommunication
{
    public partial class App
    {
        protected override void OnStartup(System.Windows.StartupEventArgs e)
        {
            System.Windows.MessageBox.Show(
                "This application is incomplete and completing it at this stage would provide no real value."
                + " As such, this application will now exit.", "Incomplete Application",
                System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Exclamation);

            Shutdown(exitCode: -1);
        }
    }
}