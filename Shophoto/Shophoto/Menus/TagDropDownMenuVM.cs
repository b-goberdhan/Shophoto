using Shophoto.Command;
using Shophoto.Services;
using Shophoto.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Shophoto.Menus
{
    public class TagDropDownMenuVM : BaseVM
    {
        public TagDropDownMenuVM(TagsService tagsService)
        {
            TagsService = tagsService;
        }

        public TagsService TagsService { get; }

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


    }
}
