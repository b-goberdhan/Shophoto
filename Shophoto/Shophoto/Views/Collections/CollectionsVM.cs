using Shophoto.Buttons;
using Shophoto.Command;
using Shophoto.Image.Thumbnail;
using Shophoto.InputBox;
using Shophoto.Menus;
using Shophoto.ViewModels;
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
        Upload
    }
    public class CollectionsVM : BaseVM
    {
        public CollectionsVM(
            CollectionsFABButtonVM fabPlusButtonVM, 
            UploadVM uploadVM, 
            SearchBoxVM searchBoxVM, 
            SortDropdownMenuVM sortDropdownMenuVM,
            DeleteConfirmationBarVM deleteConfirmationVM)
        {
            State = CollectionsState.Main;
            CollectionsFABButtonVM = fabPlusButtonVM;
            UploadVM = uploadVM;
            SearchBoxVM = searchBoxVM;
            SortDropdownMenuVM = sortDropdownMenuVM;
            DeleteConfirmationBarVM = deleteConfirmationVM;
            RegisterEvents();
            ImageThumbnails = new ObservableCollection<ImageThumbnailCollectionsVM>();

        }

        private void RegisterEvents()
        {
            CollectionsFABButtonVM.OnUploadClicked += FabPlusButtonVM_OnUploadClicked;
            CollectionsFABButtonVM.OnDeleteClicked += FABPlusButtonVM_OnDeleteClicked; 
            UploadVM.OnGoBackClicked += UploadVM_OnGoBackClicked;
            UploadVM.OnUploadClicked += UploadVM_OnUploadClicked;
            SearchBoxVM.PropertyChanged += SearchBoxVM_PropertyChanged;
            SortDropdownMenuVM.PropertyChanged += SortDropdownMenuVM_PropertyChanged;
            DeleteConfirmationBarVM.OnDeleteConfirmed += DeleteConfirmationBarVM_OnDeleteConfirmed;
            DeleteConfirmationBarVM.PropertyChanged += DeleteConfirmationBarVM_PropertyChanged;
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
                thumbnail.OnCheckboxClicked += Thumbnail_OnCheckboxClicked;
            }
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

        public UploadVM UploadVM { get; }
        private void UploadVM_OnUploadClicked(ICollection<ImageThumbnailCollectionsVM> uploadedImages)
        {
            if (State == CollectionsState.Upload)
            {
                foreach (ImageThumbnailCollectionsVM thumbnail in uploadedImages)
                {
                    _imageThumbnails.Add(thumbnail);
                }
                State = CollectionsState.Main;
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
                FilterCollectionImagesByText(SearchBoxVM.InputText);
            }
        }
        private void FilterCollectionImagesByText(string text)
        {
            foreach (ImageThumbnailCollectionsVM thumbnails in _imageThumbnails)
            {

                thumbnails.Visible = thumbnails.Name.ToLower().IndexOf(text.ToLower()) == 0;
            }
        }


        public SortDropdownMenuVM SortDropdownMenuVM { get; }
        private void SortDropdownMenuVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SortingState")
            {
                if (SortDropdownMenuVM.SortingState == SortDropdownState.Alphabetical)
                {
                    ImageThumbnails = new ObservableCollection<ImageThumbnailCollectionsVM>(ImageThumbnails.OrderBy((thumbnail) =>
                    {
                        return thumbnail.Name;
                    }));
                }
                else if(SortDropdownMenuVM.SortingState == SortDropdownState.Date)
                {
                    ImageThumbnails = new ObservableCollection<ImageThumbnailCollectionsVM>(ImageThumbnails.OrderBy((thumbnail) => 
                    {
                        return thumbnail.DateUploaded; 
                    }));
                }
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

        private ObservableCollection<ImageThumbnailCollectionsVM> _imageThumbnails;
        public ObservableCollection<ImageThumbnailCollectionsVM> ImageThumbnails
        {
            get { return _imageThumbnails; }
            set {
                _imageThumbnails = value;
                NotifyPropertyChanged();
            }
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
