using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using Contacts.Core.Domain;
using Contacts.Core.Repository;
using Contacts.Properties;
using Contacts.ViewModels.Base;

namespace Contacts.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            var groupItems = new Dictionary<string, string>
            {
                {"По имени", "FirstLetter"},
                {"По отделу", "Department"},
                {"Без группировки", string.Empty}
            };
            GroupItems = groupItems;
            
            AddCommand = new RelayCommand(x => AddMethod());
            EditCommand = new RelayCommand(x => EditMethod());
            DeleteCommand = new RelayCommand(x => DeleteMethod());

            ColumnsCommand = new RelayCommand(x => ShowColumnsWindow());
            RefreshCommand = new AsyncCommand(x => Refresh());
            var asyncCommand = (AsyncCommand) RefreshCommand;
            asyncCommand.RunWorkerCompleted += RefreshCommandComplete;
            RefreshCommand.Execute(null);
        }

        private void RefreshCommandComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            Settings.Default.GroupContactsSelectedItem = (string)GroupSelectedItem;
            Settings.Default.Save();
        }

        private object _selectedItem;
        public object SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged("SelectedItem"); }
        }

        private ICollectionView _itemList;
        public ICollectionView ItemList
        {
            get { return _itemList; }
            set { _itemList = value; OnPropertyChanged("ItemList"); }
        }

        public string SearchString { get; set; } = string.Empty;

        private Dictionary<string, string> _groupItems = new Dictionary<string, string>();
        public Dictionary<string, string> GroupItems
        {
            get { return _groupItems; }
            set { _groupItems = value; OnPropertyChanged("GroupItems"); }
        }

        private object _groupSelectedItem = string.Empty;
        public object GroupSelectedItem
        {
            get { return _groupSelectedItem; }
            set
            {
                _groupSelectedItem = value;
                OnPropertyChanged("GroupSelectedItem");
                RefreshCommand.Execute(null);
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand ColumnsCommand { get; set; }
        public Action ShowColumnsWindow { get; set; }
        public Action<Contact> AddEditAction { get; set; }

        public void LoadSettings()
        {
            var groupContactSetting = Settings.Default.GroupContactsSelectedItem;
            GroupSelectedItem = groupContactSetting;
        }

        private void AddMethod()
        {
            AddEditAction(null);
            RefreshCommand.Execute(null);
        }

        private void EditMethod()
        {
            var item = SelectedItem as Contact;
            if (item == null) return;

            AddEditAction(item);
            RefreshCommand.Execute(null);
        }

        private void DeleteMethod()
        {
            var item = SelectedItem as Contact;
            if (item == null) return;

            var phoneRepository = new Repository<Phone>();
            foreach (var phone in item.Phones)
                phoneRepository.Delete(phone);

            var repository = new Repository<Contact>();
            repository.Delete(item);
        }

        private void Refresh()
        {
            var repository = new ContactRepository();

            var items = string.IsNullOrEmpty(SearchString) ? repository.GetAll(false) : repository.Find(SearchString);
            var collection = CollectionViewSource.GetDefaultView(items);
            
            //Grouping
            var group = GroupSelectedItem as string;
            if (!string.IsNullOrEmpty(group))
                collection.GroupDescriptions.Add(new PropertyGroupDescription(group));

            ItemList = collection;
        }
    }
}