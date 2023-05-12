using System.Windows;

namespace Client;
public partial class App
{
    private void ApplicationStart(object sender, StartupEventArgs e)
    {
        var mainWindow = new MainWindow();
        mainWindow.Show();
    }
}
