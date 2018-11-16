using Shophoto.Buttons;
using Shophoto.Command;
using Shophoto.Image.Thumbnail;
using Shophoto.ViewModels;
using Shophoto.Views.Collections.Aux;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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


            _fabPlusButtonVM.OnUploadClicked += FabPlusButtonVM_OnUploadClicked;
            _uploadVM.OnGoBackClicked += UploadVM_OnGoBackClicked;


            _imageThumbnails = new ObservableCollection<ImageThumbnailVM>();
            _imageThumbnails.Add(new ImageThumbnailVM() { Name = "Image 1" });
            _imageThumbnails.Add(new ImageThumbnailVM() { Name = "Image 2"});

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



        private ObservableCollection<ImageThumbnailVM> _imageThumbnails;
        public ObservableCollection<ImageThumbnailVM> ImageThumbnails
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
