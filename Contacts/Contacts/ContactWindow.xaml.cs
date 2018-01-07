using System.Windows;
using Contacts.Core.Domain;
using Contacts.ViewModels;

namespace Contacts
{
    /// <summary>
    /// Логика взаимодействия для ConcactWindow.xaml
    /// </summary>
    public partial class ContactWindow : Window
    {
        public ContactWindow()
        {
            InitializeComponent();
            if (ViewModel.CloseAction == null)
                ViewModel.CloseAction = Close;
        }

        public ContactWindow(Contact arg)
        {
            InitializeComponent();
            ViewModel = new ContactViewModel(arg.Id);
            DataContext = ViewModel;

            if (ViewModel.CloseAction == null)
                ViewModel.CloseAction = Close;
        }
    }
}
