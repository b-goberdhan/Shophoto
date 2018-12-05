using Shophoto.Command;
using Shophoto.Services;
using Shophoto.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Shophoto.Views.Collections.Aux
{
    public class TagItemVM : BaseVM
    {
        public TagItemVM(TagsService tagsService)
        {
            TagsService = tagsService;
        }

        public TagsService TagsService { get; }

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

        private ICommand _deleteTagCommand;
        public ICommand DeleteTagCommand
        {
            get
            {
                return _deleteTagCommand ?? (_deleteTagCommand = new CommandHandler(() =>
                {
                    TagsService.Tags.Remove(this);
                }));
            }
        }
    }
}
