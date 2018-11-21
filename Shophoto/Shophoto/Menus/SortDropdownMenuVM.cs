using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shophoto.ViewModels;
using Shophoto.Command;
using System.Windows.Input;
namespace Shophoto.Menus
{
    public class SortDropdownMenuVM : BaseVM
    {
        public SortDropdownMenuVM() : base()
        {

        }

        private bool _isDropdownOpen;
        public bool IsDropdownOpen
        {
            get { return _isDropdownOpen; }
            set
            {
                _isDropdownOpen = value;
                NotifyPropertyChanged();
            }
        }

        private ICommand _onSortClicked;
        public ICommand OnSortClicked
        {
            get
            {
                return _onSortClicked ?? (_onSortClicked = new CommandHandler(() =>
                {
                    IsDropdownOpen = !IsDropdownOpen;
                }));
            }
        }

        private ICommand _onAlphaSort;
        public ICommand OnAlphaSort
        {
            get
            {
                return _onAlphaSort ?? (_onAlphaSort = new CommandHandler(() =>
                {

                }));
            }
        }

        private ICommand _onDataSort;
        public ICommand OnDataSort
        {
            get
            {
                return _onDataSort ?? (_onDataSort = new CommandHandler(() =>
                {

                }));
            }
        }

    }
}
