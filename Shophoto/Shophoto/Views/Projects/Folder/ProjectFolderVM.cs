﻿using Ninject;
using Shophoto.Command;
using Shophoto.ViewModels;
using Shophoto.Views.Collections;
using Shophoto.Views.Collections.Aux;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Shophoto.Views.Projects.Folder
{
    public class ProjectFolderVM : BaseVM
    {
        public event EventHandler OnProjectFolderOpen;
        public ProjectFolderVM()
        {
            IsVisible = true;
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

        private bool _isVisible;
        public bool IsVisible
        {
            get
            {
                return _isVisible;
            }
            set
            {
                _isVisible = value;
                NotifyPropertyChanged();
            }
        }

        public DateTime DateCreated { get; set; }

        private CollectionsVM _collectionsVM;
        public CollectionsVM CollectionsVM
        {
            get { return _collectionsVM ?? (_collectionsVM = NinjectLocator.CollectionsVM); }
        }
        private ICommand _leftClickCommand;
        public ICommand LeftClickCommand
        {
            get
            {
                return _leftClickCommand ?? (_leftClickCommand = new CommandHandler(()=> 
                {
                    OnProjectFolderOpen?.Invoke(this, null);
                }));
            }
        }
    }


}
