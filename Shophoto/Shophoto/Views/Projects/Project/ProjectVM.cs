using Shophoto.Command;
using Shophoto.ViewModels;
using Shophoto.Views.Collections;
using Shophoto.Views.Projects.Folder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Shophoto.Views.Projects.Project
{
    public class ProjectVM : BaseVM
    {
        public event EventHandler OnGoBackClick;
        public ProjectVM()
        { 
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

        private string _summary;
        public string Summary
        {
            get { return _summary; }
            set
            {
                _summary = value;
                NotifyPropertyChanged();
            }
        }

        private string _customerName;
        public string CustomerName
        {
            get { return _customerName; }
            set
            {
                _customerName = value;
                NotifyPropertyChanged();
            }
        }

        private string _customerEmail;
        public string CustomerEmail
        {
            get { return _customerEmail; }
            set
            {
                _customerEmail = value;
                NotifyPropertyChanged();
            }
        }

        private ICommand _goBackClickCommand;
        public ICommand GoBackClickCommand
        {
            get
            {
                return _goBackClickCommand ?? (_goBackClickCommand = new CommandHandler(() =>
                {
                    CollectionsVM.CollectionsFABButtonVM.IsOpen = false;
                    OnGoBackClick?.Invoke(this, null);
                }));
            }
        }


        private CollectionsVM _collectionsVM;
        public CollectionsVM CollectionsVM
        {
            get { return _collectionsVM; }
            set
            {
                _collectionsVM = value;
                NotifyPropertyChanged();
            }
        }
    }
}
