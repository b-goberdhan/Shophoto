using Shophoto.Command;
using Shophoto.InputBox;
using Shophoto.Services;
using Shophoto.ViewModels;
using Shophoto.Views.Collections.Aux;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Shophoto.Views.Collections.Aux
{
    public class TagVM : BaseVM
    {
        public TagVM(
            InputBoxVM desiredTagNameInputBox,
            TagsService tagService)
        {
            DesiredTagNameInputBox = desiredTagNameInputBox;
            DesiredTagNameInputBox.PlaceHolderText = "MyTag";
            DesiredTagNameInputBox.HasWhiteSpace = false;
            DesiredTagNameInputBox.HasErrorMessage = true;
            DesiredTagNameInputBox.ErrorMessage = "Tag name already exist";
            
            TagsService = tagService;RegisterEvents();
        }

        public const string ERROR_MESSAGE = "Tag name already exist";

        private void RegisterEvents()
        {
            DesiredTagNameInputBox.PropertyChanged += DesiredTagNameInputBox_PropertyChanged;
            DesiredTagNameInputBox.OnEnterPressed += DesiredTagNameInputBox_OnEnterPressed;
        }



        public InputBoxVM DesiredTagNameInputBox { get; }
        private void DesiredTagNameInputBox_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
     
        }
        private void DesiredTagNameInputBox_OnEnterPressed(object sender, EventArgs e)
        {
            AddTag();
        }

        public TagsService TagsService { get; }

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

        private ICommand _closeCommand;
        public ICommand CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new CommandHandler(() =>
                {
                    IsVisible = false;
                    Reset();
                }));
            }
        }

        private ICommand _toggleInfoCommand;
        public ICommand ToggleInfoCommand
        {
            get
            {
                return _toggleInfoCommand ?? (_toggleInfoCommand = new CommandHandler(() =>
                {
                    IsInfoVisible = !IsInfoVisible;
                }));
            }
        }

        private ICommand _addCommand;
        public ICommand AddCommand
        {
            get
            {
                return _addCommand ?? (_addCommand = new CommandHandler(() =>
                {
                    AddTag();
                }));
            }
        }
        private void AddTag()
        {
            var tag = new TagItemVM(TagsService)
            {
                Name = DesiredTagNameInputBox.InputText
            };
            if (TagsService.TagExist(tag))
            {
                DesiredTagNameInputBox.HasError = true;
                DesiredTagNameInputBox.ErrorMessage = ERROR_MESSAGE;
            }
            else
            {
                DesiredTagNameInputBox.HasError = false;
                DesiredTagNameInputBox.InputText = "";

                TagsService.Tags.Add(tag);
                NotifyPropertyChanged("HasTags");
            }
        }

        private bool _isInfoVisible;
        public bool IsInfoVisible
        {
            get { return _isInfoVisible; }
            set
            {
                _isInfoVisible = value;
                NotifyPropertyChanged();
            }
        }

        public bool HasTags
        {
            get
            {
                return TagsService.Tags.Count > 0;
            }
        }

        public void Reset()
        {
            DesiredTagNameInputBox.HasError = false;
            DesiredTagNameInputBox.InputText = "";
        }


    }
}
