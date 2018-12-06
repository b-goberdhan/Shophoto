using Shophoto.ViewModels;
using Shophoto.Views.Collections.Aux;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shophoto.Views.Collections.Aux
{
    public class SelectableTagItemVM : BaseVM
    {
        public SelectableTagItemVM()
        {

        }

        private TagItemVM _tag;
        public TagItemVM Tag
        {
            get { return _tag; }
            set
            {
                _tag = value;
                NotifyPropertyChanged();
            }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                NotifyPropertyChanged();
            }
        }
    }
}
