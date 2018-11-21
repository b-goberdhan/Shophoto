using Shophoto.Buttons;
using Shophoto.Command;
using Shophoto.Image.Thumbnail;
using Shophoto.InputBox;
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
        
        public CollectionsVM(FABPlusButtonVM fabPlusButtonVM, UploadVM uploadVM)
        {
            State = CollectionsState.Main;
            _fabPlusButtonVM = fabPlusButtonVM;
            _uploadVM = uploadVM;
            _searchBoxVM = new SearchBoxVM();

            _fabPlusButtonVM.OnUploadClicked += FabPlusButtonVM_OnUploadClicked;
            _uploadVM.OnGoBackClicked += UploadVM_OnGoBackClicked;
            _uploadVM.OnUploadClicked += UploadVM_OnUploadClicked;
            SearchBoxVM.PropertyChanged += SearchBoxVM_PropertyChanged;
            _imageThumbnails = new ObservableCollection<ImageThumbnailCollectionsVM>();
        }

        private void SearchBoxVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "InputText")
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

        private SearchBoxVM _searchBoxVM;
        public SearchBoxVM SearchBoxVM
        {
            get { return _searchBoxVM; }
            set
            {
                _searchBoxVM = value;
                NotifyPropertyChanged();
            }
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

        public UploadVM UploadVM
        {
            get { return _uploadVM; }
        }

        private void UploadVM_OnGoBackClicked(object sender, EventArgs e)
        {
            if (State == CollectionsState.Upload)
            {
                State = CollectionsState.Main;
            }
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
