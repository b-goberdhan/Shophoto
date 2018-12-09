using Shophoto.Command;
using Shophoto.Menus.Context;
using Shophoto.Views.Collections.Aux;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Shophoto.Image.Thumbnail
{
    public class ImageThumbnailCollectionsVM : ImageThumbnailVM
    {
        public event EventHandler OnCheckboxClicked;
        public event EventHandler OnOpenImage;
        public event EventHandler OnTagClicked;
        public ImageThumbnailCollectionsVM() : base()
        {
            base.QuickButtonState = QuickButtonState.Tag;
            Tags = new ObservableCollection<TagItemVM>();
            
        }
        //TODO: Implement the context menu to occur. For now, this is not available since QuickButtonSate
        // is set to None!
        #region IGNORE
        private ICommand _threeDotQuickButtonClick;
        public override ICommand ThreeDotButtonClick
        {
            get
            {
                return _threeDotQuickButtonClick ?? (_threeDotQuickButtonClick = new CommandHandler(() =>
                {
                    //put code for click here!
                    ImageThumbnailContextMenuVM.IsOpen = !ImageThumbnailContextMenuVM.IsOpen;
                }));
            }
        }

        private bool _isContextMenuVisible;
        public override bool IsContextMenuVisible
        {
            get
            {
                return _isContextMenuVisible;
            }
            set
            {
                _isContextMenuVisible = value;
                NotifyPropertyChanged();
            }
        }

        private ImageThumbnailContextMenuVM _imageThumbnailContextMenuVM;
        public override ImageThumbnailContextMenuVM ImageThumbnailContextMenuVM
        {
            get
            {
                return _imageThumbnailContextMenuVM ??
                    (_imageThumbnailContextMenuVM = new ImageThumbnailContextMenuVM(OnContextMenuEdit, OnContextMenuDelete, OnContextMenuDownload));
            }
        }

        private void OnContextMenuEdit()
        {
            ImageThumbnailContextMenuVM.IsOpen = false;
        }

        private void OnContextMenuDelete()
        {
            ImageThumbnailContextMenuVM.IsOpen = false;
        }

        private void OnContextMenuDownload()
        {
            ImageThumbnailContextMenuVM.IsOpen = false;
        }
        #endregion

        private ICommand _quickButtonOnCheckBoxClick;
        public override ICommand QuickButtonOnCheckBoxClicked
        {
            get
            {
                return _quickButtonOnCheckBoxClick ?? (_quickButtonOnCheckBoxClick = new CommandHandler(() =>
                {
                    base.IsChecked = !base.IsChecked;
                    OnCheckboxClicked?.Invoke(this, null);
                }));
            }
        }

        private ICommand _leftClick;
        public override ICommand LeftClick
        {
            get
            {
                return _leftClick ?? (_leftClick = new CommandHandler(() =>
                {
                    OnOpenImage?.Invoke(this, null);
                }));
            }
        }

        private ICommand _tagButtonClick;
        public override ICommand TagButtonClick
        {
            get
            {
                return _tagButtonClick ?? (_tagButtonClick = new CommandHandler(() =>
                {
                    OnTagClicked?.Invoke(this, null);
                }));
            }
        }

        public void ShowDeleteCheckbox()
        {
            base.QuickButtonState = QuickButtonState.Checkbox;
        }

        public void HideDeleteCheckbox()
        {
            base.IsChecked = false;
            base.QuickButtonState = QuickButtonState.Tag;
        }

        private ObservableCollection<TagItemVM> _tags;
        public ObservableCollection<TagItemVM> Tags
        {
            get { return _tags; }
            set
            {
                _tags = value;
                NotifyPropertyChanged();
            }
        }

        public void AddTag(TagItemVM tagItemVM)
        {
            Tags.Add(tagItemVM);
            NotifyPropertyChanged("Tags");
            NotifyPropertyChanged("HasTags");
        }

        public void RemoveTag(TagItemVM tagItemVM)
        {
            Tags.Remove(tagItemVM);
            NotifyPropertyChanged("Tags");
            NotifyPropertyChanged("HasTags");
        }

        public override bool HasTags
        {
            get { return Tags.Count > 0; }
        }
        public void NotifyHasTags()
        {
            NotifyPropertyChanged("HasTags");
        }

    }
}
