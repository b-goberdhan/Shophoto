using Shophoto.Command;
using Shophoto.Services;
using Shophoto.ViewModels;
using Shophoto.Views.Collections.Aux;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Shophoto.Menus
{
    public class TagDropDownMenuVM : BaseVM
    {
        public TagDropDownMenuVM(
            TagsService tagsService,
            ProjectService projectService)
        {
            TagsService = tagsService;
            ProjectService = projectService;
        }

        public TagsService TagsService { get; }
        public ProjectService ProjectService { get; }

        private bool _isDropdownOpen;
        public bool IsDropdownOpen
        {
            get { return _isDropdownOpen; }
            set
            {
                _isDropdownOpen = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("Tags");
                NotifyPropertyChanged("HasFilterSelected");
            }
        }

        private ObservableCollection<SelectableTagItemVM> _tags;
        public ObservableCollection<SelectableTagItemVM> Tags
        {
            get
            {
                if (!IsDropdownOpen)
                {
                    _tags = null;
                }
                else if (IsDropdownOpen && _tags == null)
                {
                    _tags = _tags = new ObservableCollection<SelectableTagItemVM>();
                    foreach (var tag in TagsService.Tags)
                    {
                        _tags.Add(new SelectableTagItemVM()
                        {
                            Tag = tag,
                            IsSelected = ProjectService.CurrentlyOpenedProject.CollectionsVM.TagFilters.Contains(tag)
                        });
                    }
                }
                return _tags;
            }
            private set
            {
                _tags = value;
                NotifyPropertyChanged();
            }
        }

        public void Reset()
        {
            Tags = null;
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

        private ICommand _onTagFilterToggled;
        public ICommand OnTagFilterToggled
        {
            get
            {
                return _onTagFilterToggled ?? (_onTagFilterToggled = new CommandHandler((obj) =>
                {
                    var selectedTag = obj as SelectableTagItemVM;
                    if (TagsService.TagExist(selectedTag.Tag) &&
                        !ProjectService.CurrentlyOpenedProject.CollectionsVM.TagFilters.Contains(selectedTag.Tag))
                    {
                        selectedTag.IsSelected = true;
                        ProjectService.CurrentlyOpenedProject.CollectionsVM.TagFilters.Add(selectedTag.Tag);

                        //todo: update the actual filter mechanism
                    }
                    else
                    {
                        selectedTag.IsSelected = false;
                        ProjectService.CurrentlyOpenedProject.CollectionsVM.TagFilters.Remove(selectedTag.Tag);
                    }

                    ProjectService.CurrentlyOpenedProject.CollectionsVM.ApplyAllFilters();
                    NotifyPropertyChanged("HasFilterSelected");
                }));
            }
        }

        public bool HasFilterSelected
        {
            get
            {
                return ProjectService.CurrentlyOpenedProject.CollectionsVM.TagFilters.Count > 0;
            }
        }

        private bool _hasImagesAndTags;
        public bool HasImagesAndTags
        {
            get { return _hasImagesAndTags; }
            set
            {
                _hasImagesAndTags = value;
                NotifyPropertyChanged();
            }
        }

    }
}
