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
    public class InputBoxVM : BaseVM
    {
        public InputBoxVM()
        {
            
        }
        private string _inputText;
        public string InputText
        {
            get
            {
                if (_inputText == null)
                {
                    _inputText = "";
                }
                return _inputText;
            }
            set
            {
                if (_inputText != value && !IsDirty)
                {
                    IsDirty = true;
                }
                _inputText = value;
                NotifyPropertyChanged();
            }
        }

        private bool _hasError;
        public bool HasError
        {
            get { return _hasError; }
            set
            {
                _hasError = value;
                NotifyPropertyChanged();
            }
        }

        private bool _isDirty;
        public bool IsDirty
        {
            get { return _isDirty; }
            private set
            {
                _isDirty = value;
            }
        }

        public void Reset()
        {
            IsDirty = false;
            InputText = "";
        }

        public virtual string PlaceHolderText { get; set; }
        public virtual bool HasMultiLineText { get; }
    }
}
