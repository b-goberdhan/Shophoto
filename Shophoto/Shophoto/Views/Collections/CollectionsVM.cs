using Shophoto.Buttons;
using Shophoto.Command;
using Shophoto.Image.Thumbnail;
using Shophoto.InputBox;
using Shophoto.Menus;
using Shophoto.ViewModels;
using Shophoto.Views.Collections.Aux;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Shophoto.Menus;
namespace Shophoto.Views.Collections
{

    public enum CollectionsState
    {
        Main,
        Upload
    }
    public class CollectionsVM : BaseVM
    {
        private readonly FABPlusButtonVM _fabPlusButtonVM;
        private readonly UploadVM _uploadVM;
        private readonly SearchBoxVM _searchBoxVM;
        private readonly SortDropdownMenuVM _sortDropdownMenuVM;


        public CollectionsVM(FABPlusButtonVM fabPlusButtonVM, UploadVM uploadVM, SearchBoxVM searchBoxVM, SortDropdownMenuVM sortDropdownMenuVM)
        {
            State = CollectionsState.Main;
            _fabPlusButtonVM = fabPlusButtonVM;
            _uploadVM = uploadVM;
            _searchBoxVM = searchBoxVM;
            _sortDropdownMenuVM = sortDropdownMenuVM;

            RegisterEvents();
            ImageThumbnails = new ObservableCollection<ImageThumbnailCollectionsVM>();

        }

        private void RegisterEvents()
        {
            FABPlusButtonVM.OnUploadClicked += FabPlusButtonVM_OnUploadClicked;
            UploadVM.OnGoBackClicked += UploadVM_OnGoBackClicked;
            UploadVM.OnUploadClicked += UploadVM_OnUploadClicked;
            SearchBoxVM.PropertyChanged += SearchBoxVM_PropertyChanged;
            
        }

        public FABPlusButtonVM FABPlusButtonVM
        {
            get { return _fabPlusButtonVM; }
        }

        private void FabPlusButtonVM_OnUploadClicked(object sender, EventArgs e)
        {
            //Switch to the upload view!

            State = CollectionsState.Upload;
            _uploadVM.Reset();
            _fabPlusButtonVM.IsOpen = false;
        }

        public UploadVM UploadVM
        {
            get { return _uploadVM; }
        }
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


        public SearchBoxVM SearchBoxVM
        {
            get { return _searchBoxVM; }
        }
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


        public SortDropdownMenuVM SortDropdownMenuVM
        {
            get { return _sortDropdownMenuVM; }
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

    }
}
