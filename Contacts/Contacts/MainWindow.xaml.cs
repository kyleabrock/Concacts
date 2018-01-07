using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Contacts.Core.Domain;
using Contacts.Properties;
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
            if (ViewModel.AddEditAction == null)
                ViewModel.AddEditAction = AddEditAction;

            ViewModel.LoadSettings();
            LoadSettings();
        }

        private void AddEditAction(Contact contact)
        {
            var dialog = (contact != null) ? new ContactWindow(contact) : new ContactWindow();
            dialog.Owner = GetWindow(this);
            dialog.ShowDialog();
        }

        private void LoadSettings()
        {
            //Do binding with converter
            DataGrid.Columns[0].Width = new DataGridLength(Settings.Default.Column0Width);
            DataGrid.Columns[1].Width = new DataGridLength(Settings.Default.Column1Width);
            DataGrid.Columns[2].Width = new DataGridLength(Settings.Default.Column2Width);
            DataGrid.Columns[3].Width = new DataGridLength(Settings.Default.Column3Width);
            DataGrid.Columns[4].Width = new DataGridLength(Settings.Default.Column4Width);
            DataGrid.Columns[5].Width = new DataGridLength(Settings.Default.Column5Width);
            DataGrid.Columns[6].Width = new DataGridLength(Settings.Default.Column6Width);
            DataGrid.Columns[7].Width = new DataGridLength(Settings.Default.Column7Width);
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
            Settings.Default.Save();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Settings.Default.Column0Width = DataGrid.Columns[0].Width.Value;
            Settings.Default.Column1Width = DataGrid.Columns[1].Width.Value;
            Settings.Default.Column2Width = DataGrid.Columns[2].Width.Value;
            Settings.Default.Column3Width = DataGrid.Columns[3].Width.Value;
            Settings.Default.Column4Width = DataGrid.Columns[4].Width.Value;
            Settings.Default.Column5Width = DataGrid.Columns[5].Width.Value;
            Settings.Default.Column6Width = DataGrid.Columns[6].Width.Value;
            Settings.Default.Column7Width = DataGrid.Columns[7].Width.Value;

            Settings.Default.Save();
        }

        private void DataGrid_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var element = e.MouseDevice.DirectlyOver as FrameworkElement;
            if (element != null && element.Parent is DataGridCell)
            {
                var datagrid = sender as DataGrid;
                if (datagrid != null)
                    ViewModel.EditCommand.Execute(null);
            }
        }
    }
}
