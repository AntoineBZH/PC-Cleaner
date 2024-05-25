using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PC_Cleaner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_History_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("The feature is coming soon!", "History", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Button_Update_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("The application is up-to-date!", "Update", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Button_Web_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo("https://github.com/AntoineBZH/PC-Cleaner")
                {
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occured while opening the web site: {ex.Message}", "Web site", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}