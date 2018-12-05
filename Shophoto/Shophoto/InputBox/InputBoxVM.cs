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
        public event EventHandler OnEnterPressed;
        public InputBoxVM()
        {
            
        }

        private ICommand _enterCommand;
        public ICommand EnterCommand
        {
            get
            {
                return _enterCommand ?? (_enterCommand = new CommandHandler(() =>
                {
                    OnEnterPressed?.Invoke(this, null);
                }));
            }
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
                if (!HasWhiteSpace)
                {
                    _inputText = _inputText.Replace(" ", "");
                }
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

        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
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

        public bool HasWhiteSpace { get; set; } = true;
        public virtual bool HasErrorMessage { get; set; }
        public virtual string PlaceHolderText { get; set; }
        public virtual bool HasMultiLineText { get; }

    }
}
