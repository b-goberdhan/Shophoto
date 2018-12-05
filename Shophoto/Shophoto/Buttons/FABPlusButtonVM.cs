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
    public abstract class FABPlusButtonVM : BaseVM
    {

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
                _isOpen = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("IsClosed");
            }
        }
        public bool IsClosed
        {
            get
            {
                return !IsOpen;
            }
        }

        public virtual bool HasDeleteButton { get; }
        public virtual bool HasTagButton { get; }
        public virtual bool HasDownloadButton { get; }
        public virtual bool HasAddProjectButton { get; } 
        public virtual bool HasUploadButton { get; }

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

        public virtual ICommand DeleteButtonCommand { get; }
        public virtual ICommand TagButtonCommand { get; }
        public virtual ICommand DownloadButtonCommand { get; }
        public virtual ICommand UploadButtonCommand { get; }
        public virtual ICommand AddProjectButtonCommand { get; }


    }
}
