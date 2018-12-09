using Shophoto.Buttons;
using Shophoto.Command;
using Shophoto.Image.Thumbnail;
using Shophoto.InputBox;
using Shophoto.Menus;
using Shophoto.Services;
using Shophoto.ViewModels;
using Shophoto.Views.Collections.Aux;
using Shophoto.Views.Collections.Aux;
using Shophoto.Views.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Shophoto.Views.Collections
{

    public enum CollectionsState
    {
        Main,
        Upload,
        Tag
    }
    public class CollectionsVM : BaseVM
    {
        public CollectionsVM(
            CollectionsFABButtonVM fabPlusButtonVM, 
            UploadVM uploadVM, 
            TagVM tagVM,
            TagImageDialogVM tagImageDialogVM,
            SearchBoxVM searchBoxVM, 
            TagDropDownMenuVM tagDropdownMenuVM,
            SortDropdownMenuVM sortDropdownMenuVM,
            DeleteConfirmationBarVM deleteConfirmationVM,
            ImageViewerVM imageViewerVM,
            ProjectService projectService,
            TagsService tagsService)
        {
            State = CollectionsState.Main;
            CollectionsFABButtonVM = fabPlusButtonVM;
            UploadVM = uploadVM;
            TagImageDialogVM = tagImageDialogVM;
            TagVM = tagVM;
            SearchBoxVM = searchBoxVM;
            SearchBoxVM.HasErrorMessage = true;
            SearchBoxVM.ErrorMessage = "No search results";
            SearchBoxVM.PlaceHolderText = "Search by name";
            TagDropdownMenuVM = tagDropdownMenuVM;
            SortDropdownMenuVM = sortDropdownMenuVM;
            DeleteConfirmationBarVM = deleteConfirmationVM;
            ImageViewerVM = imageViewerVM;
            ProjectService = projectService;
            TagsService = tagsService;
            RegisterEvents();
            ImageThumbnails = new ObservableCollection<ImageThumbnailCollectionsVM>();

        }

        private void RegisterEvents()
        {
            CollectionsFABButtonVM.OnUploadClicked += FabPlusButtonVM_OnUploadClicked;
            CollectionsFABButtonVM.OnDeleteClicked += FABPlusButtonVM_OnDeleteClicked;
            CollectionsFABButtonVM.OnTagClicked += CollectionsFABButtonVM_OnTagClicked;
            UploadVM.OnGoBackClicked += UploadVM_OnGoBackClicked;
            UploadVM.OnUploadClicked += UploadVM_OnUploadClicked;
            SearchBoxVM.PropertyChanged += SearchBoxVM_PropertyChanged;
            SortDropdownMenuVM.PropertyChanged += SortDropdownMenuVM_PropertyChanged;
            DeleteConfirmationBarVM.OnDeleteConfirmed += DeleteConfirmationBarVM_OnDeleteConfirmed;
            DeleteConfirmationBarVM.PropertyChanged += DeleteConfirmationBarVM_PropertyChanged;
            ImageViewerVM.OnClickLeft += ImageViewerVM_OnClickLeft;
            ImageViewerVM.OnClickRight += ImageViewerVM_OnClickRight;
            TagVM.PropertyChanged += TagVM_PropertyChanged;
            ProjectService.PropertyChanged += ProjectService_PropertyChanged;
        }

       

        public CollectionsFABButtonVM CollectionsFABButtonVM { get; }

        private void FabPlusButtonVM_OnUploadClicked(object sender, EventArgs e)
        {
            State = CollectionsState.Upload;
            UploadVM.Reset();
            CollectionsFABButtonVM.IsOpen = false;
        }

        private void FABPlusButtonVM_OnDeleteClicked(object sender, EventArgs e)
        {
            DeleteConfirmationBarVM.IsVisible = true;
            CollectionsFABButtonVM.IsOpen = false;
            foreach (var thumbnail in ImageThumbnails)
            {
                thumbnail.ShowDeleteCheckbox();
                thumbnail.OnCheckboxClicked -= Thumbnail_OnCheckboxClicked;
                thumbnail.OnCheckboxClicked += Thumbnail_OnCheckboxClicked;
            }
        }

        private void CollectionsFABButtonVM_OnTagClicked(object sender, EventArgs e)
        {
            TagVM.IsVisible = true;
            CollectionsFABButtonVM.IsOpen = false;
        }

        private void Thumbnail_OnCheckboxClicked(object sender, EventArgs e)
        {
            var thumbnail = sender as ImageThumbnailCollectionsVM;
            if (thumbnail.IsChecked)
            {
                DeleteConfirmationBarVM.NumberSelected++;
            }
            else
            {
                DeleteConfirmationBarVM.NumberSelected--;
            }
        }

        public TagVM TagVM { get; }
        

        public TagImageDialogVM TagImageDialogVM { get; }

        public UploadVM UploadVM { get; }
        private void UploadVM_OnUploadClicked(ICollection<ImageThumbnailCollectionsVM> uploadedImages)
        {
            if (State == CollectionsState.Upload)
            {
                foreach (ImageThumbnailCollectionsVM thumbnail in uploadedImages)
                {
                    thumbnail.OnOpenImage += Thumbnail_OnOpenImage;
                    thumbnail.OnTagClicked += Thumbnail_OnTagClicked;
                    _imageThumbnails.Add(thumbnail);
                }
                State = CollectionsState.Main;

                ApplySorting();
            }
            TagDropdownMenuVM.HasImagesAndTags = ImageThumbnails.Count != 0 && TagsService.Tags.Count != 0;
        }

        private void Thumbnail_OnTagClicked(object sender, EventArgs e)
        {
            var thumbnail = sender as ImageThumbnailCollectionsVM;
            TagImageDialogVM.CurrentThumbnail = thumbnail;
            TagImageDialogVM.IsVisible = true;
        }

        private void Thumbnail_OnOpenImage(object sender, EventArgs e)
        {
            if (!DeleteConfirmationBarVM.IsVisible)
            {
                ImageViewerVM.Image = (sender as ImageThumbnailCollectionsVM);
                SetDisabledLeftRight();
                ImageViewerVM.IsVisible = true;
            }

        }

        private void UploadVM_OnGoBackClicked(object sender, EventArgs e)
        {
            if (State == CollectionsState.Upload)
            {
                State = CollectionsState.Main;
            }
        }


        public SearchBoxVM SearchBoxVM { get; }
        private void SearchBoxVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "InputText")
            {
                //FilterCollectionImagesBySearchText();
                ApplyAllFilters();
            }
        }

        public TagDropDownMenuVM TagDropdownMenuVM { get; }

        public SortDropdownMenuVM SortDropdownMenuVM { get; }
        private void SortDropdownMenuVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SortingState")
            {
                ApplySorting();
            }
        }

        public DeleteConfirmationBarVM DeleteConfirmationBarVM { get; }
        private void DeleteConfirmationBarVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsVisible")
            {
                if (!DeleteConfirmationBarVM.IsVisible)
                {
                    //Remove all the checkboxes for this!
                    foreach (var thumbnail in ImageThumbnails)
                    {
                        thumbnail.HideDeleteCheckbox();
                    }
                }
            }
            TagDropdownMenuVM.HasImagesAndTags = ImageThumbnails.Count > 0 && TagVM.HasTags;
        }
        private void DeleteConfirmationBarVM_OnDeleteConfirmed(object sender, EventArgs e)
        {
            var imagesToDelete = ImageThumbnails.Where((thumbail) =>
            {
                return thumbail.IsChecked;
            }).ToList();

            foreach (var thumbnail in imagesToDelete)
            {
                thumbnail.OnCheckboxClicked -= Thumbnail_OnCheckboxClicked;
                ImageThumbnails.Remove(thumbnail);
            }
        }

        public ImageViewerVM ImageViewerVM { get; }
        private void SetDisabledLeftRight()
        {
            ImageViewerVM.IsLeftEnabled = ImageThumbnails.IndexOf(ImageViewerVM.Image) > 0;
            ImageViewerVM.IsRightEnabled = ImageThumbnails.IndexOf(ImageViewerVM.Image) < ImageThumbnails.Count - 1;
        }
        private void ImageViewerVM_OnClickRight(object sender, EventArgs e)
        {
            
            if (ImageViewerVM.IsRightEnabled)
            {
                var currentIndex = ImageThumbnails.IndexOf(ImageViewerVM.Image);
                ImageViewerVM.Image = ImageThumbnails[currentIndex + 1];
            }
            SetDisabledLeftRight();
        }

        private void ImageViewerVM_OnClickLeft(object sender, EventArgs e)
        {
            
            if (ImageViewerVM.IsLeftEnabled)
            {
                var currentIndex = ImageThumbnails.IndexOf(ImageViewerVM.Image);
                ImageViewerVM.Image = ImageThumbnails[currentIndex - 1];
            }
            SetDisabledLeftRight();


        }

        public ProjectService ProjectService { get; }
        private void ProjectService_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            
        }

        public TagsService TagsService { get; }
        private void TagVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            TagDropdownMenuVM.HasImagesAndTags = ImageThumbnails.Count > 0 && TagVM.HasTags;
        }
        private ObservableCollection<ImageThumbnailCollectionsVM> _imageThumbnails;
        public ObservableCollection<ImageThumbnailCollectionsVM> ImageThumbnails
        {
            get { return _imageThumbnails; }
            set {
                _imageThumbnails = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<TagItemVM> _tagFilters;
        public ObservableCollection<TagItemVM> TagFilters
        {
            get { return _tagFilters ?? (_tagFilters = new ObservableCollection<TagItemVM>()); }
            set
            {
                _tagFilters = value;
                NotifyPropertyChanged();
            }
        }

        private void ApplyTagFilter()
        {
            if (ImageThumbnails == null || ImageThumbnails.Count == 0)
            {
                return;
            }
            foreach (var image in ImageThumbnails)
            {
                bool isImageVisibleInTagFilter = true;
                foreach (var tag in TagFilters)
                {
                    if (!image.Tags.Contains(tag))
                    {
                        isImageVisibleInTagFilter = false;
                        break;
                    }

                }
                image.Visible = image.Visible && isImageVisibleInTagFilter;
            }

        }
        private void FilterCollectionImagesBySearchText()
        {
            int visibleImages = 0;
            foreach (ImageThumbnailCollectionsVM thumbnails in ImageThumbnails)
            {

                thumbnails.Visible = thumbnails.Name.ToLower().IndexOf(SearchBoxVM.InputText.ToLower()) == 0;
                if (thumbnails.Visible)
                {
                    visibleImages++;
                }
            }
            SearchBoxVM.HasError = visibleImages == 0 && SearchBoxVM.InputText != "";
        }
        private void ApplySorting()
        {
            if (SortDropdownMenuVM.SortingState == SortDropdownState.Alphabetical)
            {
                ImageThumbnails = new ObservableCollection<ImageThumbnailCollectionsVM>(ImageThumbnails.OrderBy((thumbnail) =>
                {
                    return thumbnail.Name;
                }));
            }
            else if (SortDropdownMenuVM.SortingState == SortDropdownState.Date)
            {
                ImageThumbnails = new ObservableCollection<ImageThumbnailCollectionsVM>(ImageThumbnails.OrderByDescending((thumbnail) =>
                {
                    return thumbnail.DateUploaded;
                }));
            }
        }

        public void ApplyAllFilters()
        {           
            FilterCollectionImagesBySearchText();
            ApplyTagFilter();
        }


        

        private CollectionsState _state;
        public CollectionsState State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("IsOnMainView");
                NotifyPropertyChanged("IsOnUploadView");
                NotifyPropertyChanged("IsOnTagView");
                NotifyPropertyChanged("HasImages");
            }
        }

        public bool IsOnMainView
        {
            get
            {
                return State == CollectionsState.Main;
            }
        }
        public bool IsOnUploadView
        {
            get
            {
                return State == CollectionsState.Upload;
            }
        }
        public bool IsOnTagView
        {
            get
            {
                return State == CollectionsState.Tag;
            }
        }
        public bool HasImages
        {
            get
            {
                return ImageThumbnails.Count > 0;
            }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                NotifyPropertyChanged();
            }
        }

    }
}
