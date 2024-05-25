using System.Diagnostics;
using System.IO;
using System.Windows;

namespace PC_Cleaner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Members
        private DirectoryInfo _winTemp;
        private DirectoryInfo _appTemp;
        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            _winTemp = new DirectoryInfo(@"C:\Windows\Temp");
            _appTemp = new DirectoryInfo(Path.GetTempPath());
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Get the size of a folder by adding recursively the size of all the files.
        /// </summary>
        /// <param name="dir">Directory to analyze.</param>
        /// <returns></returns>
        private long DirSize(DirectoryInfo dir)
        {
            try
            {
                return dir.GetFiles().Sum(file => file.Length) + dir.GetDirectories().Sum(DirSize);
            }
            catch (UnauthorizedAccessException ex) { MessageBox.Show(ex.Message, "Analyze", MessageBoxButton.OK, MessageBoxImage.Error); }
            return 0;
        }

        /// <summary>
        /// Clear recursively the content of a folder.
        /// </summary>
        /// <param name="dir">Directory to clear.</param>
        private void ClearTempData(DirectoryInfo dir)
        {
            foreach (FileInfo file in dir.GetFiles())
            {
                try { file.Delete(); }
                catch
                {
                    // It may fail depending on the user rights
                }
            }

            foreach (DirectoryInfo subDir in dir.GetDirectories())
            {
                try { subDir.Delete(recursive: true); }
                catch
                {
                    // It may fail depending on the user rights
                }
            }
        }
        #endregion

        #region Events
        private void Button_Analyze_Click(object sender, RoutedEventArgs e)
        {
            Space.Content = $"{(DirSize(_winTemp) + DirSize(_appTemp)) / 1_000_000} MB";
            Date.Content = $"{DateTime.Now:d MMM yyyy HH:mm}";
            Prompt.Content = "Analyse effectué !";
        }

        private void Button_Clean_Click(object sender, RoutedEventArgs e)
        {
            CleanButton.Content = "NETTOYAGE EN COURS";
            try
            {
                Clipboard.Clear();
                ClearTempData(_winTemp);
                ClearTempData(_appTemp);
            }
            catch(Exception ex)
            {
                MessageBox.Show($"An error occured while cleaning your computer: {ex.Message}", "Web site", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            CleanButton.Content = "NETTOYAGE TERMINE";
            Prompt.Content = "Nettoyage effectué !";
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
        #endregion
    }
}