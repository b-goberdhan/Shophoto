using Microsoft.Win32;
using Shophoto.Command;
using Shophoto.Image;
using Shophoto.Image.Thumbnail;
using Shophoto.InputBox;
using Shophoto.Services;
using Shophoto.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Shophoto.Views.Collections.Aux
{
    public delegate void ImagesUploadedHandler(ICollection<ImageThumbnailCollectionsVM> uploadedImages);
    public class UploadVM : BaseVM
    {
        public event EventHandler OnGoBackClicked;
        public event ImagesUploadedHandler OnUploadClicked;
        private readonly TagsService _imageService;
        public UploadVM(TagsService imageService, SearchBoxVM AHAHA)
        {
            _imageService = imageService;
            ImagesToUpload = new ObservableCollection<ImageThumbnailUploadVM>();
        }

        public void Reset()
        {
            ImagesToUpload = new ObservableCollection<ImageThumbnailUploadVM>();
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

        private ICommand _browseCommand;
        public ICommand BrowseCommand
        {
            get
            {
                return _browseCommand ?? (_browseCommand = new CommandHandler(EnableOpenFileDialog));
            }
        }

        private ICommand _uploadCommand;
        public ICommand UploadCommand
        {
            get
            {
                return _uploadCommand ?? (_uploadCommand = new CommandHandler(() =>
                {
                    var imagesUploaded = new List<ImageThumbnailCollectionsVM>();
                    foreach(var uploadThumbnail in _imageToUpload)
                    {
                        imagesUploaded.Add(new ImageThumbnailCollectionsVM()
                        {
                            ImageSource = uploadThumbnail.ImageSource.Clone(),
                            Name = uploadThumbnail.Name
                        });
                    }
                    OnUploadClicked?.Invoke(imagesUploaded);
                }));
            }
        }

        public bool CanClickUpload
        {
            get { return _imageToUpload.Count > 0; }
        }

        private ObservableCollection<ImageThumbnailUploadVM> _imageToUpload;
        public ObservableCollection<ImageThumbnailUploadVM> ImagesToUpload
        {
            get { return _imageToUpload; }
            set
            {
                _imageToUpload = value;
                NotifyPropertyChanged();
            }
        }

        private string ParseImageName(string path)
        {
            int indexOfLastSlash = path.LastIndexOf('/');
            if (indexOfLastSlash + 1 >= path.Length)
            {
                return path;
            }
            if (indexOfLastSlash == -1)
            {
                indexOfLastSlash = path.LastIndexOf('\\');
            }
            if (indexOfLastSlash == -1)
            {
                return path;
            }

            return path.Substring(indexOfLastSlash + 1);

        }
        private void EnableOpenFileDialog()
        {
            OpenFileDialog fileBrowser = new OpenFileDialog();
            fileBrowser.Multiselect = true;
            fileBrowser.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            var result = fileBrowser.ShowDialog();
            if (result == true)
            {
                List<ImageSource> images = new List<ImageSource>();
                foreach (FileStream file in fileBrowser.OpenFiles())
                {
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = file;
                    image.EndInit();
                    ImageThumbnailUploadVM imageThumbnailVM = new ImageThumbnailUploadVM(_imageToUpload);
                    imageThumbnailVM.ImageSource = image;
                    imageThumbnailVM.Name = ParseImageName(file.Name);
                    imageThumbnailVM.DateUploaded = DateTime.Now;
                    _imageToUpload.Add(imageThumbnailVM);

                    
                }
                NotifyPropertyChanged("CanClickUpload");
            }
        }
    }
}
