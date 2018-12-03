using Shophoto.ViewModels;
using Shophoto.Views.Collections;
using Shophoto.Views.Projects.Folder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shophoto.Views.Projects.Project
{
    public class ProjectVM : BaseVM
    {

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
