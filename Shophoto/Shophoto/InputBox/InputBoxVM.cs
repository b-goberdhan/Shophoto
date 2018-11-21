using Shophoto.Command;
using Shophoto.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Shophoto.InputBox
{
    public abstract class InputBoxVM : BaseVM
    {
        private string _inputText;
        public string InputText
        {
            get { return _inputText; }
            set
            {
                _inputText = value;
                NotifyPropertyChanged();
            }
        }

        public virtual string PlaceHolderText { get; set; }

    }
}
