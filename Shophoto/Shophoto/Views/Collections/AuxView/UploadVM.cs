using Microsoft.Win32;
using Shophoto.Command;
using Shophoto.Image;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Shophoto.Views.Collections.Aux
{
    public class UploadVM : BaseVM
    {
        public event EventHandler OnGoBackClicked;
        private readonly ImageService _imageService;
        public UploadVM(ImageService imageService)
        {
            _imageService = imageService;
            _imageToUpload = new ObservableCollection<ImageThumbnailVM>();
        }

        public void Reset()
        {
            _imageToUpload = new ObservableCollection<ImageThumbnailVM>();
        }

        private ICommand _goBackCommand;
        public ICommand GoBackCommand
        {
            get
            {
                return _goBackCommand ?? (_goBackCommand = new CommandHandler(() =>
                {
                    OnGoBackClicked?.Invoke(this, null);
                }));
            }
        }

        private ICommand _uploadCommand;
        public ICommand UploadCommand
        {
            get
            {
                return _uploadCommand ?? (_uploadCommand = new CommandHandler(EnableOpenFileDialog));
            }
        }

        private ObservableCollection<ImageThumbnailVM> _imageToUpload;
        public ObservableCollection<ImageThumbnailVM> ImagesToUpload
        {
            get { return _imageToUpload; }
            set
            {
                _imageToUpload = value;
                NotifyPropertyChanged();
            }
        }
        private void EnableOpenFileDialog()
        {
            OpenFileDialog fileBrowser = new OpenFileDialog();
            var result = fileBrowser.ShowDialog();
            if (result == true)
            {
                List<ImageSource> images = new List<ImageSource>();
                foreach (var file in fileBrowser.OpenFiles())
                {
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = file;
                    image.EndInit();
                    ImageThumbnailVM imageThumbnailVM = new ImageThumbnailVM();
                    imageThumbnailVM.ImageSource = image;
                    _imageToUpload.Add(imageThumbnailVM);
                }
            }
        }
    }
}
