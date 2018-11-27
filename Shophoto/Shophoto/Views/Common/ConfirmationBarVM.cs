using Shophoto.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Shophoto.Views.Common
{
    public abstract class ConfirmationBarVM : BaseVM
    {

        private bool _isVisible;
        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                NotifyPropertyChanged();
            }
        }

        public virtual bool HasSelectedCounter { get; } = true;

        private int _numberSelected;
        public int NumberSelected
        {
            get { return _numberSelected; }
            set
            {
                _numberSelected = value;
                NotifyPropertyChanged();
            }
        }

        public abstract ICommand OkClickedCommand { get; }
        public abstract ICommand CancelClickedCommand { get; }

        private bool _isOnConfirmation;
        public virtual bool IsOnConfirmation
        {
            get { return _isOnConfirmation; }
            set
            {
                _isOnConfirmation = value;
                NotifyPropertyChanged();
            }
        }

        public virtual string ConfrimationText { get; }
        public virtual ICommand ConfirmationYesClickedCommand { get; }
        public virtual ICommand ConfirmationNoClickedCommand { get; }


    }
}
