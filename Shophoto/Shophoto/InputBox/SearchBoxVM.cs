using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shophoto.InputBox
{
    public class SearchBoxVM : InputBoxVM
    {
        public SearchBoxVM() : base()
        {
            PlaceHolderText = "Search...";
        }

        private string _placeHolderText;
        public override string PlaceHolderText
        {
            get { return _placeHolderText; }
            set
            {
                _placeHolderText = value;
                NotifyPropertyChanged();
            }
        }
    }
}
