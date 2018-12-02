using Shophoto.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Shophoto.Buttons
{
    public class ProjectsFABButtonVM : FABPlusButtonVM
    {
        public event EventHandler OnAddProjectClicked;

        public override bool HasAddProjectButton => true;
        public override bool HasDeleteButton => true;

        private ICommand _deleteButtonCommand;
        public override ICommand DeleteButtonCommand
        {
            get
            {
                return _deleteButtonCommand ?? (_deleteButtonCommand = new CommandHandler(() =>
                {
                    //TODO?
                }));
            }
        }

        private ICommand _addProjectButtonCommand;
        public override ICommand AddProjectButtonCommand
        {
            get
            {
                return _addProjectButtonCommand ?? (_addProjectButtonCommand = new CommandHandler(() =>
                {
                    //DEF TODO
                    OnAddProjectClicked?.Invoke(this, null);
                }));
            }
        }
    }
}
