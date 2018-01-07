using System.Windows;

namespace Contacts
{
    /// <summary>
    /// Логика взаимодействия для ColumnsWindow.xaml
    /// </summary>
    public partial class ColumnsWindow : Window
    {
        public ColumnsWindow()
        {
            InitializeComponent();

            if (ViewModel.CloseAction == null)
                ViewModel.CloseAction = Close;
        }
    }
}
