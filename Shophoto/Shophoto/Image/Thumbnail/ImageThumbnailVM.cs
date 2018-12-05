using Shophoto.Command;
using Shophoto.Menus.Context;
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
        Tag,
        ThreeDot,
        Edit,
        Checkbox
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

        private QuickButtonState _quickButtonState;
        protected QuickButtonState QuickButtonState {
            get
            {
                return _quickButtonState;
            }
            set
            {
                _quickButtonState = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("HasQuickButton");
                NotifyPropertyChanged("HasDeleteQuickButton");
                NotifyPropertyChanged("HasTagQuickButton");
                NotifyPropertyChanged("HasThreeDotQuickButton");
                NotifyPropertyChanged("HasEditQuickButton");
                NotifyPropertyChanged("HasCheckbox");

            }
        }

        public bool HasQuickButton
        {
            get { return QuickButtonState != QuickButtonState.None; }
        }
        public bool HasDeleteQuickButton
        {
            get { return QuickButtonState == QuickButtonState.Delete; }
        }
        public bool HasTagQuickButton
        {
            get { return QuickButtonState == QuickButtonState.Tag; }
        }
        public bool HasThreeDotQuickButton
        {
            get { return QuickButtonState == QuickButtonState.ThreeDot; }
        }
        public bool HasEditQuickButton
        {
            get { return QuickButtonState == QuickButtonState.Edit; }
        }
        public bool HasCheckbox
        {
            get { return QuickButtonState == QuickButtonState.Checkbox; }
        }

        private bool _isChecked;
        public bool IsChecked
        {
            get { return _isChecked && QuickButtonState == QuickButtonState.Checkbox; }
            set {
                _isChecked = value;
                NotifyPropertyChanged();
            }
        }
        public virtual ICommand QuickButtonOnCheckBoxClicked { get; }

        public virtual ICommand DeleteQuickButtonClick { get; }
        public virtual ICommand TagButtonClick { get; }
        public virtual ICommand ThreeDotButtonClick { get; }
        public virtual ICommand EditQuickButtonClick { get; }

        #region IGNORE THIS
        public virtual bool IsContextMenuVisible { get; set; }
        public virtual ImageThumbnailContextMenuVM ImageThumbnailContextMenuVM { get; }
        #endregion


    }
}
