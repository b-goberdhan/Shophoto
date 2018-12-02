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
    public enum SortDropdownState
    {
        Alphabetical,
        Date
    }

    public class SortDropdownMenuVM : BaseVM
    {
        public SortDropdownMenuVM() : base()
        {

        }

        public string CurrentSortingSelected
        {
            get
            {
                if (SortingState == SortDropdownState.Date)
                {
                    return "Date Upload";
                }
                else if (SortingState == SortDropdownState.Alphabetical)
                {
                    return "Name";
                }
                return "";
            }
        }

        private SortDropdownState _sortingState;
        public SortDropdownState SortingState
        {
            get { return _sortingState; }
            set
            {
                _sortingState = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("CurrentSortingSelected");
            }
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

        private ICommand _onOpenDropdownClicked;
        public ICommand OnOpenDropdownClicked
        {
            get
            {
                return _onOpenDropdownClicked ?? (_onOpenDropdownClicked = new CommandHandler(() =>
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
                    SortingState = SortDropdownState.Alphabetical;
                    IsDropdownOpen = false;
                }));
            }
        }

        private ICommand _onDateSort;
        public ICommand OnDateSort
        {
            get
            {
                return _onDateSort ?? (_onDateSort = new CommandHandler(() =>
                {
                    SortingState = SortDropdownState.Date;
                    IsDropdownOpen = false;
                }));
            }
        }


    }
}
