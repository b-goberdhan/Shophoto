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
            DesiredTagNameInputBox.PlaceHolderText = "My Tag";
            DesiredTagNameInputBox.HasWhiteSpace = false;
            TagsService = tagService;RegisterEvents();
        }

        private void RegisterEvents()
        {
            DesiredTagNameInputBox.PropertyChanged += DesiredTagNameInputBox_PropertyChanged;
        }



        public InputBoxVM DesiredTagNameInputBox { get; }
        private void DesiredTagNameInputBox_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
     
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
                    var tag = new TagItemVM(TagsService)
                    {
                        Name = DesiredTagNameInputBox.InputText
                    };
                    if (TagsService.TagExist(tag))
                    {
                        DesiredTagNameInputBox.HasError = true;
                    }
                    else
                    {
                        DesiredTagNameInputBox.HasError = false;
                        DesiredTagNameInputBox.InputText = "";
                        TagsService.Tags.Add(tag);
                        NotifyPropertyChanged("HasTags");
                    }
                    
                    


                }));
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


    }
}
