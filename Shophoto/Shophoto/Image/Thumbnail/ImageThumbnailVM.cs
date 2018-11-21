using Shophoto.Command;
using Shophoto.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace Shophoto.Image.Thumbnail
{
    public enum QuickButtonState
    {
        None,
        Delete,
        ThreeDot,
        Edit
    }

    public abstract class ImageThumbnailVM : BaseVM
    {
        public ImageThumbnailVM()
        {
            _visible = true;
        }

        private ImageSource _imageSource;
        public ImageSource ImageSource
        {
            get
            {
                return _imageSource;
            }
            set
            {
                _imageSource = value;
                NotifyPropertyChanged();
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged();
            }
        }

        public DateTime _dateUploaded;
        public DateTime DateUploaded
        {
            get { return _dateUploaded; }
            set
            {
                _dateUploaded = value;
                NotifyPropertyChanged();
            }
        }
        private bool _visible;
        public bool Visible
        {
            get { return _visible; }
            set
            {
                _visible = value;
                NotifyPropertyChanged();
            }
        }

        public virtual ICommand LeftClick
        {
            get;
        }

        public virtual ICommand RightClick
        {
            get;
        }

        protected virtual QuickButtonState QuickButtonState { get; set; } = QuickButtonState.None;

        public bool HasQuickButton
        {
            get { return QuickButtonState != QuickButtonState.None; }
        }
        public bool HasDeleteQuickButton
        {
            get { return QuickButtonState == QuickButtonState.Delete; }
        }
        public bool HasThreeDotQuickButton
        {
            get { return QuickButtonState == QuickButtonState.ThreeDot; }
        }
        public bool HasEditQuickButton
        {
            get { return QuickButtonState == QuickButtonState.Edit; }
        }
       

        public virtual ICommand DeleteQuickButtonClick { get; }
        public virtual ICommand ThreeDotButtonClick { get; }
        public virtual ICommand EditQuickButtonClick { get; }
    }
}
