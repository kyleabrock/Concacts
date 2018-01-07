using System;
using System.Collections.Generic;
using System.Windows.Input;
using Contacts.Utils;
using Contacts.ViewModels.Base;
using Remotion.Linq.Collections;

namespace Contacts.ViewModels
{
    public class ColumnsViewModel : ViewModelBase
    {
        public ColumnsViewModel()
        {
            FillColumns();
            var itemList = new ObservableCollection<CheckBoxItem<string>>();
            foreach (var column in _columns)
                itemList.Add(new CheckBoxItem<string> {Item = column.Key, IsChecked = (bool) Properties.Settings.Default[column.Value]});
            ItemList = itemList;
            
            OkCommand = new RelayCommand(x => SaveSettings());
            CancelCommand = new RelayCommand(x => CloseAction());
        }

        private void SaveSettings()
        {
            foreach (var checkBoxItem in ItemList)
            {
                var property = _columns[checkBoxItem.Item];
                Properties.Settings.Default[property] = checkBoxItem.IsChecked;
            }

            Properties.Settings.Default.Save();
            CloseAction();
        }

        private ObservableCollection<CheckBoxItem<string>> _itemList;
        public ObservableCollection<CheckBoxItem<string>> ItemList
        {
            get { return _itemList; }
            set { _itemList = value; OnPropertyChanged("ItemList"); }
        }

        public ICommand OkCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public Action CloseAction { get; set; }

        private readonly Dictionary<string, string> _columns = new Dictionary<string, string>();

        private void FillColumns()
        {
            _columns.Add("Id", "ColumnIdIsVisible");
            _columns.Add("Фамилия", "ColumnLastNameIsVisible");
            _columns.Add("Имя", "ColumnFirstNameIsVisible");
            _columns.Add("Отчество", "ColumnPatronymicIsVisible");
            _columns.Add("Отображать как", "ColumnDisplayNameIsVisible");
            _columns.Add("Отдел", "ColumnDepartmentIsVisible");
            _columns.Add("Должность", "ColumnPositionIsVisible");
            _columns.Add("Кабинет", "ColumnCabinetIsVisible");
            _columns.Add("Примечание", "ColumnCommentsIsVisible");
        }
    }
}
