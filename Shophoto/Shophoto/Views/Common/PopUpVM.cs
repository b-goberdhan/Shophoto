using Shophoto.Command;
using Shophoto.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Shophoto.Views.Common
{
    /**
     * This viewmodel will bind to PopUp.xaml...
     * 
     * 
     * */
    public class PopUpVM : BaseVM
    {
        public PopUpVM()
        {

        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                NotifyPropertyChanged();
            }
        }

        private string _info;
        public string Info
        {
            get { return _info; }
            set
            {
                _info = value;
                NotifyPropertyChanged();
            }
        }

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

        private ICommand _onDismissedClicked; 
        public ICommand OnDismissedClicked
        {
            get
            {
                return _onDismissedClicked ?? (_onDismissedClicked = new CommandHandler(() =>
                {
                     IsVisible = false;
                }));
            }
        }

    }
}
