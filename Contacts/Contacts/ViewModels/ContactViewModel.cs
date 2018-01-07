using System;
using System.Windows.Input;
using Contacts.Core.Domain;
using Contacts.Core.Repository;
using Contacts.ViewModels.Base;
using Remotion.Linq.Collections;

namespace Contacts.ViewModels
{
    public class ContactViewModel : ViewModelBase
    {
        public ContactViewModel()
        {
            Item = new Contact();

            OkCommand = new RelayCommand(x => SaveMethod());
            CancelCommand = new RelayCommand(x => CloseAction());
        }

        public ContactViewModel(int contactId)
        {
            var repository = new Repository<Contact>();
            var item = repository.GetById(contactId);
            if (!item.IsNew)
                Item = item;

            OkCommand = new RelayCommand(x => SaveMethod());
            CancelCommand = new RelayCommand(x => CloseAction());
        }

        private Contact _item;
        public Contact Item
        {
            get { return _item; }
            set { _item = value; OnPropertyChanged("Item"); }
        }

        private ObservableCollection<Phone> _phones = new ObservableCollection<Phone>();
        public ObservableCollection<Phone> Phones
        {
            get { return _phones; }
            set { _phones = value; OnPropertyChanged("Phones"); }
        }

        public ICommand OkCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public Action CloseAction { get; set; }

        private void SaveMethod()
        {
            foreach (var phone in Phones)
            {
                phone.Contact = Item;
                Item.Phones.Add(phone);
            }

            var repository = new Repository<Contact>();
            repository.Save(Item);
            CloseAction();
        }
    }
}
