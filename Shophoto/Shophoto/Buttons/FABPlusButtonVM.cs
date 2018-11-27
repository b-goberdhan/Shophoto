using Shophoto.Command;
using Shophoto.ViewModels;
using Shophoto.Views.Collections;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Shophoto.Buttons
{
    public class FABPlusButtonVM : BaseVM
    {
        public event EventHandler OnUploadClicked;
        public event EventHandler OnDeleteClicked;
        public FABPlusButtonVM()
        {
            IsOpen = false;
        }

        private bool _isOpen;
        public bool IsOpen
        {
            get
            {
                return _isOpen;
            }
            set
            {
                NotifyPropertyChanged();
                NotifyPropertyChanged("IsClosed");
                _isOpen = value;
            }
        }
        public bool IsClosed
        {
            get
            {
                return !IsOpen;
            }
        }

        private ICommand _toggleButton;
        public ICommand ToggleButtonCommand
        {
            get
            {
                return _toggleButton ?? (_toggleButton = new CommandHandler(() =>
                {
                    IsOpen = !IsOpen;
                }));
            }
        }

        private ICommand _deleteButtonCommand;
        public ICommand DeleteButtonCommand
        {
            get
            {
                return _deleteButtonCommand ?? (_deleteButtonCommand = new CommandHandler(() =>
                {
                    OnDeleteClicked?.Invoke(this, null);
                }));
            }
        }

        private ICommand _tagButtonCommand;
        public ICommand TagButtonCommand
        {
            get
            {
                return _tagButtonCommand ?? (_tagButtonCommand = new CommandHandler(() =>
                {

                }));
            }
        }

        private ICommand _downloadButtonCommand;
        public ICommand DownloadButtonCommand
        {
            get
            {
                return _downloadButtonCommand ?? (_downloadButtonCommand = new CommandHandler(() =>
                {

                }));
            }
        }

        private ICommand _uploadButtonCommand;
        public ICommand UploadButtonCommand
        {
            get
            {
                return _uploadButtonCommand ?? (_uploadButtonCommand = new CommandHandler(() =>
                {
                    OnUploadClicked?.Invoke(this, null);
                }));
            }
        }

    }
}
