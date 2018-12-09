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
            SortDateText = "Upload Date";
            SortAlphaText = "Name";
        }

        private string _sortDateText;
        public string SortDateText
        {
            get
            {
                return _sortDateText;
            }
            set
            {
                _sortDateText = value;
                NotifyPropertyChanged();
            }
        }

        private string _sortAplhaText;
        public string SortAlphaText
        {
            get { return _sortAplhaText; }
            set
            {
                _sortAplhaText = value;
                NotifyPropertyChanged();
            }
        }

        public string CurrentSortingSelected
        {
            get
            {
                if (SortingState == SortDropdownState.Date)
                {
                    return SortDateText;
                }
                else if (SortingState == SortDropdownState.Alphabetical)
                {
                    return SortAlphaText;
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
                NotifyPropertyChanged("SortAlphaText");
                NotifyPropertyChanged("SortDateText");
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
                NotifyPropertyChanged("SortAlphaText");
                NotifyPropertyChanged("SortDateText");
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
