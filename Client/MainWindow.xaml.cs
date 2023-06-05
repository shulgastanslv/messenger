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
        WindowState = WindowState.Minimized;
    }
    private void MaximizeRestoreButton_OnClick(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
    }
    private void CloseButton_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }
    private void Window_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)
        {
            DragMove();
        }
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
        MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
    }
}
