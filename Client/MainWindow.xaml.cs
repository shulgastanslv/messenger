using System.Windows;
using System.Windows.Input;

namespace Client;
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void MinimizeButton_OnClick(object sender, RoutedEventArgs e)
    {
        var window = (System.Windows.Window)((FrameworkElement)sender).TemplatedParent;
        window.WindowState = WindowState.Minimized;
    }
    private void MaximizeRestoreButton_OnClick(object sender, RoutedEventArgs e)
    {
        //var window = (System.Windows.Window)((FrameworkElement)sender).TemplatedParent;
        //window.WindowState = window.WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
    }
    private void CloseButton_OnClick(object sender, RoutedEventArgs e)
    {
        var window = (System.Windows.Window)((FrameworkElement)sender).TemplatedParent;
        window.Close();
    }
    private void Window_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)
        {
            var window = (System.Windows.Window)((FrameworkElement)sender).TemplatedParent;

            window.DragMove();
        }
    }
}
