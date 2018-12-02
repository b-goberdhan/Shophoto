using Shophoto.Command;
using Shophoto.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Shophoto.Views.Projects.AuxView
{
    public class CreateProjectVM : BaseVM
    {
        public event EventHandler OnGoBackClicked;
        public event EventHandler OnCreateProjectClicked;
        public CreateProjectVM()
        {

        }


        private ICommand _goBackCommand;
        public ICommand GoBackCommand
        {
            get
            {
                return _goBackCommand ?? (_goBackCommand = new CommandHandler(() =>
                {
                    OnGoBackClicked?.Invoke(this, null);
                }));
            }
        }

        private ICommand _createProjectCommand;
        public ICommand CreateProjectCommand
        {
            get
            {
                return _createProjectCommand ?? (_createProjectCommand = new CommandHandler(() =>
                {
                    OnCreateProjectClicked?.Invoke(this, null);
                }));
            }
        }
    }
}
