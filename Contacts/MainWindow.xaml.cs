using System.Windows.Input;
using Microsoft.Windows.Controls;

namespace Contacts
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            SeachFocusCommand.InputGestures.Add(new KeyGesture(Key.F3));

            if (ViewModel.ShowColumnsWindow == null)
                ViewModel.ShowColumnsWindow = ShowColumnsWindow;

            ViewModel.LoadSettings();
        }

        public static RoutedCommand SeachFocusCommand = new RoutedCommand();

        private void ShowColumnsWindow()
        {
            var dialog = new ColumnsWindow {Owner = GetWindow(this)};
            dialog.Closed += (s, e) => ViewModel.RefreshCommand.Execute(null);
            dialog.ShowDialog();
        }

        private void CommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            SearchTb.Focus();
        }

        private void DataGrid_OnColumnDisplayIndexChanged(object sender, DataGridColumnEventArgs e)
        {
            Properties.Settings.Default.Save();
        }
    }
}
