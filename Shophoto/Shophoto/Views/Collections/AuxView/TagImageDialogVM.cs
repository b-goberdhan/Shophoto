using Shophoto.Command;
using Shophoto.Image.Thumbnail;
using Shophoto.Services;
using Shophoto.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Shophoto.Views.Collections.Aux
{
    public class TagImageDialogVM : BaseVM
    {
        public TagImageDialogVM(
            TagsService tagsService,
            ProjectService projectService)
        {
            TagsService = tagsService;
            ProjectService = projectService;
        }

        private ImageThumbnailCollectionsVM _currentThumbnail;
        public ImageThumbnailCollectionsVM CurrentThumbnail
        {
            get { return _currentThumbnail; }
            set
            {
                _currentThumbnail = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("CurrentTags");
            }
        }

        public ObservableCollection<TagItemVM> CurrentTags
        {
            get { return CurrentThumbnail?.Tags; }
        }

        private ObservableCollection<SelectableTagItemVM> _tags;
        public ObservableCollection<SelectableTagItemVM> Tags
        {
            get
            {
                if (!IsVisible)
                {
                    _tags = null;
                }
                else if (IsVisible && _tags == null)
                {
                    _tags = _tags = new ObservableCollection<SelectableTagItemVM>();
                    foreach (var tag in TagsService.Tags)
                    {
                        _tags.Add(new SelectableTagItemVM()
                        {
                            Tag = tag,
                            IsSelected = CurrentThumbnail.Tags.Contains(tag)
                        });
                    }
                }
                return _tags;
            }
        }

        private bool _isVisible;
        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                
                _isVisible = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("Tags");
            }
        }

        private ICommand _closeCommand;
        public ICommand CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new CommandHandler(() =>
                {
                    IsVisible = false;
                    CurrentThumbnail = null;
                }));
            }

        }

        private ICommand _toggleTagSelected;
        public ICommand ToggleTagSelected
        {
            get
            {
                return _toggleTagSelected ?? (_toggleTagSelected = new CommandHandler((obj) =>
                {
                    var tagItem = obj as SelectableTagItemVM;
                    if (!CurrentThumbnail.Tags.Contains(tagItem.Tag))
                    {
                        CurrentThumbnail.Tags.Add(tagItem.Tag);
                        tagItem.IsSelected = true;
                    }
                    else
                    {
                        CurrentThumbnail.Tags.Remove(tagItem.Tag);
                        tagItem.IsSelected = false ;
                    }
                }));
            }
        }




        public TagsService TagsService { get; }
        public ProjectService ProjectService { get; }
    }
}
